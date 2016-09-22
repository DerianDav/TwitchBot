using System; 
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot
{
    class Mod
    {
        StreamReader input = new StreamReader("../../../BannedWords.txt");
        StreamReader settings = new StreamReader("../../../Settings.txt");
        StreamWriter timeoutRecord = new StreamWriter("../../../TimeoutRecord.txt");
        int record = 0;

        private List<string> bannedWords = new List<string>();


        public Mod() {
            while(input.EndOfStream == false)
            {
                bannedWords.Add(input.ReadLine().ToLower());
            }

            string recordReader = settings.ReadLine();
            recordReader = recordReader.Remove(0, recordReader.Length - 1);
            if (recordReader == "1") 
            record = 1;

        }//end of mod()

        //returns 1 if the message has a word that is banned
        //if recording is set to 1 it will write to a file whenever someone is timed out

        public int containsBannedWord(string message, string user, IrcClient irc) {
            string[] banned = bannedWords.ToArray();
            for (int i = 0; i < banned.Length; i++)
                if (message.Contains(banned[i]))
                {
                    irc.sendChatMessage("record = " + record);
                    if (record == 1) {
                        char[] spil = { ':' };
                        string[] userMessage = message.Split(spil);
                     
                        string write = "TimedOut: " + user + "            Word Used = " + banned[i] + "               Complete text = " + userMessage[2];
                        irc.sendChatMessage("write = " + write);
                        timeoutRecord.Flush();
                    }

                    timeout(user, irc);
                    return 1;

                }
            return 0;
        }//end of isBanned

        public void timeout(string user, IrcClient irc)
        {
            irc.sendChatMessage(".timeout " + user);
            irc.sendChatMessage(".unban " + user);
        }

    }//end of class mod()




}
