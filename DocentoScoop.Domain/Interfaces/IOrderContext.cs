using DocentoScoop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocentoScoop.Domain.Interfaces
{
    public interface IOrderContext
    {
        IOrderState GetState();

        void SetState(IOrderState state);

        DateTime GetScreeningDate();

        ContactMethod GetPreferredContactMethod();

        void SetOrderExporter(IOrderExporter orderExporter);

    }
}
