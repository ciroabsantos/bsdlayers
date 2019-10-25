namespace BsdLayers.Business.Specs
{
    /// <summary>
    /// Represents a business specification
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBusinessSpec<T>
    {
        BusinessSpecResult IsSatisfiedBy(T bo);
    }
}
