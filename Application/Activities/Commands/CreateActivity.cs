using Domain;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Activities.Commands
{
    public class CreateActivity
    {
        public class Command: IRequest<string>
        {
            public required Activity Activity { get; set; }
        }

        public class Handler : IRequestHandler<Command, string>
        {
            private readonly AppDbContext dbContext;

            public Handler(AppDbContext dbContext)
            {
                this.dbContext = dbContext;
            }

            public async Task<string> Handle(Command request, CancellationToken cancellationToken)
            {
                dbContext.Activities.Add(request.Activity);

                await dbContext.SaveChangesAsync(cancellationToken);

                return request.Activity.Id;
            }
        }
    }
}
