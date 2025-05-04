using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackerServer: MonoBehaviour
{
    IEnumerator Listen()
    {
        const int port = 8080;
        TcpListener server = new TcpListener(IPAddress.Any, port);
        
        server.Start();
        Console.WriteLine("Сервер запущен. Ожидание соединения...");

        while (true)
        {
            using (TcpClient client = server.AcceptTcpClient())
            {
                Console.WriteLine("Клиент подключен.");

                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                
                string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Получено сообщение: " + message);
            }

            yield return new WaitForSeconds(0.01f);
        }
    }

    void Awake(){
    	StartCoroutine("Listen");
    }
}
