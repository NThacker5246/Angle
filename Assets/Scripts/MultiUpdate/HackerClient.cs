using System;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HackerClient: MonoBehaviour
{
	[SerializeField] private InputField ip;
	[SerializeField] private InputField text;
    public void SendMessage()
    {
        string serverIp = ip.text; // Замените на IP адрес сервера
        const int port = 8080;

        using (TcpClient client = new TcpClient(serverIp, port))
        {
            NetworkStream stream = client.GetStream();
            string message = text.text;
            byte[] data = Encoding.ASCII.GetBytes(message);
            
            stream.Write(data, 0, data.Length);
            Console.WriteLine("Сообщение отправлено: " + message);
        }
    }
}
