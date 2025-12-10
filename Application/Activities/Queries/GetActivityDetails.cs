using Domain;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Activities.Queries
{
    public class GetActivityDetails
    {
        public class Query: IRequest<Activity>
        {
            public required string Id { get; set; }
        }

        public class Handlder: IRequestHandler<Query, Activity>
        {
            private readonly AppDbContext dbContext;

            public Handlder(AppDbContext dbContext)
            {
                this.dbContext = dbContext;
            }

            public async Task<Activity> Handle(Query request, CancellationToken cancellationToken)
            {
                var activity = await dbContext.Activities
                    .FindAsync([request.Id], cancellationToken);

                if (activity == null) throw new Exception("Activities 未找到。");

                return activity;
            }
        }
    }
}
