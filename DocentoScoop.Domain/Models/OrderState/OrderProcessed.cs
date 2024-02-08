﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocentoScoop.Domain.Models.OrderState
{
    public class OrderProcessed : IOrderState
    {
        public void Cancel() => throw new InvalidOperationException("Order is processed, your operation is futile");

        public void Change() => throw new InvalidOperationException("Order is processed, your operation is futile");

        public void Pay() => throw new InvalidOperationException("Order is processed, your operation is futile");

        public void CheckPayment(bool paid) => throw new InvalidOperationException("Order is processed, your operation is futile");

        public void SendTickets() => throw new InvalidOperationException("Order is processed, your operation is futile");

        public void Submit() => throw new InvalidOperationException("Order is processed, your operation is futile");
    }
}
