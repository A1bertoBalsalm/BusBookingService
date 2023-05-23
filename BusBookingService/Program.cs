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
        //Rows = platser
        //Columm 0 = Förnamn
        //Columm 1 = Efternamn
        //Columm 2 = Personnummer
        //Columm 3 = Kön
        //Columm 4 = Ålder
        //Columm 5 = Plats

        static Boolean[] Booked = new bool[21]; //Varje plats om den är tagen eller inte
        static String[,] BusSeats = new string[21, 6]; // Information om varje passagerae
  
        static int free = 0;
        static int freewindowseats = 0;

        static int Adults = 0;
        static int Kids = 0;
        static int Elderly = 0;

        static double AdultsPrice = 299.90;
        static double KidsPrice = 149.90;
        static double ElderlyPrice = 200.0;

       




        static void Main(string[] args) => Meny();

        static void Meny()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" BUS BOOKING SERVICE \n" +("").PadRight(24, '-')+ "\n 1. Book \n 2. Cancel Booking \n 3. Print Out ");

            string MenyChoice = Console.ReadLine();
         ;
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

            Search(); //Metod för att hitta lediga plateser

            Console.WriteLine("\n BOOK \n" + ("").PadRight(24, '-')+"\n Antalet fria plaster: " + free+"\n Antalet fria fönster plaster: "+freewindowseats+"\n" + ("").PadRight(24, '-'));

            Console.WriteLine(" [1] Fönsterplats \n [2] Vanlig plats \n [3] Meny ");

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
                    if(freewindowseats == free)
                    {
                        SearchBooking(false);

                    }
                    else
                    {
                        SearchBooking(false);
                        // Samma här
                    }
                    

                    break;
                case 3:
                    Meny();
                    break;


                default:
                    Meny();
                    break;
            }

        }


        static void Search() // Leter efter lediga platser
        {
            
            free = 0;
            freewindowseats = 0;
            int findwindowseats = 0;
            Boolean freewindow = false;

            for (int i = 0; i < Booked.Length; i++)
            {


                if (false == Booked[i]) // Om platen är inte tagen
                {
                    free++;
                    freewindow = true;


                    
                }
                if (i == 17) // Vid 17 är det baksidan av bussen så tre sätten imellan
                {

                    findwindowseats -= 3;

                }
                if (findwindowseats == 0 || findwindowseats == 3) // Om det är en fönsterplats för att varje 0 och 3 varv i loopen finns det en fönsterplats
                {
                    
                    if (freewindow) // Om vi vet att den är ledig från innan
                    {
                        freewindowseats++;
                    }
        
                    if (findwindowseats == 3) 
                    {
                        findwindowseats = -1;

                    }
                    findwindowseats++;

                }
                else
                {
                    findwindowseats++;
                }
            
                freewindow = false;





            }

 

        }



        static void SearchBooking(Boolean Window) // Ungeför likdan men window säger om kunden vill ha en fönsterplats
        {
            
            int findwindowseats = 0;
            
            

            for (int i = 0; i < Booked.Length; i++)
            {

             
                if (i == 17)
                {

                    findwindowseats -= 3;

                }
                else if (Window == true && (findwindowseats == 0 || findwindowseats == 3))     
                {


                    if (Booked[i] == false)
                    {
                        UserInfo(i);
                        break;

                    }

                    if (findwindowseats == 3)
                    {
                        findwindowseats = -1;

                    }
                    findwindowseats++;



                   
                }
                else if (Window == false && (findwindowseats != 0 && findwindowseats != 3))
                {

                    if (Booked[i] == false)
                    {
                        UserInfo(i);
                        break;
                        

                    }

                }
                else
                {
                    findwindowseats++;
                   
                    
                }
                

                

            }
                





        }





        

        static void UserInfo(int i) // Här skriver kunden in sin data
        {
            Console.WriteLine("Plats: "+i);

            Console.WriteLine("Namn ");

            string Name = Console.ReadLine();

            Console.WriteLine("Efternman ");

            string Lastname = Console.ReadLine();

            Console.WriteLine("Gender 1. Man 2. Kvinna 3. Annan");

            string Gender = Console.ReadLine();

            switch (Int32.Parse(Gender))
            {
                case 1:
                    Gender = "Man";
                    break;
                case 2:
                    Gender = "Kvinna";
                    break;

                default:
                    Gender = "Annan";
                    break;
            }

            Console.WriteLine("Personummer YYYYMMDD ");

            // SocialSecurityNumber
            string SSN = Console.ReadLine();

         
            BusSeats[i, 0] = Name.ToLower();     // Så att vi kan söka efter den senare 

            BusSeats[i, 1] = Lastname.ToLower(); 

            BusSeats[i, 3] = Gender; 

            BusSeats[i, 2] = SSN;

            // Plats
            BusSeats[i, 5] = i.ToString();
           

            // Tycke att det skulle vara tråkigt att göra det vanligt så här är lösningen för 05:or
            int Year = Convert.ToInt32(SSN.Substring(0,4));
            int Month = Convert.ToInt32(SSN.Substring(4,2));
            int Day = Convert.ToInt32(SSN.Substring(6,2));

            var born = new DateTime(Year, Month, Day);
            var today = DateTime.Now;
            var diffOfDates = today - born;
            // Dagar delet till år som sedan kollar där nera om det är 18 upp
            int Age = Convert.ToInt32(diffOfDates.TotalDays) / 365;

            BusSeats[i, 4] =  Age.ToString();

            

            Double Pris;
            // Kollar vilket pris kunden ska få och lägger till dens årsgrupp i bussen
            if(Age < 18)
            {
                Console.WriteLine("du är underårig");
                Kids++;
                Pris = KidsPrice;


            }
            else if(Age > 69) 
            {

            

              
                Console.WriteLine("du är gammal");
                Elderly++;
                Pris = ElderlyPrice;

            }
            else
            {

                Console.WriteLine("du är myndig");
                Adults++;
                Pris = AdultsPrice;

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

        static void SearchUser(String DataSearch, int Datatype) // Dataserach är värdet på det som söks och Datatyp säger vilken sorts det är
        {
            DataSearch = DataSearch.ToLower();
            //Columm 0 = Förnamn
            //Columm 1 = Efternamn
            //Columm 2 = Personnummer 

            int[] FindBooked = new int[21];


            int j = 0;
           
            //Koller efter en match
            for (int i = 0; i < 21; i++)
            {
                if (DataSearch == BusSeats[i, Datatype])
                {
                    FindBooked[j] = i;
                    j++;


                }

            }
            
            // Denna loop fungerar med att dubbel kolla om det är verkligen du 
            for (int i = 0;i < j;i++) {

                Console.WriteLine("Säte: " + BusSeats[FindBooked[i], 5] + " Namn: "+BusSeats[FindBooked[i], 0]+" Personnummer: "+BusSeats[FindBooked[i],2]);
                Console.WriteLine("VARNING: ÄR DETTA DIN PLATS Y/N, N: Gå till nästa match");
                String ChooseYourSeat = Console.ReadLine().ToLower();
                if (ChooseYourSeat == "y")
                {
                    int AgeRemovePrice = Convert.ToInt32(BusSeats[FindBooked[i], 4]);
                    if (AgeRemovePrice < 18)
                    {
                        
                        Kids--;
                        

                    }
                    else if (AgeRemovePrice >= 69)
                    {
                    

                        Elderly--;


                    }
                    else
                    {
                        Adults--;

                     
                    }

                    int CancelSeat = Int32.Parse(BusSeats[FindBooked[i], 5]);
                    Booked[CancelSeat] = false;
 
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
            //Bubblesort
            for (int i = 0; i < 21; i++)
            {

                for (int j = 0; j < 21 - 1; j++)
                {
                    if (Convert.ToInt32(BusSeats[j, 4]) > Convert.ToInt32(BusSeats[j + 1, 4]))
                    {
                        for (int k = 0; k < 6; k++)
                        {
                        
                            (BusSeats[j + 1, k], BusSeats[j, k]) = (BusSeats[j, k], BusSeats[j + 1, k]);
                        }




                    }
                }
            }


            //Skriver ut personerna
            int rows = BusSeats.GetLength(0);
            int columm = BusSeats.GetLength(1);

            //Header
            Console.WriteLine("Förnamn\tEfternamn\tPersonnummer\t\tKön\t\tÅlder\tPlats");

            // Loop genom varje row
            for (int i = 0; i < rows; i++)
            {
                
                bool blank = false;


                // Loop genom varje column
                for (int j = 0; j < columm; j++)
                {
                    string Isitblank = BusSeats[i, j];
                    
                    if (string.IsNullOrEmpty(Isitblank)) // Är row blank
                    {
                        blank = true;
                        break;
                    }

                    Console.Write($"{BusSeats[i, j]}\t");
                    

                    // justera avståndet för namn och efternamns kolumnerna
                    if (j == 1 || j == 2 || j == 3 )
                    {
                        Console.Write("\t");
                    }
                   


                }
                if (!blank) // Om den är inte blank gå till nästa linje
                {
                    Console.WriteLine();
                }

               
            }


          
            string ost = Console.ReadLine();
            if (ost == "1")
            {
                Meny();

            }


        }
 

    }
}

