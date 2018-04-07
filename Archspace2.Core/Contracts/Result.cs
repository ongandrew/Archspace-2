namespace Archspace2
{
    public class Result
    {
        public ResultType Type { get; set; }
        public string Message { get; set; }

        public Result()
        {
        }

        public Result(ResultType aType)
        {
            Type = aType;
        }

        public Result (ResultType aType, string aMessage)
        {
            Type = aType;
            Message = aMessage;
        }
    }
}
