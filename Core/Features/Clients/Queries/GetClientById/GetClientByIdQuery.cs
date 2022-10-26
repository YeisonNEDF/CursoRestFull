using AutoMapper;
using Core.Domain.Entities;
using Core.DTOs;
using Core.Interfaces;
using Core.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Features.Queries.GetClientById
{
    public class GetClientByIdQuery : IRequest<Response<ClientDto>>
    {
        public int Id { get; set; }

        public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, Response<ClientDto>>
        {
            private readonly IRepositoryAsync<Cliente> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetClientByIdQueryHandler(IRepositoryAsync<Cliente> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<ClientDto>> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
            {
               var client = await _repositoryAsync.GetByIdAsync(request.Id);

                if (client == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado cone el id {request.Id}");
                }
                else
                {
                    var dto = _mapper.Map<ClientDto>(client);
                    return new Response<ClientDto>(dto);
                }
                
            }
        }
    }
}