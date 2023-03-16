using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusBookingService
{
    internal class Program
    {
        // Sista 10 av platserna är fönster plaster
        static Boolean[] Booked = new bool[21];
        
        //Rows = platser
        //Columm 0 = Förnamn
        //Columm 1 = Efternamn
        //Columm 2 = Personnummer
        //Columm 3 = Kön
        //Columm 4 = Ålder
        static String[,] BusSeats = new string[21, 5];

        static int[] BusSeatsSorted = new int[21];

        static int free = 0;
        static int freewindowseats = 0;

        static int Adults = 0;
        static int Kids = 0;
        static int Elderly = 0;

        static double AdultsPrice = 299.90;
        static double KidsPrice = 149.90;
        static double ElderlyPrice = 200.0;





        static void Main(string[] args)
        {
            Meny();
        }

        static void Meny()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" BUS BOOKING SERVICE \n" +("").PadRight(24, '-')+ "\n 1. Book \n 2. Cancel Booking \n 3. Print Out ");

            string MenyChoice = Console.ReadLine();
            Console.WriteLine(MenyChoice);
            switch (Convert.ToInt32(MenyChoice))
            {
                case 1:
                    Book();
                    break;
                case 2:
                    CancelBooking();

                    break;
                case 3:
                    BusData();

                    break;
                case 4:
                    System.Environment.Exit(1);
                    break;

                default:
                    Meny();

                    break;
            }

        }


        // BOOKING //

       
        static void Book()
        {

            Search();

            Console.WriteLine("\n BOOK \n" + ("").PadRight(24, '-')+"\n Antalet fria plaster: " + free+"\n Antalet fria fönster plaster: "+freewindowseats+"\n" + ("").PadRight(24, '-'));

            Console.WriteLine(" 1. Fönsterplats \n 2. Vanlig plats \n 3. Meny ");

            string BookChoice = Console.ReadLine();
            



            switch (Convert.ToInt32(BookChoice))
            {
                case 1:
                    if (freewindowseats > 0)
                    {
                        SearchBooking(true);
                        Console.WriteLine("dumbom");
                    }
                    else
                    {
                        // FIXA SENARE
                    }
                    break;
                case 2:
                    SearchBooking(false);

                    break;
                case 3:
                    Meny();
                    break;

                default:
                    Meny();
                    break;
            }

        }


        static void Search()
        {
            
            free = 0;
            freewindowseats = 0;
            for (int i = 0; i < 21; i++)
            {

                if (false == Booked[i])
                {
                    if(false == Booked[i] && i >= 9)
                    {
                        freewindowseats++;

                    }
                    free++;
                }
            }


        }



        static void SearchBooking(Boolean Window)
        {
            int j = 0;

            if (Window)
            {
                j = 11;  
            }

            for (int i = j; i < 21; i++)
            {

                if (false == Booked[i])
                {
                    UserInfo(i); 
                }
            }
            

        }

        static void UserInfo(int i)
        {
            Console.WriteLine("Plats: "+i);

            Console.WriteLine("Namn ");

            string Name = Console.ReadLine();

            Console.WriteLine("Efternman ");

            string Lastname = Console.ReadLine();

            Console.WriteLine("Gender 1. Man 2. Kvinna 3. Annan");

            string Gender = Console.ReadLine();

            Console.WriteLine("Personummer YYYYMMDD ");

            // SocialSecurityNumber
            string SSN = Console.ReadLine();


            BusSeats[i, 0] = Name.ToLower(); 

            BusSeats[i, 1] = Lastname.ToLower(); 

            BusSeats[i, 3] = Gender; 

            BusSeats[i, 2] = SSN; 

          
            int Year = Convert.ToInt32(SSN.Substring(0,4));
            int Month = Convert.ToInt32(SSN.Substring(4,2));
            int Day = Convert.ToInt32(SSN.Substring(6,2));

            var born = new DateTime(Year, Month, Day);
            var today = DateTime.Now;
            var diffOfDates = today - born;

            int Age = Convert.ToInt32(diffOfDates.TotalDays) / 365;

            BusSeats[i, 4] =  Age.ToString();

            

            Double Pris;

            if(Age < 18)
            {
                Console.WriteLine("du är underårig");
                Kids++;
                Pris = KidsPrice;


            }
            else if(Age >=18 && Age > 69) 
            {

                // BRAM VARFÖR HAR DU INTE BARA DEN HÄR PÅ ELSE !!!!!!!!!!!!!!!

                Console.WriteLine("du är myndig");
                Adults++;
                Pris = AdultsPrice;

            }
            else
            {
                Console.WriteLine("du är gammal");
                Elderly++;
                Pris = Elderly;
            }

            Console.WriteLine("Total Priset "+(Pris.ToString())+" Confimera 1."+" Cancel 2.");

            String Confirm = Console.ReadLine();

            switch(Convert.ToInt16(Confirm))
            {
                case 1:
                    Booked[i] = true;
                    Meny();
                    break;
                case 2:
                    Meny();

                    break;
                default:
                    Console.WriteLine("ost");
                    break;  

            }


        }


        // CANCEL BOOKING


        private static void CancelBooking()
        {
            Console.WriteLine("1. Avboka med ditt förnamn 2. Avboka med ditt Personnummer 3. Menu");
            String Cancel = Console.ReadLine();
            switch (Convert.ToInt16(Cancel))
            {
                case 1:
                    Console.WriteLine("Skriv Ditt Namn");
                    String Name = Console.ReadLine();

                    SearchUser(Name, 0);
                    break;
                case 2:
                    Console.WriteLine("Skriv Ditt personnummer");
                    String SSN = Console.ReadLine();

                    SearchUser(SSN, 2);
                    break;
                case 3:
                    Meny();
                    break;
                default:
                    break;
            }

            
           
        }

        static void SearchUser(String DataSearch, int Datatype)
        {
            DataSearch = DataSearch.ToLower();
            //Columm 0 = Förnamn
            //Columm 1 = Efternamn
            //Columm 2 = Personnummer 

            int[] FindBooked = new int[21];


            int j = 0;

            for (int i = 0; i < 21; i++)
            {
                if (DataSearch == BusSeats[i, Datatype])
                {
                    FindBooked[j] = i;
                    j++;


                }

            }
            Console.WriteLine("Ostbåge");
            for (int i = 0;i < j;i++) {

                Console.WriteLine("Säte: " + FindBooked[i] +" Namn: "+BusSeats[FindBooked[i], 0]+" Personnummer: "+BusSeats[FindBooked[i],2]);
                Console.WriteLine("VARNING: ÄR DETTA DIN PLATS Y/N, N: Gå till nästa match");
                String ChooseYourSeat = Console.ReadLine().ToLower();
                if (ChooseYourSeat == "y")
                {
                    int AgeRemovePrice = Convert.ToInt32(BusSeats[FindBooked[i], 5]);
                    if (AgeRemovePrice < 18)
                    {
                        
                        Kids--;
                        


                    }
                    else if (AgeRemovePrice >= 18 && AgeRemovePrice > 69)
                    {
                        // HÄR OCKSÅ!!!
                        
                        Adults--;
                        

                    }
                    else
                    {
         
                        Elderly--;
                    }


                    Booked[FindBooked[i]] = false;
 
                    Console.WriteLine("Avbokad");


                }
                else
                {
                    continue;
                }

            }
            



            

        }

        static void BusData()
        {
            BubbleSort();

            // Output the header.
            Console.Write("Plats".PadRight(15));
            Console.Write("Förnamn".PadRight(15));
            Console.Write("Efternamn".PadRight(17));
            Console.Write("Personnummer".PadRight(20));
            Console.Write("Kön".PadRight(11));
            Console.WriteLine("Ålder");

            // Output array.
            for (int i = 0; i < 21; i++)
            {
                if(true == Booked[i])
                {
                    Console.Write(BusSeats[BusSeatsSorted[i], i].PadRight(15));
                    Console.Write(BusSeats[BusSeatsSorted[i], 0].PadRight(15));
                    Console.Write(BusSeats[BusSeatsSorted[i], 1].PadRight(17));
                    Console.Write(BusSeats[BusSeatsSorted[i], 2].PadRight(20));
                    Console.Write(BusSeats[BusSeatsSorted[i], 3].PadRight(11));
                    Console.WriteLine(BusSeats[BusSeatsSorted[i], 4]);

                }
             
                
            }

       
        }
        static void BubbleSort()
        {

            for (int i = 0;i < 21;i++)
            {
                if(true == Booked[i])
                {
                    BusSeatsSorted[i] = Convert.ToInt16(BusSeats[i, 4]);
                }
                
            }
            int temp = 0;

            for (int write = 0; write < BusSeatsSorted.Length; write++)
            {
                for (int sort = 0; sort < BusSeatsSorted.Length - 1; sort++)
                {
                    if (BusSeatsSorted[sort] > BusSeatsSorted[sort + 1])
                    {
                        temp = BusSeatsSorted[sort + 1];
                        BusSeatsSorted[sort + 1] = BusSeatsSorted[sort];
                        BusSeatsSorted[sort] = temp;
                    }
                }
            }

        }



    }
}
