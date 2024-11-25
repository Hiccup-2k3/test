using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace learning
{
    class Program
    {
        static int GetNumber()
        {
            while (true)
            {
                try
                {
                    
                    string input = Console.ReadLine();
                    int number;
                    if (int.TryParse(input, out number))
                    {
                        Console.WriteLine("You chose  : " + number);
                        Console.WriteLine("");
                        return number;
                    }

                    else
                    {
                        throw new Exception("enter again:");

                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }


            }
        }

        static string Rock_Paper_Scissors()
        {
            List<string> weapons = new List<string> { "rock", "paper", "scissors" };
            
            while(true)
            {
                string res = Console.ReadLine();
                res.ToLower();
                if (weapons.Contains(res))
                    return res;
                else
                {
                    Console.WriteLine("rechose your weapons, only rock paper or scissors");
                    Console.WriteLine("");
                }
            }

           
        }
        static List<int> Solving(int begin, int end, int step )
        {
            List<int> res = new List<int>();
            for(int i = end;i>=begin;i=i-step-1)
                res.Add(i);
            res.Reverse();
            return res;

        }

        static int WhoFirst(string p,string c,out bool turn)
        {
            if (c == p)
            {
                Console.WriteLine("my choice : "+ c); Console.WriteLine("");
                Console.WriteLine("tie so we chose again !! ");
                turn = false;
                return 0; 
            }

            
            if ((c == "rock" && p == "scissors") ||
                (c == "paper" && p == "rock") ||
                (c == "scissors" && p == "paper"))
            {
                Console.WriteLine("my choice : " + c); Console.WriteLine("");
                Console.WriteLine("so i win, i go first !!"); Console.WriteLine("");
                turn = false;
                return -1; 
            }
            else
            {
                Console.WriteLine("my choice : " + c); Console.WriteLine("");
                Console.WriteLine("so you win, you go first !!"); Console.WriteLine("");
                turn = true;
                return 1; 
            }
        }
        static void Main()
        {
            Player player = new Player();
            Computer computer = new Computer();
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Hello Champion ! welcome to Take Number Game ");
            Console.WriteLine("Enter your name : ");
            string s = Console.ReadLine();
            player.SetName(s);
            Console.WriteLine("Shall we begin !!!! "); Console.WriteLine("");
            Console.WriteLine("Chose the begin number : ");
            int nBegin_Number = GetNumber();
            Console.WriteLine("Chose the end number : ");
            int nEnd_Number = GetNumber();
            Console.WriteLine("Chose the maximums numbers each one can get per turn : ");
            int nStep = GetNumber();


            //pre gaming
            bool man_first ;
            Console.WriteLine("Chose rock, paper or scissors to know who play first !! "); Console.WriteLine("");
            do
            {

                

                computer.SetWeapon();
                player.SetWeapon(Rock_Paper_Scissors());
            }
            while (WhoFirst(player.GetWeapon(), computer.GetWeapon(),out man_first) == 0);

            // decide who goes first 

            List<int> com_play = Solving(nBegin_Number,nEnd_Number,nStep);
            List<int> com_get = new List<int>();
            List<int> per_get = new List<int>();
            int nCurrent_Number = 0;
            Random random = new Random();
            int index = 0;
            if (man_first)
            {
                
                while (nCurrent_Number < nEnd_Number)
                {
                    int oldnumber=nCurrent_Number;
                    Console.WriteLine("please enter how many numbers do you want to get this turn : "); 
                    Console.WriteLine("");
                    nCurrent_Number += GetNumber();
                    
                    for(int i=oldnumber+1;i<=nCurrent_Number;i++)
                        per_get.Add(i);

                    Console.Write("so you have : ");foreach(int i in per_get)  Console.Write(i+"  ");
                    Console.WriteLine("");

                    if (index < com_play.Count &&  com_play[index]<=nCurrent_Number+nStep && com_play[index]>nCurrent_Number)
                    {
                        Console.WriteLine("i take {0} numbers", com_play[index]-nCurrent_Number); Console.WriteLine("");
                        Console.Write("i have : "); foreach (int i in com_get) Console.Write(i + "  ");
                        for (int i=nCurrent_Number+1;i<=com_play[index];i++)
                        {
                            Console.Write(i+"  ");
                            com_get.Add(i);
                            
                        }
                        
                        nCurrent_Number=com_play[index++];
                        Console.WriteLine("");
                    }
                    else
                    {
                        if (nCurrent_Number == nEnd_Number) break;
                        int temp = random.Next(1,nStep+1);
                        Console.WriteLine("i take {0} numbers ",temp); Console.WriteLine("");
                        Console.Write("i have : "); foreach(int i in com_get) Console.Write(i+"  ");
                        for (int i=nCurrent_Number+1; i <=nCurrent_Number+temp;i++)
                        {
                            Console.Write(i+"  ");
                            com_get.Add(i);
                        }
                        nCurrent_Number += temp;
                        Console.WriteLine("");
                    }



                }
                
            }
            else
            {
                
                while (nCurrent_Number < nEnd_Number)
                {
                    

                    if (index < com_play.Count && com_play[index] <= nCurrent_Number + nStep && com_play[index] > nCurrent_Number)
                    {
                        Console.WriteLine("i take {0} numbers", com_play[index] - nCurrent_Number); Console.WriteLine("");
                        Console.Write("i have : "); foreach (int i in com_get) Console.Write(i + "  ");
                        for (int i = nCurrent_Number + 1; i <= com_play[index]; i++)
                        {
                            Console.Write(i + "  ");
                            com_get.Add(i);

                        }

                        nCurrent_Number = com_play[index++];
                        Console.WriteLine("");
                    }
                    else
                    {
                        
                        int temp = random.Next(1, nStep + 1);
                        Console.WriteLine("i take {0} numbers ", temp); Console.WriteLine("");
                        Console.Write("i have : "); foreach (int i in com_get) Console.Write(i + "  ");
                        for (int i = nCurrent_Number + 1; i <= nCurrent_Number + temp; i++)
                        {
                            Console.Write(i + "  ");
                            com_get.Add(i);
                        }
                        nCurrent_Number += temp;
                        Console.WriteLine("");
                    }
                    if (nCurrent_Number == nEnd_Number) break;
                    Console.WriteLine("please enter how many numbers do you want to get this turn : ");
                    Console.WriteLine("");
                    int oldnumber = nCurrent_Number;
                    nCurrent_Number += GetNumber();

                    for (int i = oldnumber + 1; i <= nCurrent_Number; i++)
                        per_get.Add(i);

                    Console.Write("so you have : "); foreach (int i in per_get) Console.Write(i + "  ");
                    Console.WriteLine("");

                    



                }
                
            }

            if (per_get[per_get.Count() - 1] == nEnd_Number)
                Console.WriteLine("You Win !! Congratuation");
            else
                Console.WriteLine("HAHAHA!! I WIN YOU LOSE :> *chicken*");






        }
    }
}
