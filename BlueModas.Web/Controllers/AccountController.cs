using BlueModas.Web.Models;
using BlueModas.Web.Services;
using BlueModas.Web.Services.Interface;
using BlueModas.Web.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueModas.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly ClienteService clienteService;
        public AccountController(ClienteService clienteService)
        {
            this.clienteService = clienteService;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegisterSuccess()
        {
            MensagemSucesso = "Cadastro bem sucedido.";
            return RedirectToAction("Checkout", "Carrinho");
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegistroViewModel cadastro)
        {
            if (ModelState.IsValid)
            {
               var resultado = await  clienteService.CadastrarCliente(cadastro);
                if (resultado)
                {
                    return RedirectToAction(nameof(Login));
                }
            }
            MensagemErro = "Algo deu errado";
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            var usuarioItedentity = await clienteService.ObterPorEmail(login.Email);
            if (usuarioItedentity != null)
            {
                var usuarioIdentityResult = await clienteService.Login(login, usuarioItedentity);
                if (usuarioIdentityResult.Succeeded)
                {
                    Response.Cookies.Append("EmailUsuarioIdentity", usuarioItedentity.Email, new CookieOptions() { IsEssential = true });
                    Response.Cookies.Append("NomeUsuarioIdentity", usuarioItedentity.FullName, new CookieOptions() { IsEssential = true });
                    Response.Cookies.Append("IdUsuarioIdentity", usuarioItedentity.Id, new CookieOptions() { IsEssential = true });
                    MensagemSucesso = "Login Realizado com Sucesso!";
                    return RedirectToAction("Checkout", "Carrinho");
                }
                MensagemErro = "email ou senha invalidos";
                return View(login);
            }
            MensagemErro = "Email não encontrado";
            return View(login);
        }

        public ActionResult Sair()
        {
            clienteService.Sair();
            MensagemSucesso = "Logout efetuado com sucesso";
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
