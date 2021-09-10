using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace EmailNotification
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            //var myService = new EmailNotification();
            //myService.OnDebug();
            //System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new EmailNotification()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
