using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Net;
using System.Net.Sockets;

public class PortListener : MonoBehaviour
{
	public InputField port;
	public InputField ip;

	public Text result;

	public void ConnectToGame(){
		try {
			IPAddress ipAddress = IPAddress.Parse(ip.text);
			IPEndPoint localEndPoint  = new IPEndPoint(ipAddress, int.Parse(port.text));

	        Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
	        listener.Bind(localEndPoint);
	        listener.Listen(10);
	        result.text = "Debugger: Server is listening on port " + port.text;
	        //Socket handler = listener.Accept();
	    } catch (Exception e) {
	    	result.text = "Debugger: " + e.ToString();
	    }
	}

}
