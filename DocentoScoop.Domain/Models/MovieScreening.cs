namespace DocentoScoop.Domain.Models;

public class MovieScreening
{
    private readonly DateTime dateAndTime;
    private readonly decimal pricePerSeat;
    private readonly Movie movie;

    public MovieScreening(Movie movie,DateTime dateAndTime, decimal pricePerSeat)
    {
        this.dateAndTime = dateAndTime;
        this.pricePerSeat = pricePerSeat;
        this.movie = movie;
    }

    public DateTime getDate() => dateAndTime;

    public decimal getPricePerSeat() => pricePerSeat;

    public override string ToString() => $"{movie} | {getDate()} {getPricePerSeat()} | ";
}


