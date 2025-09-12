using ExlaqiNasiri.Application.Features.Command.Article.Cretate;
using ExlaqiNasiri.Application.Features.Command.Article.Delete;
using ExlaqiNasiri.Application.Features.Command.Article.Update;
using ExlaqiNasiri.Application.Features.Query.Article.GetAll;
using ExlaqiNasiri.Application.Features.Query.Article.GetSingle;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExlaqiNasiri.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        readonly IMediator _mediator;

        public ArticleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetAllArticleQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleAsync([FromRoute] Guid id)
        {
            var request = new GetSingleArticleQueryRequest { Id = id };
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> CreateAsync([FromForm] CreateArticleCommondRequest request)
        {
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var request = new DeleteArticleCommandRequest { Id = id };
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> UpdateAsync([FromForm] UpdateArticleCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }
    }
}
