
using Rsss.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rsss.DatabaseWriter
{
    public static class Writer
    {
        public static void Write()
        {
            // write test
            using (var db = new RssContext())
            {
                RssChannel channel1 = new RssChannel();
                channel1.ChannelName = "Reset.com";
                
                db.RssChannel.Add(channel1);
                db.SaveChanges();
             
            }

        }

    }
}
