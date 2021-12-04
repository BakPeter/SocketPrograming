using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;

namespace UDPAsynchronousChat
{
    public class UDPAsynchronousChatClient
    {
        Socket mSocketBroadcastSender;
        IPEndPoint mIPEPBroadcst;
        IPEndPoint mIPEPLocal;
        private EndPoint mChatServerEP;

        public UDPAsynchronousChatClient(int _localPort, int _remotePort)
        {
            mIPEPBroadcst = new IPEndPoint(IPAddress.Broadcast, _remotePort);
            mIPEPLocal = new IPEndPoint(IPAddress.Any, _localPort);

            mSocketBroadcastSender = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Dgram,
                ProtocolType.Udp);
            mSocketBroadcastSender.EnableBroadcast = true;
        }

        public void SendBroadcast(string strDataForBroadcast)
        {
            if (string.IsNullOrEmpty(strDataForBroadcast))
            {
                return;
            }

            try
            {
                if (!mSocketBroadcastSender.IsBound)
                {
                    mSocketBroadcastSender.Bind(mIPEPLocal);
                }

                var dataBytes = Encoding.ASCII.GetBytes(strDataForBroadcast);

                SocketAsyncEventArgs saea = new SocketAsyncEventArgs();
                saea.SetBuffer(dataBytes, 0, dataBytes.Length);
                saea.RemoteEndPoint = mIPEPBroadcst;
                saea.Completed += SendCompletedCallBack;

                mSocketBroadcastSender.SendToAsync(saea);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        private void SendCompletedCallBack(object sender, SocketAsyncEventArgs e)
        {
            Console.WriteLine($"Data send succesfully, to: {e.RemoteEndPoint }");

            if (Encoding.ASCII.GetString(e.Buffer).Equals("<CONFIRM >"))
            {
                ReceivedTextFromServer(expecteValue: "<CONFIRM>", IPEPReceiverLocal: mIPEPLocal);
            }
        }

        private void ReceivedTextFromServer(string expecteValue, IPEndPoint IPEPReceiverLocal)
        {
            if(IPEPReceiverLocal != null)
            {
                Console.WriteLine("No IPEndPoint specified");
                return;
            }

            SocketAsyncEventArgs saeaSendConfitrmation = new SocketAsyncEventArgs();
            saeaSendConfitrmation.SetBuffer(new byte[1024], 0, 1024);
            saeaSendConfitrmation.RemoteEndPoint = IPEPReceiverLocal;

            saeaSendConfitrmation.UserToken = expecteValue;

            saeaSendConfitrmation.Completed += ReceiveConfirmationCompleted;

            mSocketBroadcastSender.ReceiveFromAsync(saeaSendConfitrmation);
        }

        private void ReceiveConfirmationCompleted(object sender, SocketAsyncEventArgs e)
        {
            if(e.BytesTransferred == 0)
            {
                Debug.WriteLine($"Zero bytes transferred, socket error: {e.SocketError}");
            }
            
            var receivedText = Encoding.ASCII.GetString(e.Buffer, 0, e.BytesTransferred);
            
            if(receivedText.Equals(Convert.ToString(e.UserToken)))
            {
                Console.WriteLine($"Received confirmation from server {e.RemoteEndPoint}");
                mChatServerEP = e.RemoteEndPoint;
                ReceivedTextFromServer(string.Empty, mChatServerEP as IPEndPoint);
            }
            else
            {
                Console.WriteLine("Excpected text not received.");
            }
        }
    }
}
