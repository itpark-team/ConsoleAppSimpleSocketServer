using ConsoleAppSimpleSocketServer;

ServerEngine serverEngine = new ServerEngine("127.0.0.1", 34536);
serverEngine.StartServer();
serverEngine.AcceptClient();

string messageFromClient = serverEngine.ReceiveMessage();

string messageToClient = messageFromClient + " - сообщение успешно получено!";

serverEngine.SendMessage(messageToClient);

serverEngine.CloseClientSocket();
serverEngine.CloseServerSocket();