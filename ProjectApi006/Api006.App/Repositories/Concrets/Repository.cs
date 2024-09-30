using Api006.App.Context;
using Api006.App.Entities.Base;
using Api006.App.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Api006.App.Repositories.Concrets
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            //_context.Remove(entity);
            _context.Set<T>().Remove(entity);
        }

        public IQueryable<T> GetAll()
        {
           return _context.Set<T>().AsQueryable();
        }

        public IQueryable<T> GetAllWhere(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression).AsQueryable();
        }

        public async Task<T> GetByIdAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(expression);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            //_context.Update(entity);
            _context.Set<T>().Update(entity);
        }
    }
}
