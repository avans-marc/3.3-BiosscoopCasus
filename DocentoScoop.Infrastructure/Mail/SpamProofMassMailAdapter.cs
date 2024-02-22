using DocentoScoop.Domain.Interfaces;
using DocentoScoop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocentoScoop.Infrastructure.Mail
{
    /// <summary>
    /// 
    /// </summary>
    public class SpamProofMassMailAdapter : IExternalNotificationService
    {
        private readonly SpamProofMassMailService _spamProofMassMailService;

        public SpamProofMassMailAdapter(SpamProofMassMailService spamProofMassMailService)
        {
            _spamProofMassMailService = spamProofMassMailService;
        }

        public ContactMethod ContactMethod => ContactMethod.Email;

        public void SendMessage() => _spamProofMassMailService.SendSpamProofMail();

    }
}
