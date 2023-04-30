using Idt.Features.Dtos.Adresses;
using Idt.Features.Interfaces.Services;
using Idt.Features.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Idt.Api.Controllers
{
    public class AdressesController : BaseApiController
    {
        private readonly IServiceDadresse _service;

        public AdressesController(IServiceDadresse service)
        {
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AdresseDto>> AjouterUneAdresse([FromBody]AdresseACreerDto adresseDto)
        {
            var operation = await _service.AjouterUneAdresse(adresseDto).ConfigureAwait(false);
            return StatusCode(operation.StatusCode, operation.Data);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<AdresseDto>>> LireToutesLesAdresses()
        {
            var operation = await _service.LireToutesLesAdresses();
            return StatusCode(operation.StatusCode, operation.Data);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AdresseDto>> ModifierUneAdresse(Guid id, AdresseAModifierDto adresseDto)
        {
            var operation = await _service.ModifierUneAdresse(id, adresseDto);
            return StatusCode(operation.StatusCode, operation.Data);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AdresseDetailDto>> LireDetailDUneAdresse(Guid id)
        {
            var operation = await _service.LireDetailDUneAdresse(id);
            return StatusCode(operation.StatusCode, operation.Data);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> SupprimerUneAdresse(Guid id)
        {
            var operation = await _service.SupprimerUneAdresse(id);
            return StatusCode(operation.StatusCode, operation.Data);
        }
    }
}
