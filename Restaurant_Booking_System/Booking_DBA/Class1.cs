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
using System.Reflection;

namespace Booking_DBA
{
    public class Class1
    {
        //宏定义
        private const bool RECEPTIONIST = true;
        private const bool LEADER = false;

        //析构函数
        ~Class1()
        {
        }
        //登录,比对数据库
        public bool Login(string username, string userpwd, bool check)
        {
            //数据库地址
            String connectionstring = "Data Source=OLIVER_BAO;Initial Catalog=Restaurant_Booking_System;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionstring);
            conn.Open();
            string sql = null;
            if (check == RECEPTIONIST)
            {
                sql = "select * from Receptionist where Rname ='" + username + "' and Rpassword = '" + userpwd + "';";
            }
            else
            {
                sql = "select * from Leader where Lname ='" + username + "' and Lpassword = '" + userpwd + "';";
            }
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            try
            {
                if (reader.Read())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            } 
            finally
            {
                reader.Dispose();
                cmd.Dispose();
                conn.Dispose();
                conn.Close();
            }
        }
        //查询员工的编号和姓名
        public void Query_name(ref Label label1, ref Label label2, string username, string password, bool check)
        {
            String connectionstring = "Data Source=OLIVER_BAO;Initial Catalog=Restaurant_Booking_System;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionstring);
            conn.Open();
            //sql = select Cname, Cphone, Cnumber from Customer where Cid = (select Tid from Record where Rdate = date)
            //select * from Restaurant_Booking_System.dbo.Record,Restaurant_Booking_System.dbo.Customer,Restaurant_Booking_System.dbo.Table where Restaurant_Booking_System.dbo.Record.Cid = Restaurant_Booking_System.dbo.Customer.Cid; 
            string sql = null;
            if (check == LEADER)
            {
                sql = "select * from Leader where Lname = '" + username + "' and Lpassword = '" + password + "';";
            }
            else 
            {
                sql = "select * from Receptionist where Rname = '" + username + "' and Rpassword = '" + password + "';";
            }
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            try
            {
                if (reader.Read())
                {
                    int id;
                    string name;
                    if (check == LEADER)
                    {
                        id = reader.GetInt32(reader.GetOrdinal("Lid"));
                        name = reader.GetString(reader.GetOrdinal("Lname"));
                    }
                    else
                    {
                        id = reader.GetInt32(reader.GetOrdinal("Rid"));
                        name = reader.GetString(reader.GetOrdinal("Rname"));
                    }
                    label1.Text = "员工编号: " + id.ToString();
                    label2.Text = "员工姓名: " + name;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                reader.Dispose();
                cmd.Dispose();
                conn.Dispose();
                conn.Close();
            }
        }
        //查询记录，将数据库的记录查询出来返回DataSet
        public DataSet Query_Record(string date)
        {
            String connectionstring = "Data Source=OLIVER_BAO;Initial Catalog=Restaurant_Booking_System;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionstring);
            conn.Open();
            string sql = null;
            //如果输入的日期不为空则查询指定日期的记录
            if (date.Length != 0)
            {
                sql = "select R.Rid 预定编号,R.Rdate 日期, R.Rtime 时间, C.Cname 姓名, C.Cphone 电话," +
                    "C.Cnumber 人数, D.Dnumber 餐桌号,R.Rstatus 订单状态 from Restaurant_Booking_System.dbo.Record as R," +
                    "Restaurant_Booking_System.dbo.Customer as C,Restaurant_Booking_System.dbo.Desk as D" + 
                    " where R.Cid = C.Cid and R.Did = D.Did and R.Rdate = '" + date + "';"; 
            }
            //否则查询所有的记录
            else
            {
                sql = "select R.Rid 预定编号,R.Rdate 日期, R.Rtime 时间, C.Cname 姓名, C.Cphone 电话," +
                    "C.Cnumber 人数, D.Dnumber 餐桌号,R.Rstatus 订单状态 from Restaurant_Booking_System.dbo.Record as R," +
                    "Restaurant_Booking_System.dbo.Customer as C,Restaurant_Booking_System.dbo.Desk as D" +
                    " where R.Cid = C.Cid and R.Did = D.Did;"; 
            }
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();
                da.Fill(ds, "Record");
                return ds;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                da.Dispose();
                cmd.Dispose();
                conn.Dispose();
                conn.Close();
            }
        }
        
