using AutoFixture;
using Idt.Application.Services;
using Idt.Features.Dtos.Messages;
using Idt.Features.Responses;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Idt.Features.Core.Commandes.MessagesCommandes.Commands;
using Idt.Features.Core.Commandes.MessagesCommandes.Requettes;

namespace Idt.Application.Tests.ServicesTests
{
    public class ServiceDemessageTests
    {
        private Mock<IMediator> _mediatorMock;
        private ServiceDemessage _serviceDemessage;
        private Fixture _fixture;
        public ServiceDemessageTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _serviceDemessage = new ServiceDemessage(_mediatorMock.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task AjouterUnMessage_Should_Return_ReponseDeRequette_With_MessageDto()
        {
            // Arrange
            var messageDto = _fixture.Create<MessageACreerDto>();
            var expectedResponse = _fixture.Create<ReponseDeRequette<MessageDto>>();
            _mediatorMock.Setup(m => m.Send(It.IsAny<AjouterUnMessageCmd>(), default)).ReturnsAsync(expectedResponse);

            // Act
            var actualResponse = await _serviceDemessage.AjouterUnMessage(messageDto);

            // Assert
            actualResponse.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async Task LireTousLesMessages_Should_Return_ReponseDeRequette_With_ListMessageDto()
        {
            // Arrange
            var expectedResponse = _fixture.Create<ReponseDeRequette<List<MessageDto>>>();
            _mediatorMock.Setup(m => m.Send(It.IsAny<LireTousLesMessagesCmd>(), default)).ReturnsAsync(expectedResponse);

            // Act
            var actualResponse = await _serviceDemessage.LireTousLesMessages();

            // Assert
            actualResponse.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async Task LireDetailDUnMessage_Should_Return_ReponseDeRequette_With_MessageDetailDto()
        {
            // Arrange
            var messageId = Guid.NewGuid();
            var expectedResponse = _fixture.Create<ReponseDeRequette<MessageDetailDto>>();
            _mediatorMock.Setup(m => m.Send(It.IsAny<LireDetailDunMessageCmd>(), default)).ReturnsAsync(expectedResponse);

            // Act
            var actualResponse = await _serviceDemessage.LireDetailDUnMessage(messageId);

            // Assert
            actualResponse.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async Task ModifierUnMessage_Should_Return_ReponseDeRequette_With_MessageDto()
        {
            // Arrange
            var messageId = Guid.NewGuid();
            var messageAModifuer = _fixture.Create<MessageAModifierDto>();
            var expectedResponse = _fixture.Create<ReponseDeRequette<MessageDto>>();
            _mediatorMock.Setup(m => m.Send(It.IsAny<ModifierUnMessageCmd>(), default)).ReturnsAsync(expectedResponse);

            // Act
            var actualResponse = await _serviceDemessage.ModifierUnMessage(messageId, messageAModifuer);

            // Assert
            actualResponse.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async Task SupprimerUnMessage_Should_Return_ReponseDeRequette_With_MessageDto()
        {
            // Arrange
            var messageId = Guid.NewGuid();
            var expectedResponse = _fixture.Create<ReponseDeRequette<bool>>();
            _mediatorMock.Setup(m => m.Send(It.IsAny<SupprimerUnMessageCmd>(), default)).ReturnsAsync(expectedResponse);

            // Act
            var actualResponse = await _serviceDemessage.SupprimerUnMessage(messageId);

            // Assert
            actualResponse.Should().BeEquivalentTo(expectedResponse);
        }
    }
}