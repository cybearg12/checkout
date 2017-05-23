using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Checkout
{
    public interface ICheckout
    {
        void Scan(string item);
        decimal GetTotalPrice();
    }
}
