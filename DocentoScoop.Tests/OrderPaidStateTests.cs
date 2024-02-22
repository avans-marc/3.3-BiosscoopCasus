using DocentoScoop.Domain.Interfaces;
using DocentoScoop.Domain.Models.OrderState;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocentoScoop.Tests
{
    [TestClass]
    public class OrderPaidStateTests
    {


        [TestMethod]
        public void Change_ThrowsException_WhenTriggered()
        {
            // Arrange
            var orderContext = new Mock<IOrderContext>();
            var orderState = new OrderPaidState(orderContext.Object);

            // Act

            // Assert
            Assert.ThrowsException<InvalidOperationException>(orderState.Change);

        }


        [TestMethod]
        public void SendTickets_IsPerformed_WhenTriggered()
        {
            // Arrange
            var orderContext = new Mock<IOrderContext>();
            var orderState = new OrderPaidState(orderContext.Object);

            // Act
            orderState.SendTickets();

            // Assert
            Assert.IsTrue(true); // Nothing to assert
        }
    }
}
