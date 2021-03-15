using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coursAspNetMVC.ViewComponents
{
    public class MenuComponent : ViewComponent
    {
        public MenuComponent()
        {
        }

        public IViewComponentResult Invoke()
        {
            List<string> liste = new List<string>() { "menu 1", "menu 2", "Menu 3", "menu4" };
            return View(liste);
        }
    }
}
