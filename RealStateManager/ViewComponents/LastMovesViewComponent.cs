using RealStateManager.DAL.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace RealStateManager.ViewComponents
{
    public class LastMovesViewComponent : ViewComponent
    {
        private readonly IHistoricResourcesRepository _historicResourcesRepository;

        public LastMovesViewComponent(IHistoricResourcesRepository historicResourcesRepository)
        {
            _historicResourcesRepository = historicResourcesRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _historicResourcesRepository.GetLatestMoves());
        }
    }
}
