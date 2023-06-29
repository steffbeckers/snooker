using Snooker.Clubs;
using Snooker.TeamPlayers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;

namespace Snooker.Players;

public class Player : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    public Player(
        Guid id,
        string firstName,
        string lastName)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
    }

    private Player()
    {
    }

    public int? Class { get; set; }

    public Club? Club { get; set; }

    public Guid? ClubId { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public Guid? ProfilePictureFileId { get; set; }

    public ICollection<TeamPlayer> Teams { get; set; } = new Collection<TeamPlayer>();

    public Guid? TenantId { get; private set; }

    public IdentityUser? User { get; set; }

    public Guid? UserId { get; set; }
}