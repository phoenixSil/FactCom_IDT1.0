using AutoFixture;
using AutoMapper;
using Idt.Api.Controllers;
using Idt.Features.Dtos.Utilisateurs;
using Idt.Features.Interfaces.Services;
using Idt.Features.Responses;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace Idt.Api.Tests.ControllersTests
{
    public class UtilisateursControllerTests
    {
        private readonly Mock<IServiceDutilisateur> _service;
        private readonly IFixture _fixture;
        private readonly IMapper _mapper;
        private UtilisateursController _controller;

        public UtilisateursControllerTests()
        {
            _service = new Mock<IServiceDutilisateur>();
            _fixture = new Fixture();

            var configuration = new MapperConfiguration(cfg => { cfg.CreateMap<UtilisateurACreerDto, UtilisateurDto>(); });

            _mapper = configuration.CreateMapper();
            _controller = new UtilisateursController(_service.Object);
        }

        #region Ajouter Une Utilisateur

        [Fact]
        public async Task AjouterUnUtilisateur_ReturnsOkEtUtilisateurDto_QuandLesParametresDonneesSontValides()
        {
            // Arrange
            var dtoUtilisateur = _fixture.Create<UtilisateurACreerDto>();
            var expectedData = _mapper.Map<UtilisateurDto>(dtoUtilisateur);
            var resultatOperation = _fixture.Build<ReponseDeRequette<UtilisateurDto>>()
                .With(x => x.Data, expectedData)
                .With(x => x.StatusCode, (int)HttpStatusCode.OK)
                .Create();

            _service.Setup(x => x.AjouterUnUtilisateur(dtoUtilisateur)).ReturnsAsync(resultatOperation);

            // Act
            var resultOperation = await _controller.AjouterUnUtilisateur(dtoUtilisateur);

            // Assert
            resultOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);

            resultOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeOfType<UtilisateurDto>()
                .Which.Should().BeEquivalentTo(expectedData);
        }

        [Fact]
        public async Task AjouterUnUtilisateur_ReturnsBadRequest_QuandUtilisateurDtoEstInvalide()
        {
            // Arrange
            var dtoUtilisateur = _fixture.Build<UtilisateurACreerDto>()
                .With(x => x.Nom, string.Empty)
                .Create();
            UtilisateurDto? expectedResult = null;
            var resultatOperation = _fixture.Build<ReponseDeRequette<UtilisateurDto>>()
                .With(x => x.Data, expectedResult)
                .With(x => x.StatusCode, (int)HttpStatusCode.BadRequest)
                .Create();

            _service.Setup(x => x.AjouterUnUtilisateur(dtoUtilisateur)).ReturnsAsync(resultatOperation);

            // Act
            var resultOperation = await _controller.AjouterUnUtilisateur(dtoUtilisateur);

            // Assert
            resultOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

            resultOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeNull();
        }

        [Fact]
        public async Task AjouterUnUtilisateur_ReturnsInternalServerError_QuandUneErreurInconnuSuviensLorsDeLajout()
        {
            // Arrange
            var dtoUtilisateur = _fixture.Create<UtilisateurACreerDto>();
            UtilisateurDto? expectedResult = null;
            var resultatOperation = _fixture.Build<ReponseDeRequette<UtilisateurDto>>()
                 .With(x => x.Data, expectedResult)
                .With(x => x.StatusCode, (int)HttpStatusCode.InternalServerError)
                .Create();

            _service.Setup(x => x.AjouterUnUtilisateur(dtoUtilisateur)).ReturnsAsync(resultatOperation);

            // Act
            var resultOperation = await _controller.AjouterUnUtilisateur(dtoUtilisateur);

            // Assert
            resultOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);

            resultOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeNull();
        }

        #endregion

        #region Test Modification d'une utilisateur

        [Fact]
        public async Task ModifierUnUtilisateur_RetourneOKEtUtilisateurDto_QuandLesParametresSontValides()
        {
            var utilisateurId = _fixture.Create<Guid>();

            var utilisateurAModifier = _fixture.Build<UtilisateurAModifierDto>()
                .With(x => x.Id, utilisateurId)
                .Create();

            var utilisateurAttendu = _fixture.Build<UtilisateurDto>()
                .With(x => x.Nom, "Nouveau Nom")
                .Create();

            var resultatAttendu = _fixture.Build<ReponseDeRequette<UtilisateurDto>>()
                .With(x => x.Success, true)
                .With(x => x.Data, utilisateurAttendu)
                .With(x => x.StatusCode, (int)HttpStatusCode.OK)
                .Create();

            _service.Setup(x => x.ModifierUnUtilisateur(utilisateurId, utilisateurAModifier)).ReturnsAsync(resultatAttendu);

            var resultatOperation = await _controller.ModifierUnUtilisateur(utilisateurId, utilisateurAModifier);

            // Assert
            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);

            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeOfType<UtilisateurDto>()
                .Which.Should().BeEquivalentTo(utilisateurAttendu);
        }

        [Fact]
        public async Task ModifierUnUtilisateur_RetourneNotFoundEtNull_QuandIdNexistePas()
        {

            var utilisateurAModifier = _fixture.Build<UtilisateurAModifierDto>()
                .With(x => x.Id, Guid.NewGuid())
                .Create();

            UtilisateurDto? utilisateurAttendu = null;

            var resultatAttendu = _fixture.Build<ReponseDeRequette<UtilisateurDto>>()
                .With(x => x.Success, false)
                .With(x => x.Data, utilisateurAttendu)
                .With(x => x.StatusCode, (int)HttpStatusCode.NotFound)
                .Create();

            _service.Setup(x => x.ModifierUnUtilisateur(It.IsAny<Guid>(), utilisateurAModifier)).ReturnsAsync(resultatAttendu);

            var resultatOperation = await _controller.ModifierUnUtilisateur(Guid.NewGuid(), utilisateurAModifier);

            // Assert
            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeNull();
        }

        [Fact]
        public async Task ModifierUnUtilisateur_RetourneBAdRequestEtNull_QuandIdEtUtilisateurIdDifferent()
        {

            var utilisateurAModifier = _fixture.Build<UtilisateurAModifierDto>()
                .With(x => x.Id, Guid.NewGuid())
                .Create();

            UtilisateurDto? utilisateurAttendu = null;

            var resultatAttendu = _fixture.Build<ReponseDeRequette<UtilisateurDto>>()
                .With(x => x.Success, false)
                .With(x => x.Data, utilisateurAttendu)
                .With(x => x.StatusCode, (int)HttpStatusCode.BadRequest)
                .Create();

            _service.Setup(x => x.ModifierUnUtilisateur(It.IsAny<Guid>(), utilisateurAModifier)).ReturnsAsync(resultatAttendu);

            var resultatOperation = await _controller.ModifierUnUtilisateur(Guid.NewGuid(), utilisateurAModifier);

            // Assert
            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeNull();
        }

        #endregion

        #region LireTousLesUtilisateurs

        [Fact]
        public async Task LireToutesLesUtilisateurs_ReturnOKEtUneListeDutilisateur()
        {
            var resultatAttendu = _fixture.Build<ReponseDeRequette<List<UtilisateurDto>>>()
                .With(x => x.Success, true)
                .With(x => x.StatusCode, (int)HttpStatusCode.OK)
                .Create();
            _service.Setup(x => x.LireTousLesUtilisateurs()).ReturnsAsync(resultatAttendu);

            var resultatOperation = await _controller.LireToutesLesUtilisateurs();

            resultatOperation.Result.Should().BeOfType<ObjectResult>()
               .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);

            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeOfType<List<UtilisateurDto>>()
                .And.BeEquivalentTo(resultatAttendu.Data);
        }

        [Fact]
        public async Task LireToutesLesUtilisateurs_ReturnNoContent()
        {
            var resultatAttendu = _fixture.Build<ReponseDeRequette<List<UtilisateurDto>>>()
                .With(x => x.Data, new List<UtilisateurDto>())
                .With(x => x.StatusCode, (int)HttpStatusCode.NoContent)
                .Create();
            _service.Setup(x => x.LireTousLesUtilisateurs()).ReturnsAsync(resultatAttendu);

            var resultatOperation = await _controller.LireToutesLesUtilisateurs();

            resultatOperation.Result.Should().BeOfType<ObjectResult>()
               .Which.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeOfType<List<UtilisateurDto>>()
                .And.BeEquivalentTo(resultatAttendu.Data);
        }


        #endregion

        #region Lire le detail dune Utilisateur

        [Fact]
        public async Task LireDetailDUnUtilisateur_ReturnOKEtUtilisateurDetailDto_QuandLesParametresSontValides()
        {
            var utilisateurId = Guid.NewGuid();
            var resultatAttendu = _fixture.Build<ReponseDeRequette<UtilisateurDetailDto>>()
                .With(x => x.StatusCode, (int)HttpStatusCode.OK)
                .Create();
            _service.Setup(x => x.LireDetailDUnUtilisateur(It.IsAny<Guid>())).ReturnsAsync(resultatAttendu);

            var resultatOperation = await _controller.LireDetailDUnUtilisateur(utilisateurId);

            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);

            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeOfType<UtilisateurDetailDto>()
                .And.BeEquivalentTo(resultatAttendu.Data);
        }

        [Fact]
        public async Task LireDetailDUnUtilisateur_ReturnNotFound_QuandQuandLidNesPasTrouver()
        {
            var utilisateurId = Guid.NewGuid();
            var resultatAttendu = _fixture.Build<ReponseDeRequette<UtilisateurDetailDto>>()
                .With(x => x.StatusCode, (int)HttpStatusCode.NotFound)
                .Create();
            _service.Setup(x => x.LireDetailDUnUtilisateur(It.IsAny<Guid>())).ReturnsAsync(resultatAttendu);

            var resultatOperation = await _controller.LireDetailDUnUtilisateur(utilisateurId);

            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        }

        [Fact]
        public async Task LireDetailDUnUtilisateur_ReturnBadRequest_QuandLidEstDefault()
        {
            var utilisateurId = Guid.Empty;
            var resultatAttendu = _fixture.Build<ReponseDeRequette<UtilisateurDetailDto>>()
                .With(x => x.StatusCode, (int)HttpStatusCode.BadRequest)
                .Create();
            _service.Setup(x => x.LireDetailDUnUtilisateur(It.IsAny<Guid>())).ReturnsAsync(resultatAttendu);

            var resultatOperation = await _controller.LireDetailDUnUtilisateur(utilisateurId);

            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

        }

        #endregion

        #region Supprimer Une Utilisateur

        [Fact]
        public async Task SupprimerUnUtilisateur_ReturnsOkEtTrue_QuandLesParametresSontValides()
        {
            var utilisateurId = Guid.Empty;
            var resultatAttendu = _fixture.Build<ReponseDeRequette<bool>>()
                .With(x => x.Data, true)
                .With(x => x.StatusCode, (int)HttpStatusCode.OK)
                .Create();
            _service.Setup(x => x.SupprimerUnUtilisateur(It.IsAny<Guid>())).ReturnsAsync(resultatAttendu);

            var resultatOperation = await _controller.SupprimerUnUtilisateur(utilisateurId);

            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);

            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeOfType<bool>()
                .And.BeEquivalentTo(true);
        }


        [Fact]
        public async Task SupprimerUnUtilisateur_ReturnsBadRequestFalse_QuandLesParametresNonValides()
        {
            var utilisateurId = Guid.Empty;
            var resultatAttendu = _fixture.Build<ReponseDeRequette<bool>>()
                .With(x => x.Data, false)
                .With(x => x.StatusCode, (int)HttpStatusCode.OK)
                .Create();
            _service.Setup(x => x.SupprimerUnUtilisateur(It.IsAny<Guid>())).ReturnsAsync(resultatAttendu);

            var resultatOperation = await _controller.SupprimerUnUtilisateur(utilisateurId);

            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);

            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeOfType<bool>()
                .And.BeEquivalentTo(false);
        }


        #endregion

        #region Obtenir Utilisateur Par Email et MotDePasse

        [Fact]
        public async Task ObtenirUtilisateurParEmailMotDePasse_returnOKEtLoggedUserDto_QuandLesParametresSontOk()
        {
            var parametredentree = new LoggingUserDto
            {
                Email = "xxx@ymail.com",
                Password = "password"
            };
            var resultatAttendu = new ReponseDeRequette<LoggedUserDto>
            {
                Data = new LoggedUserDto
                {
                    Email = "xxx@ymail.com",
                    Id = Guid.NewGuid(),
                    Nom = "xxxx",
                    Prenom = "xxxxxx"
                },
                Errors = null,
                StatusCode = (int)HttpStatusCode.OK,
                Success = true
            };
                
            _service.Setup(x => x.ObtenirUtilisateurParMailEtPwd(It.IsAny<LoggingUserDto>())).ReturnsAsync(resultatAttendu);

            var resultatOperation = await _controller.ObtenirUtilisateurParMailEtPwd(parametredentree);

            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);

            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeOfType<LoggedUserDto>()
                .And.BeEquivalentTo(resultatAttendu.Data);
        }

        [Fact]
        public async Task ObtenirUtilisateurParEmailMotDePasse_returnNotFound()
        {
            var parametredentree = new LoggingUserDto
            {
                Email = "xxx@ymail.com",
                Password = "password"
            };
            var resultatAttendu = new ReponseDeRequette<LoggedUserDto>
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                Success = false
            };

            _service.Setup(x => x.ObtenirUtilisateurParMailEtPwd(It.IsAny<LoggingUserDto>())).ReturnsAsync(resultatAttendu);

            var resultatOperation = await _controller.ObtenirUtilisateurParMailEtPwd(parametredentree);

            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ObtenirUtilisateurParEmailMotDePasse_returnBadRequest()
        {
            var parametredentree = new LoggingUserDto
            {
                Email = "xxx@ymail.com",
                Password = "password"
            };
            var resultatAttendu = new ReponseDeRequette<LoggedUserDto>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Success = false
            };

            _service.Setup(x => x.ObtenirUtilisateurParMailEtPwd(It.IsAny<LoggingUserDto>())).ReturnsAsync(resultatAttendu);

            var resultatOperation = await _controller.ObtenirUtilisateurParMailEtPwd(parametredentree);

            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ObtenirUtilisateurParEmailMotDePasse_returnUNauthorized()
        {
            var parametredentree = new LoggingUserDto
            {
                Email = "xxx@ymail.com",
                Password = "password"
            };
            var resultatAttendu = new ReponseDeRequette<LoggedUserDto>
            {
                StatusCode = (int)HttpStatusCode.Unauthorized,
                Success = false
            };

            _service.Setup(x => x.ObtenirUtilisateurParMailEtPwd(It.IsAny<LoggingUserDto>())).ReturnsAsync(resultatAttendu);

            var resultatOperation = await _controller.ObtenirUtilisateurParMailEtPwd(parametredentree);

            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.Unauthorized);
        }


        #endregion

    }


}
