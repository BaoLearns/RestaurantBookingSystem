using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Booking_System
{
    class Table
    {
        //桌子id
        int Tid { set; get; }
        //容量
        int Tcapacity { set; get; }
        //状态：free used
        string Tstatus { set; get; }
    }
}
