using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Net.Sockets;
    using System.Net;
    using System.IO;


  
        class IrcClient
        {
            private string userName;
            private string channel;

            private TcpClient tcpClient;
            private StreamReader inputStream;
            private StreamWriter outputStream;

            public IrcClient(string ip, int port, string userName, string password)
            {
                this.userName = userName;

                tcpClient = new TcpClient(ip, port);
                inputStream = new StreamReader(tcpClient.GetStream());
                outputStream = new StreamWriter(tcpClient.GetStream());

                outputStream.WriteLine("PASS " + password);
                outputStream.WriteLine("NICK " + userName);
                outputStream.WriteLine("USER " + userName + " 8 * :" + userName);
                outputStream.Flush();

            }

            public void joinRoom(string channel)
            {
                this.channel = channel;
                outputStream.WriteLine("JOIN #" + channel);
                outputStream.Flush();


            }//end of joinRoom

            public void sendIrcMessage(string message)
            {
                outputStream.WriteLine(message);
                outputStream.Flush();
            }//end of sendIrcMessage


            public void sendChatMessage(string message)
            {
                sendIrcMessage(":" + userName + "!" + userName + "@" + userName +
                    ".tmi.twitch.tv PRIVMSG #" + channel + " :" + message);
            }//end of sendchatmeesage

            public string readMessage()
            {
                string message = inputStream.ReadLine();
            if (message == null)
                message = "";
                return message;

            }//end of readmessage

            public string getUserName(string message)
            {
                string[] split = message.Split('!');
                split[0] = split[0].Remove(0, 1);
                return split[0];
            }

       
    }//end of ircclient
    }


