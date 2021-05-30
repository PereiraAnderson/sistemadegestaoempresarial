using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SGE.Context.Repositories.Interfaces;

namespace SGE.Context.Repositories
{
    public class SGERepository<T> : ISGERepository<T> where T : class
    {
        protected readonly SGEDbContext _context;
        private readonly DbSet<T> _dbSet;

        public SGERepository(SGEDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public T Add(T model)
        {
            _context.Entry(model).Property("Ativo").CurrentValue = true;
            _context.Entry(model).Property("DataCriacao").CurrentValue = DateTimeOffset.Now;
            return _dbSet.Add(model).Entity;
        }

        public void Add(IEnumerable<T> models)
        {
            foreach (dynamic model in models) Add(model);
        }

        public IQueryable<T> GetAll() =>
            _dbSet.AsQueryable().AsNoTracking();

        public T Get(dynamic key, IEnumerable<string> includes = null)
        {
            var model = _dbSet.Find(key);

            if (model == null)
                throw new Exception("Não encontrado");

            if (includes != null && includes.Count() > 0)
                foreach (string include in includes)
                    Include(model, include.Split('.'));

            return model;
        }

        public T Update(T model)
        {
            var id = _context.Entry(model).Property("Id").CurrentValue?.ToString();

            if (string.IsNullOrWhiteSpace(id) || id.Equals("0"))
                throw new Exception("Id não informado");

            _context.Entry(model).Property("DataModificacao").CurrentValue = DateTimeOffset.Now;
            return _dbSet.Update(model).Entity;
        }

        public void Update(IEnumerable<T> models)
        {
            foreach (dynamic model in models) Update(models);
        }

        public T Remove(T model)
        {
            if (_context.Entry(model).Property("Id").CurrentValue == null)
                throw new Exception("Id não informado");

            _context.Entry(model).Property("Ativo").CurrentValue = !(bool)_context.Entry(model).Property("Ativo").CurrentValue;
            return Update(model);
        }

        public T Remove(dynamic key) =>
            Remove(Get(key));

        public void Remove(IEnumerable<T> models)
        {
            foreach (dynamic model in models) Remove(model);
        }

        public void Remove(IEnumerable<dynamic> keys)
        {
            foreach (dynamic key in keys) Remove(key);
        }

        public int SaveChanges() =>
            _context.SaveChanges();

        protected void Include(dynamic model, string[] includes)
        {
            if (includes.Count() > 0)
            {
                var include = includes.First();
                includes = includes.Skip(1).ToArray();

                var type = model.GetType();
                var isHashSet = (type.IsGenericType && (typeof(HashSet<>) == type.GetGenericTypeDefinition()));
                if (isHashSet)
                    foreach (var next in model)
                    {
                        try
                        {
                            _context.Entry(next).Reference(include).Load();
                        }
                        catch
                        {
                            _context.Entry(next).Collection(include).Load();
                        }

                        var m = next.GetType().GetProperty(include).GetValue(next, null);
                        Include(m, includes);
                    }
                else
                {
                    try
                    {
                        _context.Entry(model).Reference(include).Load();
                    }
                    catch
                    {
                        _context.Entry(model).Collection(include).Load();
                    }

                    var m = model.GetType().GetProperty(include).GetValue(model, null);
                    Include(m, includes);
                }
            }
        }
    }
}