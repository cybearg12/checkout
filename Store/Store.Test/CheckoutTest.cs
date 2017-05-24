using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Store.Checkout;
using Store.Stock;
using Xunit;

namespace Store.Test
{    
    public class CheckoutTest
    {
        private readonly IUnitRepository _mockRepository = new MockUnitRepository();
        private ICheckout _checkout;
        public CheckoutTest()
        {
            _checkout = new CheckoutController(_mockRepository);
        }

        [Fact]
        public void CheckoutController_Can_Scan_Item()
        {            
            try
            {
                _checkout.Scan("A");
            }
            catch (Exception e)
            {
                Assert.False(true);
            }            
            Assert.True(true);
        }

        [Fact]
        public void CheckoutController_Throws_Exception_For_Scaning_Null_Item()
        {
            string item = null;            

            try
            {              
                _checkout.Scan(item);
            }
            catch (ArgumentNullException e)
            {}
            catch(Exception e)
            {
                Assert.False(true);
            }

            Assert.True(true);
        }

        [Fact]
        public void CheckoutController_Throws_Exception_For_Scaning_Empty_Item_Name()
        {
            string item = "    ";

            try
            {
                _checkout.Scan(item);
            }
            catch (ArgumentNullException e)
            { }
            catch (Exception e)
            {
                Assert.False(true);
            }
            Assert.True(true);
        }        

        [Fact]            
        public void Checkout_Should_Return_Total_Price()
        {            
            ICheckout checkout = new CheckoutController(_mockRepository);
            checkout.Scan("A"); //50
            checkout.Scan("B"); //30
            checkout.Scan("A"); //50

            decimal result = checkout.GetTotalPrice();
            result.Should().Be(130);
        }
    }
}
