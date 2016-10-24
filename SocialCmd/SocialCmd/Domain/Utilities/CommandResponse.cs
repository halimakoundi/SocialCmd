namespace SocialCmd.Domain.Utilities
{
    public class CommandResponse
    {
        public bool Success { get; set; } = true;
        public string Value { get; set; } = string.Empty;

        public CommandResponse(bool result)
        {
            this.Success = result;
        }
        public CommandResponse(bool result, string value)
        {
            this.Success = result;
            this.Value = value;
        }
        public CommandResponse()
        {
        }
    }
}

