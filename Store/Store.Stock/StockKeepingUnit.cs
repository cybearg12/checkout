using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Stock
{
    public class StockKeepingUnit
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public StockKeepingUnit(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public override bool Equals(object obj)
        {
            StockKeepingUnit other = obj as StockKeepingUnit;

            if (ReferenceEquals(other, null))
            {
                return false;
            }

            if (!other.Name.Equals(Name, StringComparison.CurrentCultureIgnoreCase))
                return false;

            if (other.Price != Price)
                return false;

            return true;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ Price.GetHashCode();
        }

        /// <summary>
        /// Overrides the == operator in order to use the Equals method
        /// </summary>
        public static bool operator ==(StockKeepingUnit a, StockKeepingUnit b)
        {
            //null equals null so return true
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            {
                return true;
            }

            //if only one of them is null, they are not equal
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(StockKeepingUnit a, StockKeepingUnit b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return $"{Name}:{Price}";
        }
    }
}
