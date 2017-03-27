using Rsss.Database;
using Rsss.DatabaseWriter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssService
{
    class Program
    {
        
        static void Main(string[] args)
        {
            RssContext db = new RssContext();
            DbWriter.Write();

            Console.WriteLine("Database write successes !!!");
            Console.ReadKey();
        }
    }
}
