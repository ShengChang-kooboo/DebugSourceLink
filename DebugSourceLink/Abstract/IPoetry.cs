using System;

namespace DebugSourceLink.Abstract
{
    public abstract class CustomItem
    {
        #region Members.
        public DateTime StartServiceDateTime { get; set; }
        public Guid LocalUniqueId { get; set; }
        #endregion

        #region Constructors.
        public CustomItem(Guid guid = default(Guid))
        {
            StartServiceDateTime = DateTime.UtcNow;
            if (guid == default(Guid))
            {
                LocalUniqueId = Guid.NewGuid();
            }
            else
            {
                LocalUniqueId = guid;
            }
        }
        #endregion

        #region Methods.
        /// <summary>
        /// 若一个继承对象继承了一个抽象类和一个接口，抽象类和接口有同名、同参数、同返回值的方法成员，
        /// 抽象类中的该方法会覆盖接口的同名方法，亦即继承对象不用实现接口中的对应方法。
        /// </summary>
        /// <returns></returns>
        protected string GetCustomUniqueInfo()
        {
            return $"StartServiceDateTime: {StartServiceDateTime}, LocalUniqueId: {LocalUniqueId}";
        }
        #endregion
    }

    public interface IPoetry
    {
        string PleaseRecitePoem(string peotryTitle);
        string InterfaceMethodMustImpleInDerivedClass();
    }

    public interface IAmateurPoetry : IPoetry
    {
        string AmateurPoetryStyle(string peotryTitle);
    }

    public interface IProfessionalPoetry : IPoetry
    {
        string ProfessionalPoetryStyle(string peotryTitle);
    }
}
