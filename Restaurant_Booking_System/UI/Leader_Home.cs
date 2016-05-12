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
using System.Data.SqlClient;
using System.Reflection;
using System.Threading;

namespace UI
{
    public partial class Leader_Home : Form
    {
        //登录类型的定义
        private const bool RECEPTIONIST = true;
        private const bool LEADER = false;
        //用户名，密码，登录类型
        public string USERNAME, PASSWORD;
        public bool USERTYPE;
        //数据库操作对象
        private Class1 DBA = new Class1();

        
        //点击查询记录按钮后新增加的控件
        Button Query_Button1;
        Label Query_Label1;
        TextBox Query_TextBox1;

        //记录到达按钮新增加的控件
        Label Arrive_Label1;
        TextBox Arrive_TextBox1;
        Button Arrive_Button1;

        //临时订餐的控件
        Label Temp_Label1;
        Label Temp_Label2;
        TextBox Temp_TextBox1;
        TextBox Temp_TextBox2;
        Button Temp_Button1;

        //改变餐桌的控件
        Label Change_Label1;
        Label Change_Label2;
        TextBox Change_TextBox1;
        TextBox Change_TextBox2;
        Button Change_Button1;

        //用餐完成的控件
        Label Complete_Label1;
        TextBox Complete_TextBox1;
        Button Complete_Button1;

        //在页面上画线
        
        //无参构造函数
        public Leader_Home()
        {

            InitializeComponent();
            init();
        }

        //有参构造函数，用户名，密码，用户类型
        public Leader_Home(string username, string password, bool usertype)
        {
            USERNAME = username;
            PASSWORD = password;
            USERTYPE = usertype;
            InitializeComponent();
            init();
        }

