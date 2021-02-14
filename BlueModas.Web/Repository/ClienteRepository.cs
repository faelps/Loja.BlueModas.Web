using BlueModas.Web.Models;
using BlueModas.Web.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueModas.Web.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly UserDbContext userDbContext;
        public ClienteRepository(UserDbContext userDbContext)
        {
            this.userDbContext = userDbContext;
        }
        public void CadastrarCliente(Cliente clienteModel)
        {
            userDbContext.Add(clienteModel);
            userDbContext.SaveChanges();
        }
    }
}
