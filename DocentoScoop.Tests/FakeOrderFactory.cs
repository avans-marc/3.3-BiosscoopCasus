using DocentoScoop.Domain.Interfaces;
using DocentoScoop.Domain.Models;
using DocentoScoop.Domain.Rules;
using DocentoScoop.Domain.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocentoScoop.Tests
{
    public static class FakeOrderFactory
    {
        public static Order CreateFakeOrder(int numberOfTickets, decimal basePrice, bool isPremium, bool isStudentOrder, bool isWeekend)
        {
            // Move to factory later
            var ticketPriceRules = AssemblyScanner.GetInstancesOfType<ITicketPriceRule>();

            Movie movie = new Movie("The Matrix");

            // Create a non-weekend movie screening
            DateTime date = isWeekend ? new DateTime(2024, 1, 27, 19, 0, 0, DateTimeKind.Local) : new DateTime(2024, 1, 31, 19, 0, 0, DateTimeKind.Local);
            MovieScreening movieScreening = new MovieScreening(movie, date, basePrice);
            Order order = new Order(1, isStudentOrder, ticketPriceRules);
            for (int i = 0; i < numberOfTickets; i++)
                order.AddSeatReservation(new MovieTicket(movieScreening, 1, 1, isPremium));

            return order;
        }
    }
}
