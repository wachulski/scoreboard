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
}