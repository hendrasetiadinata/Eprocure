using ApplicationCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface ITender
    {
        Task<List<Tender>> GetTender();
        Task<Tender> GetTender(string Id);
        Task AddTender(Tender domain);
        Task UpdateTender(Tender user);
        Task<bool> DeleteTender(string id);
    }
}
