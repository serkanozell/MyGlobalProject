﻿using AutoMapper;
using MediatR;
using MyGlobalProject.Application.Dto.UserAddressDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.Wrappers;
using Serilog;

namespace MyGlobalProject.Application.Features.UserAddresses.Commands.DeleteUserAddress
{
    public class DeleteUserAddressCommand : IRequest<GenericResponse<DeleteUserAddressDTO>>
    {
        public Guid Id { get; set; }

        public class DeleteUserAddressCommandHandler : IRequestHandler<DeleteUserAddressCommand, GenericResponse<DeleteUserAddressDTO>>
        {
            private readonly IUserAddressReadRepository _userAddressReadRepository;
            private readonly IUserAddressWriteRepository _userAddressWriteRepository;
            private readonly IMapper _mapper;
            public DeleteUserAddressCommandHandler(IUserAddressReadRepository userAddressReadRepository, IUserAddressWriteRepository userAddressWriteRepository, IMapper mapper)
            {
                _userAddressReadRepository = userAddressReadRepository;
                _userAddressWriteRepository = userAddressWriteRepository;
                _mapper = mapper;
            }

            public async Task<GenericResponse<DeleteUserAddressDTO>> Handle(DeleteUserAddressCommand request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<DeleteUserAddressDTO>();

                var currentUserAddress = await _userAddressReadRepository.GetByIdAsync(request.Id);

                if (currentUserAddress == null)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "No address to delete";

                    return response;
                }

                await _userAddressWriteRepository.DeleteAsync(currentUserAddress, cancellationToken);

                var mappedUserAddress = _mapper.Map<DeleteUserAddressDTO>(currentUserAddress);

                response.Data = mappedUserAddress;
                response.Message = "Deleted successfully";

                Log.Information($"User address deleted. UserAddressId = {currentUserAddress.Id}");

                return response;
            }
        }
    }
}
