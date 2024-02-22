using DocentoScoop.Domain.Models;

namespace DocentoScoop.Domain.Interfaces
{
    public interface IOrderExporter
    {
        void Export(Order order);

    }
}