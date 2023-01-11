using AutoMapper;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Repository;
using Biblioteca.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Infra.Data.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly ClassContext _context;
        public IMapper Mapper;
        public ClientRepository(ClassContext context)
        {
            _context = context;
        }
        public void AddClient(Client client)
        {
            _context.Clients.Add(client);
            _context.SaveChanges();
        }

        public void DeleteClient(int id)
        {
            var client = GetClient(id);
            _context.Clients.Remove(client);
            _context.SaveChanges();
        }

        public Client GetClient(int id)
        {
            //Depois incluir todos os relacionamentos para aparecer quando necessário
            return _context.Clients.AsNoTracking().FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<Client> GetClients()
        {
            return _context.Clients.ToList();
        }

        public void UpdateClient(Client client)
        {
            _context.Clients.Update(client);
            _context.SaveChanges();
        }
    }
}
