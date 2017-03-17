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
    public partial class FormReg : Form
    {
        public FormReg()
        {
            InitializeComponent();
        }

        private TCPModel tcpReg;
        int port;
        string IP;

        private void Connect()
        {
            try
            {
                port = 13000;
                IP = "127.0.0.1";
                tcpReg = new TCPModel(IP, port);
                tcpReg.ConnectToServer();
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR......" + e.StackTrace);
            }
        }

        private void btnReg_Click(object sender, EventArgs e)
        {
            tcpReg.SendData("reg");
            string info = this.txtUser.Text + " " + this.txtPass.Text + " " + this.txtSex.Text + " " + this.txtAge.Text + " " + this.txtRelation.Text;
            tcpReg.SendData(info);
            string status = tcpReg.ReadData();
            if (status == "1")
            {
                MessageBox.Show("Bạn đã đăng ký thành công", "Thông báo");
                this.Hide();
            }else
            {
                this.lbelStatus.ForeColor = Color.Red;
                this.lbelStatus.Text = "Tài khoản đã tồn tại!";
                this.txtUser.Clear();
                this.txtPass.Clear();
                this.txtUser.Focus();

            }
        }

        private void FormReg_Load(object sender, EventArgs e)
        {
            Connect();
        }

       
    }
}
