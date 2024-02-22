using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocentoScoop.Domain.Interfaces
{
    public interface IObserver<in T>
    {
        void Update(T changed);
    }
}
