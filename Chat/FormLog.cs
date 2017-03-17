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
    public partial class Client : Form
    {
        
        public Client()
        {
            InitializeComponent();
            this.Text = "LOG IN";
        }
        
        private TCPModel tcpLog;
        int port;
        string IP;

        private void Connect()
        {
            try {
                port = 13000;
                IP = "127.0.0.1";
                tcpLog = new TCPModel(IP, port);
                tcpLog.ConnectToServer();   
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR......" + e.StackTrace);
            }
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            tcpLog.SendData("log");
            this.lbelStatus.Text = "";
            string info = this.txtUsername.Text + " " + this.txtPass.Text;
            tcpLog.SendData(info);
            string check = tcpLog.ReadData();
            if (this.txtUsername.TextLength == 0 || this.txtPass.TextLength == 0)
            {
                this.lbelStatus.ForeColor = Color.Red;
                this.lbelStatus.Text = "Bạn chưa nhập tài khoản hoặc mật khẩu";
            }
            else
            {
                if (check == "1") // 1:tài khoản có tồn tại, 0: tài khoản không tồn tại, 2: tài khoản đã được đăng nhập
                {
                    this.Hide();
                    
                    FormRoom frmRoom = new FormRoom();
                    frmRoom.Sender(txtUsername.Text);
                    frmRoom.Show();
                    tcpLog.SendData("close");
                    tcpLog.CloseConnection();
                    
                }
                else
                {
                    if (check == "2")
                    {
                        this.lbelStatus.ForeColor = Color.Red;
                        this.lbelStatus.Text = "Tài khoản đã được đăng nhập";
                        this.txtUsername.Clear();
                        this.txtPass.Clear();
                        this.txtUsername.Focus();
                    }
                    else
                    {
                        this.lbelStatus.ForeColor = Color.Red;
                        this.lbelStatus.Text = "Tài khoản không tồn tại hoặc sai mật khẩu";
                        this.txtUsername.Clear();
                        this.txtPass.Clear();
                        this.txtUsername.Focus();
                    }
                }
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Connect();//connect to server
        }

        private void btnReg_Click(object sender, EventArgs e)
        {
            FormReg frmReg = new FormReg();
            frmReg.Show();
        }
    }
}
