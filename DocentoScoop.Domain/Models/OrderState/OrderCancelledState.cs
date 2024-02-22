using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocentoScoop.Domain.Interfaces;

namespace DocentoScoop.Domain.Models.OrderState
{
    public class OrderCancelledState : IOrderState
    {
        public void Cancel() => throw new InvalidOperationException("Order already cancelled");

        public void Change() => throw new InvalidOperationException("Order cancelled, cannot change");
        
        public void CheckPayment(bool paid) => throw new InvalidOperationException("Order cancelled, payment not checked");
        
        public void Submit() => throw new InvalidOperationException("Order cancelled, cannot submit");
        
        public void SendTickets() => throw new InvalidOperationException("Order cancelled, cannot send tickets");

        public bool IsNotifyable() => true;
       
    }
}
