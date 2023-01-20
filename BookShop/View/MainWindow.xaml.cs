using BookShop.Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace BookShop.View
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

        public static readonly DependencyProperty MyPropertyBooks =
            DependencyProperty.Register("Books", typeof(ObservableCollection<Book>), typeof(MainWindow));



        public MainWindow()
        {
            InitializeComponent();
            UpdateDataBase();
        }

        void UpdateDataBase()
        {
            Books = new ObservableCollection<Book>(App.db.Book);
        }

        private void ImageAwesome_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(ListBooks.SelectedItem == null)
            App.Book = (ListBooks.SelectedItem as Book);
            new EditWindow().Show();
        }
    }
}
