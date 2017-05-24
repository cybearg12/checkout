﻿using Store.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Checkout
{
    public class CheckoutController : ICheckout
    {
        private readonly List<ItemPile> _basket = new List<ItemPile>();
        private readonly IUnitRepository _unitRepository;

        public CheckoutController(IUnitRepository unitRepository)
        {
            _unitRepository = unitRepository;
        }

        public void Scan(string item)
        {
            if (string.IsNullOrWhiteSpace(item))
                throw new ArgumentNullException(nameof(item)); //if this was a web api controller it should return a Bad Request

            StockKeepingUnit unit = _unitRepository.GetByName(item);
            if (unit == null)
                throw new Exception($"Item {item} does not exist in stock.");

            ItemPile existingPile = _basket.FirstOrDefault(p => p.Unit == unit);

            if (existingPile != null)
            {
                existingPile.AddToPile(1);
            }
            else
            {
                _basket.Add(new ItemPile(unit, 1));
            }
        }

        public decimal GetTotalPrice()
        {
            //if (_basket.Count == 0)
            //    return 0;
            throw new NotImplementedException();

        }
    }
}
