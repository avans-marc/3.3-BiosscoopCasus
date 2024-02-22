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
    public class OrderReservedStateTests
    {


        [TestMethod]
        public void Cancel_SetsOrderCancelled_WhenTriggered()
        {
            // Arrange
            var orderContext = new Mock<IOrderContext>();
            var orderState = new OrderReservedState(orderContext.Object);

            // Act
            orderState.Cancel();

            // Assert
            orderContext.Verify(x => x.SetState(It.IsAny<OrderCancelledState>()));
        }

        [TestMethod]
        public void CheckPayment_SetsOrderPaid_WhenPaid()
        {
            // Arrange
            var orderContext = new Mock<IOrderContext>();
            var orderState = new OrderReservedState(orderContext.Object);

            // Act
            orderState.CheckPayment(true);

            // Assert
            orderContext.Verify(x => x.SetState(It.IsAny<OrderPaidState>()));
        }

        [TestMethod]
        public void CheckPayment_SetsOrderProvisioned_WhenNotPaid()
        {
            // Arrange
            var orderContext = new Mock<IOrderContext>();
            var orderState = new OrderReservedState(orderContext.Object);

            // Act
            orderState.CheckPayment(false);

            // Assert
            orderContext.Verify(x => x.SetState(It.IsAny<OrderProvisionedState>()));
        }



        [TestMethod]
        public void Change_IsPerformed_WhenTriggered()
        {
            // Arrange
            var orderContext = new Mock<IOrderContext>();
            var orderState = new OrderReservedState(orderContext.Object);

            // Act
            orderState.Change();

            // Assert
            Assert.IsTrue(true); // Nothing to assert
        }
    }
}
