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

namespace Core.Features.Commands.DeleteClientCommand
{
    public class DeleteClientCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }

    public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Cliente> _repositoryAsync;

        public DeleteClientCommandHandler(IRepositoryAsync<Cliente> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<int>> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _repositoryAsync.GetByIdAsync(request.Id);
            if (client == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }
            else
            {
               await _repositoryAsync.DeleteAsync(client);

                return new Response<int>(client.Id);

            }
        }
    }
}
