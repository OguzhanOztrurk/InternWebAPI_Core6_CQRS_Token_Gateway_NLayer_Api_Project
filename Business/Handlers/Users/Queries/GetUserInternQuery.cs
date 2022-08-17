using Core.Wrappers;
using DataAccess.Abstract;
using Entities.Dto;
using MediatR;

namespace Business.Handlers.Users.Queries;

public class GetUserInternQuery:IRequest<IResponse>
{
    public class GetUserInternQueryHandler:IRequestHandler<GetUserInternQuery, IResponse>
    {
        private readonly ICurrentRepository _currentRepository;
        private readonly IInternRepository _internRepository;

        public GetUserInternQueryHandler(ICurrentRepository currentRepository, IInternRepository internRepository)
        {
            _currentRepository = currentRepository;
            _internRepository = internRepository;
        }

        public async Task<IResponse> Handle(GetUserInternQuery request, CancellationToken cancellationToken)
        {
            _currentRepository.UserControl(_currentRepository.UserId());
            var intern = await _internRepository.GetUserInternInfo(_currentRepository.UserId());
            return new Response<UserInternDTO>(intern);
        }
    }
}