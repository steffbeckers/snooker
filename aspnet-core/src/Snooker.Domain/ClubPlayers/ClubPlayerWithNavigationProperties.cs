using Snooker.Clubs;
using Snooker.Players;
using System;

namespace Snooker.ClubPlayers
{
    public class ClubPlayerWithNavigationProperties
    {
        public Club Club { get; set; }
        public Guid ClubId { get; set; }
        public Guid Id { get; set; }
        public bool IsPrimaryClubOfPlayer { get; set; }
        public Player Player { get; set; }
        public Guid PlayerId { get; set; }
    }
}