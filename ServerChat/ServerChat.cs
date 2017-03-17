using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;

// SERVER
namespace Server
{
    public partial class ServerChat : Form
    {
        public ServerChat()
        {
            InitializeComponent();
        }

       // private List<SocketModel> socketlist1;
        private SocketModel[] socketlist1;
        private SocketModel[] socketlist2;
        private TCPModel tcp;
        private int numOfClient = 200;
        private int currentClient = 0;
        List<int> Adam = new List<int>(); //lưu các client kết nối vào phòng Adam
        List<int> Eva = new List<int>();//lưu các client kết nối vào phòng Eva
        List<int> Date = new List<int>();//lưu các client kết nối vào phòng Date
        List<int> Teen = new List<int>();//lưu các client kết nối vào phòng Teen
        int slAdam = 0, slEva = 0, slDate = 0, slTeen = 0; //số lượng user trong phòng
        string oldmessAdam = "", oldmessEva = "", oldmessDate = "", oldmessTeen = "";

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            StartServer();
            Thread th = new Thread(ServeClient);
            th.Start();
        }

        public void StartServer()
        {
            string IP = txtIP.Text;
            int Port = int.Parse(txtPort.Text);
            tcp = new TCPModel(IP, Port);
            tcp.Listen();
            btnStart.Enabled = false;
            Console.WriteLine("OK!");
        }

        public void ServeClient(){
            socketlist1 = new SocketModel[numOfClient];
            socketlist2 = new SocketModel[numOfClient];
            for (int i = 0; i < numOfClient; i++) 
            {
                ServeAClient();
               
            }
        }

        public void AcceptUser() //socket de gui va nhan tin cua user hien tai
        {
            int status = -1;
            Socket s = tcp.SetUpANewConnection( ref status);
            //socketlist1.Add(
            socketlist1[currentClient] = new SocketModel(s);
            

            string st = socketlist1[currentClient].GetRemoteEndpoint();
            string st1 = "New connect from: " + st;

            Console.WriteLine(st1);
            lbManageConnect.Items.Add(st1);
            

        }

        public void AccceptOther() //socket de gui tin den nhung nguoi dung khac 
        {
            int status = -1;
            Socket s = tcp.SetUpANewConnection(ref status);
            socketlist2[currentClient] = new SocketModel(s);    
           
        }

        public void ServeAClient()
        {
            AcceptUser();
            AccceptOther();

            currentClient++;

            Thread th = new Thread(Communicated);
            th.Start(currentClient - 1);
            
        }

        public void Communicated(object o)
        {
            int pos = (Int32)o;

            string request = socketlist1[pos].ReceiveData();  //kết nối vào phòng
            Console.WriteLine("Connect to " + request + " room");
            
            while (request != "close")
            {                
                close(ref request, pos);
                              
                if (request == "adam")
                {
                    sendMess(ref Adam, pos, ref oldmessAdam, ref slAdam, ref request);
                }

                if (request == "eva")
                {
                    sendMess(ref Eva, pos, ref oldmessEva, ref slEva, ref request);
                }

                if (request == "date")
                {
                    sendMess(ref Date, pos, ref oldmessDate, ref slDate, ref request);
                }

                if (request == "teen")
                {
                    sendMess(ref Teen, pos, ref oldmessTeen, ref slTeen, ref request);
                }
            
                
            } 
        }

        public void sendMess (ref List<int> UserList, int pos, ref string oldmess, ref int sl, ref string request )
        {
            int inew = -1;
            socketlist1[pos].SendData("connected");
            if (checkUser(UserList, pos) == 1)
            {
                UserList.Add(pos);
                sl++;
                inew = 1;
            }

            if (inew == 1 && sl != 1) // gửi các tin cũ đến user mới vào
            {
                int cur = UserList[sl - 1];
                socketlist2[cur].SendData(oldmess);
            }

            string mess = socketlist1[pos].ReceiveData();
            oldmess += mess + "*";
            if (close(ref mess, pos) == 1)
            {
                request = mess;
                UserList.Remove(pos);
            }
            else
                Broastcast(UserList, pos, mess);
        }

        public void Broastcast(List<int>Other, int pos, string mess)
        {
            socketlist1[pos].SendData(mess); //gửi lại cho chính user
            
            foreach (int indexOther in Other) //gửi cho các usr khác trong room
                if ( socketlist2[pos].GetRemoteEndpoint() != socketlist2[indexOther].GetRemoteEndpoint())
                     socketlist2[indexOther].SendData(mess);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            this.Text = "SERVER CHAT";
            Console.WriteLine("SERVER CHAT");
            
        }

        private int checkUser(List<int>list, int pos) //check user nào đã vào phòng rồi thì ko add vào ds các user đang sd phòng chat nữa
        {
            foreach (int user in list)
                if (user == pos)
                    return 0;
            return 1;
        }

        private int close(ref string request,int pos)
        {
            if (request.StartsWith("Socket is closed"))
            {
                socketlist1[pos].CloseSocket();
                socketlist2[pos].CloseSocket();
                request = "close";
                return 1;            
            }
            return 0;
        }

        private void FormServerChat_Closing(object sender, FormClosingEventArgs e)
        {
            tcp.Shutdown();
        }
    }
}
