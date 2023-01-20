using System;

namespace BookShop.Model
{
    partial class Book
    {
        public int CostWithDiscount => (int)Math.Round((double)(Cost - (Discount * Cost / 100)));

        public byte[] ImageSource => Photo ?? App.ConvertToByteCollection(@"C:\Users\232002\Source\Repos\BookShop\BookShop\Resourses\EmptyPicture.png");
    }
}
