using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace demyUDPBroadcastReceiver
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket sockBroadcastReceiver = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint ipepLocal = new IPEndPoint(IPAddress.Any, 23000);

            byte[] receivedBuffer = new byte[512];
            int nCountReceived;
            string textReceived = string.Empty;

            IPEndPoint ipepSender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint epSender = (EndPoint)ipepSender;

            try
            {
                sockBroadcastReceiver.Bind(ipepLocal);

                while (true)
                {
                    //nCountReceived = sockBroadcastReceiver.Receive(receivedBuffer);
                    nCountReceived = sockBroadcastReceiver.ReceiveFrom(receivedBuffer, ref epSender);
                    textReceived = Encoding.ASCII.GetString(receivedBuffer, 0, nCountReceived);

                    Console.WriteLine("----------------------------------------------------");
                    Console.WriteLine($"Number of bytes received: {nCountReceived}");
                    Console.WriteLine($"Data: {textReceived.Substring(0, nCountReceived)}");
                    Console.WriteLine($"Received from: {epSender}");
                    Console.WriteLine("----------------------------------------------------");
            

                    if(textReceived.Equals("<ECHO>"))
                    {
                        sockBroadcastReceiver.SendTo(receivedBuffer, 0, nCountReceived, SocketFlags.None, epSender);
                        Console.WriteLine("Text echoed back");
                    }
                    Array.Clear(receivedBuffer, 0, receivedBuffer.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
    }
}
