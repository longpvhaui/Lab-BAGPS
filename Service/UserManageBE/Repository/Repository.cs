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
        /// <summary>Xóa 1 bản ghi.</summary>
        /// <param name="id">The identifier.</param>
        /// <Modified>
        /// Name Date Comments
        /// longpv 3/30/2023 created
        /// </Modified>
        public void Delete(int id)
        {
            _context.SaveChanges();
        }

        /// <summary>Lấy toàn bộ dữ liệu </summary>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <Modified>
        /// Name Date Comments
        /// longpv 3/30/2023 created
        /// </Modified>
        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }


        /// <summary>Lấy dữ liệu theo id.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <Modified>
        /// Name Date Comments
        /// longpv 3/30/2023 created
        /// </Modified>
        public T GetById(int id)
        {
            return entities.Find(id);
        }

        /// <summary>Thêm dữ liệu.</summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="System.ArgumentNullException">entity</exception>
        /// <Modified>
        /// Name Date Comments
        /// longpv 3/30/2023 created
        /// </Modified>
        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            _context.SaveChanges();
        }
        /// <summary>Cập nhật dữ liệu</summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="System.ArgumentNullException">entity</exception>
        /// <Modified>
        /// Name Date Comments
        /// longpv 3/30/2023 created
        /// </Modified>
        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _context.SaveChanges();
        }


        public void SaveChanges()
        {
            _context.SaveChanges();
        }

     
    }
}
