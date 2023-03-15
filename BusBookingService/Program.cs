using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusBookingService
{
    internal class Program
    {
        // Första 10 av platserna är fönster plaster
        static Boolean[] Booked = new bool[20];
        
        //Rows = platser
        //Columm 0 = Förnamn
        //Columm 1 = Efternamn
        //Columm 2 = Personnummer
        //Columm 3 = Kön
        String[,] BusSeats = new string[20, 3];

        static int free = 0;
        static int freewindowseats = 0;



        static void Main(string[] args)
        {
            Meny();
        }

        static void Meny()
        {
            string MenyChoice;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" BUS BOOKING SERVICE \n" +("").PadRight(24, '-')+ "\n 1. Book \n 2. Cancel Booking \n 3. Print Out ");

            MenyChoice = Console.ReadLine();
            switch (Convert.ToInt32(MenyChoice))
            {
                case 1:
                    Book();
                    break;
                case 2:

                    break;
                case 3:

                default:
                    Meny();

                    break;
            }

        }

        static void Book()
        {
            Search();
            Console.WriteLine("\n BOOK \n" + ("").PadRight(24, '-')+"\n Antalet fria plaster: " + free+"\n Antalet fria fönster plaster: "+freewindowseats+"\n" + ("").PadRight(24, '-'));

            Console.WriteLine(" 1. Fönsterplats \n 2. Vanlig plats \n 3. Meny ");
            
            while (true)
            {
                String BookChoice = Console.ReadLine();
                switch (Convert.ToInt32(BookChoice))
                {
                    case 1:
                        if(freewindowseats == 0)
                        {
                            Console.WriteLine("\n Inga Fönster plaster tillgänliga");
                            continue;
                        }
                        else
                        {

        
                        }
                        break;
                    case 2:


                        break;
                    case 3:
                        Meny();
                        break;

                    default:
                        continue;


                }

            }
         



        }

        static void Booking()
        {


        }

        static void Search()
        {
            
            free = 0;
            freewindowseats = 0;
            for (int i = 0; i < 20; i++)
            {

                if (false == Booked[i])
                {
                    if(false == Booked[i] && i <= 9)
                    {
                        freewindowseats++;

                    }
                    free++;
                }
            }

        }


    }
}
