using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class PortOpener : MonoBehaviour
{
	public InputField port;
	public InputField ip;
    public Text result;
    public string temp;
    public InputField ipAddressToSend;
    

    private void StartListening(Socket listener)
    {
        listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);
    }

    private void AcceptCallback(IAsyncResult ar)
    {
        Socket listener = (Socket)ar.AsyncState;
        Socket handler = listener.EndAccept(ar);

        // Handle incoming connection here
    }

    public void ListenNow(){
    	StartConnection();
    } 

    void StartConnection()
    {
        try
        {
            IPAddress ipAddress = IPAddress.Parse(ip.text);
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, int.Parse(port.text));

	        // Sender
	        string messageToSend = "Hello, Russia!";
	        client(localEndPoint);
	        server(localEndPoint);


        }
        catch (Exception e)
        {
            result.text += e.ToString();
        }
    }
    /*
    async void SentAndList(IPEndPoint ipEndPoint){
        Socket client = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        client.Bind(ipEndPoint);
        client.Listen(100);

        await client.ConnectAsync(ipEndPoint);
        while (true)
        {
            // Send message.
            var message = "login";
            var messageBytes = Encoding.UTF8.GetBytes(message);
            _ = await client.SendAsync(messageBytes, SocketFlags.None);
            result.text += $"Socket client sent message: \"{message}\"";

            // Receive ack.
            var handler = await client.AcceptAsync();
            var buffer = new byte[1_024];
            var received = await handler.ReceiveAsync(buffer, SocketFlags.None);
            var response = Encoding.UTF8.GetString(buffer, 0, received);

            result.text += $"Socket client received acknowledgment: \"{response}\"";
            break;

            // Sample output:
            //Socket client sent message: "Hi friends 👋!<|EOM|>"
            //Socket client received acknowledgment: "<|ACK|>"
        }

        client.Shutdown(SocketShutdown.Both);
    }
    */
    async void client(IPEndPoint ipEndPoint){
	    Socket client = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

		await client.ConnectAsync(ipEndPoint);
		while (true)
		{
		    // Send message.
		    var message = "key=value;";
		    var messageBytes = Encoding.UTF8.GetBytes(message);
		    _ = await client.SendAsync(messageBytes, SocketFlags.None);
		    result.text += $"Socket client sent message: \"{message}\"";

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
		        result.text += $"Socket client received message: \"{response.Replace(ack, "")}\"";
		        break;
		    }
		    // Sample output:
		    //     Socket client sent message: "Hi friends 👋!<|EOM|>"
		    //     Socket client received acknowledgment: "<|ACK|>"
		}

		client.Shutdown(SocketShutdown.Both);
	}

	async void server(IPEndPoint ipEndPoint){
		Socket listener = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

		listener.Bind(ipEndPoint);
		listener.Listen(100);

		var handler = await listener.AcceptAsync();
		while (true)
		{
		    // Receive message.
		    var buffer = new byte[1_024];
		    var received = await handler.ReceiveAsync(buffer, SocketFlags.None);
		    var response = Encoding.UTF8.GetString(buffer, 0, received);
		    
		    var eom = ";";
		    if (response.IndexOf(eom) > -1 /* is end of message */)
		    {
		        result.text += $"Socket server received message: \"{response.Replace(eom, "")}\"";

		        var ackMessage = "dataCollected;";
		        var echoBytes = Encoding.UTF8.GetBytes(ackMessage);
		        await handler.SendAsync(echoBytes, 0);
		        result.text += $"Socket server sent acknowledgment: \"{ackMessage}\"";

		        break;
		    }
		    // Sample output:
		    //    Socket server received message: "Hi friends 👋!"
		    //    Socket server sent acknowledgment: "<|ACK|>"
		}
	}
    /*

        public void SendPacket(string message, string ipAddress, int port)
        {
            UdpClient client = new UdpClient();
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);
            byte[] data = Encoding.ASCII.GetBytes(message);
            client.Send(data, data.Length, endPoint);
            Console.WriteLine("Sent message: " + message);
        }

        public string ReceivePacket(int port)
        {
            UdpClient listener = new UdpClient(port);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
            byte[] receivedData = listener.Receive(ref endPoint);
            string receivedMessage = Encoding.ASCII.GetString(receivedData);
            listener.Close();
            return receivedMessage;
        }*/
}
