using HtmlAgilityPack;
using PolarRegionBuff.Models;
using System.Net.Http;
using System.Windows;
using System.Windows.Input;

namespace PolarRegionBuff
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<string> _herosName;
        public List<HeroDto> _heros;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var heros = await ReadData();
            if (heros is not null)
            {
                _heros = heros.Map().ToList();
                _herosName = _heros.Select(hero => hero.Name).ToList();
                this.heroBox.ItemsSource = _herosName;
            }
            else
            {
                MessageBox.Show("爬取BUFF数据错误!");
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (heroBox.SelectedItem != null)
            {
                var hero = _heros.Where(x => x.Name == this.heroBox.Text).SingleOrDefault();
                if (hero is not null)
                {
                    this.herosList.Items.Add(hero.Name);
                    this.herosList.Items.Add(hero.WinPercentage);
                    this.herosList.Items.Add(hero.CauseDamage);
                    this.herosList.Items.Add(hero.TakeDamage);
                    this.herosList.Items.Add(hero.Treatment);
                    this.herosList.Items.Add(hero.Shield);
                    this.herosList.Items.Add(hero.Cd);
                    this.herosList.Items.Add(hero.Toughness);
                    this.herosList.Items.Add(hero.Other);
                    this.herosList.Items.Add("-----------------------");
                    this.listBoxScrollViewer.ScrollToEnd();
                }
            }
            else
            {
                MessageBox.Show("请选择英雄!");
            }
        }

        private void HeroBox_KeyUp(object sender, KeyEventArgs e)
        {
            List<string> heroList = new List<string>();
            heroList = _herosName.FindAll(x => x.Contains(heroBox.Text.Trim()));
            heroBox.ItemsSource = heroList;
            heroBox.IsDropDownOpen = true;
        }


        public static async Task<List<Hero>> ReadData()
        {
            string url = "http://www.jddld.com/index.php?s=api&c=api&m=template&format=jsonp&name=index-table.html&this_order=shenglu&this_role=&this_keyword=&callback=jQuery36002587527713364861_1706529644064&_=1706529644065";
            HttpClient client = new HttpClient();
            string html = await client.GetStringAsync(url);
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);
            var htmldate = document.DocumentNode.SelectNodes("//td").Select(x => x.InnerText).ToList();
            var htmlName = document.DocumentNode.SelectNodes("//a[@href]//span[1]").Select(x => x.InnerText).ToList();
            var buffList = SplitList(htmldate, 9);
            List<Hero> heros = new List<Hero>();
            foreach (var item in buffList)
            {
                var hero = new Hero()
                {
                    Name = item[0],
                    WinPercentage = item[1],
                    CauseDamage = item[2],
                    TakeDamage = item[3],
                    Treatment = item[4],
                    Shield = item[5],
                    Cd = item[6],
                    Toughness = item[7],
                    Other = item[8]
                };
                heros.Add(hero);
            }
            for (int i = 0; i < heros.Count && i < htmlName.Count; i++)
            {
                heros[i].Name = htmlName[i];
            }
            return heros;
        }

        public static List<List<T>> SplitList<T>(List<T> list, int chunkSize)
        {
            int index = 0;
            List<List<T>> result = new List<List<T>>();
            for (int i = 0; i < list.Count; i++)
            {
                var splitList = list.Skip(index).Take(chunkSize).ToList();
                if (splitList.Count != 0)
                {
                    result.Add(splitList);
                }
                index += chunkSize;
            }
            return result;
        }
    }
}