using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueModas.Web.Models
{
    [ViewComponent(Name = "Carrinho")]
    public class CarrinhoComponente : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("~/Views/Shared/components/_carrinhoDeCompras.cshtml");
        }
    }
}
