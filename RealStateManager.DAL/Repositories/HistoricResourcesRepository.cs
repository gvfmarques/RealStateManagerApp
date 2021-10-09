using RealStateManager.BLL.Models;
using RealStateManager.DAL.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateManager.DAL.Repositories
{
    public class HistoricResourcesRepository : RepositoryGeneric<HistoricResource>, IHistoricResourcesRepository
    {
        private readonly RealStateManagerContext _realStateManagerContext;
        public HistoricResourcesRepository(RealStateManagerContext realStateManagerContext) : base(realStateManagerContext)
        {
            _realStateManagerContext = realStateManagerContext;
        }

        public object GetHistoricExpense(int year)
        {
            try
            {
                return _realStateManagerContext.HistoricResources.Include(hr => hr.Month)
                    .Where(hr => hr.Year == year && hr.Type == Types.Output).ToList()
                    .OrderBy(hr => hr.MonthId).GroupBy(hr => hr.Month.Name)
                    .Select(hr => new { Months = hr.Key, Values = hr.Sum(v => v.Value) });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public object GetHistoricIncomes(int year)
        {
            try
            {
                return _realStateManagerContext.HistoricResources.Include(hr => hr.Month)
                    .Where(hr => hr.Year == year && hr.Type == Types.Input).ToList()
                    .OrderBy(hr => hr.MonthId).GroupBy(hr => hr.Month.Name)
                    .Select(hr => new { Months = hr.Key, Values = hr.Sum(v => v.Value) });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<decimal> GetSumExpenses(int year)
        {
            try
            {
                return await _realStateManagerContext.HistoricResources.Include(hr => hr.Month)
                    .Where(hr => hr.Year == year && hr.Type == Types.Output)
                    .SumAsync(hr => hr.Value);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<decimal> GetSumIncomes(int year)
        {
            try
            {
                return await _realStateManagerContext.HistoricResources.Include(hr => hr.Month)
                    .Where(hr => hr.Year == year && hr.Type == Types.Input)
                    .SumAsync(hr => hr.Value);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<HistoricResource>> GetLatestMoves()
        {
            try
            {
                return await _realStateManagerContext.HistoricResources.Include(hr => hr.Month).OrderByDescending(hr => hr.HistoricResourceId)
                    .Take(5).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
