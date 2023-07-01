# Notes

## Entities

- Associations (BBSA Limburg)
- Seasons (2022-2023)
- Leagues (Interclub)
- Divisions (honorary, 1st, 2nd, 3rd, 4th, 5th, saturday)
- Clubs (NRG Paal)
- Users (Login)
- Players (Steff Beckers, Marco Vitali, Yoshi Lecocq)
- Teams (Set of x players)
- Tournaments (Set of matches)
- Matches (Set of frames)
- Frames (1 game of snooker)
- Breaks (snooker break higher than X)

## Platform functionality

- Users can have a player profile
- Clubs have management users
- Clubs can manage their players
- Clubs are linked to an association
- Players can register for tournaments

## League planning

- Teams play 1 home and 1 away match against all other teams in the same division (double round robin)
- Clubs have a number of available tables which need to be match ready
- Club open on holidays?
- Team's favorite day to play at own club
- Teams within the same division within the same club must play their matches as early as possible. Avoids match fixing at the end of the season?

## ChatGTP prompt

We have 10 snooker clubs which play an interclub league against each other. Overall there are +-450 players registered, spread over those 10 clubs, the smallest club has about 10 players, the largest about 60. Each club has one or more teams of players. The interclub league is split-up in multiple divisions, to match players and teams with about the same level of snooker skills. Currently the divisions are from best to worst: Honorary, 1st, 2nd, 3rd, 4th, 5th. The count of divisions are based on amount of players which are registered to play in the interclub. Each division contains around 16 teams. During 1 season of the interclub those teams play a double round robin tournament schedule. A season starts around the 1st of september, after the summer break of juli and august. Each team plays 2 matches against each other team in the same division, at their home club and at the away team's club. A match is played 3 vs 3 players. A match is played in the evening of a weekday, from monday to thursday. Per division there's a frame count defined. Currently in the honorary, 1st and 2nd divisions 18 frames of snooker are played per match, 2 frames against each opponent. In the 3rd and 4th division, 12 frames are played, 2 frames against 1 opponent, 1 frame against to 2 other opponents. In the 5th division, only 9 frames are played, 1 against each opponent. Each team has a preferred day in the week to play their home matches. When multiple teams of the same club play in the same division, those matches need to be scheduled at the start of each round of the league. Since each club only has a number of tables available (minimum 2) for this interclub league, a lot of planning is required. We want to schedule the interclub league for the next season when we know which players are registered to play in the interclub. Some clubs are not opened on holidays, so no match can be scheduled on those days. We want to generate an entire schedule for a season automatically as best as we can. Can you create a snooker league tournament scheduler application in .NET C#? Ask me more questions when needed.
