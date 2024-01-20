using AutoMapper;
using Guest.Data;
using Guest.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Guest.CQRS.Queries
{
    public class GetAllGuestQuery : IRequest<IEnumerable<GuestsItem>>
    {
        public class GetAllGuestQueryHandler : IRequestHandler<GetAllGuestQuery, IEnumerable<GuestsItem>>
        {
            private DataContext context;
            private readonly IMapper _mapper;
            public GetAllGuestQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this._mapper = mapper;
            }
            public async Task<IEnumerable<GuestsItem>> Handle(GetAllGuestQuery query, CancellationToken cancellationToken)
            {
                return await Task.Run(
                async () =>
                {
                    var result = await context.Guests.ToListAsync();
                    return _mapper.Map<IEnumerable<GuestsItem>>(result);
                });
            }
        }
    }
}
