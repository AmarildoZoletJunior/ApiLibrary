using Biblioteca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Repository
{
    public interface IClientRepository
    {
        public IEnumerable<Client> GetClients();
        public void AddClient(Client client);
        public void DeleteClient(int id);
        public void UpdateClient(Client client);
        public Client GetClient(int id);
        public bool GetCpf(string cpf);
    }
}