        //控件的申请空间
        private void init()
        {
            Query_Label1 = new Label();
            Query_TextBox1 = new TextBox();
            Query_Button1 = new Button();

            Arrive_Label1 = new Label();
            Arrive_TextBox1 = new TextBox();
            Arrive_Button1 = new Button();

            Temp_Label1 = new Label();
            Temp_Label2 = new Label();
            Temp_TextBox1 = new TextBox();
            Temp_TextBox2 = new TextBox();
            Temp_Button1 = new Button();

            Change_Label1 = new Label();
            Change_Label2 = new Label();
            Change_TextBox1 = new TextBox();
            Change_TextBox2 = new TextBox();
            Change_Button1 = new Button();

            Complete_Label1 = new Label();
            Complete_TextBox1 = new TextBox();
            Complete_Button1 = new Button();


            label4.Text = "";
            Query_Label1.AutoSize = true;
            Query_Label1.Location = new System.Drawing.Point(220, 100);
            Query_Label1.Name = "提示框";
            Query_Label1.Text = "输入日期";
            Query_Label1.Visible = false;
            this.Controls.Add(Query_Label1);

            Query_TextBox1.Location = new System.Drawing.Point(300, 100);
            Query_TextBox1.Name = "input";
            Query_TextBox1.Size = new System.Drawing.Size(100, 100);
            Query_TextBox1.Visible = false;
            this.Controls.Add(Query_TextBox1);

            Query_Button1.Location = new System.Drawing.Point(420, 100);
            Query_Button1.Text = "查询";
            Query_Button1.AutoSize = true;
            Query_Button1.Name = "Query_Button1";
            Query_Button1.Click += new System.EventHandler(this.Query_Button1_Click);
            Query_Button1.Visible = false;
            this.Controls.Add(Query_Button1);

            Arrive_Label1.AutoSize = true;
            Arrive_Label1.Location = new System.Drawing.Point(220, 100);
            //Arrive_Label1.Size = new System.Drawing.Size(100, 50);
            Arrive_Label1.Name = "提示框";
            Arrive_Label1.Text = "输入预定编号";
            Arrive_Label1.Visible = false;
            Arrive_Label1.AutoSize = true;
            this.Controls.Add(Arrive_Label1);

            Arrive_TextBox1.Location = new System.Drawing.Point(320, 100);
            Arrive_TextBox1.Size = new System.Drawing.Size(100, 100);
            Arrive_TextBox1.Visible = false;
            this.Controls.Add(Arrive_TextBox1);

            Arrive_Button1.Location = new System.Drawing.Point(440, 100);
            //Arrive_Button1.Size = new Size(100, 50);
            Arrive_Button1.Text = "提交";
            Arrive_Button1.Name = "Arrive_Button1";
            Arrive_Button1.Click += new EventHandler(this.Arrive_Button1_Click);
            Arrive_Button1.Visible = false;
            this.Controls.Add(Arrive_Button1);

            Temp_Label1.Text = "输入餐桌号";
            Temp_Label1.Location = new System.Drawing.Point(220, 100);
            Temp_Label1.AutoSize = true;
            Temp_Label1.Visible = false;
            this.Controls.Add(Temp_Label1);

            Temp_Label2.Text = "输入顾客姓名";
            Temp_Label2.Location = new System.Drawing.Point(220, 130);
            Temp_Label2.AutoSize = true;
            Temp_Label2.Visible = false;
            this.Controls.Add(Temp_Label2);

            Temp_TextBox1.Location = new System.Drawing.Point(320, 100);
            Temp_TextBox1.Size = new System.Drawing.Size(100, 100);
            Temp_TextBox1.Visible = false;
            this.Controls.Add(Temp_TextBox1);

            Temp_TextBox2.Location = new System.Drawing.Point(320, 130);
            Temp_TextBox2.Size = new System.Drawing.Size(100, 100);
            Temp_TextBox2.Visible = false;
            this.Controls.Add(Temp_TextBox2);

            Temp_Button1.Location = new System.Drawing.Point(460, 106);
            Temp_Button1.AutoSize = true;
            Temp_Button1.Text = "提交";
            Temp_Button1.Visible = false;
            Temp_Button1.Name = "Temp_Button1";
            Temp_Button1.Click += new EventHandler(this.Temp_Button1_Click);
            this.Controls.Add(Temp_Button1);

            Change_Label1.Location = new System.Drawing.Point(220, 100);
            Change_Label1.AutoSize = true;
            Change_Label1.Visible = false;
            Change_Label1.Text = "输入原来的餐桌编号";
            this.Controls.Add(Change_Label1);

            Change_Label2.Location = new System.Drawing.Point(220, 130);
            Change_Label2.AutoSize = true;
            Change_Label2.Visible = false;
            Change_Label2.Text = "输入调换的餐桌编号";
            this.Controls.Add(Change_Label2);

            Change_TextBox1.Location = new System.Drawing.Point(360, 100);
            Change_TextBox1.Size = new System.Drawing.Size(100, 100);
            Change_TextBox1.Visible = false;
            this.Controls.Add(Change_TextBox1);

            Change_TextBox2.Location = new System.Drawing.Point(360, 130);
            Change_TextBox2.Size = new System.Drawing.Size(100, 100);
            Change_TextBox2.Visible = false;
            this.Controls.Add(Change_TextBox2);

            Change_Button1.Location = new System.Drawing.Point(520, 106);
            Change_Button1.Text = "提交";
            Change_Button1.Visible = false;
            Change_Button1.AutoSize = true;
            Change_Button1.Click += new EventHandler(Change_Button1_Click);
            this.Controls.Add(Change_Button1);

            Complete_Label1.Location = new System.Drawing.Point(220, 100);
            Complete_Label1.AutoSize = true;
            Complete_Label1.Visible = false;
            Complete_Label1.Text = "请输入用餐编号";
            this.Controls.Add(Complete_Label1);

            Complete_TextBox1.Location = new System.Drawing.Point(320, 100);
            Complete_TextBox1.Size = new System.Drawing.Size(100, 100);
            Complete_TextBox1.Visible = false;
            this.Controls.Add(Complete_TextBox1);

            Complete_Button1.Location = new System.Drawing.Point(440, 100);
            Complete_Button1.AutoSize = true;
            Complete_Button1.Text = "提交";
            Complete_Button1.Visible = false;
            Complete_Button1.Click += new EventHandler(Complete_Button1_Click);
            this.Controls.Add(Complete_Button1);

            this.dataGridView1.Visible = false;
        }
        //隐藏按钮
        private void Hidden()
        {
            Query_Button1.Visible = false;
            Query_Label1.Visible = false;
            Query_TextBox1.Visible = false;

            Arrive_Button1.Visible = false;
            Arrive_Label1.Visible = false;
            Arrive_TextBox1.Visible = false;

            Temp_Button1.Visible = false;
            Temp_Label1.Visible = false;
            Temp_Label2.Visible = false;
            Temp_TextBox1.Visible = false;
            Temp_TextBox2.Visible = false;

            Change_Button1.Visible = false;
            Change_Label1.Visible = false;
            Change_Label2.Visible = false;
            Change_TextBox1.Visible = false;
            Change_TextBox2.Visible = false;

            Complete_Button1.Visible = false;
            Complete_Label1.Visible = false;
            Complete_TextBox1.Visible = false;
        
            this.dataGridView1.Visible = false;
            Leader_Home_Shown(new object(), new EventArgs());
        }
        private void Leader_Home_Paint(object sender, PaintEventArgs e)
        {

        }

        

