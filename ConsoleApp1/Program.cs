using System.Collections.Concurrent;
using System;
using Grpc.Net.Client;
using ConsoleApp1;
using Stream;
using Grpc.Core;


//Console.WriteLine("Press any key to start processing");
//Console.ReadKey();
//Console.WriteLine("Processing");
//Task.Factory.StartNew(ProcessIntData);

//var chanel = GrpcChannel.ForAddress("https://localhost:7257");
//var client = new Inviter.InviterClient(chanel);

//var response = await client.InviteAsync(new Request { Name = "Rustem" });

//var eventInvokation = response.Invitation;
//var eventDateTime = response.Start.ToDateTime();
//var eventDuration = response.Duration.ToTimeSpan();

//Console.WriteLine(eventInvokation);
//Console.WriteLine($"Начало: {eventDateTime.ToString("dd:MM:HH:mm")}  Длительность:{eventDuration.TotalHours} часа");

string[] messages = { "Привет", "Как дела?", "Че молчишь?", "Ты че, спишь", "Ну пока" };

using var channel = GrpcChannel.ForAddress("https://localhost:7257");
var client = new Messenger.MessengerClient(channel);

var call = client.DataStream();

var readTask = Task.Run(async () =>
{
    await foreach (var response in call.ResponseStream.ReadAllAsync())
    {
        Console.WriteLine($"Server: {response.Content}");
    }
});

foreach (var message in messages)
{
    await call.RequestStream.WriteAsync(new Stream.Request { Content = message });
    Console.WriteLine(message);
    await Task.Delay(2000);

}
await call.RequestStream.CompleteAsync();
await readTask;


Console.ReadLine();



