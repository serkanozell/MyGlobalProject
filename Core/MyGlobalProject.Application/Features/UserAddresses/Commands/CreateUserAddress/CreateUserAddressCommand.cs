using AutoMapper;
using MediatR;
using MyGlobalProject.Application.Dto.UserAddressDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.Wrappers;
using MyGlobalProject.Domain.Entities;

namespace MyGlobalProject.Application.Features.UserAddresses.Commands.CreateUserAddress
{
    public class CreateUserAddressCommand : IRequest<GenericResponse<CreateUserAddresDTO>>
    {
        public Guid UserId { get; set; }
        public string Address { get; set; }
        public string AddressTitle { get; set; }

        public class CreateUserAddressCommandHandler : IRequestHandler<CreateUserAddressCommand, GenericResponse<CreateUserAddresDTO>>
        {
            private readonly IUserAddressReadRepository _userAddressReadRepository;
            private readonly IUserAddressWriteRepository _userAddressWriteRepository;
            private readonly IMapper _mapper;

            public CreateUserAddressCommandHandler(IUserAddressReadRepository userAddressReadRepository, IUserAddressWriteRepository userAddressWriteRepository, IMapper mapper)
            {
                _userAddressReadRepository = userAddressReadRepository;
                _userAddressWriteRepository = userAddressWriteRepository;
                _mapper = mapper;
            }

            public async Task<GenericResponse<CreateUserAddresDTO>> Handle(CreateUserAddressCommand request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<CreateUserAddresDTO>();
                var mappedUserAddress = _mapper.Map<UserAddress>(request);

                var addedUserAddress = await _userAddressWriteRepository.AddAsync(mappedUserAddress);

                var mappedResultUserAddress = _mapper.Map<CreateUserAddresDTO>(addedUserAddress);

                response.Data = mappedResultUserAddress;
                response.Message = "Success";

                return response;
            }
        }
    }
}
