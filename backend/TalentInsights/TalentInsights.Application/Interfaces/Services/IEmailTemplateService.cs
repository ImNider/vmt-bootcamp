namespace TalentInsights.Application.Interfaces.Services
{
    public interface IEmailTemplateService
    {
        //Task<EmailTemplateDto> Get(string name, Directory<string, string> );
        Task Init();
        Task Restart();
    }
}
