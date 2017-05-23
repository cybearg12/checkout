using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Store.Checkout;

namespace Store.Test
{    
    public class CheckoutTest
    {   
        [Theory]            
        public void Checkout_Should_Return_Total_Price()
        {
            ICheckout checkout = new CheckoutController();
            checkout.Scan("A");
            checkout.Scan("B");
            checkout.Scan("A");

            decimal result = checkout.GetTotalPrice();
            result.Should().NotBe(null);
        }
    }
}
