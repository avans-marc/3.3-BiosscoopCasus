using DocentoScoop.Domain.Interfaces;
using DocentoScoop.Domain.Models.OrderState;
using DocentoScoop.Domain.Rules;

namespace DocentoScoop.Domain.Models;

/// <summary>
/// This class has become a design pattern zoo. Don't mind the mess and the noise, just enjoy watching the patterns.
/// </summary>
public class Order : IOrderContext, Interfaces.IObservable<IOrderContext>
{
    private readonly int _orderNr;
    private readonly bool _isStudentOrder;

    private readonly List<MovieTicket> _tickets = new List<MovieTicket>();
    private readonly IEnumerable<ITicketPriceRule> _ticketPriceRules = new List<ITicketPriceRule>();
    private readonly List<Interfaces.IObserver<IOrderContext>> _observers = new List<Interfaces.IObserver<IOrderContext>>();

    private IOrderState _currentState;
    private ContactMethod _contactMethod;
    private IOrderExporter? _orderExporter;

    public Order(int orderNr, bool isStudentOrder, IEnumerable<ITicketPriceRule> ticketPriceRules)
    {
        // Parameters
        this._orderNr = orderNr;
        this._isStudentOrder = isStudentOrder;
        this._ticketPriceRules = ticketPriceRules;

        // Defaults
        this._currentState = new OrderCreatedState(this);
        this._contactMethod = ContactMethod.Email;
    }

    #region Setters/Getters

    public int GetOrderNr() => _orderNr;

    public bool IsStudentOrder() => _isStudentOrder;

    public int GetTicketCount() => _tickets.Count;

    public ContactMethod GetPreferredContactMethod() => _contactMethod;

    public void SetPreferredContactMethod(ContactMethod preferredContactMethod) => _contactMethod = preferredContactMethod;

    public void AddSeatReservation(MovieTicket ticket) => _tickets.Add(ticket);

    public IEnumerable<MovieTicket> GetTickets() => this._tickets;

    public DateTime GetScreeningDate() => this._tickets.Select(x => x.GetScreeningDate()).OrderBy(x => x).First();

    #endregion

    #region Strategy Pattern Stuff

    /// <summary>
    /// Classic Strategy Pattern
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    public void PerformOrderExporter()
    {
        if (this._orderExporter == null)
            throw new InvalidOperationException($"No order export 'behavior' set");

        this._orderExporter.Export(this);
    }

    public void SetOrderExporter(IOrderExporter orderExporter) => _orderExporter = orderExporter;

    /// <summary>
    /// Strategy deluxe++, not letting the outside world
    /// decide what strategy to use, but instead create open/close-proof price calculation rules
    /// </summary>
    /// <returns></returns>
    public decimal CalculatePrice()
    {
        decimal total = decimal.Zero;

        for (int i = 0; i < _tickets.Count; i++)
        {
            var ticket = _tickets[i];
            var ticketPrice = ticket.GetPrice();

            foreach (var pricingRule in this._ticketPriceRules)
                if (ticketPrice > decimal.Zero)
                    ticketPrice = pricingRule.CalculateNewPrice(ticketPrice, i + 1, ticket, this);

            total += ticketPrice;
        }

        return total;
    }

    #endregion Strategy Pattern Stuff

    #region State Pattern Stuff

    /// <summary>
    /// The order doesn't care what state it is in.
    /// Behavior and transitions are managed within the states
    /// </summary>
    /// <param name="state"></param>
    public void SetState(IOrderState state)
    {
        this._currentState = state;
        this.NotifyObservers();
    }

    public IOrderState GetState() => _currentState;

    public void Submit() => this._currentState!.Submit();

    public void Change(/* some params here */) => this._currentState!.Change(/* some params here */);

    #endregion

    #region Observable Pattern Stuff

    /// <summary>
    /// This is pretty generic solution, we also could wire it to other changes within the order
    /// but for now we only stick to state changes.
    /// </summary>
    /// <param name="observer"></param>
    public void Register(Interfaces.IObserver<IOrderContext> observer) => this._observers.Add(observer);

    public void NotifyObservers()
    {
        foreach (var observer in _observers)
            observer.Update(this);
    }



    #endregion

}