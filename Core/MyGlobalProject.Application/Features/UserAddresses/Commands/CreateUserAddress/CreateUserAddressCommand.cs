using AutoMapper;
using MediatR;
using MyGlobalProject.Application.Dto.UserAddressDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.Wrappers;
using MyGlobalProject.Domain.Entities;
using Serilog;

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
            private readonly IUserReadRepository _userReadRepository;
            private readonly IMapper _mapper;

            public CreateUserAddressCommandHandler(IUserAddressReadRepository userAddressReadRepository, IUserAddressWriteRepository userAddressWriteRepository, IMapper mapper, IUserReadRepository userReadRepository)
            {
                _userAddressReadRepository = userAddressReadRepository;
                _userAddressWriteRepository = userAddressWriteRepository;
                _userReadRepository = userReadRepository;
                _mapper = mapper;
            }

            public async Task<GenericResponse<CreateUserAddresDTO>> Handle(CreateUserAddressCommand request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<CreateUserAddresDTO>();

                var existUser = await _userReadRepository.GetByIdAsync(request.UserId);
                if (existUser is null)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "Wrong user id";

                    return response;
                }

                var mappedUserAddress = _mapper.Map<UserAddress>(request);

                var addedUserAddress = await _userAddressWriteRepository.AddAsync(mappedUserAddress);

                var mappedResultUserAddress = _mapper.Map<CreateUserAddresDTO>(addedUserAddress);

                response.Data = mappedResultUserAddress;
                response.Message = "Success";

                Log.Information($"User address created. UserId = {addedUserAddress.Id}");

                return response;
            }
        }
    }
}
