namespace Scoreboard;

public class Match(int id, string homeTeam, string awayTeam)
{
    public int Id { get; } = id;
    
    public string HomeTeam { get; } = homeTeam;

    public string AwayTeam { get;} = awayTeam;

    public int HomeTeamScore { get; set; }

    public int AwayTeamScore { get; set; }

    public override string ToString()
    {
        return $"{HomeTeam} {HomeTeamScore} - {AwayTeam} {AwayTeamScore}";
    }
}