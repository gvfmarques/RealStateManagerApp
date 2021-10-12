using RealStateManager.DAL.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace RealStateManager.ViewComponents
{
    public class LatestMovementsViewComponent : ViewComponent
    {
        private readonly IHistoricResourcesRepository _historicResourcesRepository;

        public LatestMovementsViewComponent(IHistoricResourcesRepository historicResourcesRepository)
        {
            _historicResourcesRepository = historicResourcesRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _historicResourcesRepository.GetLatestMoves());
        }
    }
}
