namespace PatchKit.Api
{
    public interface IBaseApiConnectionFactory
    {
        IBaseApiConnection Create(ApiConnectionSettings settings);
    }
}