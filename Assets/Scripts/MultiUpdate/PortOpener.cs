using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Globalization;


public class PortOpener : MonoBehaviour
{
	public InputField port;
	public InputField ip;
    public Text result;
    public string temp;
    public InputField ipAddressToSend;

    public string yourIP;
    public GameObject yourPlayer;
    public Players pl;
    public CMPOS cm;

    void Start(){
        pl.StartWarp();
        string hostName = Dns.GetHostName();  
        yourIP = "127.0.0.1";
        pl.ht.Insert(yourIP);
        yourPlayer = pl.ht.Find(yourIP);
        cm.player = yourPlayer.GetComponent<PlayerContr>();
    }
    

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
		        result.text += $"Socket server received message: \"{response.Replace(eom, "")}\"\n";
                /*
                foreach(string package in response.Split(";")){
                    

                }
                */
                string package = response.Replace(";", "");
                string[] keyValues = package.Split("&");
                string key = "";
                Vector3 position = new Vector3(0, 0, 0);
                string ip4 = "";

                foreach(string keyValue in keyValues){
                    string[] keyAndValue = keyValue.Split("=");
                    string k = keyAndValue[0];
                    string val1 = keyAndValue[1%keyAndValue.Length];
                    if(k == "ip"){
                        ip4 = val1;
                        print("IP4 Key");
                    } else if(k == "x"){
                        position = new Vector3(float.Parse(val1.Replace(",", "."), CultureInfo.InvariantCulture.NumberFormat), position.y, position.z);
                    } else if(k == "y"){
                        position = new Vector3(position.x, float.Parse(val1.Replace(",", "."), CultureInfo.InvariantCulture.NumberFormat), position.z);
                    } else if(k == "z"){
                        position = new Vector3(position.x, position.y, float.Parse(val1.Replace(",", "."), CultureInfo.InvariantCulture.NumberFormat));
                    } else if(k == "key"){
                        key = val1;
                    }
                }
                Debug.Log(position);
                Debug.Log(key);
                Debug.Log(ip4);

                if(pl.ht.IsExists(ip4)){
                    GameObject playerSet = pl.ht.Find(ip4);
                    playerSet.transform.position = position;
                } else {
                    pl.ht.Insert(ip4);
                    GameObject playerSet = pl.ht.Find(ip4);
                    playerSet.transform.position = position;
                }
                var ackMessage = "dataCollected;";
                var echoBytes = Encoding.UTF8.GetBytes(ackMessage);
                await handler.SendAsync(echoBytes, 0);
                result.text += $"Socket server sent acknowledgment: \"{ackMessage}\"\n";
                
		    }

		    // Sample output:
		    //    Socket server received message: "Hi friends 👋!"
		    //    Socket server sent acknowledgment: "<|ACK|>"
		}

        pl.ht.Symetric();
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
