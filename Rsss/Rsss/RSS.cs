using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rsss;

namespace Rsss
{
    public class Rss
    {
        [Serializable]
        public class Items
        {
            public string Title;

            public string Link;


            public DateTime Date;
        }
    }
}
