namespace WMS.Practice.Application.Commands.AuthCommands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserResultDTO>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IEmployeeRepository _employeeRepository;

        public CreateUserCommandHandler(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IEmployeeRepository employeeRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _employeeRepository = employeeRepository;
        }

        public async Task<CreateUserResultDTO> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            _ = await _employeeRepository.GetEmployeeByIdAsync(request.EmployeeId)
                ?? throw new EntityNotFoundException(nameof(Employee), request.EmployeeId);

            foreach (var role in request.Roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    throw new EntityNotFoundException(nameof(AppRole), role);
                }
            }

            var user = new AppUser
            {
                UserName = request.UserName,
                Email = request.Email,
                EmployeeId = request.EmployeeId
            };

            var createResult = await _userManager.CreateAsync(user, request.Password);
            if (!createResult.Succeeded)
            {
                throw new IdentityOperationException(createResult.Errors);
            }

            var addToRolesResult = await _userManager.AddToRolesAsync(user, request.Roles);
            if (!addToRolesResult.Succeeded)
            {
                throw new IdentityOperationException(addToRolesResult.Errors);
            }

            return new CreateUserResultDTO
            {
                UserId = user.Id,
                UserName = user.UserName,
                Roles = request.Roles,
                EmployeeId = user.EmployeeId
            };
        }
    }
}
