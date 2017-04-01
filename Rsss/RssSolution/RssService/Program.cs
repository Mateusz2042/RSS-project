using Rsss.Database;
using Rsss.DatabaseWriter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
namespace RssService
{
    class Program
    {

        static void Main(string[] args)
        {
            HostFactory.Run(z =>
            {
                z.Service<ServiceCore>(s =>
                {
                    s.ConstructUsing(c => new ServiceCore());
                    s.WhenStarted(c => c.Start());
                    s.WhenStopped(c => c.Stop());
                });
                z.RunAsLocalSystem();
                z.StartAutomatically();


            });
        }
    }
}