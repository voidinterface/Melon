using System.Collections.Generic;
using MediatR;
using Melon.Features.Libraries.CreateLibrary;
using Melon.Features.Libraries.GetAllLibraries;
using Melon.Features.Libraries.ScanLibrary;
using Melon.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Melon.Features.Libraries
{
    public static class DependencyInjection
    {
        public static void AddLibraryFeature(this IServiceCollection services)
        {
            // I Believe MediatR will find all these services?

            //services.AddTransient<IRequest<int>, CreateLibraryCommand>();
            //services.AddTransient<IRequestHandler<CreateLibraryCommand, int>, CreateLibraryCommandHandler>();

            //services.AddTransient<IRequest<List<Library>>, GetAllLibrariesQuery>();
            //services.AddTransient<IRequestHandler<GetAllLibrariesQuery, List<Library>>, GetAllLibrariesQueryHandler>();

            //services.AddTransient<IRequest<int>, ScanLibraryCommand>();
            //services.AddTransient<IRequestHandler<ScanLibraryCommand, int>, ScanLibraryCommandHandler>();

            services.AddTransient<ILibraryScanner, LibraryScanner>();

            services.AddTransient<LibraryViewModel>();
        }
    }
}