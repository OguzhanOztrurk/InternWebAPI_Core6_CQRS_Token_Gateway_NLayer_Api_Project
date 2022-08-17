using Core.Wrappers;
using DataAccess.Abstract;
using Entities.Dto;
using MediatR;

namespace Business.Handlers.Users.Commands;

public class UpdateUserInternCommand:IRequest<IResponse>
{
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Number { get; set; }
    public string? Email { get; set; }
    public string? Adress { get; set; }
    
    public class UpdateUserInternCommandHandler:IRequestHandler<UpdateUserInternCommand, IResponse>
    {
        
        private readonly IUserRepository _userRepository;
        private readonly ICurrentRepository _currentRepository;
        private readonly IInternRepository _internRepository;

        public UpdateUserInternCommandHandler(IUserRepository userRepository, ICurrentRepository currentRepository, IInternRepository internRepository)
        {
            _userRepository = userRepository;
            _currentRepository = currentRepository;
            _internRepository = internRepository;
        }

        public async Task<IResponse> Handle(UpdateUserInternCommand request, CancellationToken cancellationToken)
        {
            _currentRepository.UserControl(_currentRepository.UserId());
            _userRepository.UserNameControl(request.UserName);
            
            var updateUser = _userRepository.Get(x => x.UserId == _currentRepository.UserId());
            updateUser.UserName = request.UserName;
            updateUser.FirstName = request.FirstName;
            updateUser.LastName = request.LastName;
            updateUser.Number = request.Number;
            updateUser.Email = request.Email;
            
            _userRepository.Update(updateUser);
            await _userRepository.SaveChangesAsync();

            var updateIntern = _internRepository.Get(x => x.UserId == _currentRepository.UserId());
            updateIntern.Adress = request.Adress;

            _internRepository.Update(updateIntern);
            await _userRepository.SaveChangesAsync();
            var result = new UserInternDTO()
            {
                UserName = updateUser.UserName,
                FirstName = updateUser.FirstName,
                LastName = updateUser.LastName,
                Number = updateUser.Number,
                Email = updateUser.Email,
                Adress = updateIntern.Adress
            };
            return new Response<UserInternDTO>(result);
        }
    }
}