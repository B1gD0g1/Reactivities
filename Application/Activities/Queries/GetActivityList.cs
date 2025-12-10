using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Activities.Queries
{
    public class GetActivityList
    {
        public class Query: IRequest<List<Activity>> { }

        public class Handler: IRequestHandler<Query, List<Activity>>
        {
            private readonly AppDbContext dbContext;

            public Handler(AppDbContext dbContext)
            {
                this.dbContext = dbContext;
            }

            public async Task<List<Activity>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await dbContext.Activities.ToListAsync(cancellationToken);
            }
        }
    }
}
