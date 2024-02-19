using System.Text;

namespace Scoreboard;

public class Scoreboard
{
    private int _nextMatchId = 0;
    private readonly IList<Match> _matches = new List<Match>();
    
    public int StartMatch(string homeTeam, string awayTeam)
    {
        var match = new Match(_nextMatchId, homeTeam, awayTeam);
        _matches.Add(match);
        _nextMatchId++;
        
        return match.Id;
    }

    public void UpdateMatch(int matchId, int homeTeamScore, int awayTeamScore)
    {
        var existing = _matches.SingleOrDefault(match => matchId == match.Id);

        existing.HomeTeamScore = homeTeamScore;
        existing.AwayTeamScore = awayTeamScore;
    }

    public void FinishMatch(int matchId)
    {
        _matches.Remove(_matches.Single(match => matchId == match.Id));
    }

    public string GetSummary()
    {
        var ordered = _matches.OrderByDescending(match => match.HomeTeamScore + match.AwayTeamScore);

        return Print(ordered);
    }

    public override string ToString() => Print(_matches);

    private static string Print(IEnumerable<Match> matches)
    {
        var sb = new StringBuilder();
        
        foreach (var match in matches)
        {
            sb.AppendLine(match.ToString());
        }

        return sb.ToString().TrimEnd('\n').TrimEnd('\r');
    }
}