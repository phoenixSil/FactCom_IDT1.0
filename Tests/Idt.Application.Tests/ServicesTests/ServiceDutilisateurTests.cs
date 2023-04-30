using AutoFixture;
using Idt.Application.Services;
using Idt.Features.Dtos.Utilisateurs;
using Idt.Features.Responses;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Idt.Features.Core.Commandes.UtilisateursCommandes.Commands;
using Idt.Features.Core.Commandes.UtilisateursCommandes.Requettes;

namespace Idt.Application.Tests.ServicesTests
{
    public class ServiceDutilisateurTests
    {
        private Mock<IMediator> _mediatorMock;
        private ServiceDutilisateur _serviceDutilisateur;
        private Fixture _fixture;
        public ServiceDutilisateurTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _serviceDutilisateur = new ServiceDutilisateur(_mediatorMock.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task AjouterUnUtilisateur_Should_Return_ReponseDeRequette_With_UtilisateurDto()
        {
            // Arrange
            var utilisateurDto = _fixture.Create<UtilisateurACreerDto>();
            var expectedResponse = _fixture.Create<ReponseDeRequette<UtilisateurDto>>();
            _mediatorMock.Setup(m => m.Send(It.IsAny<AjouterUnUtilisateurCmd>(), default)).ReturnsAsync(expectedResponse);

            // Act
            var actualResponse = await _serviceDutilisateur.AjouterUnUtilisateur(utilisateurDto);

            // Assert
            actualResponse.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async Task LireTousLesUtilisateurs_Should_Return_ReponseDeRequette_With_ListUtilisateurDto()
        {
            // Arrange
            var expectedResponse = _fixture.Create<ReponseDeRequette<List<UtilisateurDto>>>();
            _mediatorMock.Setup(m => m.Send(It.IsAny<LireTousLesUtilisateursCmd>(), default)).ReturnsAsync(expectedResponse);

            // Act
            var actualResponse = await _serviceDutilisateur.LireTousLesUtilisateurs();

            // Assert
            actualResponse.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async Task LireDetailDUnUtilisateur_Should_Return_ReponseDeRequette_With_UtilisateurDetailDto()
        {
            // Arrange
            var utilisateurId = Guid.NewGuid();
            var expectedResponse = _fixture.Create<ReponseDeRequette<UtilisateurDetailDto>>();
            _mediatorMock.Setup(m => m.Send(It.IsAny<LireDetailDunUtilisateurCmd>(), default)).ReturnsAsync(expectedResponse);

            // Act
            var actualResponse = await _serviceDutilisateur.LireDetailDUnUtilisateur(utilisateurId);

            // Assert
            actualResponse.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async Task ModifierUnUtilisateur_Should_Return_ReponseDeRequette_With_UtilisateurDto()
        {
            // Arrange
            var utilisateurId = Guid.NewGuid();
            var utilisateurAModifuer = _fixture.Create<UtilisateurAModifierDto>();
            var expectedResponse = _fixture.Create<ReponseDeRequette<UtilisateurDto>>();
            _mediatorMock.Setup(m => m.Send(It.IsAny<ModifierUnUtilisateurCmd>(), default)).ReturnsAsync(expectedResponse);

            // Act
            var actualResponse = await _serviceDutilisateur.ModifierUnUtilisateur(utilisateurId, utilisateurAModifuer);

            // Assert
            actualResponse.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async Task SupprimerUnUtilisateur_Should_Return_ReponseDeRequette_With_UtilisateurDto()
        {
            // Arrange
            var utilisateurId = Guid.NewGuid();
            var expectedResponse = _fixture.Create<ReponseDeRequette<bool>>();
            _mediatorMock.Setup(m => m.Send(It.IsAny<SupprimerUnUtilisateurCmd>(), default)).ReturnsAsync(expectedResponse);

            // Act
            var actualResponse = await _serviceDutilisateur.SupprimerUnUtilisateur(utilisateurId);

            // Assert
            actualResponse.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async Task ObtenirUtilisateurParMailEtPwd_Should_Return_ReponseDeRequette_With_LoggedUserDto()
        {
            // Arrange
            var body = _fixture.Create<LoggingUserDto>();
            var expectedResponse = _fixture.Create<ReponseDeRequette<LoggedUserDto>>();
            _mediatorMock.Setup(m => m.Send(It.IsAny<ObtenirUtilisateurParEmalPwdCmd>(), default)).ReturnsAsync(expectedResponse);

            // Act
            var actualResponse = await _serviceDutilisateur.ObtenirUtilisateurParMailEtPwd(body);

            // Assert
            actualResponse.Should().BeEquivalentTo(expectedResponse);
        }
    }
}