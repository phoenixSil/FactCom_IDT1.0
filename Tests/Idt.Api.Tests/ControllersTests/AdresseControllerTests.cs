using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Idt.Api.Controllers;
using Idt.Features.Dtos.Adresses;
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

namespace Idt.Api.Tests.ControllersTests
{
    public class AdresseControllerTests
    {
        private readonly Mock<IServiceDadresse> _service;
        private readonly IFixture _fixture;
        private readonly IMapper _mapper;
        private AdressesController _controller;

        public AdresseControllerTests()
        {
            _service = new Mock<IServiceDadresse>();
            _fixture = new Fixture();
            
            var configuration = new MapperConfiguration(cfg => { cfg.CreateMap<AdresseACreerDto, AdresseDto>(); });

            _mapper = configuration.CreateMapper();
            _controller = new AdressesController(_service.Object);
        }

        #region Ajouter Une Adresse

        [Fact]
        public async Task AjouterUneAdresse_ReturnsOkEtAdresseDto_QuandLesParametresDonneesSontValides()
        {
            // Arrange
            var dtoAdresse = _fixture.Create<AdresseACreerDto>();
            var expectedData = _mapper.Map<AdresseDto>(dtoAdresse);
            var resultatOperation = _fixture.Build<ReponseDeRequette<AdresseDto>>()
                .With(x => x.Data, expectedData)
                .With(x => x.StatusCode, (int)HttpStatusCode.OK)
                .Create();

            _service.Setup(x => x.AjouterUneAdresse(dtoAdresse)).ReturnsAsync(resultatOperation);

            // Act
            var resultOperation = await _controller.AjouterUneAdresse(dtoAdresse);

            // Assert
            resultOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);

            resultOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeOfType<AdresseDto>()
                .Which.Should().BeEquivalentTo(expectedData);
        }

        [Fact]
        public async Task AjouterUneAdresse_ReturnsBadRequest_QuandAdresseDtoEstInvalide()
        {
            // Arrange
            var dtoAdresse = _fixture.Build<AdresseACreerDto>()
                .With(x => x.Pays, string.Empty)
                .Create();
            AdresseDto? expectedResult = null;
            var resultatOperation = _fixture.Build<ReponseDeRequette<AdresseDto>>()
                .With(x => x.Data, expectedResult)
                .With(x => x.StatusCode, (int)HttpStatusCode.BadRequest)
                .Create();

            _service.Setup(x => x.AjouterUneAdresse(dtoAdresse)).ReturnsAsync(resultatOperation);

            // Act
            var resultOperation = await _controller.AjouterUneAdresse(dtoAdresse);

            // Assert
            resultOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

            resultOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeNull();
        }

        [Fact]
        public async Task AjouterUneAdresse_ReturnsInternalServerError_QuandUneErreurInconnuSuviensLorsDeLajout()
        {
            // Arrange
            var dtoAdresse = _fixture.Create<AdresseACreerDto>();
            AdresseDto? expectedResult = null;
            var resultatOperation = _fixture.Build<ReponseDeRequette<AdresseDto>>()
                 .With(x => x.Data, expectedResult)
                .With(x => x.StatusCode, (int)HttpStatusCode.InternalServerError)
                .Create();

            _service.Setup(x => x.AjouterUneAdresse(dtoAdresse)).ReturnsAsync(resultatOperation);

            // Act
            var resultOperation = await _controller.AjouterUneAdresse(dtoAdresse);

            // Assert
            resultOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);

            resultOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeNull();
        }

        #endregion

        #region Test Modification d'une adresse

        [Fact]
        public async Task ModifierUneAdresse_RetourneOKEtAdresseDto_QuandLesParametresSontValides()
        {
            var adresseId = _fixture.Create<Guid>();

            var adresseAModifier = _fixture.Build<AdresseAModifierDto>()
                .With(x => x.Id, adresseId)
                .Create();

            var adresseAttendu = _fixture.Build<AdresseDto>()
                .With(x => x.Pays, "Nouveau Pays")
                .Create();

            var resultatAttendu = _fixture.Build<ReponseDeRequette<AdresseDto>>()
                .With(x => x.Success, true)
                .With(x => x.Data, adresseAttendu)
                .With(x => x.StatusCode, (int)HttpStatusCode.OK)
                .Create();

            _service.Setup(x => x.ModifierUneAdresse(adresseId, adresseAModifier)).ReturnsAsync(resultatAttendu);

            var resultatOperation = await _controller.ModifierUneAdresse(adresseId, adresseAModifier);

            // Assert
            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);

            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeOfType<AdresseDto>()
                .Which.Should().BeEquivalentTo(adresseAttendu);
        }

        [Fact]
        public async Task ModifierUneAdresse_RetourneNotFoundEtNull_QuandIdNexistePas()
        {

            var adresseAModifier = _fixture.Build<AdresseAModifierDto>()
                .With(x => x.Id, Guid.NewGuid())
                .Create();

            AdresseDto? adresseAttendu = null;

            var resultatAttendu = _fixture.Build<ReponseDeRequette<AdresseDto>>()
                .With(x => x.Success, false)
                .With(x => x.Data, adresseAttendu)
                .With(x => x.StatusCode, (int)HttpStatusCode.NotFound)
                .Create();

            _service.Setup(x => x.ModifierUneAdresse(It.IsAny<Guid>(), adresseAModifier)).ReturnsAsync(resultatAttendu);

            var resultatOperation = await _controller.ModifierUneAdresse(Guid.NewGuid(), adresseAModifier);

            // Assert
            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
                
            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeNull();
        }

        [Fact]
        public async Task ModifierUneAdresse_RetourneBAdRequestEtNull_QuandIdEtAdresseIdDifferent()
        {

            var adresseAModifier = _fixture.Build<AdresseAModifierDto>()
                .With(x => x.Id, Guid.NewGuid())
                .Create();

            AdresseDto? adresseAttendu = null;

            var resultatAttendu = _fixture.Build<ReponseDeRequette<AdresseDto>>()
                .With(x => x.Success, false)
                .With(x => x.Data, adresseAttendu)
                .With(x => x.StatusCode, (int)HttpStatusCode.BadRequest)
                .Create();

            _service.Setup(x => x.ModifierUneAdresse(It.IsAny<Guid>(), adresseAModifier)).ReturnsAsync(resultatAttendu);

            var resultatOperation = await _controller.ModifierUneAdresse(Guid.NewGuid(), adresseAModifier);

            // Assert
            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeNull();
        }

        #endregion

        #region LireToutesLesAdresses

        [Fact]
        public async Task LireToutesLesAdresses_ReturnOKEtUneListeDadresse()
        {
            var resultatAttendu = _fixture.Build<ReponseDeRequette<List<AdresseDto>>>()
                .With(x => x.Success, true)
                .With(x => x.StatusCode, (int)HttpStatusCode.OK)
                .Create();
            _service.Setup(x => x.LireToutesLesAdresses()).ReturnsAsync(resultatAttendu);

            var resultatOperation = await _controller.LireToutesLesAdresses();

            resultatOperation.Result.Should().BeOfType<ObjectResult>()
               .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);

            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeOfType<List<AdresseDto>>()
                .And.BeEquivalentTo(resultatAttendu.Data);
        }

        [Fact]
        public async Task LireToutesLesAdresses_ReturnNoContent()
        {
            var resultatAttendu = _fixture.Build<ReponseDeRequette<List<AdresseDto>>>()
                .With(x => x.Data, new List<AdresseDto>())
                .With(x => x.StatusCode, (int)HttpStatusCode.NoContent)
                .Create();
            _service.Setup(x => x.LireToutesLesAdresses()).ReturnsAsync(resultatAttendu);

            var resultatOperation = await _controller.LireToutesLesAdresses();

            resultatOperation.Result.Should().BeOfType<ObjectResult>()
               .Which.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeOfType<List<AdresseDto>>()
                .And.BeEquivalentTo(resultatAttendu.Data);
        }


        #endregion

        #region Lire le detail dune Adresse

        [Fact]
        public async Task LireDetailDUneAdresse_ReturnOKEtAdresseDetailDto_QuandLesParametresSontValides()
        {
            var adresseId = Guid.NewGuid();
            var resultatAttendu = _fixture.Build<ReponseDeRequette<AdresseDetailDto>>()
                .With(x => x.StatusCode, (int)HttpStatusCode.OK)
                .Create();
            _service.Setup(x => x.LireDetailDUneAdresse(It.IsAny<Guid>())).ReturnsAsync(resultatAttendu);

            var resultatOperation = await _controller.LireDetailDUneAdresse(adresseId);

            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);

            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeOfType<AdresseDetailDto>()
                .And.BeEquivalentTo(resultatAttendu.Data);
        }

        [Fact]
        public async Task LireDetailDUneAdresse_ReturnNotFound_QuandQuandLidNesPasTrouver()
        {
            var adresseId = Guid.NewGuid();
            var resultatAttendu = _fixture.Build<ReponseDeRequette<AdresseDetailDto>>()
                .With(x => x.StatusCode, (int)HttpStatusCode.NotFound)
                .Create();
            _service.Setup(x => x.LireDetailDUneAdresse(It.IsAny<Guid>())).ReturnsAsync(resultatAttendu);

            var resultatOperation = await _controller.LireDetailDUneAdresse(adresseId);

            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        }

        [Fact]
        public async Task LireDetailDUneAdresse_ReturnBadRequest_QuandLidEstDefault()
        {
            var adresseId = Guid.Empty;
            var resultatAttendu = _fixture.Build<ReponseDeRequette<AdresseDetailDto>>()
                .With(x => x.StatusCode, (int)HttpStatusCode.BadRequest)
                .Create();
            _service.Setup(x => x.LireDetailDUneAdresse(It.IsAny<Guid>())).ReturnsAsync(resultatAttendu);

            var resultatOperation = await _controller.LireDetailDUneAdresse(adresseId);

            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

        }

        #endregion

        #region Supprimer Une Adresse

        [Fact]
        public async Task SupprimerUneAdresse_ReturnsOkEtTrue_QuandLesParametresSontValides()
        {
            var adresseId = Guid.Empty;
            var resultatAttendu = _fixture.Build<ReponseDeRequette<bool>>()
                .With(x => x.Data, true)
                .With(x => x.StatusCode, (int)HttpStatusCode.OK)
                .Create();
            _service.Setup(x => x.SupprimerUneAdresse(It.IsAny<Guid>())).ReturnsAsync(resultatAttendu);

            var resultatOperation = await _controller.SupprimerUneAdresse(adresseId);

            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);

            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeOfType<bool>()
                .And.BeEquivalentTo(true);
        }


        [Fact]
        public async Task SupprimerUneAdresse_ReturnsBadRequestFalse_QuandLesParametresNonValides()
        {
            var adresseId = Guid.Empty;
            var resultatAttendu = _fixture.Build<ReponseDeRequette<bool>>()
                .With(x => x.Data, false)
                .With(x => x.StatusCode, (int)HttpStatusCode.OK)
                .Create();
            _service.Setup(x => x.SupprimerUneAdresse(It.IsAny<Guid>())).ReturnsAsync(resultatAttendu);

            var resultatOperation = await _controller.SupprimerUneAdresse(adresseId);

            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);

            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeOfType<bool>()
                .And.BeEquivalentTo(false);
        }


        #endregion

    }
}
