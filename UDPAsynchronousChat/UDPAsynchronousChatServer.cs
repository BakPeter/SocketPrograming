using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UDPAsynchronousChat
{
    public class UDPAsynchronousChatServer
    {
        Socket mSocketBroadcastReceiver;
        IPEndPoint mIPEPLocal;
        private int mRetryCount = 0;

        List<EndPoint> mListOfClients;

        public UDPAsynchronousChatServer()
        {
            mSocketBroadcastReceiver = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Dgram,
                ProtocolType.Udp);
            mSocketBroadcastReceiver.EnableBroadcast = true;

            mIPEPLocal = new IPEndPoint(IPAddress.Any, 23000);

            mListOfClients = new List<EndPoint>(0);
        }

        public void StartReceivingData()
        {
            try
            {
                SocketAsyncEventArgs saea = new SocketAsyncEventArgs();
                saea.SetBuffer(new byte[1024], 0, 1024);
                saea.RemoteEndPoint = new IPEndPoint(IPAddress.Any, 0);

                if (!mSocketBroadcastReceiver.IsBound)
                {
                    mSocketBroadcastReceiver.Bind(mIPEPLocal);
                }

                //saea.Completed += ReceiveCompletedCallBack;
                saea.Completed += new EventHandler<SocketAsyncEventArgs>(ReceiveCompletedCallBack);



                if (!mSocketBroadcastReceiver.ReceiveFromAsync(saea))
                {
                    Console.WriteLine($"Failed to receive data - socket error: {saea.SocketError}");

                    if (mRetryCount++ >= 10)
                    {
                        return;
                    }
                    else
                    {
                        StartReceivingData();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        private void ReceiveCompletedCallBack(object sender, SocketAsyncEventArgs e)
        {
            string textReceived = Encoding.ASCII.GetString(e.Buffer, 0, e.BytesTransferred);
            Console.WriteLine(
                $"Text received: {textReceived}{Environment.NewLine}" +
                $"Number of bytes received: {e.BytesTransferred}{Environment.NewLine}" +
                $"Received data from endpoint: {e.RemoteEndPoint}{Environment.NewLine}");

            if (textReceived.Equals("<DISCOVER>"))
            {
                mListOfClients.Add(e.RemoteEndPoint);
                Console.WriteLine("Total Cliebtd: " + mListOfClients.Count);
            }

            SendTextToEndPoint("<CONFIRMED>", e.RemoteEndPoint);

            StartReceivingData();
        }

        private void SendTextToEndPoint(string textToSend, EndPoint remoteEndPoint)
        {
            if(string.IsNullOrEmpty(textToSend))
            {
                return;
            }

            SocketAsyncEventArgs saeaSend = new SocketAsyncEventArgs();
            saeaSend.RemoteEndPoint = remoteEndPoint;

            var bytesToSend = Encoding.ASCII.GetBytes(textToSend);

            saeaSend.SetBuffer(bytesToSend, 0, bytesToSend.Length);
            saeaSend.Completed += SendTextToEndPointCompleted;

            mSocketBroadcastReceiver.SendToAsync(saeaSend);
        }

        private void SendTextToEndPointCompleted(object sender, SocketAsyncEventArgs e)
        {
            Console.WriteLine($"Completed sending text to {e.RemoteEndPoint}");
        }
    }
}