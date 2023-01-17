﻿using System.Text.Json;
using ConsoleAppSimpleSocketServer;
using ConsoleAppSimpleSocketServer.NetEngine;
using ConsoleAppSimpleSocketServer.NetModel;

ServerEngine serverEngine = new ServerEngine("127.0.0.1", 34536);
serverEngine.StartServer();
serverEngine.AcceptClient();

string messageFromClient = serverEngine.ReceiveMessage();

Requset requset = JsonSerializer.Deserialize<Requset>(messageFromClient);
Response response;

if (requset.Command == Commands.AddAge)
{
    Man man = JsonSerializer.Deserialize<Man>(requset.JsonData);
    man.Age += 5;

    response = new Response()
    {
        Status = Statuses.Ok,
        JsonData = JsonSerializer.Serialize(man)
    };
}
else
{
    response = new Response()
    {
        Status = Statuses.UnknownCommand
    };
}

string messageToClient = JsonSerializer.Serialize(response);

serverEngine.SendMessage(messageToClient);

serverEngine.CloseClientSocket();
serverEngine.CloseServerSocket();