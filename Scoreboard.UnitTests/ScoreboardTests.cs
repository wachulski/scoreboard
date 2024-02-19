using FluentAssertions;

namespace Scoreboard.UnitTests;

public class ScoreboardTests
{
    private readonly Scoreboard _sut = new ();

    [Fact]
    public void StartMatch_WhenTwoTeamsProvided_ShouldAddTheMatchToTheBoard()
    {
        _sut.StartMatch("Home Team", "Away Team");

        _sut.ToString().Should().Be("Home Team 0 - Away Team 0");
    }

    [Fact]
    public void UpdateMatch_WhenNewScorePairProvided_ShouldReplaceTheScoreWithNewScore()
    {
        var matchId = _sut.StartMatch("Home Team", "Away Team");
        
        _sut.UpdateMatch(matchId, 2, 3);
        
        _sut.ToString().Should().Be("Home Team 2 - Away Team 3");
    }
    
    [Fact]
    public void UpdateMatch_WhenOtherMatchesOnBoard_ShouldReplaceTheOneReferencedByTheCaller()
    {
        _sut.StartMatch("Blues", "Greens");
        var matchId = _sut.StartMatch("Reds", "Yellows");
        
        _sut.UpdateMatch(matchId, 2, 3);
        
        _sut.ToString().Should().Contain("Reds 2 - Yellows 3");
    }

    [Fact]
    public void FinishMatch_WhenOnBoard_ShouldRemoveFromTheBoard()
    {
        var matchId = _sut.StartMatch("Reds", "Yellows");

        _sut.FinishMatch(matchId);
        
        _sut.ToString().Should().BeEmpty();
    }
}