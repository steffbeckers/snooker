using System;
using Volo.Abp.Domain.Repositories;

namespace Snooker.Interclub.Frames;

public interface IFrameRepository : IRepository<Frame, Guid>
{
}