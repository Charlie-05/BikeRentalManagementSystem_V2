using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRentalManagement_V2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu();
            Console.ReadLine();
        }
        public static void Menu()
        {
            BikeRepository repo = new BikeRepository();
            while (true)
            {
                Console.WriteLine("Bike Rental management System");
                Console.WriteLine("1. Add New Bike");
                Console.WriteLine("2. Read All Bikes");
                Console.WriteLine("3. Update A Bike");
                Console.WriteLine("4. Delete A bike");
                Console.WriteLine("5. Exit");
                Console.Write("Choose an option : ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        repo.AddBike();
                        break;
                    case "2":
                       Console.WriteLine(repo.ReadAllBikes());
                        break;
                    case "3":
                        repo.UpdateBike();
                        break;
                    case "4":
                        repo.deleteBike();
                        break;
                    case "5":
                        Console.WriteLine("Thank You...");
                        return;
                    default:
                        Console.Clear();
                        Menu();
                        break;

                }
            }
        }

    }
}
