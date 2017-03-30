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
using RssWpf.ReadFromdb;


namespace RssWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RssContext db;
        List<Notice> notices;

        public MainWindow()
        {
            db = new RssContext();
            InitializeComponent();

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //odczyt z bazy
            //using (db)
            //{
            //    // wszzystkie notatki
            //    notices = db.Notice.ToList();
            //}

            string s = textBox.Text;
            int i = int.Parse(s);
            Reader r = new Reader();

            r.FindNoticeByID(i);
            foreach (var item in notices)
            {
                textBlock.Text += item.Title;
            }


        }

    }
}
