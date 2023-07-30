using Snooker.Interclub.Players;
using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Snooker.Interclub.Frames;

public class Break : FullAuditedEntity<Guid>, IMultiTenant
{
    public Break(
        Guid id,
        Frame frame,
        Player player,
        int value)
    {
        Id = id;
        Frame = frame;
        FrameId = frame.Id;
        Player = player;
        PlayerId = player.Id;
        Value = value;
    }

    protected Break()
    {
    }

    public virtual Frame Frame { get; set; }

    public Guid FrameId { get; set; }

    public virtual Player Player { get; }

    public Guid PlayerId { get; set; }

    public Guid? TenantId { get; set; }

    public int Value { get; set; }
}