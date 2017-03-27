using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Collections.ObjectModel;
using Rsss;


namespace Rsss
{
	[Serializable]
	class RssManager : IDisposable
	{
		private string _url;
		private string _feedTitle;
		private string _feedDescription;
		private Collection<Rss.Items> _rssItems = new Collection<Rss.Items>();
		private bool _IsDisposed;

		public RssManager()
		{
			_url = string.Empty;
		}

		public RssManager(string feedUrl)
		{
			_url = feedUrl;
		}

		public string Url
		{
			get { return _url; }
			set { _url = value; }
		}

		public Collection<Rss.Items> RssItems
		{
			get { return _rssItems; }
		}

		public string FeedTitle
		{
			get { return _feedTitle; }
		}

		public string FeedDescription
		{
			get { return _feedDescription; }
		}

		public Collection<Rss.Items> GetFeed()
		{

			if (String.IsNullOrEmpty(Url))

				throw new ArgumentException("You must provide a feed URL");

			using (XmlReader reader = XmlReader.Create(Url))
			{
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.Load(reader);

				ParseDocElements(xmlDoc.SelectSingleNode("//channel"), "title", ref _feedTitle);
				ParseDocElements(xmlDoc.SelectSingleNode("//channel"), "description", ref _feedDescription);
				ParseRssItems(xmlDoc);

				return _rssItems;
			}
		}

		private void ParseRssItems(XmlDocument xmlDoc)
		{
			_rssItems.Clear();
			XmlNodeList nodes = xmlDoc.SelectNodes("rss/channel/item");

			foreach (XmlNode node in nodes)
			{
				Rss.Items item = new Rss.Items();
				ParseDocElements(node, "title", ref item.Title);

				ParseDocElements(node, "link", ref item.Link);

				string date = null;
				ParseDocElements(node, "pubDate", ref date);
				DateTime.TryParse(date, out item.Date);

				_rssItems.Add(item);
			}
		}

		private void ParseDocElements(XmlNode parent, string xPath, ref string property)
		{
			XmlNode node = parent.SelectSingleNode(xPath);
			if (node != null)
				property = node.InnerText;
			else
				property = "Unresolvable";
		}

		private void Dispose(bool disposing)
		{
			if (disposing && !_IsDisposed)
			{
				_rssItems.Clear();
				_url = null;
				_feedTitle = null;
				_feedDescription = null;
			}

			_IsDisposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
