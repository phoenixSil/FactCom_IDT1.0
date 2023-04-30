using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Idt.Application.Services;
using Idt.Features.Core.Commandes.AdressesCommandes.Commands;
using Idt.Features.Core.Commandes.AdressesCommandes.Requettes;
using Idt.Features.Dtos.Adresses;
using Idt.Features.Responses;
using MediatR;
using Moq;

namespace Idt.Application.Tests.ServicesTests
{
    public class ServiceDadresseTests
    {

        private Mock<IMediator> _mediatorMock;
        private ServiceDadresse _serviceDadresse;
        private Fixture _fixture;
        public ServiceDadresseTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _serviceDadresse = new ServiceDadresse(_mediatorMock.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task AjouterUneAdresse_Should_Return_ReponseDeRequette_With_AdresseDto()
        {
            // Arrange
            var adresseDto = _fixture.Create<AdresseACreerDto>();
            var expectedResponse = _fixture.Create<ReponseDeRequette<AdresseDto>>();
            _mediatorMock.Setup(m => m.Send(It.IsAny<AjouterUneAdresseCmd>(), default)).ReturnsAsync(expectedResponse);

            // Act
            var actualResponse = await _serviceDadresse.AjouterUneAdresse(adresseDto);

            // Assert
            actualResponse.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async Task LireToutesLesAdresses_Should_Return_ReponseDeRequette_With_ListAdresseDto()
        {
            // Arrange
            var expectedResponse = _fixture.Create<ReponseDeRequette<List<AdresseDto>>>();
            _mediatorMock.Setup(m => m.Send(It.IsAny<LireToutesLesAdressesCmd>(), default)).ReturnsAsync(expectedResponse);

            // Act
            var actualResponse = await _serviceDadresse.LireToutesLesAdresses();

            // Assert
            actualResponse.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async Task LireDetailDUneAdresse_Should_Return_ReponseDeRequette_With_AdresseDetailDto()
        {
            // Arrange
            var adresseId = Guid.NewGuid();
            var expectedResponse = _fixture.Create<ReponseDeRequette<AdresseDetailDto>>();
            _mediatorMock.Setup(m => m.Send(It.IsAny<LireDetailDuneAdresseCmd>(), default)).ReturnsAsync(expectedResponse);

            // Act
            var actualResponse = await _serviceDadresse.LireDetailDUneAdresse(adresseId);

            // Assert
            actualResponse.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async Task ModifierUneAdresse_Should_Return_ReponseDeRequette_With_AdresseDto()
        {
            // Arrange
            var adresseId = Guid.NewGuid();
            var adresseAModifuer = _fixture.Create<AdresseAModifierDto>();
            var expectedResponse = _fixture.Create<ReponseDeRequette<AdresseDto>>();
            _mediatorMock.Setup(m => m.Send(It.IsAny<ModifierUneAdresseCmd>(), default)).ReturnsAsync(expectedResponse);

            // Act
            var actualResponse = await _serviceDadresse.ModifierUneAdresse(adresseId, adresseAModifuer);

            // Assert
            actualResponse.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async Task SupprimerUneAdresse_Should_Return_ReponseDeRequette_With_AdresseDto()
        {
            // Arrange
            var adresseId = Guid.NewGuid();
            var expectedResponse = _fixture.Create<ReponseDeRequette<bool>>();
            _mediatorMock.Setup(m => m.Send(It.IsAny<SupprimerUneAdresseCmd>(), default)).ReturnsAsync(expectedResponse);

            // Act
            var actualResponse = await _serviceDadresse.SupprimerUneAdresse(adresseId);

            // Assert
            actualResponse.Should().BeEquivalentTo(expectedResponse);
        }
    }
}
