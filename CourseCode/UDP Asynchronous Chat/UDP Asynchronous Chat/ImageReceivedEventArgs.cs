using System;

namespace UDP_Asynchronous_Chat
{
    public class ImageReceivedEventArgs: EventArgs
    {
        public readonly string fileName;
        public readonly byte[] fileData;
        public readonly string message;

        public ImageReceivedEventArgs(string fileName, byte[] fileData, string message)
        {
            this.fileName = fileName;
            this.fileData = fileData;
            this.message = message;
        }
    }
}