
// See https://aka.ms/new-console-template for more information
using System.Data.SqlClient;
using StackExchange.Redis;
using RabbitMQ.Client;

Console.WriteLine("Connection tests started...");

// connect to sql server localhost with sql auth
var connectionString = $"Server=localhost;Database=master;User Id=sa;Password={Environment.GetEnvironmentVariable("SQL_CONTAINER_SA_PASSWORD")};";
using (var connection = new SqlConnection(connectionString))
{
    connection.Open();
    Console.WriteLine("Connection  to SQL SERVER state: {0}", connection.State);
    Console.WriteLine("Connection successful.");
}

// connect to redis localhost with no auth
var redis = ConnectionMultiplexer.Connect("localhost");
Console.WriteLine("Connection  to REDIS state: {0}", redis.IsConnected);
redis.Dispose();

// connect to rabbitmq localhost with no auth
var factory = new ConnectionFactory() { HostName = "localhost" };
using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    Console.WriteLine("Connection  to RABBITMQ state: {0}", connection.IsOpen);
    Console.WriteLine("Connection successful.");
}