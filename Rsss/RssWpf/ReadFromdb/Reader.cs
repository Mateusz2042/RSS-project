using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rsss.Database;
using Rsss.DatabaseWriter;
using RssWpf;



namespace RssWpf.ReadFromdb
{
    public class Reader : MainWindow
    {
        RssContext db = new RssContext();
        List<Notice> notices;
        List<Notice> id;

        MainWindow m;
        internal void FindNoticeByID(int noticeid)
        {

            Notice note = db.Notice.FirstOrDefault(c => c.NoticeID == noticeid);
            notices = db.Notice.ToList();
            //foreach (var item in notices)
            //{
            //    m.textBlock.Text += item;

            //}
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
            DateTime startday = new DateTime();
            DateTime endday = new DateTime();
          
            startday = DateTime.Now;
            endday = DateTime.Now.AddTicks(-1).AddDays(1);

          
            

            var note = db.Notice.Where(c => c.PublishDate > startday && c.PublishDate < endday ).ToList();
            var noteAll = db.Notice.ToList();
            listView.ItemsSource = noteAll;

            // var query = from e in db.Notice
            //             where e.PublishDate == DateTime.Today
            //             select e;
            //var querry = db.Notice.ToList();

        }

      

        }
    }

