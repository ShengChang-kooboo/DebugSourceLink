using System;
using UtilityLibrary.Helper;

namespace ExecutionClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //var result = WebHelper.GetWebPageSourceCode(new Uri("https://www.baidu.com")).Result;
            //Console.WriteLine(result);

            /*ref和out关键字应用场景：
              值类型参数占用空间大，业务上若无复制修改该值类型参数的需求，可为值类型参数加上ref、out。浅拷贝而不是深拷贝。
              引用类型参数以引用方式传递，若不会在方法体内修改指针指向的内存地址，可不用加ref、out关键字*/
            TestRefAndOut.TestRefKeyword();
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
