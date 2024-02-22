using DocentoScoop.Domain.Interfaces;
using DocentoScoop.Domain.Models;
using DocentoScoop.Domain.Models.OrderState;
using DocentoScoop.Domain.Services;
using Moq;

namespace DocentoScoop.Tests
{
    [TestClass]
    public class OrderNotificationServiceTests
    {
        [TestMethod]
        public void Update_InvokesExternCommunicationService_OrderStateChangedToNotifyableState()
        {
            // Arrange
            var notifyableState = CreateFakeOrderState(isNotifyable: true);
            var emailService = CreateMockExternalCommunicationService(ContactMethod.Email);
            var order = FakeOrderFactory.CreateFakeOrder(10, 10, false, true, true);
            order.SetPreferredContactMethod(ContactMethod.Email);

            var orderNotificationService = new OrderNotificationService(new() { emailService.Object });
            orderNotificationService.Attach(order);

            // Act
            order.SetState(notifyableState);

            // Assert
            emailService.Verify(x => x.SendMessage(), Times.Once);
        }



        [TestMethod]
        public void Update_InvokesOnlyOneCommunicationService_ForPreferredContactMethod()
        {
            // Arrange
            var notifyableState = CreateFakeOrderState(isNotifyable: true);
            var emailService = CreateMockExternalCommunicationService(ContactMethod.Email);
            var smsService = CreateMockExternalCommunicationService(ContactMethod.Sms);
            var order = FakeOrderFactory.CreateFakeOrder(10, 10, false, true, true);
            order.SetPreferredContactMethod(ContactMethod.Sms);

            var orderNotificationService = new OrderNotificationService(new() { emailService.Object, smsService.Object });
            orderNotificationService.Attach(order);

            // Act
            order.SetState(notifyableState);

            // Assert
            smsService.Verify(x => x.SendMessage(), Times.Once);
            emailService.Verify(x => x.SendMessage(), Times.Never);
        }


        [TestMethod]
        public void Update_InvokesNoExternalCommunicationService_ForOrderInUnnotifyableState()
        {
            // Arrange
            var notifyableState = CreateFakeOrderState(isNotifyable: false);
            var emailService = CreateMockExternalCommunicationService(ContactMethod.Email);
            var order = FakeOrderFactory.CreateFakeOrder(10, 10, false, true, true);
            order.SetPreferredContactMethod(ContactMethod.Email);

            var orderNotificationService = new OrderNotificationService(new() { emailService.Object});
            orderNotificationService.Attach(order);

            // Act
            order.SetState(notifyableState);

            // Assert
            emailService.Verify(x => x.SendMessage(), Times.Never);
        }

        private static Mock<IExternalNotificationService> CreateMockExternalCommunicationService(ContactMethod method)
        {
            var mailService = new Mock<IExternalNotificationService>();
            mailService.Setup(x => x.ContactMethod).Returns(method);
            return mailService;
        }

        private static IOrderState CreateFakeOrderState(bool isNotifyable)
        {
            var notifyableState = new Mock<IOrderState>();
            notifyableState.Setup(x => x.IsNotifyable()).Returns(isNotifyable);
            return notifyableState.Object;
        }
    }
}