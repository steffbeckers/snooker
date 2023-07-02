using Snooker.Seasons;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Snooker.Leagues;

public class League : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    private string _name;

    public League(
        Guid id,
        string name)
    {
        Id = id;
        Name = name;
    }

    private League()
    {
    }

    public string Name
    {
        get => _name;
        set
        {
            Check.NotNull(value, nameof(Name));
            Check.Length(value, nameof(Name), LeagueConsts.NameMaxLength);
            _name = value;
        }
    }

    public virtual ICollection<Season> Seasons { get; private set; } = new Collection<Season>();

    public Guid? TenantId { get; private set; }
}