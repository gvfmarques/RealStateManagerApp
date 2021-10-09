using RealStateManager.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealStateManager.DAL.Interface
{
    public interface IHistoricResourcesRepository : IRepositoryGeneric<HistoricResource>
    {
        object GetHistoricIncomes(int year);
        object GetHistoricExpense(int year);

        public Task<decimal> GetSumExpenses(int year);
        public Task<decimal> GetSumIncomes(int year);

        public Task<IEnumerable<HistoricResource>> GetLatestMoves();
    }
}
