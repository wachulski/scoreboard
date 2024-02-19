using FluentAssertions;

namespace Scoreboard.UnitTests;

public class ScoreboardTests
{
    [Fact]
    public void StartMatch_WhenTwoTeamsProvided_ShouldAddTheMatchToTheBoard()
    {
        var scoreboard = new Scoreboard();

        scoreboard.StartMatch("Home Team", "Away Team");

        scoreboard.ToString().Should().Be("Home Team 0 - Away Team 0");
    }

    [Fact]
    public void UpdateMatch_WhenNewScorePairProvided_ShouldReplaceTheScoreWithNewScore()
    {
        var scoreboard = new Scoreboard();
        var matchId = scoreboard.StartMatch("Home Team", "Away Team");
        
        scoreboard.UpdateMatch(matchId, 2, 3);
        
        scoreboard.ToString().Should().Be("Home Team 2 - Away Team 3");
    }
    
    [Fact]
    public void UpdateMatch_WhenOtherMatchesOnBoard_ShouldReplaceTheOneReferencedByTheCaller()
    {
        var scoreboard = new Scoreboard();
        scoreboard.StartMatch("Blues", "Greens");
        var matchId = scoreboard.StartMatch("Reds", "Yellows");
        
        scoreboard.UpdateMatch(matchId, 2, 3);
        
        scoreboard.ToString().Should().Contain("Reds 2 - Yellows 3");
    }

    [Fact]
    public void FinishMatch_WhenOnBoard_ShouldRemoveFromTheBoard()
    {
        var scoreboard = new Scoreboard();
        var matchId = scoreboard.StartMatch("Reds", "Yellows");

        scoreboard.FinishMatch(matchId);
        
        scoreboard.ToString().Should().BeEmpty();
    }
}