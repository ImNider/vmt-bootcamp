using TalentInsights.Application.Interfaces.Services;
using TalentInsights.Application.Models.Services.EmailTemplates;
using TalentInsights.Domain.Interfaces.Repositories;

namespace TalentInsights.Application.Services
{
    public class EmailTemplateService(EmailTemplatesData data, IEmailTemplateRepository repository) : IEmailTemplateService
    {
        /*public Task<EmailTemplateDto> Get(string name)
        {
            var template = data.Data.First(data => data.Name == name);
        }*/

        public Task Init()
        {
            throw new NotImplementedException();
        }

        public Task Restart()
        {
            throw new NotImplementedException();
        }
    }
}
