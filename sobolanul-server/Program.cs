using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace sobolanul_server
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}