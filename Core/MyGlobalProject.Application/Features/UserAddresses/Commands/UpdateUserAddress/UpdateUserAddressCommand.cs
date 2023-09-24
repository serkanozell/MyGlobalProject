using AutoMapper;
using MediatR;
using MyGlobalProject.Application.Dto.UserAddressDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.Wrappers;
using MyGlobalProject.Domain.Entities;
using Serilog;

namespace MyGlobalProject.Application.Features.UserAddresses.Commands.UpdateUserAddress
{
    public class UpdateUserAddressCommand : IRequest<GenericResponse<UpdateUserAddressDTO>>
    {
        public Guid Id { get; set; }
        public string AddressTitle { get; set; }
        public string Address { get; set; }

        public class UpdateUserAddressCommandHandler : IRequestHandler<UpdateUserAddressCommand, GenericResponse<UpdateUserAddressDTO>>
        {
            private readonly IUserAddressReadRepository _userAddressReadRepository;
            private readonly IUserAddressWriteRepository _userAddressWriteRepository;
            private readonly IMapper _mapper;

            public UpdateUserAddressCommandHandler(IUserAddressReadRepository userAddressReadRepository, IUserAddressWriteRepository userAddressWriteRepository, IMapper mapper)
            {
                _userAddressReadRepository = userAddressReadRepository;
                _userAddressWriteRepository = userAddressWriteRepository;
                _mapper = mapper;
            }

            public async Task<GenericResponse<UpdateUserAddressDTO>> Handle(UpdateUserAddressCommand request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<UpdateUserAddressDTO>();
                var mappedUserAddress = _mapper.Map<UserAddress>(request);

                var currentUserAddress = await _userAddressReadRepository.GetByIdAsync(mappedUserAddress.Id);
                if (currentUserAddress == null)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "There is no address to delete";

                    return response;
                };

                currentUserAddress.Address = mappedUserAddress.Address;
                currentUserAddress.AddressTitle = mappedUserAddress.AddressTitle;

                await _userAddressWriteRepository.UpdateAsync(currentUserAddress);

                var updatedUserAddressDto = _mapper.Map<UpdateUserAddressDTO>(currentUserAddress);

                response.Data = updatedUserAddressDto;
                response.Message = "Updated successfully";

                Log.Information($"UserAddress updated. \n" +
                    $"Old address = {currentUserAddress.Address} - New Address = {updatedUserAddressDto.Address} \n" +
                    $"Old AddressTitle = {currentUserAddress.AddressTitle} - New AddressTitle = {updatedUserAddressDto.AddressTitle}");

                return response;
            }
        }
    }
}
