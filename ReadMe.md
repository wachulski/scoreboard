# Scoreboard

## Assumptions

1. A score can be updated to an arbitrary score (even downwards) as e.g. VAR systems may overturn initial score decision.
  Since the board is meant to be 'live', then we assume it may report '0 - 1' and then back to '0 - 0'.
2. For the sake of simplicity the summary is string based. Normally it should return a structure and output formatting
  should be done by the caller.