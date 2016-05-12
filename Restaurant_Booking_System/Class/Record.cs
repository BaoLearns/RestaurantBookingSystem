using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Booking_System
{
    class Record
    {
        //记录id
        int Rid { set; get; }
        //桌子id
        int Tid { set; get; }
        //消费者id
        int Cid { set; get; }
        //订餐日期
        string date{ set; get; }
        //订餐时间
        string time { set; get; }
    }
}