        //窗体加载函数
        private void Leader_Home_Load(object sender, EventArgs e)
        {
            
            DBA.Query_name(ref label1, ref label2, USERNAME, PASSWORD, LEADER);


        }

        
        //单击了查询记录，增添一些控件
        private void button6_Click(object sender, EventArgs e)
        {
            Hidden();
            this.dataGridView1.Visible = true;
            label4.Text = "查看记录";
            //提示输入日期
            Query_Label1.Visible = true;
            Query_TextBox1.Visible = true;
            Query_Button1.Visible = true;
            
        }
        //点击查询，查询数据库，并返回结果
        private void Query_Button1_Click(object sender, EventArgs e)
        {
            string date = Query_TextBox1.Text;
            bool f = true;
            if (date.Length < 8 || date.Length > 10)
                f = false;
            int cnt1 = 0, cnt2 = 0;
            for (int i = 0; i < date.Length; ++i)
            {
                if (date[i] == '-') cnt1++;
                if (date[i] == '/') cnt2++;
                if(date[i] < '0' || date[i] > 9)
                {
                    f = false;
                    break;
                }
            }
            if (cnt1 != 2 && cnt2 != 2)
            {
               f = false;
            }
            if (f || date.Length == 0)
            {
                
                dataGridView1.DataSource = DBA.Query_Record(Query_TextBox1.Text);
                dataGridView1.DataMember = "Record";

            }
            else
                MessageBox.Show("输入的格式错误，请输入正确的格式，如（2015-12-12或2015/12/12）");
        }
        //点击记录到达按钮
        private void button7_Click(object sender, EventArgs e)
        {
            Hidden();
            this.label4.Text = "记录到达";
            Arrive_Label1.Visible = true;
            Arrive_TextBox1.Visible = true;
            Arrive_Button1.Visible = true;
        }
        //点击提交，更新数据库
        private void Arrive_Button1_Click(object sender, EventArgs e)
        {
            string Rid = this.Arrive_TextBox1.Text;
            if (Rid.Length == 0)
            {
                MessageBox.Show("编号不能为空");
                return;
            }
            for (int i = 0; i < Rid.Length; ++i)
            {
                if (Rid[i] < '0' || Rid[i] > '9')
                {
                    MessageBox.Show("输入的编号错误，只能是数字");
                    return;
                }
            }
            if (DBA.Update(Rid))
            {
                MessageBox.Show("修改成功");
            }
            else
            {
                MessageBox.Show("无此预定记录");
            }
        }
        //点击临时订餐按钮，
        private void Temp_Button1_Click(object sender, EventArgs e)
        {
            string Did = Temp_TextBox1.Text, name = Temp_TextBox2.Text;
            if (Did.Length == 0)
            {
                MessageBox.Show("输入餐桌编号有误，不能为空");
                return;
            }
            for (int i = 0; i < Did.Length; ++i)
            {
                if (Did[i] < '0' || Did[i] > '9')
                {
                    MessageBox.Show("输入的编号错误，只能是数字");
                    return;
                }
            }
            if (name.Length == 0)
            {
                MessageBox.Show("输入顾客姓名有误，不能为空");
                return;
            }
            if (DBA.Update_Record(Temp_TextBox1.Text, Temp_TextBox2.Text))
            {
                MessageBox.Show("订餐成功");
            }
            else
            {
                MessageBox.Show("订餐失败，请确认输入是否正确");
            }
        }
       
