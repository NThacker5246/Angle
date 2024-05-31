using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Globalization;

public class Client : MonoBehaviour
{	
    public Players pl;
    public Text result;
    public string yourIP;
    public GameObject yourPlayer;

	public async void client(IPEndPoint ipEndPoint){
	    Socket client = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

		await client.ConnectAsync(ipEndPoint);
		while (true)
		{
            string key = ""; 
            if(Input.GetKeyDown(KeyCode.E)){
                key = "e";
            } else {
                key = "none";
            }
		    // Send message.
            Vector3 position = yourPlayer.transform.position;
		    var message = $"ip={yourIP}&x={position.x}&y={position.y}&z={position.z}&key={key};";
		    var messageBytes = Encoding.UTF8.GetBytes(message);
		    _ = await client.SendAsync(messageBytes, SocketFlags.None);
		    result.text += $"Socket client sent message: \"{message}\"\n";

		    // Receive ack.
		    var buffer = new byte[1_024];
		    var received = await client.ReceiveAsync(buffer, SocketFlags.None);
		    var response = Encoding.UTF8.GetString(buffer, 0, received);
		    if (response == "<|ACK|>")
		    {
		        result.text += $"Socket client received acknowledgment: \"{response}\"";
		        break;
		    }


		    var ack = ";";
		    if (response.IndexOf(ack) > -1 /* is end of message */)
		    {
		        result.text += $"Socket client received message: \"{response.Replace(ack, "")}\"\n";
		    }
		    // Sample output:
		    //     Socket client sent message: "Hi friends 👋!<|EOM|>"
		    //     Socket client received acknowledgment: "<|ACK|>"
		}

		client.Shutdown(SocketShutdown.Both);
	}
}