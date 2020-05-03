using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Api.Domain.Documents
{
    public class HandleGetDocuments : IRequestHandler<GetDocuments, IEnumerable<Document>>
    {
        public Task<IEnumerable<Document>> Handle(GetDocuments request, CancellationToken cancellationToken)
        {
            var documents = new List<Document> {
                new Document{
                    Id= Guid.NewGuid(),
                    Name = "Foo"
                },
                new Document{
                    Id= Guid.NewGuid(),
                    Name = "Bar"
                }, 
            };

            return Task.FromResult((IEnumerable<Document>)documents);
        }
    }
}