﻿using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;

public class Client
{
    static void Main()
    {
        TcpClient client = new TcpClient("127.0.0.1", 8888);
        NetworkStream stream = client.GetStream();

        using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
        using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
        {
            DataObject objToSend = new DataObject { Name = "Test", Value = 1 };

            while (true)
            {
                string json = JsonSerializer.Serialize(objToSend);
                writer.WriteLine(json);
                writer.Flush();
                Console.WriteLine($"Sent: {objToSend}");

                string response = reader.ReadLine();
                DataObject objReceived = JsonSerializer.Deserialize<DataObject>(response);
                Console.WriteLine($"Received: {objReceived}");

                objToSend = objReceived;

                Thread.Sleep(2000); // Wait for 2 seconds before sending the next object
            }
        }
    }
}
