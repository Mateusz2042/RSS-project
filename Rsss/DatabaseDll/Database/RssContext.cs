using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rsss.Database
{
    public class RssContext: DbContext
    {
        public DbSet<RssChannel> RssChannel { get; set; }
        public DbSet<Notice> Notice { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
