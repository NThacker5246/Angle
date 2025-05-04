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
	public InputField yourRoom;
	public InputField roomPl;
    public Text result;

    public string yourIP;
    public GameObject yourPlayer;
    public Players pl;
    public CMPOS cm;

    public Server server;
    public Client client;

    public InputField legacyIP;

    void Start(){
        pl.StartWarp();

        string hostName = Dns.GetHostName();
          
        // Get the IP from GetHostByName method of dns class. 
        //yourIP = Dns.GetHostByName(hostName).AddressList[0].ToString();   

        yourIP = legacyIP.text;
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
            //string ipC = GetIp(roomPl.text);
            //int port = GetPort(roomPl.text);

            IPAddress ipAddress = IPAddress.Parse(legacyIP.text);
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 8080);

	        // Sender
	        //string messageToSend = "Hello, Russia!";
            client.yourIP = yourIP;
            client.yourPlayer = yourPlayer;
	        client.client(localEndPoint);
	        server.server(localEndPoint);


        }
        catch (Exception e)
        {
            result.text += e.ToString();
        }
    }

    public string GenerateRoom(string ip, int port){
        //http://[fe80::5a7f:a086:5432:13cf]:8888 - example
        string first = "9876543210qwertyuiopasdfghjklzxcvbnm:[]"; // first alphabet
        string secon = "0123456789mnbvcxzlkjhgfdsapoiuytrewqбвг"; //second

        string cp = "[" + ip + "]" + port;
        char[] toEnc = cp.ToCharArray();
        char[] ft = first.ToCharArray();
        char[] st = secon.ToCharArray();

        string result = "";
        foreach(char letter in toEnc){
            for(int i = 0; i < ft.Length; i++){
                
                if(letter == ft[i]){
                    result += st[i];
                }
            }
        }
        Debug.Log(result);
        return result;
    }

    public string GetIp(string room){
        string first = "9876543210qwertyuiopasdfghjklzxcvbnm:[]"; // first alphabet
        string secon = "0123456789mnbvcxzlkjhgfdsapoiuytrewqбвг"; //second

        char[] toDec = room.ToCharArray();
        char[] ft = first.ToCharArray();
        char[] st = secon.ToCharArray();

        string ipPort = "";

        foreach(char letter in toDec){
            for(int i = 0; i < st.Length; i++){
                
                if(letter == st[i]){
                    ipPort += ft[i];
                }
                
            }
        }

        ipPort = ipPort.Replace("[", "");
        string[] ipp = ipPort.Split("]");
        string ip = ipp[0];
        return ip;
    }

    public int GetPort(string room){
        string first = "9876543210qwertyuiopasdfghjklzxcvbnm:[]"; // first alphabet
        string secon = "0123456789mnbvcxzlkjhgfdsapoiuytrewqбвг"; //second

        char[] toDec = room.ToCharArray();
        char[] ft = first.ToCharArray();
        char[] st = secon.ToCharArray();

        string ipPort = "";

        foreach(char letter in toDec){
            for(int i = 0; i < st.Length; i++){
                
                if(letter == st[i]){
                    ipPort += ft[i];
                }
            
            }
        }

        ipPort = ipPort.Replace("[", "");
        string[] ipp = ipPort.Split("]");
        string port = ipp[1];
        return int.Parse(port);
    }

    public void YourRoom(){
        yourRoom.text = GenerateRoom(yourIP, 8080);
    }
}
