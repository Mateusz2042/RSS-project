
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
				if (AllLinks[i][AllLinks[i].Length - 3] == 'x' && AllLinks[i][AllLinks[i].Length - 2] == 'm' && AllLinks[i][AllLinks[i].Length - 1] == 'l')
				{
					XmlLinks.Add(AllLinks[i]);
				}
			}

		}
		public static void Write()
		{
			// write test
			GetLinks();
			// write test
			using (var db = new RssContext())
			{
				foreach (var item in XmlLinks)
				{
					//int count = 1;
					RssChannel channel = new RssChannel();
					channel.ChannelName = item;
					//channel.ChannelLink = item;

					try
					{
						reader.Url = item;
						reader.GetFeed();
						noticeItems = reader.RssItems;


						for (int i = 0; i < noticeItems.Count; i++)
						{

							Notice notice = new Notice();
							notice.PageLink = item;
							notice.PublishDate = noticeItems[i].Date;
							notice.Title = noticeItems[i].Title;
							notice.Channel_Id = channel.ChannelID;
							notice.PageText = "";

							db.Notice.Add(notice);




						}

						db.RssChannel.Add(channel);
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
