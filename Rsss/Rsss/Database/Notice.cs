using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rsss.Database
{
    public class Notice
    {
        [Key]
        public int NoticeID { get; set; }
        public string Title { get; set; }
        public string PageLink { get; set; }
        public DateTime PublishDate { get; set; }
        public virtual RssChannel Channel { get; set; }


    }
}
