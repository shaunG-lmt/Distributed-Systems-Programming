using System;
using System.Net.Sockets;

namespace client
{
    class Program
    {
        static void Main(string[] args)
        {
            string hostname; int port;

            Console.Write("Please enter the destination host or IP address: "); hostname = Console.ReadLine();

            Console.Write("Please enter the port: "); port = int.Parse(Console.ReadLine());

            Console.Write("Please enter the message: ");

            String Message = Console.ReadLine(); int EncryptionKey = 1; byte[] rawData = Serialize(Message, EncryptionKey);

            TcpClient tcpClient = new TcpClient(); tcpClient.Connect(hostname, port);

            NetworkStream nStream = tcpClient.GetStream();

            nStream.Write(rawData, 0, rawData.Length);

            Console.Write("Press <ENTER> to quit the client..."); Console.ReadLine();
        }
        public static byte[] Serialize(string Message, int EncryptionKey)
        {
            int MessageLength;
            int index = 0;
            byte[] ascii = System.Text.Encoding.ASCII.GetBytes(Message);
            MessageLength = ascii.Length + 9;
            byte[] rawData = new byte[MessageLength];
            rawData[index++] = 0;
            rawData[index++] = 0;
            rawData[index++] = 0;
            rawData[index++] = 1;
            int remainingLength = ascii.Length + 3;
            rawData[index++] = (byte)((remainingLength & 0xff00) >> 8);
            rawData[index++] = (byte)(remainingLength & 0xff);
            rawData[index++] = (byte)EncryptionKey; int bodyLength = ascii.Length;
            rawData[index++] = (byte)((bodyLength & 0xff00) >> 8);
            rawData[index++] = (byte)(bodyLength & 0xff);
            Array.Copy(ascii, 0, rawData, index, ascii.Length);

            return rawData;
        }
    }
}
