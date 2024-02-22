using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocentoScoop.Domain.Interfaces
{
    public interface IObservable<out T>
    {
        void Register(IObserver<T> observer);

        void NotifyObservers();

    }
}
