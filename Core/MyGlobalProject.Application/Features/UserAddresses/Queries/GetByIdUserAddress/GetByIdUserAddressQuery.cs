using AutoMapper;
using MediatR;
using MyGlobalProject.Application.Dto.UserAddressDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.Wrappers;

namespace MyGlobalProject.Application.Features.UserAddresses.Queries.GetByIdUserAddress
{
    public class GetByIdUserAddressQuery : IRequest<GenericResponse<GetByIdUserAddressDTO>>
    {
        public Guid Id { get; set; }

        public class GetByIdUserAddressQueryHandler : IRequestHandler<GetByIdUserAddressQuery, GenericResponse<GetByIdUserAddressDTO>>
        {
            private readonly IUserAddressReadRepository _userAddressReadRepository;
            private readonly IUserAddressWriteRepository _userAddressWriteRepository;
            private readonly IMapper _mapper;

            public GetByIdUserAddressQueryHandler(IUserAddressReadRepository userAddressReadRepository, IUserAddressWriteRepository userAddressWriteRepository, IMapper mapper)
            {
                _userAddressReadRepository = userAddressReadRepository;
                _userAddressWriteRepository = userAddressWriteRepository;
                _mapper = mapper;
            }

            public async Task<GenericResponse<GetByIdUserAddressDTO>> Handle(GetByIdUserAddressQuery request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<GetByIdUserAddressDTO>();

                var currentUserAddress = await _userAddressReadRepository.GetByIdAsync(request.Id);

                if (currentUserAddress is null)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "No address to delete";

                    return response;
                }

                var mappedUserAddress = _mapper.Map<GetByIdUserAddressDTO>(currentUserAddress);

                response.Data = mappedUserAddress;
                response.Message = "Success";

                return response;
            }
        }
    }
}
