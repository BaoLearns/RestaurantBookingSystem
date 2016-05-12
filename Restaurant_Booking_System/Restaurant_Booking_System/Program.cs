using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI;
using Booking_DBA;

namespace Restaurant_Booking_System
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
            /*while (true)
            {
                Login login = new Login();
                Class1 DBA = new Class1();
                Application.Run(login);
            }*/
            //Application.Run(new Leader_Home("Royecode", "123456", false));
            //Application.Run(new Login());
            //Application.Run(new Receptionist_Home("Oliver", "abcdef", true));
        }
    }
}
