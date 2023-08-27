using AutoMapper;
using MediatR;
using MyGlobalProject.Application.Dto.UserDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.Wrappers;

namespace MyGlobalProject.Application.Features.Users.Queries.GetByIdUsers
{
    public class GetByIdUserQuery : IRequest<GenericResponse<GetByIdUserDTO>>
    {
        public Guid Id { get; set; }

        public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQuery, GenericResponse<GetByIdUserDTO>>
        {
            private readonly IUserReadRepository _userReadRepository;
            private readonly IMapper _mapper;

            public GetByIdUserQueryHandler(IUserReadRepository userReadRepository, IMapper mapper)
            {
                _userReadRepository = userReadRepository;
                _mapper = mapper;
            }

            public async Task<GenericResponse<GetByIdUserDTO>> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<GetByIdUserDTO>();

                var currenctUser = await _userReadRepository.GetByIdAsync(request.Id);

                if (currenctUser == null)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "There is no user";

                    return response;
                }

                var mappedUser = _mapper.Map<GetByIdUserDTO>(currenctUser);

                response.Data = mappedUser;
                response.Message = "Success";

                return response;
            }
        }
    }
}
