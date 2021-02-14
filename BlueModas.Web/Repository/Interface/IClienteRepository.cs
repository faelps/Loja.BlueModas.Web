using BlueModas.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueModas.Web.Repository.Interface
{
    public interface IClienteRepository
    {
        void CadastrarCliente(Cliente clienteModel);
    }
}
