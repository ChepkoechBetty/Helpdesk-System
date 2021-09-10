using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Configuration;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using FluentScheduler;

namespace EmailNotification
{
    public partial class EmailNotification : ServiceBase
    {
        private SendMail sync = new SendMail();
        static string pollingInt = ConfigurationManager.AppSettings["PollingInterval"];
        public EmailNotification()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            JobManager.AddJob(() => new SendMail().Email(), (x) => x.ToRunNow().AndEvery(Convert.ToInt32(pollingInt)).Minutes());
            sync.LogData("Service started...");
            sync.LogData("Service runs after every (" + pollingInt + ") Minutes...");
        }

        protected override void OnStop()
        {
            JobManager.RemoveAllJobs();
            sync.LogData("Service Stopped!!!");
        }
        public void OnDebug()
        {
            OnStart(null);
        }
    }
}
