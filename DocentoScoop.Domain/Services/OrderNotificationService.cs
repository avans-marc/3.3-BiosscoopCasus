using DocentoScoop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocentoScoop.Domain.Services
{
    /// <summary>
    /// Imagine this service subscribing to all orders loaded in memory using the attach method
    /// </summary>
    public class OrderNotificationService : Interfaces.IObserver<IOrderContext>
    {
        private readonly List<IExternalNotificationService> _externalNotificationServices;

        public OrderNotificationService(List<IExternalNotificationService> notificationServices)
        {
            _externalNotificationServices = notificationServices;
        }

        public void Attach(Interfaces.IObservable<IOrderContext> order) => order.Register(this);

        /// <summary>
        /// An update is coming in. This method might needs some refactoring in the future
        /// We assume the state has changed, but if the order starts notifying when other parameters
        /// have changed we start sending out messages as well. 
        /// </summary>
        /// <param name="changedOrder">The changed order</param>
        /// <exception cref="NotSupportedException">The order has a contact method that's currently not supported by the system</exception>
        public void Update(IOrderContext changedOrder)
        {
            var state = changedOrder.GetState();
            var contactMethod = changedOrder.GetPreferredContactMethod();

            if (state.IsNotifyable())
            {
                var service = _externalNotificationServices.SingleOrDefault(x => x.ContactMethod == contactMethod);

                if (service == null)
                    throw new NotSupportedException($"{contactMethod} has no service");

                service.SendMessage();
            }
        }
    }
}