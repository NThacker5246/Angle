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
    	StartCoroutine("StartConnection");
    }

    IEnumerator StartConnection()
    {
        try
        {
            IPAddress ipAddress = IPAddress.Parse(ip.text);
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, int.Parse(port.text));

            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(localEndPoint);
            listener.Listen(10);

            Debug.Log("Server is listening on port " + port.text);

            StartListening(listener);

	        // Sender
	        string messageToSend = "Hello, Russia!";
	        //Address

	        SendPacket(messageToSend, ipAddressToSend.text, 8080);

        }
        catch (Exception e)
        {
            Debug.LogError(e.ToString());
        }
        yield return null;
    }

    public void CheckWarp(){
    	StartCoroutine("Check");
    }

    IEnumerator Check()
    {
        // Receiver
        string receivedMessage = ReceivePacket(8080);
        result.text = "Received message: " + receivedMessage;
        yield return null;
    }

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
    }
}
