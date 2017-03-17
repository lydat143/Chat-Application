using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Chat
{
    public partial class FormChat : Form
    {
        private TCPModel tcpforUser;
        private TCPModel tcpforOther;
        int port;
        string IP;
        string UserChatting;
        string Request;
        string requestclose;

        // lấy username và tên phòng từ FormRoom 
        public delegate void Send(string str);
        public Send SenderUser;
        public Send SenderRequest;
        
        //Hàm có nhiệm vụ lấy tham số truyền vào
        private void GetUsername(string Message)
        {
            UserChatting = Message;
        }
        private void GetRequest(string Message)
        {
            Request = Message;
        }

        public FormChat()
        {
            InitializeComponent();
            //Tạo con trỏ tới hàm GetMessage
            SenderUser = new Send(GetUsername);
            SenderRequest = new Send(GetRequest);
        }

        private void Connect()
        {
            try
            {
                this.Text = UserChatting;
                port = 8080;
                IP = "127.0.0.1";

                tcpforUser = new TCPModel(IP, port);
                tcpforUser.ConnectToServer();

                tcpforUser.SendData(Request);

                tcpforOther = new TCPModel(IP, port);
                tcpforOther.ConnectToServer();

                string answer = tcpforUser.ReadData();
                Console.WriteLine("\n"+answer + " to " + Request + " room");
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR......" + e.StackTrace);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string message = UserChatting + ": " + this.txtInput.Text;
            tcpforUser.SendData(message);
            this.lbShow.Items.Add( message );
            this.txtInput.Clear();
            this.txtInput.Focus();
        }

        private void ShowRoomName (string request)
        {
            if (request == "adam")
                this.groupBox1.Text = "Bí mật Adam";
            if (request == "eva")
                this.groupBox1.Text = "Bí mật Eva";
            if (request == "date")
                this.groupBox1.Text = "Hẹn hò";
            if (request == "teen")
                this.groupBox1.Text = "Teen";
        }
               
        public void ListenOpponent()
        {
            while (requestclose != "close")
            {
                string str = tcpforOther.ReadData();
                string[] message = str.Split('*');
                foreach (string mess  in message)
                    lbShow.Items.Add(mess);
            }
        }

        private void FormChat_Load(object sender, EventArgs e)
        {
             CheckForIllegalCrossThreadCalls = false;
            Connect();
            ShowRoomName(Request);
            this.txtInput.Focus();
            Thread t = new Thread(ListenOpponent);
            t.Start();
        }

        private void FormChat_Closing(object sender, FormClosingEventArgs e)
        {
            requestclose = "close";
            tcpforUser.SendData(requestclose);
            tcpforUser.CloseConnection();
            tcpforOther.CloseConnection();
        } 
       

    }
}
