using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rsss.Database;
using Rsss.DatabaseWriter;
//using System.Threading;
using System.Timers;
namespace RssService
{
    class ServiceCore
    {
        readonly Timer _timer;
        public ServiceCore()
        {
            RssContext db = new RssContext();
            _timer = new Timer(1000) { AutoReset = true };
            _timer.Elapsed += (sender, eventArgs) => DbWriter.Write();



            Console.WriteLine("Database write successes !!!");
            //  Console.ReadKey();
        }
        public void Start()
        {
            _timer.Start();
            Console.WriteLine("Service started");
        }

        public void Stop()
        {
            _timer.Stop();
            Console.WriteLine("Service stopped");
        }
      
    }
}
