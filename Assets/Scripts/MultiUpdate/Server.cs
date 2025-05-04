using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Globalization;


public class Server : MonoBehaviour
{
    public Players pl;
    public Text result;
	public async void server(IPEndPoint ipEndPoint){
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
}
