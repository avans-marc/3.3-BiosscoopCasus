using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocentoScoop.Domain.Interfaces;

namespace DocentoScoop.Domain.Models.OrderState
{
    public class OrderReservedState : IOrderState
    {
        private readonly IOrderContext _context;

        public OrderReservedState(IOrderContext context)
        {
            _context = context;
        }

        public void Cancel() => _context.SetState(new OrderCancelledState());


        public void Change()
        {
            // Change order, maybe calculate a new price
            // we could set a new state, but we're already in this state so 🤷‍
        }


        public void CheckPayment(bool paid)
        {
            this._context.SetState(paid ? new OrderPaidState(_context) : new OrderProvisionedState(_context));
        } 




        public void Submit() => throw new InvalidOperationException("Order reserved, cannot submit");

        public void SendTickets() => throw new InvalidOperationException("Order reserved, cannot send tickets unless order is paid");

        public bool IsNotifyable() => false;

    }
}
