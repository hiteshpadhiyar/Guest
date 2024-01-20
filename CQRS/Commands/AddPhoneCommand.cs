using Guest.Data;
using MediatR;
using AutoMapper;

namespace Guest.CQRS.Commands
{
    public class AddPhoneCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public List<string> Phone_Numbers { get; set; } = new List<string>();
        public class AddPhoneCommandHandler : IRequestHandler<AddPhoneCommand, Guid>
        {
            private DataContext _context;
            private readonly IMapper _mapper;
            public AddPhoneCommandHandler(DataContext context, IMapper mapper)
            {
                this._context = context;
                _mapper = mapper;
            }
            public async Task<Guid> Handle(AddPhoneCommand command, CancellationToken cancellationToken)
            {
                var _Guests = _context.Guests.First(f => f.Id == command.Id);

                var Existing_PhoneNumbers = _Guests.PhoneNumbers.Split(',').ToList();
                Existing_PhoneNumbers.AddRange(command.Phone_Numbers);
                _Guests.PhoneNumbers = string.Join(",", Existing_PhoneNumbers);

                await _context.SaveChangesAsync();

                return _Guests.Id;

            }
        }
    }
}
