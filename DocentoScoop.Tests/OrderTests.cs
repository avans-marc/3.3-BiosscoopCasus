using DocentoScoop.Domain.Exports;
using DocentoScoop.Domain.Interfaces;
using DocentoScoop.Domain.Models;
using DocentoScoop.Domain.Rules;
using DocentoScoop.Domain.Tools;
using Moq;

namespace DocentoScoop.Tests
{
    [TestClass]
    public class OrderTests
    {
        [TestMethod]
        public void PerformOrderExport_ShouldInvokeOrderExporter_WhenCalleds()
        {
            // Arrange
            Mock<IOrderExporter> orderExporter = new Mock<IOrderExporter>();
            Order order = FakeOrderFactory.CreateFakeOrder(6, 10M, false, false, true);

            // Act
            order.SetOrderExporter(orderExporter.Object);
            order.PerformOrderExporter();

            // Assert
            orderExporter.Verify(x => x.Export(order), Times.Once);
        }

        [TestMethod]
        public void PerformOrderExport_ShouldExportJson_WithoutErrors()
        {
            // Arrange
            Order order = FakeOrderFactory.CreateFakeOrder(6, 10M, false, false, true);

            // Act
            order.SetOrderExporter(new OrderJsonExporter());
            order.PerformOrderExporter();

            // Assert
            Assert.IsTrue(true); // Nothing to assert
        }


        [TestMethod]
        public void PerformOrderExport_ShouldExportPlainText_WithoutErrors()
        {
            // Arrange
            Order order = FakeOrderFactory.CreateFakeOrder(6, 10M, false, false, true);

            // Act
            order.SetOrderExporter(new OrderPlainTextExporter());
            order.PerformOrderExporter();

            // Assert
            Assert.IsTrue(true); // Nothing to assert
        }

        [TestMethod]
        public void PerformOrderExport_ShouldThrowException_WhenNoExporterSet()
        {
            // Arrange
            Order order = FakeOrderFactory.CreateFakeOrder(6, 10M, false, false, true);

            // Act

            // Assert
            Assert.ThrowsException<InvalidOperationException>(order.PerformOrderExporter);
        }

        [TestMethod]
        public void CalculatePrice_ShouldApplyDiscount_ForNonStudentOrderInTheWeekendOver6Tickets()
        {
            // Arrange
            Order order = FakeOrderFactory.CreateFakeOrder(6, 10M, false, false, true);

            // Act
            var price = order.CalculatePrice();

            // Assert
            Assert.IsTrue(price == 54M);
        }

        [TestMethod]
        public void CalculatePrice_ShouldReturnFree_ForEverySecondPremiumStudentTicketInTheWeekends()
        {
            // Arrange
            Order order = FakeOrderFactory.CreateFakeOrder(6, 10M, true, true, true);

            // Act
            var price = order.CalculatePrice();

            // Assert
            Assert.IsTrue(price == 36M);
        }

        [TestMethod]
        public void CalculatePrice_ShouldNotAddPremiumFee_ForNonPremiumTickets()
        {
            // Arrange
            Order order = FakeOrderFactory.CreateFakeOrder(2, 10M, false, false, true);

            // Act
            var price = order.CalculatePrice();

            // Assert
            Assert.IsTrue(price == 20M);
        }

        [TestMethod]
        public void CalculatePrice_ShouldAddPremiumFee_ForNonStudentOrders()
        {
            // Arrange
            Order order = FakeOrderFactory.CreateFakeOrder(2, 10M, true, false, false);

            // Act
            var price = order.CalculatePrice();

            // Assert
            Assert.IsTrue(price == 13M);
        }
    }
}