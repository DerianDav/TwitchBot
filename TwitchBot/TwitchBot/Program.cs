using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;


namespace TwitchBot
{
    

    class Program {
        //takes oauth password from twitch
        static void Main(string[] args) {
            IrcClient irc = new IrcClient("irc.twitch.tv", 6667, "nightlurk", "oauth:p9znoj3v2mm3niyxd42o6n4ae5zmgs");
            irc.joinRoom("nightlurk");

            Mod mod = new Mod();
            Random rand = new Random();

            while (true) {
                string message = irc.readMessage();
                string curUser = irc.getUserName(message);

              
//                if (message != "")
  //                  irc.sendChatMessage(message);

                if (message.Contains("!banRoulette")) {
                    int random = rand.Next();
                    string result;
                    if (random % 6 == 0)
                    {
                        result = "RIP";
                        mod.timeout(curUser, irc);
                    }
                    else
                    {
                        result = "nothing happens.";
                    }
                    irc.sendChatMessage(curUser + " spins the barrel and shoots ... 1 second later and " + result);
                }

                mod.containsBannedWord(message, curUser, irc);





            } //end of while

        }//end of main


        }

    }//end of program