        //单击临时订餐
        private void button8_Click(object sender, EventArgs e)
        {

            Hidden(); 
            label4.Text = "临时订餐";
            Temp_Label1.Visible = true;
            Temp_Label2.Visible = true;
            Temp_TextBox1.Visible = true;
            Temp_TextBox2.Visible = true;
            Temp_Button1.Visible = true;
            
        }
        //改变改变餐桌
        private void button9_Click(object sender, EventArgs e)
        {

            Hidden(); 
            label4.Text = "调换餐桌";
            Change_Label1.Visible = true;
            Change_Label2.Visible = true;
            Change_TextBox1.Visible = true;
            Change_TextBox2.Visible = true;
            Change_Button1.Visible = true;
        }
        //改变餐桌单击函数
        void Change_Button1_Click(object sender, EventArgs e)
        {
            string Old_Id = Change_TextBox1.Text, New_Id = Change_TextBox2.Text;
            if (Old_Id.Length == 0)
            {
                MessageBox.Show("输入原餐桌编号，不能为空");
                return;
            }
            if (New_Id.Length == 0)
            {
                MessageBox.Show("输入新餐桌编号，不能为空");
                return;
            }
            for (int i = 0; i < Old_Id.Length; ++i)
            {
                if (Old_Id[i] < '0' || Old_Id[i] > '9')
                {
                    MessageBox.Show("输入的编号错误，只能是数字");
                    return;
                }
            }
            for (int i = 0; i < New_Id.Length; ++i)
            {
                if (New_Id[i] < '0' || New_Id[i] > '9')
                {
                    MessageBox.Show("输入的编号错误，只能是数字");
                    return;
                }
            }
            if (DBA.Update_Table(Change_TextBox1.Text, Change_TextBox2.Text))
            {
                MessageBox.Show("移动餐桌成功");
            }
            else 
            {
                MessageBox.Show("失败，请确认输入是否正确");
            }
        }
        //用餐完成
        private void button10_Click(object sender, EventArgs e)
        {

            Hidden(); 
            label4.Text = "用餐完成";
            Complete_Label1.Visible = true;
            Complete_TextBox1.Visible = true;
            Complete_Button1.Visible = true;
        }
        //用餐完成单击事件
        void Complete_Button1_Click(object sender, EventArgs e)
        {
            string Rid = Complete_TextBox1.Text;
            if (Rid.Length == 0)
            {
                MessageBox.Show("输入新餐桌编号，不能为空");
                return;
            }
            for (int i = 0; i < Rid.Length; ++i)
            {
                if (Rid[i] < '0' || Rid[i] > '9')
                {
                    MessageBox.Show("输入的编号错误，只能是数字");
                    return;
                }
            }
            if (DBA.Update_Complete(Complete_TextBox1.Text))
            {
                MessageBox.Show("修改成功");
            }
            else
            {
                MessageBox.Show("修改失败，请确认输入是否正确");
            }
        }

        //注销
        private void button11_Click(object sender, EventArgs e)
        {
            new Thread((ThreadStart)delegate
            {
                Application.Run(new Login());
            }).Start();
            this.Close();
        }
        // 
        private void timer1_Tick(object sender, EventArgs e)
        {

            this.label5.Text = DateTime.Now.ToLongDateString();
            this.label5.Text += "\n   " + DateTime.Now.ToLongTimeString();
        }

        private void Leader_Home_Shown(object sender, EventArgs e)
        {
            Pen pen = new Pen(Color.Gray, 1);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            pen.DashPattern = new float[] { 5, 5 };
            Graphics gh = this.CreateGraphics();
            gh.DrawLine(pen, 200, 0, 200, 2700);
            gh.DrawLine(pen, 0, 164, 202, 164);
            gh.DrawImage(Image.FromFile(@"C:\Users\Administrator\Documents\Visual Studio 2013\Projects\Restaurant_Booking_System\Image\show.jpg"), 202, 164, 857, 497);           
        }
    }
}
