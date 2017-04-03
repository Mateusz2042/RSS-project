
using HtmlAgilityPack;
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

		static List<string> AllLinks = new List<string>();
		static List<string> XmlLinks = new List<string>();

		private static void GetLinks()
		{
			AllLinks.Clear();
			XmlLinks.Clear();

			HtmlWeb hw = new HtmlWeb();
			HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
			doc = hw.Load("http://www.rss.lostsite.pl/index.php?rss=32");
			foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
			{
				string hrefValue = link.GetAttributeValue("href", string.Empty);
				AllLinks.Add(hrefValue);
			}

			int size = 0;
			for (int i = 0; i < AllLinks.Count; i++)
			{
				size = AllLinks[i].Length;
				if (
					AllLinks[i][AllLinks[i].Length - 3]
					== 'x' && AllLinks[i][AllLinks[i].Length - 2]
					== 'm' && AllLinks[i][AllLinks[i].Length - 1] == 'l'
				)
				{
					XmlLinks.Add(AllLinks[i]);
				}
			}

		}

		public static void Write()
		{

			GetLinks();

			using (var db = new RssContext())
			{
				var test = db.RssChannel.FirstOrDefault();
                int counter = 1;
                foreach (var item in XmlLinks)//Exception 
                {
					

					try
					{
						reader.Url = item;
						reader.GetFeed();
						noticeItems = reader.RssItems;

                        // checking db existing
                        counter++;
                        RssChannel channel = new RssChannel();
						if (test == null)
						{

							channel.ChannelName = "Channel" + counter;
							channel.ChannelLink = item;
							db.RssChannel.Add(channel);
							db.SaveChanges();
                            
						}
                        
						else
						{
							channel = db.RssChannel.Where(x =>
							x.ChannelID == counter).First();
						}
                        

						for (int i = 0; i < noticeItems.Count; i++)
						{

							Notice notice = new Notice();
							notice.PageLink = noticeItems[i].Link;
							notice.PublishDate = noticeItems[i].Date;
							notice.Title = noticeItems[i].Title;
							notice.Channel_Id = channel.ChannelID;
							
							if (db.Notice.Where(x =>
							x.Title == notice.Title).FirstOrDefault() == null)
							{
								db.Notice.Add(notice);
							}
                            
                            
						}

						//db.RssChannel.Add(channel);
						db.SaveChanges();
						

					}
					catch (Exception ex)
					{


					}

				}
			}
		}
	}

}
