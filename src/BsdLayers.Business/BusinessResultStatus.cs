namespace BsdLayers.Business
{
    /// <summary>
    /// Possible result status for business operations
    /// </summary>
    public enum BusinessResultStatus
    {
        Success = 200,
        Created = 201,

        InvalidInputs = 400,
        Unauthorized = 401,

        Forbidden = 403,
        ResourceNotFound = 404,

        Conflict = 409,
        Locked = 423,

        InternalError = 500,
        ServiceUnavailable = 503
    }
}
