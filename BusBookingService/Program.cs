using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusBookingService
{
    internal class Program
    {

        // Jag använder c# namn standard
        //Rows = platser
        //Columm 0 = Förnamn
        //Columm 1 = Efternamn
        //Columm 2 = Personnummer
        //Columm 3 = Kön
        //Columm 4 = Ålder
        //Columm 5 = Plats

        static Boolean[] Booked = new bool[21]; //Varje plats om den är tagen eller inte
        static String[,] BusSeats = new string[21, 6]; // Information om varje passagerae
  
        static int Free = 0;
        static int FreeWindowSeats = 0;

        static int Adults = 0;
        static int Kids = 0;
        static int Elderly = 0;

        static double AdultsPrice = 299.90;
        static double KidsPrice = 149.90;
        static double ElderlyPrice = 200.0;

       




        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Meny();
        } 

        static void Meny()
        {
            
            Console.WriteLine(" BUS BOOKING SERVICE \n" +("").PadRight(24, '-')+ "\n [1] Book \n [2] Cancel Booking \n [3] Print Out \n [4] Profit \n [5] Exit  ");

            string menyChoice = Console.ReadLine();
            if (int.TryParse(menyChoice, out int choice))
            {
                switch (Convert.ToInt32(choice))
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
                        double totalProfit = ProfitRecursion(Adults, Kids, Elderly);
                        Console.WriteLine("\n Total profit: " + totalProfit + "kr \n");
                        Meny();
                        break;
                    case 5:
                        System.Environment.Exit(1);
                        break;
                    default:
                        Console.WriteLine("Felaktig input");
                        Meny();
                        break;
                }


            }
            else
            {
                Console.WriteLine("Felaktig input");
                Meny();
            }


        }


        // BOOKING //

       
        static void Book()
        {

            Search(); //Metod för att hitta lediga plateser

            while (true)
            {
                Console.WriteLine("\n BOOK \n" + ("").PadRight(24, '-') + "\n Antalet fria plaster: " + Free + "\n Antalet fria fönster plaster: " + FreeWindowSeats + "\n" + ("").PadRight(24, '-'));

                Console.WriteLine(" [1] Fönsterplats \n [2] Vanlig plats \n [3] Meny ");

                string bookChoice = Console.ReadLine();

                if (int.TryParse(bookChoice, out int choice))
                {
                    switch (Convert.ToInt32(choice))
                    {
                        case 1:
                            if (FreeWindowSeats > 0)
                            {
                                SearchBooking(true);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Det finns inga fönster platser");
                                continue;
                            }
                        case 2:
                            if (FreeWindowSeats == Free)
                            {
                                Console.WriteLine("Det finns inga vanliga platser");
                                continue;

                            }
                            else
                            {
                                SearchBooking(false);
                                break;
                            }
                        case 3:
                            Meny();
                            break;
                        default:
                            Console.WriteLine("Fel input, försök igen");
                            continue;
                    }

                }
                else
                {
                    Console.WriteLine("Felaktig input");
                    continue;

                }




            }
           

        }

        static double ProfitRecursion(int adults, int kids, int elderly) // En recursive metod som räknar profit för passagare
        {
            if (adults == 0 && kids == 0 && elderly == 0)
            {
                return 0; // Inga passagera då ingen profit
            }

            double total = 0;

            if (adults > 0)
            {
                total += AdultsPrice;
                adults--;
            }
            else if (kids > 0)
            {
                total += KidsPrice;
                kids--;
            }
            else if (elderly > 0)
            {
                total += ElderlyPrice;
                elderly--;
            }

            return total + ProfitRecursion(adults, kids, elderly); // Den kallar tillbaka med minskad antal passagera och adderar profiten



        }


        static void Search() // Leter efter lediga platser
        {
            
            Free = 0;
            FreeWindowSeats = 0;
            int findWindowSeats = 0;
            Boolean freeWindow = false;

            for (int i = 0; i < Booked.Length; i++)
            {


                if (false == Booked[i]) // Om platen är inte tagen
                {
                    Free++;
                    freeWindow = true;


                    
                }
                if (i == 17) // Vid 17 är det baksidan av bussen så tre sätten imellan
                {

                    findWindowSeats -= 3;

                }
                if (findWindowSeats == 0 || findWindowSeats == 3) // Om det är en fönsterplats för att varje 0 och 3 varv i loopen finns det en fönsterplats
                {
                    
                    if (freeWindow) // Om vi vet att den är ledig från innan
                    {
                        FreeWindowSeats++;
                    }
        
                    if (findWindowSeats == 3) 
                    {
                        findWindowSeats = -1;

                    }
                    findWindowSeats++;

                }
                else
                {
                    findWindowSeats++;
                }
            
                freeWindow = false;





            }

 

        }



        static void SearchBooking(Boolean window) // Ungeför likdan men window säger om kunden vill ha en fönsterplats
        {
            
            int findWindowSeats = 0;
            
            

            for (int i = 0; i < Booked.Length; i++)
            {

             
                if (i == 17)
                {

                    findWindowSeats -= 3;

                }
                else if (window == true && (findWindowSeats == 0 || findWindowSeats == 3))     
                {


                    if (Booked[i] == false)
                    {
                        UserInfo(i);
                        break;

                    }

                    if (findWindowSeats == 3)
                    {
                        findWindowSeats = -1;

                    }
                    findWindowSeats++;



                   
                }
                else if (window == false && (findWindowSeats != 0 && findWindowSeats != 3))
                {

                    if (Booked[i] == false)
                    {
                        UserInfo(i);
                        break;
                        

                    }

                }
                else
                {
                    findWindowSeats++;
                   
                    
                }
                

                

            }
                





        }





        

        static void UserInfo(int i) // Här skriver kunden in sin data
        {
            Console.WriteLine("\nPlats: " + i);

            Console.WriteLine("\nNamn: ");

            string name = Console.ReadLine();

            Console.WriteLine("Efternman:");

            string lastname = Console.ReadLine();

            Console.WriteLine("Gender:\n [1] Man\n [2] Kvinna\n [3] Annan");

            string gender = Console.ReadLine();

            switch (Int32.Parse(gender))
            {
                case 1:
                    gender = "Man";
                    break;
                case 2:
                    gender = "Kvinna";
                    break;

                default:
                    gender = "Annan";
                    break;
            }
            string SSN;
            int age;
            while (true)
            {
                try
                {
                    Console.WriteLine("Personummer YYYYMMDD:");

                    // SocialSecurityNumber
                    SSN = Console.ReadLine();

                    // Tycke att det skulle vara tråkigt att göra det vanligt så här är lösningen för 05:or
                    int year = Convert.ToInt32(SSN.Substring(0, 4));
                    int month = Convert.ToInt32(SSN.Substring(4, 2));
                    int day = Convert.ToInt32(SSN.Substring(6, 2));

                    var born = new DateTime(year, month, day);
                    var today = DateTime.Now;
                    var diffOfDates = today - born;
                    // Dagar delet till år som sedan kollar där nera om det är 18 upp
                    age = Convert.ToInt32(diffOfDates.TotalDays) / 365;
                    break;
                }
                catch (Exception e) {
                    Console.WriteLine("Felaktig datum");
                    continue;         
                }
              
            }
          
         
            BusSeats[i, 0] = name.ToLower();     // Så att vi kan söka efter den senare 

            BusSeats[i, 1] = lastname.ToLower(); 

            BusSeats[i, 3] = gender; 

            BusSeats[i, 2] = SSN;

            // Plats
            BusSeats[i, 5] = i.ToString();
           


            BusSeats[i, 4] =  age.ToString();

            

            Double pris;
            // Kollar vilket pris kunden ska få och lägger till dens årsgrupp i bussen
            if(age < 18)
            {
                Console.WriteLine("\n Du är underårig");
                Kids++;
                pris = KidsPrice;


            }
            else if(age > 69) 
            {

            

              
                Console.WriteLine("\n Du är gammal");
                Elderly++;
                pris = ElderlyPrice;

            }
            else
            {

                Console.WriteLine("\n Du är myndig");
                Adults++;
                pris = AdultsPrice;

            }

            while (true)
            {
                Console.WriteLine("\n Total Priset " + (pris.ToString()) + "\n [1] Confimera\n [2] Cancel ");

                String confirm = Console.ReadLine();
                if (int.TryParse(confirm, out int choice))
                {
                    switch (Convert.ToInt16(choice))
                    {
                        case 1:
                            Booked[i] = true;
                            Meny();
                            break;
                        case 2:
                            Meny();
                            break;
                        default:
                            Console.WriteLine("Felaktig input");
                            continue;

                    }

                }
                else
                {
                    Console.WriteLine("Felaktig input");
                    continue;

                }


           

            }
         


        }


        // CANCEL BOOKING

        private static void CancelBooking()
        {
            Console.WriteLine(" Cancel Booking \n" + ("").PadRight(24, '-') + "\n [1] Avboka med ditt förnamn \n [2] Avboka med ditt Personnummer \n [3] Menu");
            String cancel = Console.ReadLine();

            if (int.TryParse(cancel, out int choice))
            {
                switch (Convert.ToInt16(choice))
                {
                    case 1:
                        Console.WriteLine("Skriv Ditt Namn:");
                        String name = Console.ReadLine();

                        SearchUser(name, 0);
                        break;
                    case 2:
                        Console.WriteLine("Skriv Ditt personnummer:");
                        String SSN = Console.ReadLine();
                        SearchUser(SSN, 2);
                        break;
                    case 3:
                        Meny();
                        break;
                    default:
                        Console.WriteLine("Felaktig input");
                        CancelBooking();
                        break;
                }

            }
            else
            {
                Console.WriteLine("Felaktig input");
            }



            
           
        }

        static void SearchUser(String dataSearch, int dataType) // Dataserach är värdet på det som söks och Datatyp säger vilken sorts det är så platsen på rowen
        {
            dataSearch = dataSearch.ToLower();
            //Columm 0 = Förnamn
            //Columm 1 = Efternamn
            //Columm 2 = Personnummer 

            int[] findBooked = new int[21];


            int j = 0;
           
            //Koller efter en match
            for (int i = 0; i < 21; i++)
            {
                if (dataSearch == BusSeats[i, dataType]) 
                {
                    findBooked[j] = i; //  Det är en array som ger alla siffror till alla matches
                    j++;


                }

            }
            if (j == 0)
            {
                Console.WriteLine("Inga matchingar hittade");
                CancelBooking();
            }

            // Denna loop fungerar med att dubbel kolla om det är verkligen du 
            for (int i = 0;i < j;i++) {

             
                Console.WriteLine("Plats: " + BusSeats[findBooked[i], 5] + " Namn: "+BusSeats[findBooked[i], 0]+" Personnummer: "+BusSeats[findBooked[i],2]+ "\n VARNING: ÄR DETTA DIN PLATS \n [Y] för ja/[N] för nej");
                String chooseYourSeat = Console.ReadLine().ToLower();
                if (chooseYourSeat == "y")
                {
                    int ageRemovePrice = Convert.ToInt32(BusSeats[findBooked[i], 4]);
                    if (ageRemovePrice < 18)
                    {
                        
                        Kids--;
                        

                    }
                    else if (ageRemovePrice >= 69)
                    {
                    

                        Elderly--;


                    }
                    else
                    {
                        Adults--;

                     
                    }

                    int cancelSeat = Int32.Parse(BusSeats[findBooked[i], 5]);
                    Booked[cancelSeat] = false;
                    for (int k = 0; k < 5; k++)
                    {
                        BusSeats[findBooked[i], k] = null;

         
                    }

                    Console.WriteLine("Avbokad");

                    Meny();


                }
                else
                {
                    continue;
                }

            }
            Console.WriteLine("Du har hoppat över alla");
            CancelBooking();







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
                    string isItBlank = BusSeats[i, j];
                    
                    if (string.IsNullOrEmpty(isItBlank)) // Är row blank
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


            Console.WriteLine("Klicka en knapp för att gå vidare...");
            Console.ReadKey();
            Meny();

            
            


        }
 

    }
}

