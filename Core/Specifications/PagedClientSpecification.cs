using Ardalis.Specification;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class PagedClientSpecification : Specification<Cliente>
    {
        public PagedClientSpecification(int pageSize, int PageNumber, string nombre, string apellido)
        {
            Query.Skip((PageNumber - 1) * pageSize)
                .Take(pageSize);

            if (!string.IsNullOrEmpty(nombre))           
                Query.Search(x => x.Nombre, "%" + nombre + "%");
            
                


            if (!string.IsNullOrEmpty(apellido))            
                Query.Search(x => x.Apellido, "%" + apellido + "%");
          
                
        }
    }
}
