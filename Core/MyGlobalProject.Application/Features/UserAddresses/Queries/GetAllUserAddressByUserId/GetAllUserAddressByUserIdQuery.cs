using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyGlobalProject.Application.Dto.UserAddressDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.Wrappers;

namespace MyGlobalProject.Application.Features.UserAddresses.Queries.GetAllUserAddressByUserId
{
    public class GetAllUserAddressByUserIdQuery : IRequest<GenericResponse<List<UserAddressListDTO>>>
    {
        public Guid UserId { get; set; }

        public class GetAllUserAddressByUserIdQueryHandler : IRequestHandler<GetAllUserAddressByUserIdQuery, GenericResponse<List<UserAddressListDTO>>>
        {
            private readonly IUserAddressReadRepository _userAddressReadRepository;
            private readonly IUserReadRepository _userReadRepository;
            private readonly IMapper _mapper;

            public GetAllUserAddressByUserIdQueryHandler(IUserAddressReadRepository userAddressReadRepository, IMapper mapper, IUserReadRepository userReadRepository)
            {
                _userAddressReadRepository = userAddressReadRepository;
                _mapper = mapper;
                _userReadRepository = userReadRepository;
            }

            public async Task<GenericResponse<List<UserAddressListDTO>>> Handle(GetAllUserAddressByUserIdQuery request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<List<UserAddressListDTO>>();

                var existUser =await _userReadRepository.GetByIdAsync(request.UserId);

                if (existUser == null)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "Wrong user id";

                    return response;
                }

                var userAddressList = await _userAddressReadRepository.GetBy(x => x.UserId == request.UserId).ToListAsync();

                var mappedUserAddressList = _mapper.Map<List<UserAddressListDTO>>(userAddressList);

                response.Data = mappedUserAddressList;
                response.Message = "Success";

                return response;
            }
        }
    }
}
