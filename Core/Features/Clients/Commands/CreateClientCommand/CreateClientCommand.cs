using AutoMapper;
using Core.Domain.Entities;
using Core.Interfaces;
using Core.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Features.Commands.CreateClientCommand
{
    public class CreateClientCommand : IRequest<Response<int>>
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
    }

    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Cliente> _repositoryAsync;
        private readonly IMapper _mapper;

        public CreateClientCommandHandler(IRepositoryAsync<Cliente> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            var newRecord = _mapper.Map<Cliente>(request);
            var data = await _repositoryAsync.AddAsync(newRecord);

            return new Response<int>(data.Id);
        }
    }
}
