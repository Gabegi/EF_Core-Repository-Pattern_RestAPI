using BrandApplication.Business.Handlers.Commands;
using BrandApplication.Business.Services.IServices;
using MediatR;

namespace BrandApplication.Business.Handlers.Handlers
{
    public class GetByIdHandler<T> : IRequestHandler<GetByIdCommand<T>> where T : class
    {
        public GetByIdHandler()
        {
            
        }
        public Task Handle(GetByIdCommand<T> request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
