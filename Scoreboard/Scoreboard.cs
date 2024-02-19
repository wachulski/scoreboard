namespace Scoreboard;

public class Scoreboard
{
    private readonly IList<Match> _matches = new List<Match>();
    
    public int StartMatch(string homeTeam, string awayTeam)
    {
        _matches.Add(new Match(0, homeTeam, awayTeam));
        return 0;
    }

    public void UpdateMatch(int matchId, int homeTeamScore, int awayTeamScore)
    {
        var existing = _matches.SingleOrDefault(match => matchId == match.Id);

        existing.HomeTeamScore = homeTeamScore;
        existing.AwayTeamScore = awayTeamScore;
    }

    public override string ToString()
    {
        return _matches[0].ToString();
    }
}