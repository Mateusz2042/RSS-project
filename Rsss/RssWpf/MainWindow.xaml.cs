using Rsss.Database;
using Rsss.DatabaseWriter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace RssWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        RssContext db;


        public MainWindow()
        {

            db = new RssContext();
            InitializeComponent();

            FindNoticeByID();
            FindNoticeByChannelID();

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            ////odczyt z bazy
            //using (db)
            //{
            //    // wszzystkie notatki
            //    var notices = db.Notice;
            //    listView.ItemsSource = notices.ToList();
            //}


            DateTime startday = new DateTime();
            DateTime endday = new DateTime();
            startday = DateTime.Now;
            endday = DateTime.Now.AddTicks(-1).AddDays(1);


            var note = db.Notice.Where(c => c.PublishDate > startday && c.PublishDate < endday).ToList();
            listView.ItemsSource = note;
            if (listView.AlternationCount == 0)
            {
                System.Windows.MessageBox.Show("Dzisiaj nie ma jeszcze nowych artykułów!");
            }

        }





        private void buttonBYID_Click(object sender, RoutedEventArgs e)
        {
            //wyswietli tekst po id notatki rownoznaczym z tytulem 
        }

        public void FindNoticeByID()
        {
            comboBoxNoticeID.ItemsSource = db.Notice.ToList();
            comboBoxNoticeID.DisplayMemberPath = "Title";
            comboBoxNoticeID.SelectedValuePath = "NoticeID";

        }
        public void FindNoticeByChannelID()
        {
            comboBoxChannelID.Items.Clear();
            comboBoxChannelID.ItemsSource = db.RssChannel.ToList();
            comboBoxChannelID.DisplayMemberPath = "ChannelName";
            comboBoxChannelID.SelectedValuePath = "ChannelID";

        }

        private void buttonBYChannelID_Click(object sender, RoutedEventArgs e)
        {
            //wyswietli artykuly z danego kanalu
        }
    }

}

