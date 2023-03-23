using Azure;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
/// <summary>
///   <br />
/// </summary>
/// <typeparam name="T"></typeparam>
/// <Modified>
/// Name Date Comments
/// longpv 3/22/2023 created
/// </Modified>
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly UserManageDbContext _context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;

        public Repository(UserManageDbContext context)
        {
            _context = context;
            entities = context.Set<T>();
        }
        public void Delete(int id)
        {
            _context.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }


        public T GetById(int id)
        {
            return entities.Find(id);
        }

        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            _context.SaveChanges();
        }



        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _context.SaveChanges();
        }
    }
}
