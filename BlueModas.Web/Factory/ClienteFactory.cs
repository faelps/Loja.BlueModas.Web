using BlueModas.Web.Models;
using BlueModas.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueModas.Web.Factory
{
    public static class ClienteFactory
    {
        internal static Cliente CriarCliente(RegistroViewModel cadastro)
        {
            return new Cliente
            {
                UserName = cadastro.Email,
                Email = cadastro.Email,
                FullName = cadastro.Nome,
                PhoneNumber = cadastro.Telefone,
                PasswordHash = cadastro.Password
            };
        }

        internal static Cliente Login(LoginViewModel cliente)
        {
            return new Cliente
            {
                PasswordHash = cliente.Password,
                Email = cliente.Email
            };
        }
    }
}
