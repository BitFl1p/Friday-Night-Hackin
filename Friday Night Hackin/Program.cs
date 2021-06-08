using System;
using static System.Console;
using System.Media;
using System.Timers;



namespace Friday_Night_Hackin
{

    class Program
    {
       
        private static int[] notes = { 3, 0, 4, 0, 4, 0, 0, 0, 3, 0, 4, 0, 4, 0, 0, 0 };
        private static int cycles = 0;
        private static Timer aTimer;
        private static int input;

        static void Main(string[] args)
        {
            SetWindowSize(90,50);
            SetWindowPosition(0,0);
            SoundPlayer player = new SoundPlayer();
            player.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"\Sounds\Bopeebo.wav";
            player.Play();
            aTimer = new System.Timers.Timer();
            aTimer.Interval = 275;
            aTimer.Elapsed += UpdateScreen;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
            while (true) 
            {
                switch (ReadKey().Key)
                {
                    case ConsoleKey.RightArrow: input = 1; break;
                    case ConsoleKey.DownArrow: input = 2; break;
                    case ConsoleKey.UpArrow: input = 3; break;
                    case ConsoleKey.LeftArrow: input = 4; break;
                }
            }
        }
        private static void UpdateScreen(Object source, System.Timers.ElapsedEventArgs e)
        {
            
            cycles++;
            Clear();
            SetCursorPosition(0, 0);
            string output = @"
     ::::          ::           ::          :::      
    :sss:          ss         :ssss:        :sss:    
  :ssssss:::   ss:ssss:ss   :ssssssss:   :::ssssss:  
  :ssssss:::   :ssssssss:   ss:ssss:ss   :::ssssss:  
    :sss:        :ssss:         ss          :sss:    
      :::          ::           ::          :::    

";          if (cycles + 3 > notes.Length) return;
            if(notes[(cycles-1)] != 0)
            {
                if(notes[(cycles - 1)] == input)
                {
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
                    output = @"
     ::::          ::           ::          :::      
    :sss:          ss         :ssss:        :sss:             Shit
  :ssssss:::   ss:ssss:ss   :ssssssss:   :::ssssss:  
  :ssssss:::   :ssssssss:   ss:ssss:ss   :::ssssss:  
    :sss:        :ssss:         ss          :sss:    
      :::          ::           ::          :::    

";
                }
            }

            output += ScrollArrows(notes[(cycles)]);
            output += ScrollArrows(notes[(cycles+1)]);
            output += ScrollArrows(notes[(cycles+2)]);

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

