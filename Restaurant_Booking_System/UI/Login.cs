using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Booking_DBA;
using System.Threading;
using System.Data.SqlClient;
using System.Reflection;

namespace UI
{
    public partial class Login : Form
    {
        //选择的登录类型是什么
        private const bool RECEPTIONIST = true;
        private const bool LEADER = false;
        //数据库操作类
        private Class1 DBA = new Class1();
        //登录界面的构造函数
        public Login()
        {
            InitializeComponent();
        }

        //页面加载函数
        private void Login_Load(object sender, EventArgs e)
        {
            this.label4.Text += "\nAny problem, Please Contact Royecode@163.com";
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        //按下登录按钮，登录函数
        private void button1_Click(object sender, EventArgs e)
        {
            string username = this.textBox1.Text;
            string password = this.textBox2.Text;
            textBox2.Text = "";
            if (username.Length == 0 || password.Length == 0)
            {
                MessageBox.Show("姓名或密码不能为空");
                return;
            }
            bool USERTYPE = RECEPTIONIST; //check为true表示接待员
            if (this.radioButton1.Checked)
                USERTYPE = RECEPTIONIST;
            if (this.radioButton2.Checked)
                USERTYPE = LEADER;
            if (DBA.Login(username, password, USERTYPE))
            {
                //登录成功，跳转到主页面
                if (USERTYPE)
                {
                    new Thread((ThreadStart)delegate
                    {
                        Application.Run(new Receptionist_Home(username, password, USERTYPE));
                    }).Start();
                }
                else 
                {
                    new Thread((ThreadStart)delegate
                    {
                        Application.Run(new Leader_Home(username, password, USERTYPE));
                    }).Start();
                }
                this.Close();
            }
            else
            {

                //登录失败
                MessageBox.Show("用户名或密码错误，请重新输入");
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        //按下重置按钮，清空用户名和密码框
        private void button2_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
            this.textBox2.Text = "";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.label5.Text = "日期:  " + DateTime.Now.ToLongDateString();
            this.label6.Text = "时间:  " + DateTime.Now.ToLongTimeString();
        }
    }
}
