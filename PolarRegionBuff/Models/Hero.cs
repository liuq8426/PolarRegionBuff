namespace PolarRegionBuff.Models
{
    /// <summary>
    /// 英雄
    /// </summary>
    public class Hero
    {
        /// <summary>
        /// 英雄名称
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// 胜率
        /// </summary>
        public string WinPercentage { get; set; } = null!;

        /// <summary>
        /// 造伤
        /// </summary>
        public string CauseDamage { get; set; } = null!;

        /// <summary>
        /// 承伤
        /// </summary>
        public string TakeDamage { get; set; } = null!;

        /// <summary>
        /// 治疗
        /// </summary>
        public string Treatment { get; set; } = null!;

        /// <summary>
        /// 护盾
        /// </summary>
        public string Shield { get; set; } = null!;

        /// <summary>
        /// 冷却时间
        /// </summary>
        public string Cd { get; set; } = null!;

        /// <summary>
        /// 韧性
        /// </summary>
        public string Toughness { get; set; } = null!;

        /// <summary>
        /// 其他说明
        /// </summary>
        public string Other { get; set; } = null!;
    }
}
