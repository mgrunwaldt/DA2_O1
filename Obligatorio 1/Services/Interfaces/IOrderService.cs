using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Services.Interfaces
{
    public interface IOrderService
    {
        Order GetActiveOrderFromUser(User u);
    }
}