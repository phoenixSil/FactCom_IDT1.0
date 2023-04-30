using Idt.Features.Dtos.Messages;
using Idt.Features.Interfaces.Services;
using Idt.Features.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Idt.Api.Controllers
{
    public class MessagesController : BaseApiController
    {
        private readonly IServiceDemessage _service;

        public MessagesController(IServiceDemessage service)
        {
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ReponseDeRequette<MessageDto>>> AjouterUnMessage([FromBody] MessageACreerDto messageDto)
        {
            var operation = await _service.AjouterUnMessage(messageDto).ConfigureAwait(false);
            return StatusCode(operation.StatusCode, operation.Data);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReponseDeRequette<List<MessageDto>>>> LireToutesLesMessages()
        {
            var operation = await _service.LireTousLesMessages();
            return StatusCode(operation.StatusCode, operation.Data);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ReponseDeRequette<MessageDto>>> ModifierUnMessage(Guid id, [FromBody] MessageAModifierDto messageDto)
        {
            var operation = await _service.ModifierUnMessage(id, messageDto);
            return StatusCode(operation.StatusCode, operation.Data);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ReponseDeRequette<MessageDetailDto>>> LireDetailDUnMessage(Guid id)
        {
            var operation = await _service.LireDetailDUnMessage(id);
            return StatusCode(operation.StatusCode, operation.Data);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ReponseDeRequette<bool>>> SupprimerUnMessage(Guid id)
        {
            var operation = await _service.SupprimerUnMessage(id);
            return StatusCode(operation.StatusCode, operation.Data);
        }
    }
}
