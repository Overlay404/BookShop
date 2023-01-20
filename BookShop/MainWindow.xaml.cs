using BookShop.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace BookShop
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public ObservableCollection<Book> Books
        {
            get { return (ObservableCollection<Book>)GetValue(MyPropertyBooks); }
            set { SetValue(MyPropertyBooks, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyPropertyBooks =
            DependencyProperty.Register("Books", typeof(ObservableCollection<Book>), typeof(MainWindow));



        public MainWindow()
        {
            Books = new ObservableCollection<Book>(App.db.Book.ToList());
            InitializeComponent();
            UpdateDataBase();


        }

        void UpdateDataBase()
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += CheckChangedDataBase;
            worker.RunWorkerAsync(1000);
        }

        private void CheckChangedDataBase(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                Thread.Sleep(1000);
                MessageBox.Show("sada");
            }
        }
    }
}
