using BookShop.Model;
using System.Drawing;
using System.IO;
using System.Windows;

namespace BookShop
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {

        private static BookShop1Entities _db;
        public static BookShop1Entities db
        {
            get { return _db ?? (_db = new BookShop1Entities()); }
        }

        public static Book Book { get; set; }


        public static byte[] ConvertToByteCollection(string uri)
        {
            Image ImageFromFile = Image.FromFile(uri);
            MemoryStream memoryStream = new MemoryStream();
            ImageFromFile.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
            return memoryStream.ToArray();
        }
    }
}
