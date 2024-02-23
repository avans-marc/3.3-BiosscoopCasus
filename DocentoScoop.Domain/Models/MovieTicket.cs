using System.Net.Sockets;

namespace DocentoScoop.Domain.Models;

public class MovieTicket
{
    private readonly int rowNr;
    private readonly int seatNr;
    private readonly bool isPremium;
    private readonly MovieScreening movieScreening;

    public MovieTicket(MovieScreening movieScreening, int rowNr, int seatNr, bool isPremium)
    {
        this.movieScreening = movieScreening;
        this.rowNr = rowNr;
        this.seatNr = seatNr;
        this.isPremium = isPremium;
    }

    public bool IsPremiumTicket() => isPremium;

    public decimal GetPrice() => movieScreening.getPricePerSeat();

    public int GetSeatNr() => this.seatNr;

    public int GetRowNr() => this.rowNr;

    public DateTime GetScreeningDate() => movieScreening.getDate();

    public bool IsWeekendScreening() => this.GetScreeningDate().DayOfWeek is DayOfWeek.Sunday or DayOfWeek.Saturday;

    public override string ToString() => $"{this.movieScreening.ToString()}: {GetRowNr()}{GetSeatNr()}";

}
