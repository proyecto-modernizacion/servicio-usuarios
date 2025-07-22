
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Usuarios.Infraestructura.Adaptadores.Repositorios;

namespace Usuarios.Infraestructura.Adaptadores.RepositorioGenerico
{
    [ExcludeFromCodeCoverage]
    public class RepositorioBase<T> : IRepositorioBase<T> where T : class
    {
        private readonly IServiceProvider _serviceProvider;

        public RepositorioBase(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        private UsuariosDbContext GetContext()
        {
            return _serviceProvider.GetService<UsuariosDbContext>();
        }

        protected DbSet<T> GetEntitySet()
        {
            return GetContext().Set<T>();
        }
        public async Task<T> BuscarPorLlave(object ValueKey)
        {
            var ctx = GetContext();
            var entitySet = ctx.Set<T>();
            var res = await entitySet.FindAsync(ValueKey);
            await ctx.DisposeAsync();
            return res;
        }

        public async Task<List<T>> DarListado()
        {
            var ctx = GetContext();
            var entitySet = ctx.Set<T>();
            var res = await entitySet.ToListAsync();
            await ctx.DisposeAsync();
            return res;
        }

        public async Task<T> Guardar(T entity)
        {
            var ctx = GetContext();
            var entitySet = ctx.Set<T>();
            var res = await entitySet.AddAsync(entity);
            await ctx.SaveChangesAsync();
            await ctx.DisposeAsync();
            return res.Entity;
        }

        public async Task<T> Actualizar(T entity)
        {
            var ctx = GetContext();
            var entitySet = ctx.Set<T>();
            var res = entitySet.Update(entity);
            await ctx.SaveChangesAsync();
            await ctx.DisposeAsync();
            return res.Entity;

        }

        public async Task<T> BuscarPorCampos(Expression<Func<T, bool>> expr)
        {
            var ctx = GetContext();
            var entitySet = ctx.Set<T>();
            var res = await entitySet.AsNoTracking().FirstOrDefaultAsync(expr);
            await ctx.DisposeAsync();
            return res;
        }

        public async Task<List<T>> DarListadoPorCampos(Expression<Func<T, bool>> expr)
        {
            var ctx = GetContext();
            var entitySet = ctx.Set<T>();
            var res = await entitySet.AsNoTracking().Where(expr).ToListAsync();
            await ctx.DisposeAsync();
            return res;
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                try
                {
                    var ctx = GetContext();
                    ctx.Dispose();
                }
                catch (Exception)
                { }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
