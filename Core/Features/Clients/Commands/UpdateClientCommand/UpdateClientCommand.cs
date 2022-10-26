using AutoMapper;
using Core.Domain.Entities;
using Core.Exceptions;
using Core.Interfaces;
using Core.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Features.Commands.UpdateClientCommand
{
    public class UpdateClientCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
    }

    public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Cliente> _repositoryAsync;
        private readonly IMapper _mapper;

        public UpdateClientCommandHandler(IRepositoryAsync<Cliente> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
           var client = await _repositoryAsync.GetByIdAsync(request.Id);
            if(client == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }
            else
            {
                client.Nombre = request.Nombre;
                client.Apellido = request.Apellido;
                client.FechaNacimiento = request.FechaNacimiento;
                client.Telefono = request.Telefono;
                client.Email = request.Email;
                client.Direccion = request.Direccion;

                await _repositoryAsync.UpdateAsync(client);

                return new Response<int>(client.Id);

            }
        }
    }
}
