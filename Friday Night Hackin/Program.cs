using System;
using static System.Console;
using System.Media;
using System.Timers;
using System.Diagnostics;
using static System.Threading.Thread;



namespace Friday_Night_Hackin
{

    class Program
    {
        private static int tempo = 100;
        private static string output;
        private static bool bouttaHit;
        private static int[] notes = { 3, 0, 4, 0, 4, 0, 0, 0, 3, 0, 4, 0, 4, 0, 0, 0, 2, 0, 1, 0, 4, 0, 0, 0, 2, 0, 1, 0, 4, 0, 0, 0, 2, 0, 4, 1, 2, 0, 0, 0, 2, 0, 4, 1, 2, 0, 0, 0, 4, 2, 0, 1, 3, 0, 0, 0, 4, 2, 0, 1, 3, 0, 0, 0, 1, 4, 2, 0, 0, 0, 0, 0, 1, 4, 2, 0, 0, 0, 0, 0, 2, 4, 1, 0, 0, 0, 0, 0, 2, 4, 1, 0, 0, 0, 0, 0, 3, 4, 1, 0, 0, 0, 0, 0, 3, 4, 1, 0, 0, 0, 0, 1, 4, 0, 3, 2, 0, 0, 0, 1, 4, 0, 3, 2, 0, 0, 0, 0, 3, 0, 4, 0, 1, 0, 0, 0, 3, 0, 4, 0, 1, 0, 0, 0, 2, 0, 3, 0, 2, 2, 3, 0, 2, 0, 3, 0, 2, 2, 3, 0, 3, 0, 4, 0, 1, 0, 0, 0, 3, 0, 4, 0, 1, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 3, 4, 1, 0, 0, 0, 0, 0, 3, 4, 1, 0, 0, 0, 0, 3, 1, 4, 0, 3, 2, 0, 0, 3, 1, 4, 0, 3, 2, 0, 0, 0, 3, 4, 1, 3, 2, 0, 0, 0, 3, 4, 1, 3, 2, 0, 0, 3, 1, 4, 0, 3, 2, 0, 0, 3, 1, 4, 0, 3, 2, 0, 0, 0, 0, 0, 0 };
        private static int cycles = 0;
        private static Timer aTimer;
        private static int input;
        public static bool hit;
        public static bool detectingHit;
        private static Stopwatch stopWatch = new Stopwatch();
        private static int score = 0;

        static void Main(string[] args)
        {
            SetWindowSize(Math.Min(LargestWindowWidth,90), Math.Min(LargestWindowHeight, 50));
            SetWindowPosition(0,0);
            SoundPlayer player = new SoundPlayer();
            player.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"\Sounds\Bopeebo.wav";
            player.Play();
            aTimer = new Timer();
            aTimer.Interval = 60000 / (tempo*2);
            aTimer.Elapsed += UpdateScreen;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
            while (true) 
            {
                Input();
            }

        }

        private async static void Input()
        {
            if (!bouttaHit || stopWatch.Elapsed.TotalMilliseconds >= 325)
            {
                if (bouttaHit)
                {
                    detectingHit = true;
                    bouttaHit = false;
                }
                else detectingHit = false;
                stopWatch.Stop();
                stopWatch.Reset();
                hit = false;
            }
            else
            {
                stopWatch.Start();
                if (stopWatch.Elapsed.TotalMilliseconds >= 225)
                {
                    //WriteLine(stopWatch.Elapsed.TotalMilliseconds);
                    if (KeyAvailable == false) return;
                    else
                    {
                        detectingHit = true;
                        switch (ReadKey().Key)
                        {
                            case ConsoleKey.LeftArrow: input = 1; break;
                            case ConsoleKey.DownArrow: input = 2; break;
                            case ConsoleKey.UpArrow: input = 3; break;
                            case ConsoleKey.RightArrow: input = 4; break;
                        }
                        if (input != 0)
                        {
                            if (notes[(cycles - 1)] == input)
                            {
                                hit = true;
                            }
                            else
                            {
                                hit = false;
                            }
                            detectingHit = true;
                            bouttaHit = false;
                            stopWatch.Stop();
                            stopWatch.Reset();
                        }
                    }
                }

            } 


        }
        private static async void UpdateScreen(Object source, System.Timers.ElapsedEventArgs e)
        {
            if (cycles + 3 > notes.Length) return;
            if (notes[(cycles)] != 0)
            {
                bouttaHit = true;
            }
            cycles++;
            Clear();
            SetCursorPosition(0, 0);
            output = @"
     ::::          ::           ::          :::      
    :sss:          ss         :ssss:        :sss:    
  :ssssss:::   ss:ssss:ss   :ssssssss:   :::ssssss:  
  :ssssss:::   :ssssssss:   ss:ssss:ss   :::ssssss:  
    :sss:        :ssss:         ss          :sss:    
      :::          ::           ::          :::    

";          
            if (detectingHit)
            {
                if (hit)
                {
                    score += 200;
                    output = @"
     ::::          ::           ::          :::      
    :sss:          ss         :ssss:        :sss:             Good
  :ssssss:::   ss:ssss:ss   :ssssssss:   :::ssssss:  
  :ssssss:::   :ssssssss:   ss:ssss:ss   :::ssssss:  
    :sss:        :ssss:         ss          :sss:    
      :::          ::           ::          :::    

";
                }
                else
                {
                    score += -200;
                    output = @"
     ::::          ::           ::          :::      
    :sss:          ss         :ssss:        :sss:             Shit
  :ssssss:::   ss:ssss:ss   :ssssssss:   :::ssssss:  
  :ssssss:::   :ssssssss:   ss:ssss:ss   :::ssssss:  
    :sss:        :ssss:         ss          :sss:    
      :::          ::           ::          :::    

";
                }
                detectingHit = false;
            }
            output += ScrollArrows(notes[(cycles)]);
            output += ScrollArrows(notes[(cycles+1)]);
            output += ScrollArrows(notes[(cycles+2)]);
            WriteLine(score);
            WriteLine(detectingHit);
            WriteLine(bouttaHit);
            WriteLine(stopWatch.Elapsed.TotalMilliseconds);
            WriteLine(output);
            
        }
        private static string ScrollArrows(int number)
        {
            string output = "";
            switch (number)
            {
                case 0:
                    output = @"
    






";
                    break;
                case 1:
                    output = @"
     ::::     
    :sss:
  :ssssss::: 
  :ssssss:::
    :sss:
      :::

";
                    break;
                case 2:
                    output = @"
                   ::      
                   ss 
               ss:ssss:ss  
               :ssssssss: 
                 :ssss:
                   ::  

";
                    break;
                case 3:
                    output = @"
                                ::          
                              :ssss:     
                            :ssssssss:  
                            ss:ssss:ss    
                                ss        
                                ::     

";
                    break;
                case 4:
                    output = @"
                                            :::      
                                            :sss:
                                         :::ssssss:  
                                         :::ssssss:  
                                            :sss:    
                                            :::    

";
                    break;
            }
            return output;
        }
        private static void Menu()
        {
            bool menu = true;
            while (menu)
            {
                WriteLine
                    (@"Main Menu:
1: Play Game
2: Instructions
3: Quit
                    ");
                string input = ReadLine();
            }
        }
    }

}

