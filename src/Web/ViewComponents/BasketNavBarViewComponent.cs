using Microsoft.AspNetCore.Mvc;

namespace Web.ViewComponents
{
    public class BasketNavBarViewComponent : ViewComponent
    {
        private readonly IBasketViewModelService _basketViewModelService;

        public BasketNavBarViewComponent(IBasketViewModelService basketViewModelService)
        {
            _basketViewModelService = basketViewModelService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _basketViewModelService.GetBasketViewModelAsync());
        }
    }
}
