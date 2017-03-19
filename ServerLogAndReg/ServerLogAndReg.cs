using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    public partial class ServerLogAndReg : Form
    {
        DataTable data;
        public ServerLogAndReg()
        {
            InitializeComponent();
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            this.Text = "SERVER LOG AND REG";
            Console.WriteLine("SERVER LOG AND REG");
            currentAcount = new List<string>();
        }

        private SocketModel[] socketlistLog;
        private TCPModel tcp;
        private int numOfClient = 400;
        private int currentClient = 0;
        private List<string> currentAcount; // lưu những tài khoản nào đang đăng nhập  

        public void StartServer()
        {
            string IP = txtIP.Text;
            int Port = int.Parse(txtPort.Text);
            tcp = new TCPModel(IP, Port);
            tcp.Listen();
            btnStart.Enabled = false;
            Console.WriteLine("OK!");
        }

        public void ServeClient()
        {
            socketlistLog = new SocketModel[numOfClient];
            for (int i = 0; i < numOfClient; i++)
            {
                ServeAClient();
            }
        }

        public void AcceptLog()
        {
            int status = -1;
            Socket s = tcp.SetUpANewConnection(ref status);
            socketlistLog[currentClient] = new SocketModel(s);

            string st = socketlistLog[currentClient].GetRemoteEndpoint();
            string st1 = "\nNew connection from: " + st;

            Console.WriteLine(st1);
            lbManageConnect.Items.Add(st1);

        }

        public void ServeAClient()
        {
            AcceptLog();
            currentClient++;
            Thread th = new Thread(Communicated);
            th.Start(currentClient - 1);
        }

        public void Communicated(object o)
        {
            string user = "";
            int pos = (Int32)o;
            string request = "";
            while (request != "close")
            {
                request = socketlistLog[pos].ReceiveData();
                if (request == "log") //yêu cầu đăng nhập
                {
                    string str = socketlistLog[pos].ReceiveData();
                    string[] usr = str.Split(' ');
                    string username = usr[0];
                    string pass = usr[1];
                    checkAccount(pos, username, pass);
                }
                if (request == "reg") //yêu cầu đăng ký
                {
                    string str = socketlistLog[pos].ReceiveData();
                    Console.WriteLine(str);
                    int check = Register(str);
                    if (check == 1)
                    {
                        //đăng kí thành công
                        socketlistLog[pos].SendData("1");
                    }
                    else
                    {
                        //đăng lí không thành công, user đã tồn tại
                        socketlistLog[pos].SendData("0");
                    }
                }

                if (request == "info") //yêu cầu lấy thông tin người dùng
                {
                    socketlistLog[pos].SendData("OK! Info");
                    user = socketlistLog[pos].ReceiveData();

                    data = DBSQLServerUtils.sqlQuery("SELECT age FROM UserAccount where user = "+user + ";");
                    string Info = data.ToString();
                    data = DBSQLServerUtils.sqlQuery("SELECT relationship FROM UserAccount where user = " + user + ";");
                    Info += " " + data.ToString();
                    socketlistLog[pos].SendData(Info);
                }
                if (close(ref request, pos) == 1)
                    currentAcount.Remove(user);

            }

        }

        private int close(ref string request, int pos)
        {
            if (request.StartsWith("Socket is closed"))
            {
                socketlistLog[pos].CloseSocket();
                request = "close";
                return 1;
            }
            return 0;
        }

        private void checkAccount(int pos, string username, string pass)
        {
            data = DBSQLServerUtils.sqlQuery("Count * FROM UserAccount where user = " + username +" pass = "+pass+";");
            if ( Convert.ToInt32(data) == 1) // 1: có tài khoản, 0: tài khoản không tồn tại
            {
                if (checkAccOnline(username) == 1) // tài khoản đã được đc sử dụng
                {
                    socketlistLog[pos].SendData("2");
                }
                else
                {
                    socketlistLog[pos].SendData("1");
                    currentAcount.Add(username);
                }
            }
            else
            {
                socketlistLog[pos].SendData("0");
            }
        }

        private int checkAccOnline(string username)
        {
            foreach (string Acc in currentAcount)
            {
                if (username == Acc)
                    return 1;
            }
            return 0;
        }

        private int Register(string str)
        {
            int i;
            try
            {
                string[] usr = str.Split(' ');
                string username = usr[0];
                string pass = usr[1];
                string sex = usr[2];
                int age = int.Parse(usr[3]);
                string rela = usr[4];
                string insertString = username + "," + pass + "," + sex + "," + age + "," + rela;
                data = DBSQLServerUtils.sqlQuery("INSERT INTO UserAccount VALUES ( " +insertString+ " );");
                i = Convert.ToInt32(data);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error...." + e.StackTrace);
                return -1;
            }
            return i;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            StartServer();
            Thread th = new Thread(ServeClient);
            th.Start();
        }


        private void Form1Closing(object sender, FormClosingEventArgs e)
        {
            tcp.Shutdown();
        }
    }
}
