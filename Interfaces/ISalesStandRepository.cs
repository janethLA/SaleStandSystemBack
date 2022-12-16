using MySalesStandSystem.Input;
using MySalesStandSystem.Models;
using MySalesStandSystem.Output;

namespace MySalesStandSystem.Interfaces
{
    public interface ISalesStandRepository
    {
        Task<SalesStand> CreateSalesStandAsync(SalesStand salesStand, IFormFile image);
        Task<bool> DeleteSalesStandAsync(SalesStand salesStand);
        SalesStand GetSalesStandById(int id);
        IEnumerable<SalesStand> GetSalesStands();
        Task<bool> UpdateSalesStandAsync(SalesStand  salesStand);
        Task<bool> UpdateSalesStandAsync(int id, SalesStand salesStand, IFormFile image);
        List<SalesStandOutput> GetAllSalesStands();
    }
}
