using BlueModas.Web.Factory;
using BlueModas.Web.Models;
using BlueModas.Web.Repository.Interface;
using BlueModas.Web.Services.Interface;
using BlueModas.Web.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlueModas.Web.Services
{
    public class ClienteService 
    {

        private readonly SignInManager<Cliente> _signInManager;
        private readonly UserManager<Cliente> _userManager;
        private readonly IHttpContextAccessor HttpContextAccessor;
        public ClienteService(SignInManager<Cliente> _signInManager, UserManager<Cliente> _userManager, IHttpContextAccessor HttpContextAccessor)
        {
            this._signInManager = _signInManager;
            this._userManager = _userManager;
            this.HttpContextAccessor = HttpContextAccessor;
        }
        public async Task<bool> CadastrarCliente(RegistroViewModel cadastro)
        {
            var clienteModel = ClienteFactory.CriarCliente(cadastro);
            var result = await _userManager.CreateAsync(clienteModel, clienteModel.PasswordHash);
            var cliente = await ObterPorEmail(clienteModel.Email);
            if (result.Succeeded)
            {
                var claim = new Claim("Cliente", "True");
               await _userManager.AddClaimAsync(cliente, claim);
                return true;
            }
            return false;
        }

        public async Task<SignInResult> Login(LoginViewModel cliente, Cliente usuarioItedentity)
        {
            var login = ClienteFactory.Login(cliente);
            if (usuarioItedentity != null)
                return await _signInManager.PasswordSignInAsync(usuarioItedentity, login.PasswordHash, false, lockoutOnFailure: false);

            return SignInResult.Failed;
        }

        internal async Task<Cliente> ObterPorEmail(string email)
        {
            var usuarioIdentity = await _userManager.FindByEmailAsync(email);
            return usuarioIdentity;
        }

        public async Task<string> ObterUsuarioLogadoId()
        {
            return _userManager.GetUserId(HttpContextAccessor.HttpContext.User);
        }

        internal async Task<ICollection<Claim>> BuscarClaims(string usuarioLogadoId)
        {
            var usuarioIdentity = await BuscarUsuarioPorId(usuarioLogadoId);
            return await _userManager.GetClaimsAsync(usuarioIdentity);
        }

        public async Task<Cliente> BuscarUsuarioPorId(string usuarioLogadoId)
        {
            return await _userManager.FindByIdAsync(usuarioLogadoId);
        }

        internal void Sair()
        {
            _signInManager.SignOutAsync();
        }
    }
}
