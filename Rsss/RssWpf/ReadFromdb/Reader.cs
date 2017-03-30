using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rsss.Database;
using Rsss.DatabaseWriter;



namespace RssWpf.ReadFromdb
{
    public class Reader
    {
        RssContext db = new RssContext();
        List<Notice> notices;
        List<Notice> id;
        internal void FindNoticeByID(int noticeid)
        {

            Notice note = db.Notice.First(c => c.NoticeID == noticeid);
            notices = db.Notice.ToList();
            foreach (var item in notices)
            {
                

            }
            //Notice note =
            //       (from Notice in db.Notice
            //        where Notice.NoticeID == id
            //        select Notice).FirstOrDefault();

            // notatki po kanale
            // dzisiejsze rssy
            // po id kanału
        }
        public void FindByChannelID(int channelid)
        {
            Notice note = db.Notice.First(c => c.Channel_Id == channelid);
            notices = db.Notice.ToList();


        }
        public void FindTodayNotices()
        {
            
            Notice note = db.Notice.First(c => c.PublishDate == DateTime.Today);
            notices = db.Notice.ToList();

            // var query = from e in db.Notice
            //             where e.PublishDate == DateTime.Today
            //             select e;
            //var querry = db.Notice.ToList();

        }

      

        }
    }

