namespace CandidateManagementAPI.utils
{
    public class ResponseModel<T>
    {
        public int Status { get; set; }
        public T? Entity { get; set; }
        public string? ReturnMessage { get; set; }

    }
}
