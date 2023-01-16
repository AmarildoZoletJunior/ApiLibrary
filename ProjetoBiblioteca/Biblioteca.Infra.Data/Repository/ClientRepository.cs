using AutoMapper;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Pagination;
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

        public async void DeleteClientAsync(int id)
        {
            var client = await GetClient(id);
            _context.Clients.Remove(client);
            _context.SaveChanges();
        }

        public async Task<Client> GetClient(int id)
        {
            //Depois incluir todos os relacionamentos para aparecer quando necessário
            return await _context.Clients.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        }

        public IEnumerable<Client> GetClients(PageParameters parametros)
        {
            return _context.Clients.OrderBy(x => x.Nome).Skip((parametros.PageNumber - 1) * parametros.PageSize).Take(parametros.PageSize).ToList();
        }

        public void UpdateClient(Client client)
        {
            _context.Clients.Update(client);
            _context.SaveChanges();
        }
        public bool GetCpf(string cpf)
        {
            var resultado = _context.Clients.FirstOrDefault(x => x.CPF == cpf);
            if(resultado != null)
            {
                return true;
            }
            return false;
        }
    }
}
