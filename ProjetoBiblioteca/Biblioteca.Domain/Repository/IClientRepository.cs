using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Repository
{
    public interface IClientRepository
    {
        public Task<IEnumerable<Client>> GetClients(PageParameters parametros);
        public void AddClient(Client client);
        public Task DeleteClientAsync(int id);
        public Task UpdateClient(Client client);
        public Task<Client> GetClient(int id);
        public bool GetCpf(string cpf);
    }
}
