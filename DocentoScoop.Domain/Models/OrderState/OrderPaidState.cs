using DocentoScoop.Domain.Interfaces;

namespace DocentoScoop.Domain.Models.OrderState
{
    public class OrderPaidState : IOrderState
    {
        private readonly IOrderContext _context;

        public OrderPaidState(IOrderContext context)
        {
            _context = context;
        }

        public void Cancel() => throw new InvalidOperationException("Order paid, cannot cancel");


        public void Change() => throw new InvalidOperationException("Order paid, cannot change");


        public void CheckPayment(bool paid) => throw new InvalidOperationException("Order paid, cannot process payment");
  

        public void SendTickets()
        {
            // Send some tickets and then...
            _context.SetState(new OrderProcessedState());
        }


        public void Submit() => throw new InvalidOperationException("Order paid, cannot submit");

        public bool IsNotifyable() => true;

    }
}
