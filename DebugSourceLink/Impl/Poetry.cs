using DebugSourceLink.Abstract;
using System;
using System.Text;

namespace DebugSourceLink.Impl
{
    public class Poetry : CustomItem, IAmateurPoetry, IProfessionalPoetry
    {
        #region Constructors.
        //public Poetry(Guid guid = default(Guid))
        //    : base(guid) { }
        #endregion

        #region Methods.
        [Obsolete]
        public string PleaseRecitePoem(string peotryTitle)
        {
            throw new NotImplementedException("Please use AmateurPoetryStyle and ProfessionalPoetryStyle instead.");
        }
        /// <summary>
        /// “白丁”对艺术作品的解读
        /// </summary>
        /// <param name="peotryTitle"></param>
        /// <returns></returns>
        public string AmateurPoetryStyle(string peotryTitle)
        {
            StringBuilder sbPoem = new StringBuilder();
            sbPoem.AppendLine($"{peotryTitle}(\"迷醉\"天下士子，这个词大可思量！)");
            switch (peotryTitle)
            {
                case "LiXuePian":
                    sbPoem.AppendLine("书中自有颜如玉");
                    sbPoem.AppendLine("书中自有黄金屋");
                    break;
                default:
                    sbPoem.AppendLine($"No thing found for poem {peotryTitle}");
                    break;
            }
            return sbPoem.ToString();
        }
        /// <summary>
        /// “鸿儒”对艺术作品的解读
        /// </summary>
        /// <param name="peotryTitle"></param>
        /// <returns></returns>
        public string ProfessionalPoetryStyle(string peotryTitle)
        {
            StringBuilder sbPoem = new StringBuilder();
            sbPoem.AppendLine($"{peotryTitle}(\"迷醉\"天下士子，这个词大可思量！)");
            switch (peotryTitle)
            {
                case "LiXuePian":
                    sbPoem.AppendLine("作者：赵恒  年代：宋");
                    sbPoem.AppendLine("富家不用买良田，书中自有千钟粟。");
                    sbPoem.AppendLine("安居不用架高楼，书中自有黄金屋。");
                    sbPoem.AppendLine("出门莫恨无人随，书中车马多如簇。");
                    sbPoem.AppendLine("取妻莫恨无良媒，书中自有颜如玉。");
                    sbPoem.AppendLine("男儿欲遂平生志，五经勤向窗前读。");
                    break;
                default:
                    sbPoem.AppendLine($"No thing found for poem {peotryTitle}");
                    break;
            }
            return sbPoem.ToString();
        }
        public string InterfaceMethodMustImpleInDerivedClass()
        {
            return base.GetCustomUniqueInfo();
        }
        #endregion
    }
}
