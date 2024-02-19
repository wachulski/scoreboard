namespace Scoreboard;

public class Scoreboard
{
    private readonly IList<Match> _matches = new List<Match>();
    
    public void StartMatch(string homeTeam, string awayTeam)
    {
        _matches.Add(new Match(homeTeam, awayTeam));
    }

    public override string ToString()
    {
        return _matches[0].ToString();
    }
}