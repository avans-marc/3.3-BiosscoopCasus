namespace DocentoScoop.Domain.Models;

public class Movie
{
    private readonly string title;
    private readonly List<MovieScreening> screenings = new List<MovieScreening>();

    public Movie(string title)
    {
        this.title = title;
    }

    public void AddScreening(MovieScreening screening) => screenings.Add(screening);

    public override string ToString() => title;
}

