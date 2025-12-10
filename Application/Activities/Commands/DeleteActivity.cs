using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Activities.Commands
{
    public class DeleteActivity
    {
        public class Command: IRequest
        {
            public required string Id { get; set; }
        }

        public class Handler: IRequestHandler<Command>
        {
            private readonly AppDbContext dbContext;

            public Handler(AppDbContext dbContext)
            {
                this.dbContext = dbContext;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = dbContext.Activities.Find(request.Id)
                        ?? throw new Exception("没有找到activity。");

                dbContext.Activities.Remove(activity);

                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
