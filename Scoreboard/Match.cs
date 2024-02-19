namespace Scoreboard;

public class Match(int id, string homeTeam, string awayTeam)
{
    public int Id { get; } = id;
    
    public string HomeTeam { get; } = homeTeam;

    public string AwayTeam { get;} = awayTeam;

    public int HomeTeamScore { get; internal set; }

    public int AwayTeamScore { get; internal set; }

    public int TotalScore => HomeTeamScore + AwayTeamScore;

    public DateTime Started { get; internal init; }

    public override string ToString()
    {
        return $"{HomeTeam} {HomeTeamScore} - {AwayTeam} {AwayTeamScore}";
    }
}