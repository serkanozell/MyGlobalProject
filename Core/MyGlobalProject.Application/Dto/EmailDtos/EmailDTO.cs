namespace MyGlobalProject.Application.Dto.EmailDtos
{
    public class EmailDTO
    {
        public List<string> To { get; set; } = new List<string>();
        public List<string> CC { get; set; } = new List<string>();
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }
}
