using AutoMapper;
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
    public class EditActivity
    {
        public class Command: IRequest
        {
            public required Activity Activity { get; set; }
        }

        public class Handler: IRequestHandler<Command>
        {
            private readonly AppDbContext dbContext;
            private readonly IMapper mapper;

            public Handler(AppDbContext dbContext, IMapper mapper)
            {
                this.dbContext = dbContext;
                this.mapper = mapper;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await dbContext.Activities
                    .FindAsync([request.Activity.Id], cancellationToken)
                        ?? throw new Exception("没有找到activity。");

                mapper.Map(request.Activity, activity);

                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
