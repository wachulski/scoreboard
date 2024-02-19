using FluentAssertions;
using Microsoft.Extensions.Time.Testing;

namespace Scoreboard.UnitTests;

public class ScoreboardTests
{
    private Scoreboard _sut = new ();

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
    public void UpdateMatch_WhenSettingToSameScore_ShouldAcceptThat()
    {
        var matchId = _sut.StartMatch("Italy", "Wales");
        
        _sut.UpdateMatch(matchId, 0, 0);
        
        _sut.ToString().Should().Be("Italy 0 - Wales 0");
    }
    
    [Fact]
    public void UpdateMatch_WhenSettingDownwards_ShouldAcceptThat()
    {
        var matchId = _sut.StartMatch("Italy", "Wales");
        
        _sut.UpdateMatch(matchId, 0, 1);
        _sut.UpdateMatch(matchId, 0, 2);
        _sut.UpdateMatch(matchId, 0, 1);
        
        _sut.ToString().Should().Be("Italy 0 - Wales 1");
    }
    
    [Fact]
    public void UpdateMatch_WhenReferringNonExisting_ShouldThrowArgumentException()
    {
        var matchId = _sut.StartMatch("Italy", "Wales");

        Assert.Throws<ArgumentException>(() => _sut.UpdateMatch(matchId + 1, 2, 0));
    }

    [Fact]
    public void FinishMatch_WhenOnBoard_ShouldRemoveFromTheBoard()
    {
        var matchId = _sut.StartMatch("Reds", "Yellows");

        _sut.FinishMatch(matchId);
        
        _sut.ToString().Should().BeEmpty();
    }
    
    [Fact]
    public void FinishMatch_WhenNotOnBoard_ShouldThrowArgumentException()
    {
        var matchId = _sut.StartMatch("Italy", "Wales");

        Assert.Throws<ArgumentException>(() => _sut.FinishMatch(matchId));
    }

    [Fact]
    public void GetSummary_WhenSingleMatch_ShouldReturnIt()
    {
        _sut.StartMatch("Italy", "England");

        var actual = _sut.GetSummary();
        
        actual.Should().Be("Italy 0 - England 0");
    }

    [Fact]
    public void GetSummary_WhenMultipleMatches_ShouldOrderByTotalScore()
    {
        var italyEngland = _sut.StartMatch("Italy", "England");
        _sut.UpdateMatch(italyEngland, 2, 1);
        var franceGermany = _sut.StartMatch("France", "Germany");
        _sut.UpdateMatch(franceGermany, 1, 1);
        var spainDenmark = _sut.StartMatch("Spain", "Denmark");
        _sut.UpdateMatch(spainDenmark, 0, 4);
        
        var actual = _sut.GetSummary();
        
        actual.Should().Be(@"Spain 0 - Denmark 4
Italy 2 - England 1
France 1 - Germany 1".ReplaceLineEndings());
    }

    [Fact]
    public void GetSummary_WhenManyMatchesHaveSameTotalScore_ShouldBreakTieByStartTimestamp()
    {
        var fakeTime = new FakeTimeProvider(DateTimeOffset.Now);
        _sut = new Scoreboard(fakeTime);
        fakeTime.Advance(TimeSpan.FromSeconds(1));
        var italyEngland = _sut.StartMatch("Italy", "England");
        fakeTime.Advance(TimeSpan.FromSeconds(2));
        var franceGermany = _sut.StartMatch("France", "Germany");
        fakeTime.Advance(TimeSpan.FromSeconds(3));
        var spainDenmark = _sut.StartMatch("Spain", "Denmark");

        _sut.UpdateMatch(italyEngland, 3, 1);
        _sut.UpdateMatch(franceGermany, 2, 2);
        _sut.UpdateMatch(spainDenmark, 0, 4);

        var actual = _sut.GetSummary();
        
        actual.Should().Be(@"Spain 0 - Denmark 4
France 2 - Germany 2
Italy 3 - England 1".ReplaceLineEndings());
    }
}