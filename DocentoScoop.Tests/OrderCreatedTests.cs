﻿using DocentoScoop.Domain.Models.OrderState;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocentoScoop.Tests
{
    [TestClass]
    public class OrderCreatedTests
    {


        [TestMethod]
        public void Cancel_ThrowsException_WhenTriggered()
        {
            // Arrange
            var orderContext = new Mock<IOrderContext>();
            var orderState = new OrderCreated(orderContext.Object);

            // Act

            // Assert
            Assert.ThrowsException<InvalidOperationException>(orderState.Cancel);

        }


        [TestMethod]
        public void Submitted_SetsOrderReserved_WhenTriggered()
        {
            // Arrange
            var orderContext = new Mock<IOrderContext>();
            var orderState = new OrderCreated(orderContext.Object);

            // Act
            orderState.Submit();

            // Assert
            orderContext.Verify(x => x.SetState(It.IsAny<OrderReserved>()));
        }
    }
}
