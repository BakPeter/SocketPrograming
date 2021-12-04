using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDP_Asynchronous_Chat
{
    public enum PACKET_TYPE { DISCOVERY = 0, CONFIRMATION = 1, TEXT = 2, IMAGE = 3 }
    public class ChatPacket
    { 
        public string Message { get; set; }
        public PACKET_TYPE PacketType { get; set; }
        public byte[] RawData { get; set; }
        public string FileInfo { get; internal set; }
    }
}
