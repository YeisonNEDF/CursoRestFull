using AutoMapper;
using Core.Domain.Entities;
using Core.DTOs;
using Core.Interfaces;
using Core.Specifications;
using Core.Wrappers;
using MediatR;

namespace Core.Features.Queries.GetAllCients
{
    public class GetAllClientQuery : IRequest<PagedResponse<List<ClientDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public class GetAllClientQueryHandler : IRequestHandler<GetAllClientQuery, PagedResponse<List<ClientDto>>>
        {
            private readonly IRepositoryAsync<Cliente> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetAllClientQueryHandler(IRepositoryAsync<Cliente> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }
            public async Task<PagedResponse<List<ClientDto>>> Handle(GetAllClientQuery request, CancellationToken cancellationToken)
            {
                var client = await _repositoryAsync.ListAsync(new PagedClientSpecification(
                    request.PageSize,
                    request.PageNumber,
                    request.Nombre,
                    request.Apellido
                    ));

                var clientDto = _mapper.Map<List<ClientDto>>(client);

                return new PagedResponse<List<ClientDto>>(clientDto, request.PageNumber, request.PageSize);
            }
        }
    }
}