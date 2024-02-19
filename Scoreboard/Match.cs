namespace Scoreboard;

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