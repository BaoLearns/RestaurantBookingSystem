using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Booking_System
{
    class Customer
    {
        //消费者编号
        int Cid { set; get; }
        //消费者姓名
        string Cname { set; get; }
        //消费者电话
        string Cphone { set; get; }
        //实际人数
        int Cnumber { set; get; }

        //构造函数
        public Customer(int id, string name, string phone, int number)
        {
            Cid = id;
            Cname = name;
            Cphone = phone;
            Cnumber = number;
        }
    }
}
