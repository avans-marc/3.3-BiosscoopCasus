namespace DocentoScoop.Domain.Models;

public class MovieScreening
{
    private readonly DateTime dateAndTime;
    private readonly decimal pricePerSeat;

    public MovieScreening(Movie movie, DateTime dateAndTime, decimal pricePerSeat)
    {
        this.dateAndTime = dateAndTime;
        this.pricePerSeat = pricePerSeat;
    }

    public DateTime getDate() => dateAndTime;

    public decimal getPricePerSeat() => pricePerSeat;

    public string toString() => dateAndTime.ToString() + " " + pricePerSeat;
}