        //顾客到达，点击记录到达，更新桌子状态、订单状态
        public bool Update(string Rid)
        {
            String connectionstring = "Data Source=OLIVER_BAO;Initial Catalog=Restaurant_Booking_System;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionstring);
            conn.Open();
            string date = DateTime.Now.ToShortDateString();
            //MessageBox.Show(date);
            string sql = "update Desk set Dstatus = '正在使用' where Did = (select Did from Record where Rstatus = '预定' and Rid = " + Rid + ");";
            //MessageBox.Show(sql);
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                int num = (int)cmd.ExecuteNonQuery();
                if (num > 0)
                {
                    sql = "update Record set Rstatus = '正在使用' where Rid = " + Rid + ";";
                    cmd.CommandText = sql;
                    num = (int)cmd.ExecuteNonQuery();
                    return true;
                }

                //MessageBox.Show("FUCK");
                return false;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                conn.Dispose();
                conn.Close();
            }
        }
        //临时订餐，输入餐桌编号和顾客姓名
        public bool Update_Record(string Dnumber, string name)
        {
            String connectionstring = "Data Source=OLIVER_BAO;Initial Catalog=Restaurant_Booking_System;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionstring);
            conn.Open();
            string date = DateTime.Now.ToShortDateString(); 
            //更新桌子状态
            string sql = "update Desk set Dstatus = '正在使用' where Dnumber = " + Dnumber + " and Ddate = '" + date + "' and Dstatus = '可用';";
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                int num = (int)cmd.ExecuteNonQuery();
                if (num <= 0)
                    return false;
                //插入顾客信息记录
                sql = "insert into Customer(Cname) values('" + name + "');";
                cmd.CommandText = sql;
                num = (int)cmd.ExecuteNonQuery();
                if (num <= 0)
                    return false;
                //查询插入顾客记录的Cid
                sql = "select * from Customer where Cname = '" + name + "';";
                cmd.CommandText = sql;
                SqlDataReader reader = cmd.ExecuteReader();
                int Cid = 0;
                if (reader.Read())
                {
                    Cid = reader.GetInt32(reader.GetOrdinal("Cid"));
                }
                reader.Close();
                if (num <= 0)
                    return false;
                //查询餐桌的Did
                sql = "select * from Desk where Dnumber = " + Dnumber + " and Ddate = '" + date + "';";
                cmd.CommandText = sql;
                reader = cmd.ExecuteReader();
                if (!reader.Read()) return false;
                int Did = reader.GetInt32(reader.GetOrdinal("Did"));
                //插入记录
                reader.Close();
                sql = "insert into Record(Did, Rdate, Rtime, Rstatus, Cid) values(" + Did + ",'" + date +
                    "','" + DateTime.Now.ToLongTimeString().ToString() + "', '正在使用'," + Cid + ");";
                cmd.CommandText = sql;
                num = (int)cmd.ExecuteNonQuery();
                if (num <= 0)
                    return false;
                return true;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                
                cmd.Dispose();
                conn.Dispose();
                conn.Close();
            }
        }
        //调换餐桌
        public bool Update_Table(string Old_Id, string New_Id)
        {
            String connectionstring = "Data Source=OLIVER_BAO;Initial Catalog=Restaurant_Booking_System;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionstring);
            conn.Open();
            string date = DateTime.Now.ToShortDateString();
            //更新原餐桌状态
            string sql = "select * from Desk where Dstatus = '正在使用' and Dnumber = " + Old_Id + " and Ddate = '" + date + "';";

            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            
            try
            {
                if (!reader.Read()) return false;
                int Old_Did = reader.GetInt32(reader.GetOrdinal("Did"));
                reader.Close();
                sql = "select * from Desk where Dstatus = '可用' and Dnumber = " + New_Id + " and Ddate = '" + date + "';";
                
                cmd.CommandText = sql;
                reader = cmd.ExecuteReader();
                if (!reader.Read()) return false;
                int New_Did = reader.GetInt32(reader.GetOrdinal("Did"));
                reader.Close();
                //if (num <= 0)
                  //  return false;
                //sql = "insert into Customer(Cname) values('" + name + "');";
                //num = (int)cmd.ExecuteNonQuery();
                //更新新的餐桌
                sql = "update Desk set Dstatus = '可用' where Did = " + Old_Did + ";";
                cmd.CommandText = sql;
                int num = (int)cmd.ExecuteNonQuery();
                
                if (num <= 0)
                    return false;
                sql = "update Desk set Dstatus = '正在使用' where Did = " + New_Did + ";";
                cmd.CommandText = sql;
                num = (int)cmd.ExecuteNonQuery();
                //根据原餐桌号，查询订餐记录
                sql = "select * from Record where Did = " + Old_Did + ";";
                cmd.CommandText = sql;
                reader = cmd.ExecuteReader();
                int Rid = 0;
                if (reader.Read())
                {
                    Rid = reader.GetInt32(reader.GetOrdinal("Rid"));
                    reader.Close();
                }
                else
                {
                    MessageBox.Show("fuck you man " + Old_Did.ToString());
                    return false;
                }
                //更新订餐记录
                sql = "update Record set Did = " + New_Did + " where Rid = " + Rid + ";";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                conn.Dispose();
                conn.Close();
            }
        }
        //用餐完成，更新桌子状态，更新记录
        public bool Update_Complete(string Rid)
        {

            String connectionstring = "Data Source=OLIVER_BAO;Initial Catalog=Restaurant_Booking_System;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionstring);
            conn.Open();
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string sql = "select * from Record where Rid = " + Rid + " and Rstatus = '正在使用';";
            //MessageBox.Show(sql);
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();
                
            try
            {
                if (reader.Read())
                {
                    int Did = reader.GetInt32(reader.GetOrdinal("Did"));
                    reader.Close();
                    //更新桌子
                    sql = "update Desk set Dstatus = '可用' where Did = " + Did + ";";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    //更新订单为完成状态
                    sql = "update Record set Rstatus = '用餐完毕' where Rid = " + Rid + ";";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    return true;
                }
                return false;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                reader.Dispose();
                cmd.Dispose();
                conn.Dispose();
                conn.Close();
            }
        }
        //增加预定记录
        public int Add_Record(string name, string phone, string time, string date,string number)
        {
            String connectionstring = "Data Source=OLIVER_BAO;Initial Catalog=Restaurant_Booking_System;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionstring);
            conn.Open();
            string sql = "select * from Desk where Dstatus = '可用' and Ddate = '" + date + "' and Dcapacity >= " + number + ";";
            //MessageBox.Show(name + phone + number + date + time);
            //MessageBox.Show(sql);
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            try
            {
                if (reader.Read())
                {//有空闲的桌子，则增加预定记录
                    int Did = reader.GetInt32(reader.GetOrdinal("Did"));
                    int Dnumber = reader.GetInt32(reader.GetOrdinal("Dnumber"));
                    reader.Close();
                    //插入消费者信息
                    sql = "insert into Customer(Cname, Cphone, Cnumber) values('" + name + "','" + phone + "'," + number + ");";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    sql = "select top 1 * from Customer order by Cid desc;";
                    cmd.CommandText = sql;
                    reader = cmd.ExecuteReader();
                    int Cid = 1003;
                    if (reader.Read())
                    {
                        Cid = reader.GetInt32(reader.GetOrdinal("Cid"));
                        //MessageBox.Show("查询消费者成功");
                        reader.Close();
                        //更新餐桌状态
                        sql = "update Desk set Dstatus = '预定' where Did = " + Did + ";";
                        //MessageBox.Show(sql);
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        //增加一条预定记录
                        sql = "insert into Record(Did, Cid, Rdate, Rtime, Rstatus) values(" + Did + "," + Cid + ",'" + date + "','" + time + "', '预定');";
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        sql = "select top 1 * from Record order by Rid desc;";
                        cmd.CommandText = sql;
                        int Rid = (int)cmd.ExecuteScalar();
                        return Rid;
                    }
                    return 0;
                }
                return 0;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                conn.Dispose();
                conn.Close();
            }
        }
        //  取消预订记录
        public bool Cancel_Record(string Rid)
        {
            String connectionstring = "Data Source=OLIVER_BAO;Initial Catalog=Restaurant_Booking_System;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionstring);
            conn.Open();
            string sql = "select * from Record where Rstatus = '预定' and Rid = " + Rid + ";";
            //MessageBox.Show(sql);
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            try
            {
                if (reader.Read())
                {
                    int Did = reader.GetInt32(reader.GetOrdinal("Did"));
                    int Cid = reader.GetInt32(reader.GetOrdinal("Cid"));
                    //string date = Convert.ToString(reader.GetDateTime(reader.GetOrdinal("Rdate")));
                    reader.Close();
                    //string d = reader.GetDateTime
                    sql = "update Desk set Dstatus = '可用' where Did = " + Did + ";";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    sql = "delete from Customer where Cid = " + Cid + ";";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    sql = "delete from Record where Rid = " + Rid + ";";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    return true;
                }
                return false;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                reader.Close();
                cmd.Dispose();
                conn.Dispose();
                conn.Close();
            }
        }
        //查询餐桌
        public DataSet Query_Desk(string date)
        {
            String connectionstring = "Data Source=OLIVER_BAO;Initial Catalog=Restaurant_Booking_System;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionstring);
            conn.Open();
            string sql;
            if (date.Length != 0)
                sql = "select Dnumber 餐桌号, Ddate 日期, Dcapacity 可容纳人数,Dstatus 状态 from Desk where Ddate = '" + date + "';";
            else
                sql = "select Dnumber 餐桌号, Ddate 日期, Dcapacity 可容纳人数,Dstatus 状态 from Desk";
            //MessageBox.Show(sql);
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                da.SelectCommand = cmd;
                //DataSet ds = DBA.Query();
                DataSet ds = new DataSet();
                da.Fill(ds, "Desk");
                return ds;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                da.Dispose();
                cmd.Dispose();
                conn.Dispose();
                conn.Close();
            }
        }
    }
}
