namespace Scoreboard;

public class Scoreboard
{
    private IList<Match> _matches = new List<Match>();
    
    public void StartMatch(string homeTeam, string awayTeam)
    {
        _matches.Add(new Match(homeTeam, awayTeam));
    }

    public override string ToString()
    {
        return _matches[0].ToString();
    }
}

public class Match(string homeTeam, string awayTeam)
{
    public string HomeTeam { get; } = homeTeam;

    public string AwayTeam { get;} = awayTeam;

    public int HomeTeamScore { get; set; }

    public int AwayTeamScore { get; set; }

    public override string ToString()
    {
        return $"{HomeTeam} {HomeTeamScore} - {AwayTeam} {AwayTeamScore}";
    }
}