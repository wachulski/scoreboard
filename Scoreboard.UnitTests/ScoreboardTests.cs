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
        
        scoreboard.StartMatch("Home Team", "Away Team");
        scoreboard.UpdateMatch(2, 3);
        
        scoreboard.ToString().Should().Be("Home Team 2 - Away Team 3");
    }
}