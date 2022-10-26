using Core.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Features.Queries.GetAllCients
{
    //Se crea esta clase por si se quiere buscar por algún parametro en especifico - De cliente.
    public class GetAllClientParameter : RequestParameter
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
    }
}
