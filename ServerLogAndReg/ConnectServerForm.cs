using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Server
{
    public partial class ConnectServerForm : Form
    {
        private string connectionSTR;
        public ConnectServerForm()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            ConnectServerSQL();
        }

        private void txtAuthentication_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtAuthentication.Text == "SQL Server Authentication")
            {
                txtUserName.Enabled = true;
                txtPassword.Enabled = true;
               
            }
            else
            {
                txtUserName.Enabled = false ;
                txtPassword.Enabled = false;
                
            }
        }

        private void ConnectServerSQL()
        {
            SqlConnection conn = new SqlConnection(connectionSTR);
            try
            {
                conn.Open();
                Server.Properties.Settings.Default.strConnection = connectionSTR;
                Server.Properties.Settings.Default.Save();
                MessageBox.Show("Kết nối SQL Server thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Application.Exit();
                Application.Restart();
            }
            catch (Exception)
            {
                MessageBox.Show("Kết nối không thành công, xin kiểm tra lại", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtServerName_TextChanged(object sender, EventArgs e)
        {
            if (txtAuthentication.Text == "SQL Server Authentication")
            {
                if (txtPassword.Text != null && txtUserName.Text != null && txtServerName.Text != null)
                {
                    btnConnect.Enabled = true;
                    connectionSTR = "Data Source=" + txtServerName.Text + ";Initial Catalog=QLNH;User ID=" + txtUserName.Text + ";Password=" + txtPassword.Text;
                }
            }
            else
            {
                if (txtServerName.Text != null)
                {
                    btnConnect.Enabled = true;
                    connectionSTR = "Data Source=" + txtServerName.Text + ";Initial Catalog=QLNH;Integrated Security=True";
                }
            }
        }
    }
}
