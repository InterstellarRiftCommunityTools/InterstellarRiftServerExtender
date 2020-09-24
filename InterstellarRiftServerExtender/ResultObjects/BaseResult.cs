namespace IRSE.ResultObjects
{
    public class BaseResult
    {
        public bool Error { get; set; }
        public string Status { get; set; }

        public BaseResult(bool error, string status)
        {
            Error = error;
            Status = status;
        }
    }
}