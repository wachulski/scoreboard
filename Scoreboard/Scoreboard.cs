namespace Scoreboard;

public class Scoreboard
{
    private readonly IList<Match> _matches = new List<Match>();
    
    public int StartMatch(string homeTeam, string awayTeam)
    {
        _matches.Add(new Match(homeTeam, awayTeam));
        return 0;
    }

    public void UpdateMatch(int matchId, int homeTeamScore, int awayTeamScore)
    {
        
    }

    public override string ToString()
    {
        return _matches[0].ToString();
    }
}