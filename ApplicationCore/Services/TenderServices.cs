using ApplicationCore.DatabaseContext;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class TenderServices : ITender
    {
        private readonly EProcurementContext _procContext;

        public TenderServices(EProcurementContext procContext)
        {
            _procContext = procContext;
        }

        public async Task<bool> DeleteTender(string id)
        {
            var data = await _procContext.Tender.FirstOrDefaultAsync(y => y.Id == id);

            if (data == null) throw new Exception($"Tender is not exist.");

            _procContext.Tender.Remove(data);
            var result = await _procContext.SaveChangesAsync();

            if (result > 0) return true;

            return false;
        }

        public async Task<List<Tender>> GetTender()
        {
            return await _procContext.Tender.ToListAsync();
        }

        public async Task<Tender> GetTender(string id)
        {
            return await _procContext.Tender.FirstOrDefaultAsync(y => y.Id == id);
        }

        public async Task UpdateTender(Tender domain)
        {
            var data = await _procContext.Tender.AsTracking().FirstOrDefaultAsync(y => y.Id == domain.Id);
            if (data == null) throw new Exception($"Data does not exist");

            data.Name = domain.Name;
            data.RefNumber = domain.RefNumber;
            data.ReleaseDate = domain.ReleaseDate;
            data.ClosingDate = domain.ClosingDate;
            data.LastUpdatedBy = domain.LastUpdatedBy;
            data.LastUpdatedTime = domain.LastUpdatedTime;
            data.Details = domain.Details;
            
            _procContext.Entry(data).State = EntityState.Modified;
            await _procContext.SaveChangesAsync();
        }

        public async Task AddTender(Tender domain)
        {
            var data = await _procContext.Tender.FirstOrDefaultAsync(y => y.Id == domain.Id);
            if (data != null) throw new Exception($"Data {domain.Name} is allready exist");

            await _procContext.Tender.AddAsync(domain);
            await _procContext.SaveChangesAsync();
        }
    }
}
