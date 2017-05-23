using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Stock
{
    public interface IUnitRepository
    {
        StockKeepingUnit GetByName(string name);
    }
}
