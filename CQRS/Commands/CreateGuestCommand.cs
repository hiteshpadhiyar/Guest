using Guest.Data;
using Guest.Entities;
using Guest.Models;
using MediatR;
using AutoMapper;

namespace Guest.CQRS.Commands
{
    public class CreateGuestCommand : IRequest<Guid>
    {
        public TitleType Title { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Email { get; set; }
        public List<string> Phone_Numbers { get; set; } = new List<string>();
        public class CreateGuestCommandHandler : IRequestHandler<CreateGuestCommand, Guid>
        {
            private DataContext _context;
            private readonly IMapper _mapper;
            public CreateGuestCommandHandler(DataContext context, IMapper mapper)
            {
                this._context = context;
                _mapper = mapper;
            }
            public async Task<Guid> Handle(CreateGuestCommand command, CancellationToken cancellationToken)
            {
                var _Guests = new Guests
                {
                    Id = Guid.NewGuid(),
                    Title = command.Title,
                    FirstName = command.FirstName,
                    LastName = command.LastName,
                    BirthDate = command.BirthDate,
                    Email = command.Email,
                    PhoneNumbers = string.Join(",", command.Phone_Numbers),
                };

                _context.Guests.Add(_Guests);

                await _context.SaveChangesAsync();

                return _Guests.Id;

            }
        }
    }
}
