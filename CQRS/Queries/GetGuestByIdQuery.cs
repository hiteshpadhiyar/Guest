using AutoMapper;
using Guest.Data;
using Guest.Models;
using MediatR;

namespace Guest.CQRS.Queries
{

    public class GetGuestByIdQuery : IRequest<GuestsItem>
    {
        public Guid Id { get; set; }

        public class GetProductByIdQueryHandler : IRequestHandler<GetGuestByIdQuery, GuestsItem>
        {
            private DataContext _context;
            private readonly IMapper _mapper;
            public GetProductByIdQueryHandler(DataContext context, IMapper mapper)
            {
                this._context = context;
                this._mapper = mapper;
            }
            public async Task<GuestsItem> Handle(GetGuestByIdQuery query, CancellationToken cancellationToken)
            {
                return await Task.Run(
                async () =>
                {
                    var result = await _context.Guests.FindAsync(query.Id);
                    return _mapper.Map<GuestsItem>(result);
                });
            }
        }
    }
}
