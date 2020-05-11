using System.Collections.Generic;
using Api.Domain.Documents;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentsController : ControllerBase
    {
        private readonly ILogger<DocumentsController> _logger;
        private readonly IMediator _mediator;

        public DocumentsController(ILogger<DocumentsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Document>> Get()q
        {
            var result = _mediator.Send(new GetDocuments());
            return new OkObjectResult(result);
        }
    }
}