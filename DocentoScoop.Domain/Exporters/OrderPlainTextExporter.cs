using DocentoScoop.Domain.Interfaces;
using DocentoScoop.Domain.Models;
using System.Text;

namespace DocentoScoop.Domain.Exports
{
    public class OrderPlainTextExporter : IOrderExporter
    {
        public void Export(Order order)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Order: {order.GetOrderNr()} | Price: {order.CalculatePrice():C2}");
            foreach (MovieTicket ticket in order.GetTickets())
                sb.AppendLine(ticket.ToString());

            Console.WriteLine(sb.ToString());
        }
    }
}