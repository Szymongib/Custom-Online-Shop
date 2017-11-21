using ApplicationCore.Interfaces.Data;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ApplicationInfrastructure.Data.Repositories
{
    public class EFBaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected OnlineShopDbContext _context;

        public EFBaseRepository(OnlineShopDbContext context)
        {
            _context = context;
        }



        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public IEnumerable<T> ListAll()
        {
            return _context.Set<T>().AsEnumerable();
        }
        
        public T Add(T model)
        {
            _context.Set<T>().Add(model);
            _context.SaveChanges();
            return model;
        }

        public void Delete(T model)
        {
            _context.Set<T>().Remove(model);
            _context.SaveChanges();
        }

        public void Update(T model)
        {
            _context.Entry(model).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
