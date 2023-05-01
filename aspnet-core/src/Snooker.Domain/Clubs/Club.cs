using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Snooker.Clubs;

public class Club : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
	private string _name;

	internal Club(Guid id, string name)
	{
		Id = id;
		Name = name;
	}

	private Club()
	{
	}

	public string Name
	{
		get => _name;
		set
		{
			_name = value;
		}
	}

	public Guid? TenantId { get; set; }
}
