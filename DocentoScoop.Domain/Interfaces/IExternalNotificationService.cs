using DocentoScoop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocentoScoop.Domain.Interfaces
{
    public interface IExternalNotificationService
    {
        void SendMessage();

        ContactMethod ContactMethod { get; }
    }
}
