using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    /// <summary>
    /// 生成编号（员工部门）
    /// </summary>
    public class Helper
    {
        private static readonly object Locker = new object();
        private static int _sn = 0;

        /// <summary>
        /// 生成员工编号
        /// </summary>
        /// <param name="pre">单号前缀</param>
        /// <returns></returns>
        public static string employee(string pre = "")
        {
            lock (Locker)   //lock 关键字可确保当一个线程位于代码的临界区时，另一个线程不会进入该临界区。
            {
                if (_sn == 1000)
                {
                    _sn = 0;
                }
                else
                {
                    _sn++;
                }
                Thread.Sleep(100);
                return pre + DateTime.Now.ToString("yyyyMMddHHmmss") + _sn.ToString().PadLeft(2, '0');
            }
        }

        private static readonly object Locker1 = new object();
        private static int _sn1 = 0;

        /// <summary>
        /// 生成部门编号
        /// </summary>
        /// <param name="pre">单号前缀</param>
        /// <returns></returns>
        public static string department(string pre = "")
        {
            lock (Locker1)   //lock 关键字可确保当一个线程位于代码的临界区时，另一个线程不会进入该临界区。
            {
                if (_sn1 == 1000)
                {
                    _sn1 = 0;
                }
                else
                {
                    _sn1++;
                }
                Thread.Sleep(100);
                return pre  + _sn1.ToString().PadLeft(3, '0');
            }
        }

        private static readonly object Locker2 = new object();
        private static int _sn2 = 0;

        /// <summary>
        /// 生成部门编号
        /// </summary>
        /// <param name="pre">单号前缀</param>
        /// <returns></returns>
        public static string position(string pre = "")
        {
            lock (Locker2)   //lock 关键字可确保当一个线程位于代码的临界区时，另一个线程不会进入该临界区。
            {
                if (_sn2 == 1000)
                {
                    _sn2 = 0;
                }
                else
                {
                    _sn2++;
                }
                Thread.Sleep(100);
                return pre + DateTime.Now.ToString("MMddHH") + _sn2.ToString().PadLeft(3, '0');
            }
        }
        private static readonly object Locker3 = new object();
        private static int _sn3 = 0;

        /// <summary>
        /// 生成合同编号
        /// </summary>
        /// <param name="pre">单号前缀</param>
        /// <returns></returns>
        public static string contract(string pre = "")
        {
            lock (Locker3)   //lock 关键字可确保当一个线程位于代码的临界区时，另一个线程不会进入该临界区。
            {
                if (_sn3 == 1000)
                {
                    _sn3 = 0;
                }
                else
                {
                    _sn3++;
                }
                Thread.Sleep(100);
                return pre +"10"+ DateTime.Now.ToString("yyyyMMddHHmmss") + _sn3.ToString().PadLeft(3, '0');
            }
        }


    }
}
