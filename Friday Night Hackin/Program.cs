using System;
using static System.Console;
using System.Media;
using System.Timers;
using System.Diagnostics;
using System.Collections.Generic;
using static Friday_Night_Hackin.Rainbow;

namespace Friday_Night_Hackin
{

    class Program
    {
        static int combo = 0;
        private static int tempo = 100;
        private static string output;
        private static bool bouttaHit;
        private static int[] notes = { 0, 4, 0, 4, 0, 0, 0, 3, 0, 4, 0, 4, 0, 0, 0, 2, 0, 1, 0, 4, 0, 0, 0, 2, 0, 1, 0, 4, 0, 0, 0, 2, 0, 4, 1, 2, 0, 0, 0, 2, 0, 4, 1, 2, 0, 0, 0, 4, 2, 0, 1, 3, 0, 0, 0, 4, 2, 0, 1, 3, 0, 0, 0, 1, 4, 2, 0, 0, 0, 0, 0, 1, 4, 2, 0, 0, 0, 0, 0, 2, 4, 1, 0, 0, 0, 0, 0, 2, 4, 1, 0, 0, 0, 0, 0, 3, 4, 1, 0, 0, 0, 0, 0, 3, 4, 1, 0, 0, 0, 0, 1, 4, 0, 0, 3, 2, 0, 0, 1, 4, 0, 0, 3, 2, 0, 0, 0, 3, 0, 4, 0, 1, 0, 0, 0, 3, 0, 4, 0, 1, 0, 0, 0, 2, 0, 3, 0, 2, 2, 3, 0, 2, 0, 3, 0, 2, 2, 3, 0, 3, 0, 4, 0, 1, 0, 0, 0, 3, 0, 4, 0, 1, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 3, 4, 1, 0, 0, 0, 0, 0, 3, 4, 1, 0, 0, 0, 0, 3, 1, 4, 0, 3, 2, 0, 0, 3, 1, 4, 0, 3, 2, 0, 0, 0, 3, 4, 1, 3, 2, 0, 0, 0, 3, 4, 1, 3, 2, 0, 0, 3, 1, 4, 0, 3, 2, 0, 0, 3, 1, 4, 0, 3, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        private static int cycles = 0;
        private static Timer aTimer;
        private static int input;
        public static bool hit;
        public static bool detectingHit;
        private static Stopwatch stopWatch = new Stopwatch();
        private static int score = 0;
        private static int scoreToAdd = 0;
        static bool play = true;
        static List<ConsoleKey> inputs = new List<ConsoleKey> { };
        static void Main(string[] args)
        {
            SoundPlayer player = new SoundPlayer();
            player.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"\Sounds\Menu.wav";
            player.Play();
            while(Menu());
            SetWindowSize(Math.Min(LargestWindowWidth,90), Math.Min(LargestWindowHeight, 50));
            SetWindowPosition(0,0);
            player.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"\Sounds\Bopeebo.wav";
            player.Play();
            aTimer = new Timer();
            aTimer.Interval = 60000 / (tempo*2);
            aTimer.Elapsed += UpdateScreen;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
            while (play) Input();

        }

        private static void Input()
        {
            input = 0;
            inputs.Clear();
            if ((!bouttaHit || stopWatch.Elapsed.TotalMilliseconds >= (60000 / (tempo * 2))*1.2) && !detectingHit)
            {
                if (bouttaHit)
                {
                    detectingHit = true;
                    bouttaHit = false;
                    hit = false;
                }
                else detectingHit = false;
                inputs.Clear();
                stopWatch.Stop();
                stopWatch.Reset();
            }
            else if(bouttaHit)
            {
                if(!stopWatch.IsRunning) stopWatch.Start();
                if (stopWatch.ElapsedMilliseconds >= (60000 / (tempo * 2)) * 0.8)
                {
                    if (KeyAvailable == false) return;
                    else
                    {
                        
                        while (KeyAvailable == true) inputs.Add(ReadKey().Key);
                        detectingHit = true;

                        switch (inputs[inputs.Count-1])
                        {
                            case ConsoleKey.LeftArrow: input = 1; break;
                            case ConsoleKey.DownArrow: input = 2; break;
                            case ConsoleKey.UpArrow: input = 3; break;
                            case ConsoleKey.RightArrow: input = 4; break;
                        }
                        if (input != 0)
                        {
                            
                            if (notes[cycles - 1] == input)
                            {
                                hit = true;
                                scoreToAdd = 200 - Math.Abs(225 - (int)stopWatch.ElapsedMilliseconds);
                            }
                            else
                            {
                                hit = false;
                            }
                            inputs.Clear();
                            detectingHit = true;
                            bouttaHit = false;
                            stopWatch.Stop();
                            stopWatch.Reset();
                            return;
                        }
                    }
                }
            } 
        }
        private async static void UpdateScreen(Object source, ElapsedEventArgs e)
        {
            if (cycles + 4 > notes.Length) { play = false; return; }
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
  :ssssss:::   :ssssssss:   ss:ssss:ss   :::ssssss:       Combo: " + combo + @"
    :sss:        :ssss:         ss          :sss:    
      :::          ::           ::          :::    

"; 
            //Type(detectingHit);
            if (detectingHit)
            {
                if (hit)
                {
                    combo++;
                    score += scoreToAdd +(combo * 5);
                    output = @"
     ::::          ::           ::          :::      
    :sss:          ss         :ssss:        :sss:             Good
  :ssssss:::   ss:ssss:ss   :ssssssss:   :::ssssss:           
  :ssssss:::   :ssssssss:   ss:ssss:ss   :::ssssss:       Combo: " + combo + @"
    :sss:        :ssss:         ss          :sss:    
      :::          ::           ::          :::    

";
                }
                else
                {
                    combo = 0;
                    score += -200;
                    output = @"
     ::::          ::           ::          :::      
    :sss:          ss         :ssss:        :sss:             Shit
  :ssssss:::   ss:ssss:ss   :ssssssss:   :::ssssss:  
  :ssssss:::   :ssssssss:   ss:ssss:ss   :::ssssss:       Combo Break!
    :sss:        :ssss:         ss          :sss:    
      :::          ::           ::          :::    

";
                }
                detectingHit = false;
            }

            output += ScrollArrows(notes[(cycles)]);
            output += ScrollArrows(notes[(cycles+1)]);
            output += ScrollArrows(notes[(cycles+2)]);
            Type(score.ToString());
            //Type(bouttaHit);
            //Type(stopWatch.Elapsed.TotalMilliseconds);
            TypeLines(output);
            
        }
        private static string ScrollArrows(int number)
        {
            string output = "";
            switch (number)
            {
                case 0:
                    output = "\n\n\n\n\n\n\n\n";
                    break;
                case 1:
                    output = @"
     ::::     
    :sss:
  :ssssss::: 
  :ssssss:::
    :sss:
      :::";
                    break;
                case 2:
                    output = @"
                   ::      
                   ss 
               ss:ssss:ss  
               :ssssssss: 
                 :ssss:
                   ::";
                    break;
                case 3:
                    output = @"
                                ::          
                              :ssss:     
                            :ssssssss:  
                            ss:ssss:ss    
                                ss        
                                ::";
                    break;
                case 4:
                    output = @"
                                            :::      
                                            :sss:
                                         :::ssssss:  
                                         :::ssssss:  
                                            :sss:    
                                            :::";
                    break;
            }
            return output;
        }
        private static bool Menu()
        {
            Clear();
            TypeLines(@"
___________      .__    .___               _______  .__       .__     __   
\_   ____________|__| __| ______  ___.__.  \      \ |__| ____ |  |___/  |_ 
 |    __) \_  __ |  |/ __ |\__  \<   |  |  /   |   \|  |/ ___\|  |  \   __\
 |     \   |  | \|  / /_/ | / __ \\___  | /    |    |  / /_/  |   Y  |  |  
 \___  /   |__|  |__\____ |(____  / ____| \____|__  |__\___  /|___|  |__|  
     \/                  \/     \/\/              \/  /_____/      \/      
                  ___ ___                __   .__     /\                   
                 /   |   \_____    ____ |  | _|__| ___)/                   
                /    ~    \__  \ _/ ___\|  |/ |  |/    \                   
                \    Y    // __ \\  \___|    <|  |   |  \                  
                 \___|_  /(____  /\___  |__|_ |__|___|  /                  
                       \/      \/     \/     \/       \/                   
  __  __                  
 |  \/  |                 
 | \  / | ___ _ __  _   _ 
 | |\/| |/ _ | '_ \| | | |
 | |  | |  __| | | | |_| |
 |_|  |_|\___|_| |_|\__,_|
                          
1: Play Game
2: Instructions
3: Quit");
            switch (ReadKey().Key)
            {
                case ConsoleKey.D1:
                    return false;
                case ConsoleKey.D2:
                    Clear();
                    Type(@"You play the game by pressing the arrow keys
when the arrows reach the top.");
                    ReadKey();
                    return true;
                case ConsoleKey.D3:
                    play = false;
                    return false;
                default: 
                    return true;
            }
        }
    }
    static class Rainbow
    {

        public static void TypeLines(string input)
        {
            int count = 9;
            foreach (string line in input.Split('\n'))
            {
                WriteLine(line);
                ForegroundColor = (ConsoleColor)count;
                count++;
                if (count >= 15) count = 9;

            }
        }
        public static void Type(string input)
        {
            int count = 9;
            foreach (char letter in input.ToCharArray())
            {
                Write(letter);
                ForegroundColor = (ConsoleColor)count;
                if(letter != ' ' || letter != '\n') count++;
                if (count >= 15) count = 9;

            }
        }
    }

}

