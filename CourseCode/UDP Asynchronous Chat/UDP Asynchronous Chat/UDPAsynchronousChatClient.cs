using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UDP_Asynchronous_Chat
{
    public class UDPAsynchronousChatClient : UDPChatGeneral
    {
        Socket mSockBroadCastSender;
        IPEndPoint mIPEPBroadcast;
        IPEndPoint mIPEPLocal;
        private EndPoint mChatServerEP;

        public UDPAsynchronousChatClient(int _localPort, int _remotePort)
        {
            mIPEPBroadcast = new IPEndPoint(IPAddress.Broadcast, _remotePort);
            mIPEPLocal = new IPEndPoint(IPAddress.Any, _localPort);

            mSockBroadCastSender = new Socket(
                AddressFamily.InterNetwork, 
                SocketType.Dgram, 
                ProtocolType.Udp
                );

            mSockBroadCastSender.EnableBroadcast = true; 

        }

        public void SendBroadcast(string strDataForBroadcast)
        {
            if (string.IsNullOrEmpty(strDataForBroadcast))
            {
                return;
            }
            try
            {
                if(!mSockBroadCastSender.IsBound)
                {
                    mSockBroadCastSender.Bind(mIPEPLocal);
                }

                ChatPacket objChatPacket = new ChatPacket();
                objChatPacket.Message = strDataForBroadcast;
                objChatPacket.PacketType = PACKET_TYPE.DISCOVERY;

                string strJSONDiscovery = JsonConvert.SerializeObject(objChatPacket);

                var dataBytes = Encoding.ASCII.GetBytes(strJSONDiscovery); 

                SocketAsyncEventArgs saea = new SocketAsyncEventArgs();
                saea.SetBuffer(dataBytes, 0, dataBytes.Length);
                saea.RemoteEndPoint = mIPEPBroadcast;

                saea.UserToken = objChatPacket;

                saea.Completed += SendCompletedCallback;

                mSockBroadCastSender.SendToAsync(saea); 

            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.ToString());
                throw;
            }
        }

        private void SendCompletedCallback(object sender, SocketAsyncEventArgs e)
        {
            Console.WriteLine($"Data sent succesfully to: {e.RemoteEndPoint}");

            if ((e.UserToken as ChatPacket).PacketType == PACKET_TYPE.DISCOVERY)
            {
                ReceiveTextFromServer(expectedValue: "<CONFIRM>", IPEPReceiverLocal: mIPEPLocal);
            }
        }

        public void SendImage(string fileName, byte[] fileBytes, string Message)
        {
            ChatPacket packImage = new ChatPacket();
            packImage.PacketType = PACKET_TYPE.IMAGE;
            packImage.RawData = fileBytes;
            packImage.Message = Message;
            packImage.FileInfo = fileName;

            var bytesToSend = 
                Encoding.ASCII.GetBytes(
                    JsonConvert.SerializeObject(packImage));

            SocketAsyncEventArgs saea = new SocketAsyncEventArgs();
            saea.SetBuffer(bytesToSend, 0, bytesToSend.Length);

            saea.RemoteEndPoint = mChatServerEP;

            saea.UserToken = fileName;

            saea.Completed += SendMessageToKnownServerCompletedCallback;

            var retVal = mSockBroadCastSender.SendToAsync(saea);

            OnRaisePrintStringEvent(
                new PrintStringEventArgs(
                    $"Image transfer status, returned: {retVal} - Socket Error: {saea.SocketError}"));

        }

        private void ReceiveTextFromServer(string expectedValue, IPEndPoint IPEPReceiverLocal)
        {
            if(IPEPReceiverLocal == null)
            {
                Console.WriteLine("No IPEndpoint specified");
                return; 
            }

            SocketAsyncEventArgs saeaSendCOnfirmation = new SocketAsyncEventArgs();
            saeaSendCOnfirmation.SetBuffer(new byte[64000], 0, 64000);
            saeaSendCOnfirmation.RemoteEndPoint = IPEPReceiverLocal;

            saeaSendCOnfirmation.UserToken = expectedValue;

            saeaSendCOnfirmation.Completed += ReceivedContent;

            mSockBroadCastSender.ReceiveFromAsync(saeaSendCOnfirmation);

        }

        private void ReceivedContent(object sender, SocketAsyncEventArgs e)
        {
            if (e.BytesTransferred == 0)
            {
                Debug.WriteLine($"Zero bytes transferred, socket error: {e.SocketError}");
                return;
            }

            var receivedText = Encoding.ASCII.GetString(e.Buffer, 0, e.BytesTransferred);
            var expectedText = Convert.ToString(e.UserToken);

            ChatPacket objPacketConfirmation = JsonConvert.DeserializeObject<ChatPacket>(receivedText);



            if (objPacketConfirmation.PacketType == PACKET_TYPE.CONFIRMATION 
                && 
                objPacketConfirmation.Message.Equals(expectedText))
            {
                Console.WriteLine($"Received confirmation from server. {e.RemoteEndPoint}");
                OnRaisePrintStringEvent(new PrintStringEventArgs($"Received confirmation from server. {e.RemoteEndPoint}"));

                mChatServerEP = e.RemoteEndPoint;
                ReceiveTextFromServer(string.Empty, mChatServerEP as IPEndPoint);
            }
            else if (objPacketConfirmation.PacketType == PACKET_TYPE.TEXT && 
                !string.IsNullOrEmpty(objPacketConfirmation.Message))
            {
                Console.WriteLine($"Text received: {objPacketConfirmation.Message}");
                OnRaisePrintStringEvent(new PrintStringEventArgs(
                    $"Text received: {objPacketConfirmation.Message}"));

                ReceiveTextFromServer(string.Empty, mChatServerEP as IPEndPoint);
            }
            else if(objPacketConfirmation.PacketType == PACKET_TYPE.IMAGE)
            {

                OnRaiseImageReceived(new ImageReceivedEventArgs(
                    fileName: objPacketConfirmation.FileInfo, 
                    fileData: objPacketConfirmation.RawData,
                    message: objPacketConfirmation.Message
                    ));

                // Let's receive more data :) 
                ReceiveTextFromServer(string.Empty, mChatServerEP as IPEndPoint);
            }
            else if (
                objPacketConfirmation.PacketType == PACKET_TYPE.CONFIRMATION && 
                !string.IsNullOrEmpty(expectedText) && 
                !objPacketConfirmation.Message.Equals(expectedText))
            {
                Console.WriteLine($"Expected token not returned by the server.");
                OnRaisePrintStringEvent(new PrintStringEventArgs($"Expected token not returned by the server."));

            }

        }

        public EventHandler<ImageReceivedEventArgs> ImageReceived;
        private void OnRaiseImageReceived(ImageReceivedEventArgs imageReceivedEventArgs)
        {
            EventHandler<ImageReceivedEventArgs> handler = ImageReceived;

            if(handler != null)
            {
                ImageReceived(this, imageReceivedEventArgs);
            }
            
        }

        public void SendMessageToKnownServer(string message)
        {
            try
            {
                if(string.IsNullOrEmpty(message))
                {
                    return;
                }

                ChatPacket objMessagePacket = new ChatPacket();
                objMessagePacket.Message = message;
                objMessagePacket.PacketType = PACKET_TYPE.TEXT;


                var bytesToSend = Encoding.ASCII.GetBytes(
                    JsonConvert.SerializeObject(objMessagePacket)
                    );

                SocketAsyncEventArgs saea = new SocketAsyncEventArgs();
                saea.SetBuffer(bytesToSend, 0, bytesToSend.Length);

                saea.RemoteEndPoint = mChatServerEP;

                saea.UserToken = message;

                saea.Completed += SendMessageToKnownServerCompletedCallback;

                mSockBroadCastSender.SendToAsync(saea);

            }
            catch (Exception excp)
            {

                Console.WriteLine(excp.ToString()); 
            }
        }

        private void SendMessageToKnownServerCompletedCallback(object sender, SocketAsyncEventArgs e)
        {
            Console.WriteLine($"Sent: {e.UserToken}{Environment.NewLine}Server: {e.RemoteEndPoint} - Socket Error: {e.SocketError} - Transferred: {e.BytesTransferred}");
            OnRaisePrintStringEvent(new PrintStringEventArgs(
                $"Sent: {e.UserToken}{Environment.NewLine}Server: {e.RemoteEndPoint} - Socket Error: {e.SocketError} - Transferred: {e.BytesTransferred}"
                ));
        }
    }
}
