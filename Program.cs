using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Media;
using System.Diagnostics;

/*
Drunk PC Application
Description: App that generates erratic mouse and keyboard and input, as well as system sounds, etc

    Topics:
    - Threads
    - System.windows.forms namespace & assembly
    - Hidden application
*/

namespace DrunkPC
{
    class Program
    {
        //Globals
        public static Random _random = new Random();
        public static int _startupDelay = 60;
        public static int _totalDuration = 18;
        
        /// <summary>
        /// entry point for application
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //for setting startup and duration delay locally in cmd
            if(args.Length >= 2)
            {
                _startupDelay = Convert.ToInt32(args[0]);
                _totalDuration = Convert.ToInt32(args[1]);
            }

            //Console.WriteLine("Drunk PC Application");

            //create threads that manipulate inputs/outputs of system
            Thread drunkMouseThread = new Thread(new ThreadStart(DrunkMouseThread));
            Thread drunkKeyboardThread = new Thread(new ThreadStart(DrunkKeyboardThread));
            Thread drunkSoundThread = new Thread(new ThreadStart(DrunkSoundThread));
            Thread drunkPopupThread = new Thread(new ThreadStart(DrunkPopupThread));
            //Thread openBrowser = new Thread(new ThreadStart(OpenBrowser));

            //for loop for how many times to run the program
            for (int count = 0; count < 50; count++)
            {
                //how long to wait until opening threads
                DateTime future = DateTime.Now.AddSeconds(_startupDelay);
                while (future > DateTime.Now)
                {
                    Thread.Sleep(1000);
                }

                //Start all threads
                drunkMouseThread.Start();
                drunkKeyboardThread.Start();
                drunkSoundThread.Start();
                drunkPopupThread.Start();
                //openBrowser.Start();

                //how long program runs before closing
                future = DateTime.Now.AddSeconds(_totalDuration);
                while (future > DateTime.Now)
                {
                    Thread.Sleep(1000);
                }

                //Kill all threads
                drunkMouseThread.Abort();
                drunkKeyboardThread.Abort();
                drunkSoundThread.Abort();
                drunkPopupThread.Abort();
                //openBrowser.Abort();
            }//for  
        }//main

        #region Drunk Threads
        /// <summary>
        /// this thread will randomly affect mouse movement
        /// </summary>
        public static void DrunkMouseThread()
        {
            Console.WriteLine("Mouse Started");

            int moveX = 0;
            int moveY = 0;

            while (true)
            {
                // Console.WriteLine(Cursor.Position.ToString());
                if (_random.Next(100) > 50)
                {
                    //Generate the random amount to move cursor
                    moveX = _random.Next(20) - 10;
                    moveY = _random.Next(20) - 10;

                    //Change mouse pos to new random co-ordinates
                    Cursor.Position = new System.Drawing.Point(Cursor.Position.X + moveX, Cursor.Position.Y + moveY);
                }//if
                Thread.Sleep(50);
            }
        }

        /// <summary>
        /// creates random keyboard input
        /// </summary>
        public static void DrunkKeyboardThread()
        {
            //Console.WriteLine("Keyboard Started");

            while (true)
            {
                //generate a random capital letter
                char key = (char)(_random.Next(25) + 65);

                //50/50 lower case char
                if (_random.Next(2) == 0)
                {
                    key = Char.ToLower(key);
                }

                SendKeys.SendWait(key.ToString());
                Thread.Sleep(_random.Next(500));
            }
        }

        /// <summary>
        /// creates random sounds
        /// </summary>
        public static void DrunkSoundThread()
        {
            //Console.WriteLine("Sound Started");

            while (true)
            {
                //50/50 chance of playing sound, every 4 seconds ( should play once every 8 seconds in theory )
                if (_random.Next(100) > 50)
                {
                    switch (_random.Next(5))
                    {
                        case 0:
                            SystemSounds.Asterisk.Play();
                            break;
                        case 1:
                            SystemSounds.Beep.Play();
                            break;
                        case 2:
                            SystemSounds.Exclamation.Play();
                            break;
                        case 3:
                            SystemSounds.Question.Play();
                            break;
                        case 4:
                            SystemSounds.Hand.Play();
                            break;                      
                    }//switch
                }//if
                Thread.Sleep(4000);
            }
        }

        /// <summary>
        /// creates random pop ups
        /// </summary>
        public static void DrunkPopupThread()
        {
            //Console.WriteLine("Popups Started");

            while (true)
            {
                if (_random.Next(100) > 40)
                {
                    //Determine which message to show to the user
                    switch (_random.Next(2))
                    {
                        case 0:
                            MessageBox.Show("swtor.exe has been corrupted, please remove the program",
                            "swtor",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;
                        case 1:
                            MessageBox.Show("System 32 has been modified due to a corrupted file. As a result your system is running low on resources",
                            "Microsoft Windows",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        break;
                    }
                }
                Thread.Sleep(8000);
            }
        }

       /* public static void OpenBrowser()
        {
            Process p1 = new Process();
            p1.StartInfo.FileName = "chrome.exe";
            p1.StartInfo.Arguments = "https://www.youtube.com/watch?v=dQw4w9WgXcQ";
            p1.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            p1.Start();
        }*/
        #endregion
    }
}
