using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rsss.Database
{
    public class RssChannel
    {
        [Key]
        public int ChannelID { get; set; }
        public string ChannelName { get; set; }
        public virtual List<Notice> Notices { get; set; }
    }
}
