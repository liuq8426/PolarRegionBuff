using PolarRegionBuff.Models;
using System.Text.RegularExpressions;

namespace PolarRegionBuff
{
    public static class MapperExtension
    {
        public static HeroDto Map(this Hero hero)
        {
            var heroDto = new HeroDto();
            if (hero is not null)
            {
                heroDto = new HeroDto
                {
                    Name = hero.Name.SplitString(),
                    WinPercentage = "胜率: " + hero.WinPercentage,
                    CauseDamage = "造成伤害: " + hero.CauseDamage,
                    TakeDamage = "承受伤害: " + hero.TakeDamage,
                    Treatment = "治疗效果: " + hero.Treatment,
                    Shield = "护盾: " + hero.Shield,
                    Cd = "CD: " + hero.Cd,
                    Toughness = "韧性: " + hero.Toughness,
                    Other = "其他: " + hero.Other
                };
            }
            return heroDto;
        }

        public static IEnumerable<HeroDto> Map(this IEnumerable<Hero> heros)
        {
            if (heros is null || !heros.Any())
            {
                return new List<HeroDto>();
            }
            return heros.Select(x => x.Map());
        }

        /// <summary>
        /// 清洗数据
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string SplitString(this string name)
        {
            string pattern = "[a-zA-Z0-9\\W]";
            string afterSubstringToRemove = "一键复制";
            // 去掉数字、英文、符号
            string result1 = Regex.Replace(name, pattern, string.Empty);
            // 去掉后面字符
            string result = result1.Replace(afterSubstringToRemove, string.Empty);
            // 去掉重复值
            // string result = new string(result2.Distinct().ToArray());
            return result;
        }
    }
}
