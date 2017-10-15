using System;
using WebAuction.DataAccessLayer.EntityFramework;

namespace WebAuction.CLI
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Categories: ");
            using (var db = new WebAuctionDbContext())
            {
                foreach (var category in db.Categories)
                {
                    Console.Write($"{category.Id} - {category.Name} - ");
                    Console.WriteLine("[" + category.Description + "]");
                }
            }
        }
    }
}
