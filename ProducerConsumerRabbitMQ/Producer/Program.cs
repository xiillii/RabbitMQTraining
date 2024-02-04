// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672 };

using (var connection = factory.CreateConnection()) 
using (var channel = connection.CreateModel())
{
    channel.QueueDeclare("BasicTest", false, false, false, null);

    var message = "Getting started with NET Core RabbitMQ";
    var body = Encoding.UTF8.GetBytes(message);

    channel.BasicPublish("", "BasicTest", null, body);

    Console.WriteLine("Send message {0}...", message);
}


