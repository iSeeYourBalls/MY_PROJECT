using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServiceWeather
{
    public partial class WeatherParser : ServiceBase
    {
        public WeatherParser()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Job job = new Job();
            job.CreateJob();
        }

        protected override void OnStop()
        {

        }
    }
}
