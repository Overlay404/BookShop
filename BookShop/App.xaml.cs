using BookShop.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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

    }
}
