using Store.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Test
{
    public class MockUnitRepository : IUnitRepository
    {
        private List<StockKeepingUnit> _mockUnits = new List<StockKeepingUnit>();

        public MockUnitRepository()
        {
            //initialize with mock data;
            //the repository could be extended to allow data to be added by the unit test

            _mockUnits.Add(new StockKeepingUnit("A", 50));
            _mockUnits.Add(new StockKeepingUnit("B", 30));
            _mockUnits.Add(new StockKeepingUnit("C", 20));
            _mockUnits.Add(new StockKeepingUnit("D", 15));
        }

        public StockKeepingUnit GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            return _mockUnits.FirstOrDefault(u => u.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
