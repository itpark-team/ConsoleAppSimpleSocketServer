using System.Net;
using System.Net.Sockets;
using System.Text;

void Log(string msg)
{
    Console.WriteLine($"LOG: {DateTime.Now} --- {msg}");
}

Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 34536);

listener.Bind(ipEndPoint);
listener.Listen(1);

Log("SERVER STARTED");

Socket handler = listener.Accept();

Log($"CLIENT ACCEPT FROM {handler.RemoteEndPoint}");

StringBuilder messageBuilder = new StringBuilder();
do
{
    byte[] inputBytes = new byte[1024];
    int countBytes = handler.Receive(inputBytes);
    messageBuilder.Append(Encoding.Unicode.GetString(inputBytes, 0, countBytes));
} while (handler.Available > 0);

string messageFromClient = messageBuilder.ToString();

Log($"MESSAGE FROM CLIENT RECIEVED: {messageFromClient}");

string messageToClient = messageFromClient + " - сообщение успешно получено!";

byte[] outputBytes = Encoding.Unicode.GetBytes(messageToClient);
handler.Send(outputBytes);

Log($"MESSAGE TO CLIENT SENT: {messageToClient}");

handler.Shutdown(SocketShutdown.Both);
handler.Close();

listener.Close();

Log($"SERVER FINISHED");