using Blogging.Common.Presentation.Endpoints;
using Blogging.Modules.Blog.Application.Section.CreateSection;
using Blogging.Modules.Blog.Application.Section.DeleteSection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Presentation.Sections
{
    internal sealed class DeleteSection : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("section/{id}", async (Guid id, ISender sender) =>
            {
                var cmd = new DeleteSectionCommand(id);
                var res = await sender.Send(cmd);

                return res.IsSuccess ? Results.Ok() : Results.Problem(res.Error.Description);

            }).WithTags(Tags.Sections);
        }
    }
}
