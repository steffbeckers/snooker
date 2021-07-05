namespace Snooker.Shared
{
    public class LookupDto<TKey>
    {
        public string DisplayName { get; set; }
        public TKey Id { get; set; }
    }
}