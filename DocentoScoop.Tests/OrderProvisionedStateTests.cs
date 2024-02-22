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
    public class OrderProvisionedStateTests
    {
        [TestMethod]
        public void Cancel_SetsOrderCancelled_WhenTriggered()
        {
            // Arrange
            var orderContext = new Mock<IOrderContext>();
            var orderState = new OrderProvisionedState(orderContext.Object);

            // Act
            orderState.Cancel();

            // Assert
            orderContext.Verify(x => x.SetState(It.IsAny<OrderCancelledState>()));
        }


        [TestMethod]
        public void ProcessPayment_SetsOrderPaid_WhenPaid()
        {
            // Arrange
            var orderContext = new Mock<IOrderContext>();
            var orderState = new OrderProvisionedState(orderContext.Object);

            // Act
            orderState.CheckPayment(true);

            // Assert
            orderContext.Verify(x => x.SetState(It.IsAny<OrderPaidState>()));
        }


        [TestMethod]
        public void CheckPayment_SetsOrderCancelled_WhenNotPaid()
        {
            // Arrange
            var orderContext = new Mock<IOrderContext>();
            var orderState = new OrderProvisionedState(orderContext.Object);

            // Act
            orderState.CheckPayment(false);

            // Assert
            orderContext.Verify(x => x.SetState(It.IsAny<OrderCancelledState>()));
        }

    }
}
