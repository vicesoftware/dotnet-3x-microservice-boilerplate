using System.Collections.Generic;
using MediatR;

namespace Api.Domain.Documents
{
    public class GetDocuments : IRequest<IEnumerable<Document>>
    { }
}