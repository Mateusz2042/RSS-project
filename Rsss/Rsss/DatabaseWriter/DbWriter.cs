
using Rsss.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rsss.DatabaseWriter
{
    public static class DbWriter
    {

        static RssManager reader = new RssManager();
        static Collection<Rss.Items> noticeItems;

        public static void Write(string url)
        {
            // write test
            using (var db = new RssContext())
            {

                RssChannel channel1 = new RssChannel();
                channel1.ChannelName = url;




                try
                {
                    reader.Url = url;
                    reader.GetFeed();
                    noticeItems = reader.RssItems;


                    for (int i = 0; i < noticeItems.Count; i++)
                    {

                        Notice notice = new Notice();
                        notice.PageLink = url;
                        notice.PublishDate = noticeItems[i].Date;
                        notice.Title = noticeItems[i].Title;
                        notice.Channel = channel1;

                        db.Notice.Add(notice);

                        


                    }

                    //db.RssChannel.Add(channel1);
                    db.SaveChanges();

                }
                catch (Exception ex)
                {


                }

            }







        }

    }
}
