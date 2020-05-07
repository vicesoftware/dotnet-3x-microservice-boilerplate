using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Api.Domain.Documents
{
    public class HandleGetDocuments : IRequestHandler<GetDocuments, IEnumerable<Document>>
    {
        private readonly ViceContext _context;

        public HandleGetDocuments(ViceContext context)
        {
            _context = context;
        }
        public Task<IEnumerable<Document>> Handle(GetDocuments request, CancellationToken cancellationToken)
        {
            var documents = _context.Documents.ToList();

            return Task.FromResult((IEnumerable<Document>)documents);
        }
    }
}