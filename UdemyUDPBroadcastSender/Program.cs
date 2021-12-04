using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace UdemyUDPBroadcastSender
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket sockBroadcaster = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp)
            {
                EnableBroadcast = true
            };

            IPEndPoint broadcastEP = new IPEndPoint(IPAddress.Parse("255.255.255.255"), 23000);

            byte[] broadcastBuffer = new byte[] { 0x0D, 0x0A };//stands for /r/n
            string strUserInput = string.Empty;

            IPEndPoint ipepSender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint epSender = (EndPoint)ipepSender;

            try
            {
                sockBroadcaster.Bind(new IPEndPoint(IPAddress.Any, 0));
                while(true)
                {
                    Console.WriteLine("Enter text('<EXIT>' to exit):");
                    strUserInput = Console.ReadLine();

                    if (strUserInput.Equals("<EXIT>"))
                    {
                        break;
                    }

                    broadcastBuffer = Encoding.ASCII.GetBytes(strUserInput);
                    _ = sockBroadcaster.SendTo(broadcastBuffer, broadcastEP);

                    int nCountReceived = 0;
                    string textReceived = string.Empty;

                    if (strUserInput.Equals("<ECHO>"))
                    {
                        nCountReceived = sockBroadcaster.ReceiveFrom(broadcastBuffer, ref epSender);
                        textReceived = Encoding.ASCII.GetString(broadcastBuffer, 0, nCountReceived);

                        Console.WriteLine("----------------------------------------------------");
                        Console.WriteLine($"Number of bytes received: {nCountReceived}");
                        Console.WriteLine($"Data: {textReceived.Substring(0, nCountReceived)}");
                        Console.WriteLine($"Received from: {epSender}");
                        Console.WriteLine("----------------------------------------------------");


                        Array.Clear(broadcastBuffer, 0, broadcastBuffer.Length);
                    }
                }

                sockBroadcaster.Shutdown(SocketShutdown.Both);
                sockBroadcaster.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
