using AutoMapper;
using Core.Domain.Entities;
using Core.DTOs;
using Core.Features.Commands.CreateClientCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region Commands
            CreateMap<CreateClientCommand, Cliente>();
            #endregion

            #region CommandsDto
            CreateMap<Cliente, ClientDto>();
            #endregion
        }
    }
}
