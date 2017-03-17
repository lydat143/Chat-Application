using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Chat
{
    public partial class FormRoom : Form
    {
        private TCPModel tcpInfo;
        int port;
        string IP;
        FormChat frmChat = new FormChat();    
        string UserChatting;

        // lấy username từ Form Log
        public delegate void SendUsername(string usr);
        public SendUsername Sender;
        //Hàm có nhiệm vụ lấy tham số truyền vào
        private void GetUsername(string Message)
        {
            UserChatting = Message;
        }

        public FormRoom()
        {
            InitializeComponent();
            //Tạo con trỏ tới hàm GetUsername
            Sender = new SendUsername(GetUsername);
        }

       private void Connect()
        {
            try
            {
                port = 13000;
                IP = "127.0.0.1";
                tcpInfo = new TCPModel(IP, port);
                tcpInfo.ConnectToServer();
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR......" + e.StackTrace);
            }
        }
       
        private void FormRoom_Load(object sender, EventArgs e)
        {
            this.Text = UserChatting;
            frmChat.SenderUser(UserChatting);
            
            Connect(); //kết nối lên server

            ChooseRoom();//gợi ý các phòng phù hợp
        }

       private void btnAdam_CheckedChanged(object sender, EventArgs e)
        {
            //btnDate.Enabled = false;
            //btnEva.Enabled = false;
            //btnTeen.Enabled = false;
            frmChat.SenderRequest("adam");
            frmChat.Show();
        }

        private void btnEva_CheckedChanged(object sender, EventArgs e)
        {
            //btnDate.Enabled = false;
            //btnAdam.Enabled = false;
            //btnTeen.Enabled = false;
            frmChat.SenderRequest("eva");
            frmChat.Show();
        }

        private void btnDate_CheckedChanged(object sender, EventArgs e)
        {
            //btnAdam.Enabled = false;
            //btnEva.Enabled = false;
            //btnTeen.Enabled = false;
            frmChat.SenderRequest("date");
            frmChat.Show();
        }

        private void btnTeen_CheckedChanged(object sender, EventArgs e)
        {
            //btnDate.Enabled = false;
            //btnEva.Enabled = false;
            //btnAdam.Enabled = false;
            frmChat.SenderRequest("teen");
            frmChat.Show();
        }

        private void ChooseRoom()
        {
            tcpInfo.SendData("info"); //gửi yêu cầu lấy thông tin người dùng
            tcpInfo.ReadData(); //đợi server chấp nhận gửi thông tin
            tcpInfo.SendData(UserChatting); //gửi thông tin username lên server
            string Info = tcpInfo.ReadData(); //nhận lại các thông tin như tuổi, mối quan hệ
            string []str = Info.Split();
            int Age = Int16.Parse(str[0]);
            string Rela = str[1];
            if (Age >= 18)
            {
                btnTeen.Enabled = false;

                if(Rela != "Single")
                    btnDate.Enabled = false;
            }
            else
            {
                btnDate.Enabled = false;
                btnEva.Enabled = false;
                btnAdam.Enabled = false;
            }
        }
    }
}
