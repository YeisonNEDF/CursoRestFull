using Ardalis.Specification.EntityFrameworkCore;
using Core.Interfaces;
using Infraestructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class MyRepositoryAsync<T> : RepositoryBase<T>, IRepositoryAsync<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public MyRepositoryAsync(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
