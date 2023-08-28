using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyGlobalProject.Application.Dto.UserAddressDtos;
using MyGlobalProject.Application.RepositoryInterfaces;

namespace MyGlobalProject.Application.Features.UserAddresses.Queries.GetAllUserAddress
{
    public class GetAllUserAddressQuery : IRequest<List<UserAddressListDTO>>
    {
        public class GetAllUserAddressQueryHandler : IRequestHandler<GetAllUserAddressQuery, List<UserAddressListDTO>>
        {
            private readonly IUserAddressReadRepository _userAddressReadRepository;
            private readonly IMapper _mapper;

            public GetAllUserAddressQueryHandler(IUserAddressReadRepository userAddressReadRepository, IMapper mapper)
            {
                _userAddressReadRepository = userAddressReadRepository;
                _mapper = mapper;
            }

            public async Task<List<UserAddressListDTO>> Handle(GetAllUserAddressQuery request, CancellationToken cancellationToken)
            {
                var userAddressList = await _userAddressReadRepository.GetAll().ToListAsync();

                var mappedUserAddressList = _mapper.Map<List<UserAddressListDTO>>(userAddressList);

                return mappedUserAddressList;
            }
        }
    }
}
