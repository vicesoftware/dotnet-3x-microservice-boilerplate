using Api.Domain.Documents;
using MediatR;

namespace Api.Controllers
{
    public class GetDocuments
    {
        public class Ping : IRequest<Document> { }
    }
}