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
using System.Reflection;
using System.Threading;

namespace UI
{
    public partial class Receptionist_Home : Form
    {
        private const bool RECEPTIONIST = true;
        private const bool LEADER = false;

        public string USERNAME, PASSWORD;
        public bool USERTYPE;
        private Class1 DBA = new Class1();

        //点击查询记录按钮后新增加的控件
        Button Query_Button1;
        Label Query_Label1;
        TextBox Query_TextBox1;

        //查询餐桌
        Button Query_Desk_Button1;
        Label Query_Desk_Label1;
        TextBox Query_Desk_TextBox1;

        //增加预约
        Label Add_Label1;
        Label Add_Label2;
        Label Add_Label3;
        Label Add_Label4;
        Label Add_Label5;
        TextBox Add_TextBox1;
        TextBox Add_TextBox2;
        TextBox Add_TextBox3;
        TextBox Add_TextBox4;
        TextBox Add_TextBox5;
        Button Add_Button1;

        //取消预约
        Label Cancel_Label1;
        TextBox Cancel_TextBox1;
        Button Cancel_Button1;
        //无参构造函数
        public Receptionist_Home()
        {
            InitializeComponent();
            init();
        }
        //有参构造函数
        public Receptionist_Home(string username, string password, bool usertype)
        {
            USERNAME = username;
            PASSWORD = password;
            USERTYPE = usertype;
            InitializeComponent();
            init();
        }
        private void init()
        {

            Query_Label1 = new Label();
            Query_TextBox1 = new TextBox();
            //查询按钮
            Query_Button1 = new Button();

            Query_Desk_Label1 = new Label();
            Query_Desk_TextBox1 = new TextBox();
            //查询按钮
            Query_Desk_Button1 = new Button();

            Add_Label1 = new Label(); 
            Add_Label2 = new Label();
            Add_Label3 = new Label();
            Add_Label4 = new Label();
            Add_Label5 = new Label();
            Add_TextBox1 = new TextBox();
            Add_TextBox2 = new TextBox();
            Add_TextBox3 = new TextBox();
            Add_TextBox4 = new TextBox();
            Add_TextBox5 = new TextBox();
            Add_Button1 = new Button();

            Cancel_Label1 = new Label();
            Cancel_TextBox1 = new TextBox();
            Cancel_Button1 = new Button();

            Query_Label1.AutoSize = true;
            Query_Label1.Location = new System.Drawing.Point(220, 100);
            Query_Label1.Name = "提示框";
            Query_Label1.Text = "输入日期";
            Query_Label1.Visible = false;
            this.Controls.Add(Query_Label1);
            //输入框
            Query_TextBox1.Location = new System.Drawing.Point(300, 100);
            Query_TextBox1.Name = "input";
            Query_TextBox1.Size = new System.Drawing.Size(100, 100);
            Query_TextBox1.Visible = false;
            this.Controls.Add(Query_TextBox1);

            Query_Button1.Location = new System.Drawing.Point(420, 100);
            Query_Button1.AutoSize = true;
            Query_Button1.Text = "查询";
            //Query_Button1.Size = new System.Drawing.Size(100, 100);
            Query_Button1.Name = "Query_Button1";
            Query_Button1.Click += new System.EventHandler(this.Query_Button1_Click);
            Query_Button1.Visible = false;
            this.Controls.Add(Query_Button1);


            Query_Desk_Label1.AutoSize = true;
            Query_Desk_Label1.Location = new System.Drawing.Point(220, 100);
            Query_Desk_Label1.Name = "提示框";
            Query_Desk_Label1.Text = "输入日期";
            Query_Desk_Label1.Visible = false;
            this.Controls.Add(Query_Desk_Label1);
            //输入框
            Query_Desk_TextBox1.Location = new System.Drawing.Point(300, 100);
            Query_Desk_TextBox1.Name = "input";
            Query_Desk_TextBox1.Size = new System.Drawing.Size(100, 100);
            Query_Desk_TextBox1.Visible = false;
            this.Controls.Add(Query_Desk_TextBox1);

            Query_Desk_Button1.Location = new System.Drawing.Point(420, 100);
            Query_Desk_Button1.AutoSize = true;
            Query_Desk_Button1.Text = "查询";
            //Query_Button1.Size = new System.Drawing.Size(100, 100);
            Query_Desk_Button1.Name = "Query_Button1";
            Query_Desk_Button1.Click += new System.EventHandler(this.Query_Desk_Button1_Click);
            Query_Desk_Button1.Visible = false;
            this.Controls.Add(Query_Desk_Button1);

            Add_Label1.Location = new System.Drawing.Point(220, 100);
            Add_Label1.AutoSize = true;
            Add_Label1.Visible = false;
            Add_Label1.Text = "姓名";
            this.Controls.Add(Add_Label1);

            Add_Label2.Location = new System.Drawing.Point(360, 100);
            Add_Label2.AutoSize = true;
            Add_Label2.Visible = false;
            Add_Label2.Text = "电话";
            this.Controls.Add(Add_Label2);

            Add_Label3.Location = new System.Drawing.Point(510, 100);
            Add_Label3.AutoSize = true;
            Add_Label3.Visible = false;
            Add_Label3.Text = "人数";
            this.Controls.Add(Add_Label3);

            Add_Label4.Location = new System.Drawing.Point(360, 130);
            Add_Label4.AutoSize = true;
            Add_Label4.Visible = false;
            Add_Label4.Text = "日期";
            this.Controls.Add(Add_Label4);

            Add_Label5.Location = new System.Drawing.Point(220, 130);
            Add_Label5.AutoSize = true;
            Add_Label5.Visible = false;
            Add_Label5.Text = "时间";
            this.Controls.Add(Add_Label5);

            Add_TextBox1.Location = new System.Drawing.Point(250, 100);
            Add_TextBox1.Size = new System.Drawing.Size(100, 100);
            Add_TextBox1.Visible = false;
            this.Controls.Add(Add_TextBox1);

            Add_TextBox2.Location = new System.Drawing.Point(400, 100);
            Add_TextBox2.Size = new System.Drawing.Size(100, 50);
            Add_TextBox2.Visible = false;
            this.Controls.Add(Add_TextBox2);

            Add_TextBox3.Location = new System.Drawing.Point(250, 130);
            Add_TextBox3.Size = new System.Drawing.Size(100, 50);
            Add_TextBox3.Visible = false;
            this.Controls.Add(Add_TextBox3);

            Add_TextBox4.Location = new System.Drawing.Point(400, 130);
            Add_TextBox4.Size = new System.Drawing.Size(100, 50);
            Add_TextBox4.Visible = false;
            this.Controls.Add(Add_TextBox4);

            Add_TextBox5.Location = new System.Drawing.Point(550, 100);
            Add_TextBox5.Size = new System.Drawing.Size(100, 50);
            Add_TextBox5.Visible = false;
            this.Controls.Add(Add_TextBox5);

            Add_Button1.Location = new System.Drawing.Point(570, 130);
            Add_Button1.AutoSize = true;
            Add_Button1.Visible = false;
            Add_Button1.Text = "提交";
            Add_Button1.Click += new EventHandler(Add_Button1_Click);
            this.Controls.Add(Add_Button1);

            Cancel_Label1.Location = new System.Drawing.Point(220, 100);
            Cancel_Label1.Text = "输入预定编号";
            Cancel_Label1.Visible = false;
            Cancel_Label1.AutoSize = true;
            this.Controls.Add(Cancel_Label1);

            Cancel_TextBox1.Location = new System.Drawing.Point(310, 100);
            Cancel_TextBox1.Visible = false;
            Cancel_TextBox1.Size = new System.Drawing.Size(100, 100);
            this.Controls.Add(Cancel_TextBox1);

            Cancel_Button1.Location = new System.Drawing.Point(430, 100);
            Cancel_Button1.Visible = false;
            Cancel_Button1.AutoSize = true;
            Cancel_Button1.Text = "提交";
            Cancel_Button1.Click += new EventHandler(Cancel_Button1_Click);
            this.Controls.Add(Cancel_Button1);

            label5.Visible = false;

            this.dataGridView1.Visible = false;

            Receptionist_Home_Shown(new object(), new EventArgs());
        
        }
        //加载页面
        private void Receptionist_Home_Load(object sender, EventArgs e)
        {
            //显示员工基本信息
            //MessageBox.Show("FUCK");
            DBA.Query_name(ref label1, ref label2, USERNAME, PASSWORD, RECEPTIONIST);
            
        }
        //单击了查询记录，增添一些控件
        private void button1_Click(object sender, EventArgs e)
        {

            //this.Receptionist_Home_Load(new object(), new EventArgs());
            /*Arrive_Label1.Visible = false;
            Arrive_TextBox1.Visible = false;
            Arrive_Button1.Visible = false;
            */
            //提示输入日期
            Hidden();
            label5.Text = "查看记录";
            label5.Visible = true;
            Query_Label1.Visible = true;
            Query_TextBox1.Visible = true;
            Query_Button1.Visible = true;
            dataGridView1.Visible = true;
            dataGridView1.DataSource = null;
        }
        private void Query_Button1_Click(object sender, EventArgs e)
        {
            string date = Query_TextBox1.Text;
            //MessageBox.Show(date);
            //2015/12/27
            bool f = true;
            if (date.Length < 8 || date.Length > 10)
                f = false;
            int cnt1 = 0, cnt2 = 0;
            for (int i = 0; i < date.Length; ++i)
            {
                if (date[i] == '-') cnt1++;
                else if (date[i] == '/') cnt2++;
                else if (date[i] < '0' || date[i] > '9')
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
        //单击了查询记录，增添一些控件
        private void button5_Click(object sender, EventArgs e)
        {

            //this.Receptionist_Home_Load(new object(), new EventArgs());
            /*Arrive_Label1.Visible = false;
            Arrive_TextBox1.Visible = false;
            Arrive_Button1.Visible = false;
            */
            //提示输入日期
            Hidden();
            label5.Text = "查看餐桌";
            label5.Visible = true;
            Query_Desk_Label1.Visible = true;
            Query_Desk_TextBox1.Visible = true;
            Query_Desk_Button1.Visible = true;
            dataGridView1.Visible = true;

            dataGridView1.DataSource = null;
        }
        //查询餐桌状态
        private void Query_Desk_Button1_Click(object sender, EventArgs e)
        {
            string date = Query_Desk_TextBox1.Text;
            //MessageBox.Show(date);
            bool f = true;
            if (date.Length < 8 || date.Length > 10)
                f = false;
            int cnt1 = 0, cnt2 = 0;
            for (int i = 0; i < date.Length; ++i)
            {
                if (date[i] == '-') cnt1++;
                else if (date[i] == '/') cnt2++;
                else if (date[i] < '0' || date[i] > '9')
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
                dataGridView1.DataSource = DBA.Query_Desk(Query_Desk_TextBox1.Text);
                dataGridView1.DataMember = "Desk";
            }
            else
                MessageBox.Show("输入的格式错误，请输入正确的格式，如（2015-12-12或2015/12/12）");


        }
        private void Add_Button1_Click(object sender, EventArgs e)
        {
            int num = DBA.Add_Record(Add_TextBox1.Text, Add_TextBox2.Text, Add_TextBox3.Text, Add_TextBox4.Text, Add_TextBox5.Text);
            
            if(num > 0)
            {
                MessageBox.Show("添加成功，订餐编号是" + num.ToString());
            }
            else
            {
                MessageBox.Show("添加失败");
            }
        }
        //显示时间与日期的timer函数
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.label3.Text = DateTime.Now.ToLongDateString();
            this.label3.Text += "\n   " + DateTime.Now.ToLongTimeString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hidden();
            Add_Label1.Visible = true;
            Add_Label2.Visible = true;
            Add_Label3.Visible = true;
            Add_Label4.Visible = true;
            Add_Label5.Visible = true;
            Add_TextBox1.Visible = true;
            Add_TextBox2.Visible = true;
            Add_TextBox3.Visible = true;
            Add_TextBox4.Visible = true;
            Add_TextBox5.Visible = true;
            Add_Button1.Visible = true;
            label5.Text = "增加预约";
            label5.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Hidden();
            Cancel_Label1.Visible = true;
            Cancel_TextBox1.Visible = true;
            Cancel_Button1.Visible = true;
            label5.Visible = true;
            label5.Text = "取消预约";
        }
        private void Cancel_Button1_Click(object sender, EventArgs e)
        {
            if (DBA.Cancel_Record(Cancel_TextBox1.Text))
            {
                MessageBox.Show("取消预约成功");
            }
            else
            {
                MessageBox.Show("取消预约失败");
            }
        }
        //注销
        private void button4_Click(object sender, EventArgs e)
        {
            new Thread((ThreadStart)delegate
            {
                Application.Run(new Login());
            }).Start();
            this.Close();
        }
        //画背景图片
        private void Receptionist_Home_Shown(object sender, EventArgs e)
        {
            Pen pen = new Pen(Color.Gray, 1);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            pen.DashPattern = new float[] { 5, 5 };
            Graphics gh = this.CreateGraphics();
            gh.DrawLine(pen, 200, 0, 200, 2700);
            gh.DrawLine(pen, 0, 164, 202, 164);
            gh.DrawImage(Image.FromFile(@"C:\Users\Administrator\Documents\Visual Studio 2013\Projects\Restaurant_Booking_System\Image\show.jpg"), 202, 164, 857, 497);           
            //MessageBox.Show("FCUK");
        }
        private void Hidden()
        {
           
            Query_Button1.Visible = false;
            Query_Label1.Visible = false;
            Query_TextBox1.Visible = false;


            Query_Desk_Button1.Visible = false;
            Query_Desk_Label1.Visible = false;
            Query_Desk_TextBox1.Visible = false;
            
            Add_Button1.Visible = false;
            Add_Label1.Visible = false;
            Add_Label2.Visible = false;
            Add_Label3.Visible = false;
            Add_Label4.Visible = false;
            Add_Label5.Visible = false;
            Add_TextBox1.Visible = false;
            Add_TextBox2.Visible = false;
            Add_TextBox3.Visible = false;
            Add_TextBox4.Visible = false;
            Add_TextBox5.Visible = false;

            Cancel_Button1.Visible = false;
            Cancel_Label1.Visible = false;
            Cancel_TextBox1.Visible = false;
            this.dataGridView1.Visible = false;
            Receptionist_Home_Shown(new object(), new EventArgs());
        }
        
        private void dataGridView1_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {

        }


    }
}
