using Snooker.Interclub.Clubs;
using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;

namespace Snooker.Interclub.Players;

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

    protected Player()
    {
    }

    public int? Class { get; set; }

    public virtual Club? Club { get; set; }

    public Guid? ClubId { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string FirstName { get; set; }

    public string FullName { get => $"{FirstName} {LastName}"; }

    public string LastName { get; set; }

    public string LastNameFirstName { get => $"{LastName} {FirstName}"; }

    public Guid? ProfilePictureFileId { get; set; }

    public Guid? TenantId { get; private set; }

    public virtual IdentityUser? User { get; set; }

    public Guid? UserId { get; set; }
}