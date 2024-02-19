using System.Text;

namespace Scoreboard;

public class Scoreboard(TimeProvider? timeProvider = default)
{
    private int _nextMatchId = 0;
    private readonly IList<Match> _matches = new List<Match>();
    private readonly TimeProvider _timeProvider = timeProvider ?? TimeProvider.System;

    public int StartMatch(string homeTeam, string awayTeam)
    {
        var match = new Match(_nextMatchId, homeTeam, awayTeam)
        {
            Started = _timeProvider.GetUtcNow().DateTime
        };
        _matches.Add(match);
        _nextMatchId++;
        
        return match.Id;
    }

    public void UpdateMatch(int matchId, int homeTeamScore, int awayTeamScore)
    {
        EnsureScoreNonNegative(homeTeamScore, nameof(homeTeamScore));
        EnsureScoreNonNegative(awayTeamScore, nameof(awayTeamScore));
        
        var existing = EnsureMatch(matchId);

        existing.HomeTeamScore = homeTeamScore;
        existing.AwayTeamScore = awayTeamScore;
        
        return;

        void EnsureScoreNonNegative(int score, string paramName)
        {
            if (score < 0)
                throw new ArgumentException($"Score {score} cannot be negative", paramName);
        }
    }

    public void FinishMatch(int matchId)
    {
        var existing = EnsureMatch(matchId);
        
        _matches.Remove(existing);
    }

    public string GetSummary()
    {
        var ordered = _matches.OrderByDescending(match => match.TotalScore).ThenByDescending(match => match.Started);

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

    private Match EnsureMatch(int matchId)
    {
        var existing = _matches.SingleOrDefault(match => matchId == match.Id);

        if (existing == null)
            throw new ArgumentException($"Match of given ID ({matchId}) not found", nameof(matchId));
        
        return existing;
    }
}