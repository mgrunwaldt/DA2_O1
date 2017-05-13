using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Statuses_And_Roles
{
    public static class OrderStatuses
    {
        public static int WAITING_FOR_ADDRESS = 1;
        public static int WAITING_FOR_DELIVERY = 2;
        public static int ON_ITS_WAY = 3;
        public static int PAYED = 4;
        public static int FINALIZED = 5;
        public static int CANCELLED_BY_USER = 6;
        public static int CANCELLED_BY_STORE = 7;
    }
}
