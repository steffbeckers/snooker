using Volo.Abp;

namespace Snooker.Samples
{
    public class EntityWithErrorMethod
    {
        public void ThrowBusinessException()
        {
            throw new BusinessException("Snooker:SampleError");
        }
    }
}