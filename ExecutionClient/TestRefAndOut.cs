using System;

namespace ExecutionClient
{
    internal class TestRefAndOut
    {
        #region Methods.
        /// <summary>
        /// Original Version: https://www.cnblogs.com/zhanlang/p/9651132.html
        /// </summary>
        internal static void TestRefKeyword()
        {
            //int[] arrIds = new int[] { 34, 734 };
            //int numNegativeOne = arrIds[1];
            //int numOne = WhichItemIsUserId(arrIds);
            //Console.WriteLine($"arrIds: {arrIds}");
            //Console.WriteLine($"arrIds[1]: {arrIds[1]}");
            //Console.WriteLine($"numNegativeOne == numOne: {numNegativeOne == numOne}");
            //Console.WriteLine($"numNegativeOne.Equals(numOne): {numNegativeOne.Equals(numOne)}");
            //Console.WriteLine($"ReferenceEquals(numOne, numNegativeOne): {ReferenceEquals(numOne, numNegativeOne)}");

            /*检测ref、out关键字可通过引用地址，改变值类型的值*/
            int testNormalNum = 0;
            int testRefNum = 0;
            int testOutNum = 0;
            Console.WriteLine($"testNormalNum: {testNormalNum}");
            Console.WriteLine($"testRefNum: {testRefNum}");
            Console.WriteLine($"testOutNum: {testOutNum}");
            TestNormalArgument(testRefNum);
            TestRefArgument(ref testRefNum);
            TestOutArgument(out testOutNum, 0);
            Console.WriteLine("After executing:{0}  TestNormalArgument(testRefNum);{0}  TestRefArgument(ref testRefNum);{0}  TestOutArgument(out testOutNum, 0);{0}"
                , Environment.NewLine);
            Console.WriteLine($"testNormalNum: {testNormalNum}");
            Console.WriteLine($"testRefNum: {testRefNum}");
            Console.WriteLine($"testOutNum: {testOutNum}");
        }

        /// <summary>
        /// 方法返回值可加上ref关键字
        /// </summary>
        /// <param name="variousIds"></param>
        /// <returns></returns>
        private static ref int WhichItemIsUserId(int[] variousIds)
        {
            return ref variousIds[1];
        }

        private static void TestRefArgument(ref int num)
        {
            num += 1;
        }

        private static void TestOutArgument(out int num, int initialValue)
        {
            num = initialValue;
            num += 2;
        }

        private static void TestNormalArgument(int num)
        {
            num += 3;
        }
        #endregion
    }
}
