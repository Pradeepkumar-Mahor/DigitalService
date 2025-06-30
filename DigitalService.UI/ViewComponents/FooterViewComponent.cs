using Microsoft.AspNetCore.Mvc;

namespace DigitalService.UI.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {

        public FooterViewComponent()
        {
        }

        public IViewComponentResult Invoke(string filter)
        {
            return View();
        }
    }
}
