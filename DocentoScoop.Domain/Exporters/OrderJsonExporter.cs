using DocentoScoop.Domain.Interfaces;
using DocentoScoop.Domain.Models;
using System.Text.Json.Nodes;

namespace DocentoScoop.Domain.Exports
{
    public class OrderJsonExporter : IOrderExporter
    {
        public void Export(Order order)
        {
            JsonObject jsonOrder = new JsonObject
            {
                { "orderNr", order.GetOrderNr() },
                { "isStudentOrder", order.IsStudentOrder() },
                { "totalPrice", order.CalculatePrice() }
            };

            JsonArray jsonTickets = new JsonArray();
            foreach (MovieTicket ticket in order.GetTickets())
            {
                JsonObject jsonTicket = new JsonObject
                {
                    { "screeningDate", ticket.GetScreeningDate() },
                    { "isPremiumTicket", ticket.IsPremiumTicket() },
                    { "price", ticket.GetPrice() },
                };
                jsonTickets.Add(jsonTicket);
            }
            jsonOrder.Add("tickets", jsonTickets);

            Console.WriteLine(jsonOrder.ToString());

      
        }

    }
}