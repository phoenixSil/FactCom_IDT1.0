using Idt.Features.Dtos.Utilisateurs;
using Idt.Features.Interfaces.Services;
using Idt.Features.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Idt.Api.Controllers
{
    public class UtilisateursController : BaseApiController
    {
        private readonly IServiceDutilisateur _service;

        public UtilisateursController(IServiceDutilisateur service)
        {
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ReponseDeRequette<UtilisateurDto>>> AjouterUnUtilisateur([FromBody] UtilisateurACreerDto utilisateurDto)
        {
            var operation = await _service.AjouterUnUtilisateur(utilisateurDto).ConfigureAwait(false);
            return StatusCode(operation.StatusCode, operation.Data);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReponseDeRequette<List<UtilisateurDto>>>> LireToutesLesUtilisateurs()
        {
            var operation = await _service.LireTousLesUtilisateurs();
            return StatusCode(operation.StatusCode, operation.Data);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ReponseDeRequette<UtilisateurDto>>> ModifierUnUtilisateur(Guid id, [FromBody]UtilisateurAModifierDto utilisateurDto)
        {
            var operation = await _service.ModifierUnUtilisateur(id, utilisateurDto);
            return StatusCode(operation.StatusCode, operation.Data);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ReponseDeRequette<UtilisateurDetailDto>>> LireDetailDUnUtilisateur(Guid id)
        {
            var operation = await _service.LireDetailDUnUtilisateur(id);
            return StatusCode(operation.StatusCode, operation.Data);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ReponseDeRequette<bool>>> SupprimerUnUtilisateur(Guid id)
        {
            var operation = await _service.SupprimerUnUtilisateur(id);
            return StatusCode(operation.StatusCode, operation.Data);
        }

        [HttpPost("Connection")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ReponseDeRequette<LoggedUserDto>>> ObtenirUtilisateurParMailEtPwd(LoggingUserDto userDto)
        {
            var operation = await _service.ObtenirUtilisateurParMailEtPwd(userDto);
            return StatusCode(operation.StatusCode, operation.Data);
        }
    }
}
