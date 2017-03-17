/*
 * Created by SharpDevelop.
 * User: chepchip
 * Date: 11/11/2016
 * Time: 12:07 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Net;
using System.Net.Sockets;

namespace Server
{
	/// <summary>
	/// Description of TCPModel.
	/// </summary>
	public class TCPModel
	{
		private IPAddress ipAd_of_server;
		private int port_to_listen;
		private TcpListener tcpServer;	
		
		public TCPModel(string ip, int p)
		{
			ipAd_of_server = IPAddress.Parse(ip);
			port_to_listen = p;
			tcpServer = new TcpListener(ipAd_of_server, port_to_listen);			
		}
		//function to listen
		public void Listen(){
			try{
				tcpServer.Start();
			}
			catch (Exception e){
				Console.WriteLine("Error..... " + e.StackTrace);
			}			
		}
		//accept a new connection
		public Socket SetUpANewConnection(ref int status){
			Socket socket = tcpServer.AcceptSocket();
			status = 1;//QUESTION: WHY we should use status variable here
			return socket;
		}
		//shutdown server
		public void Shutdown(){
			//students should develop script to finish the job in progress by working sockets
			tcpServer.Stop();			
		}				
	}
}
