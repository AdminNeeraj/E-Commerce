namespace API.Errors
{
    public class ApiValidationErrorResponse : ApiResponse
    {
        public ApiValidationErrorResponse() : base(400, "Validation errors occurred.")
        {
        }
        // public ApiValidationErrorResponse(int statusCode, string message = null) : base(statusCode, message)
        // {
        // }
        public IEnumerable<string> Errors { get; set; }
    }
}