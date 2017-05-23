using Store.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Checkout
{
    public class CheckoutController : ICheckout
    {
        private readonly List<StockKeepingUnit> _basket;
        private readonly IUnitRepository _unitRepository;

        public CheckoutController(IUnitRepository unitRepository)
        {
            _unitRepository = unitRepository;
        }

        public void Scan(string item)
        {
            throw new NotImplementedException();
        }

        public decimal GetTotalPrice()
        {
            throw new NotImplementedException();
        }
    }
}
