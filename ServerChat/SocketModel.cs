/*
 * Created by SharpDevelop.
 * User: chepchip
 * Date: 11/11/2016
 * Time: 12:09 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server 
{
	/// <summary>
	/// Description of SocketModel.
	/// </summary>
	public class SocketModel
	{
		private Socket socket;
		private byte[] array_to_receive_data;
		private string remoteEndPoint;		
		
		public SocketModel(Socket s)
		{
			socket = s;
			array_to_receive_data = new byte[100];
		}
		public SocketModel(Socket s,int length)
		{
			socket = s;
			array_to_receive_data = new byte[length];
		}		
		//get the IP and port of connected client
		public string GetRemoteEndpoint(){
			string str = "";
			try{
				str = Convert.ToString(socket.RemoteEndPoint);
				remoteEndPoint = str;			
			}
			catch (Exception e){
				string str1 = "Error..... " + e.StackTrace;
		        Console.WriteLine(str1);
		        str = "Socket is closed with " + remoteEndPoint;
			}
			return str;			
		}
		//receive data from client
		public string ReceiveData(){
			//server just can receive data AFTER a connection is set up between server and client
			string str = "";
			try{
				//count the length of data received (maximum is 100 bytes)
				int k = socket.Receive(array_to_receive_data);
		        Console.WriteLine("From client:");
		        //convert the byte recevied into string
		        char[] c = new char[k];
		        for (int i=0;i<k;i++){
		            Console.Write(Convert.ToChar(array_to_receive_data[i]));			
		            c[i] = Convert.ToChar(array_to_receive_data[i]);
		        }
		        str = new string(c);
			}
			catch (Exception e){
				string str1 = "Error..... " + e.StackTrace;
		        Console.WriteLine(str1);
		        str = "Socket is closed with " + remoteEndPoint;
			}
			return str;
		}
		//send data to client
		public void SendData(string str){
			//QUESTION: why use try/catch here?
			try{
		        ASCIIEncoding asen=new ASCIIEncoding();
		        socket.Send(asen.GetBytes(str));
			}
			catch (Exception e){
		        Console.WriteLine("Error..... " + e.StackTrace);
			}		
		}
		//close sockket
		public void CloseSocket(){
			socket.Close();
		}		

	}
}
