using AutoFixture;
using AutoMapper;
using Idt.Api.Controllers;
using Idt.Features.Dtos.Messages;
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
    public class MessagesControllerTests
    {
        private readonly Mock<IServiceDemessage> _service;
        private readonly IFixture _fixture;
        private readonly IMapper _mapper;
        private MessagesController _controller;

        public MessagesControllerTests()
        {
            _service = new Mock<IServiceDemessage>();
            _fixture = new Fixture();

            var configuration = new MapperConfiguration(cfg => { cfg.CreateMap<MessageACreerDto, MessageDto>(); });

            _mapper = configuration.CreateMapper();
            _controller = new MessagesController(_service.Object);
        }

        #region Ajouter Une Message

        [Fact]
        public async Task AjouterUnMessage_ReturnsOkEtMessageDto_QuandLesParametresDonneesSontValides()
        {
            // Arrange
            var dtoMessage = _fixture.Create<MessageACreerDto>();
            var expectedData = _mapper.Map<MessageDto>(dtoMessage);
            var resultatOperation = _fixture.Build<ReponseDeRequette<MessageDto>>()
                .With(x => x.Data, expectedData)
                .With(x => x.StatusCode, (int)HttpStatusCode.OK)
                .Create();

            _service.Setup(x => x.AjouterUnMessage(dtoMessage)).ReturnsAsync(resultatOperation);

            // Act
            var resultOperation = await _controller.AjouterUnMessage(dtoMessage);

            // Assert
            resultOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);

            resultOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeOfType<MessageDto>()
                .Which.Should().BeEquivalentTo(expectedData);
        }

        [Fact]
        public async Task AjouterUnMessage_ReturnsBadRequest_QuandMessageDtoEstInvalide()
        {
            // Arrange
            var dtoMessage = _fixture.Build<MessageACreerDto>()
                .With(x => x.Content, string.Empty)
                .Create();
            MessageDto? expectedResult = null;
            var resultatOperation = _fixture.Build<ReponseDeRequette<MessageDto>>()
                .With(x => x.Data, expectedResult)
                .With(x => x.StatusCode, (int)HttpStatusCode.BadRequest)
                .Create();

            _service.Setup(x => x.AjouterUnMessage(dtoMessage)).ReturnsAsync(resultatOperation);

            // Act
            var resultOperation = await _controller.AjouterUnMessage(dtoMessage);

            // Assert
            resultOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

            resultOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeNull();
        }

        [Fact]
        public async Task AjouterUnMessage_ReturnsInternalServerError_QuandUneErreurInconnuSuviensLorsDeLajout()
        {
            // Arrange
            var dtoMessage = _fixture.Create<MessageACreerDto>();
            MessageDto? expectedResult = null;
            var resultatOperation = _fixture.Build<ReponseDeRequette<MessageDto>>()
                 .With(x => x.Data, expectedResult)
                .With(x => x.StatusCode, (int)HttpStatusCode.InternalServerError)
                .Create();

            _service.Setup(x => x.AjouterUnMessage(dtoMessage)).ReturnsAsync(resultatOperation);

            // Act
            var resultOperation = await _controller.AjouterUnMessage(dtoMessage);

            // Assert
            resultOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);

            resultOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeNull();
        }

        #endregion

        #region Test Modification d'une message

        [Fact]
        public async Task ModifierUnMessage_RetourneOKEtMessageDto_QuandLesParametresSontValides()
        {
            var messageId = _fixture.Create<Guid>();

            var messageAModifier = _fixture.Build<MessageAModifierDto>()
                .With(x => x.Id, messageId)
                .Create();

            var messageAttendu = _fixture.Build<MessageDto>()
                .With(x => x.Content, "Nouveau Contenu")
                .Create();

            var resultatAttendu = _fixture.Build<ReponseDeRequette<MessageDto>>()
                .With(x => x.Success, true)
                .With(x => x.Data, messageAttendu)
                .With(x => x.StatusCode, (int)HttpStatusCode.OK)
                .Create();

            _service.Setup(x => x.ModifierUnMessage(messageId, messageAModifier)).ReturnsAsync(resultatAttendu);

            var resultatOperation = await _controller.ModifierUnMessage(messageId, messageAModifier);

            // Assert
            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);

            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeOfType<MessageDto>()
                .Which.Should().BeEquivalentTo(messageAttendu);
        }

        [Fact]
        public async Task ModifierUnMessage_RetourneNotFoundEtNull_QuandIdNexistePas()
        {

            var messageAModifier = _fixture.Build<MessageAModifierDto>()
                .With(x => x.Id, Guid.NewGuid())
                .Create();

            MessageDto? messageAttendu = null;

            var resultatAttendu = _fixture.Build<ReponseDeRequette<MessageDto>>()
                .With(x => x.Success, false)
                .With(x => x.Data, messageAttendu)
                .With(x => x.StatusCode, (int)HttpStatusCode.NotFound)
                .Create();

            _service.Setup(x => x.ModifierUnMessage(It.IsAny<Guid>(), messageAModifier)).ReturnsAsync(resultatAttendu);

            var resultatOperation = await _controller.ModifierUnMessage(Guid.NewGuid(), messageAModifier);

            // Assert
            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeNull();
        }

        [Fact]
        public async Task ModifierUnMessage_RetourneBAdRequestEtNull_QuandIdEtMessageIdDifferent()
        {

            var messageAModifier = _fixture.Build<MessageAModifierDto>()
                .With(x => x.Id, Guid.NewGuid())
                .Create();

            MessageDto? messageAttendu = null;

            var resultatAttendu = _fixture.Build<ReponseDeRequette<MessageDto>>()
                .With(x => x.Success, false)
                .With(x => x.Data, messageAttendu)
                .With(x => x.StatusCode, (int)HttpStatusCode.BadRequest)
                .Create();

            _service.Setup(x => x.ModifierUnMessage(It.IsAny<Guid>(), messageAModifier)).ReturnsAsync(resultatAttendu);

            var resultatOperation = await _controller.ModifierUnMessage(Guid.NewGuid(), messageAModifier);

            // Assert
            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeNull();
        }

        #endregion

        #region LireToutesLesMessages

        [Fact]
        public async Task LireTousLesMessages_ReturnOKEtUneListeDemessage()
        {
            var resultatAttendu = _fixture.Build<ReponseDeRequette<List<MessageDto>>>()
                .With(x => x.Success, true)
                .With(x => x.StatusCode, (int)HttpStatusCode.OK)
                .Create();
            _service.Setup(x => x.LireTousLesMessages()).ReturnsAsync(resultatAttendu);

            var resultatOperation = await _controller.LireToutesLesMessages();

            resultatOperation.Result.Should().BeOfType<ObjectResult>()
               .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);

            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeOfType<List<MessageDto>>()
                .And.BeEquivalentTo(resultatAttendu.Data);
        }

        [Fact]
        public async Task LireTousLesMessages_ReturnNoContent()
        {
            var resultatAttendu = _fixture.Build<ReponseDeRequette<List<MessageDto>>>()
                .With(x => x.Data, new List<MessageDto>())
                .With(x => x.StatusCode, (int)HttpStatusCode.NoContent)
                .Create();
            _service.Setup(x => x.LireTousLesMessages()).ReturnsAsync(resultatAttendu);

            var resultatOperation = await _controller.LireToutesLesMessages();

            resultatOperation.Result.Should().BeOfType<ObjectResult>()
               .Which.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeOfType<List<MessageDto>>()
                .And.BeEquivalentTo(resultatAttendu.Data);
        }


        #endregion

        #region Lire le detail dune Message

        [Fact]
        public async Task LireDetailDUnMessage_ReturnOKEtMessageDetailDto_QuandLesParametresSontValides()
        {
            var messageId = Guid.NewGuid();
            var resultatAttendu = _fixture.Build<ReponseDeRequette<MessageDetailDto>>()
                .With(x => x.StatusCode, (int)HttpStatusCode.OK)
                .Create();
            _service.Setup(x => x.LireDetailDUnMessage(It.IsAny<Guid>())).ReturnsAsync(resultatAttendu);

            var resultatOperation = await _controller.LireDetailDUnMessage(messageId);

            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);

            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeOfType<MessageDetailDto>()
                .And.BeEquivalentTo(resultatAttendu.Data);
        }

        [Fact]
        public async Task LireDetailDUnMessage_ReturnNotFound_QuandQuandLidNesPasTrouver()
        {
            var messageId = Guid.NewGuid();
            var resultatAttendu = _fixture.Build<ReponseDeRequette<MessageDetailDto>>()
                .With(x => x.StatusCode, (int)HttpStatusCode.NotFound)
                .Create();
            _service.Setup(x => x.LireDetailDUnMessage(It.IsAny<Guid>())).ReturnsAsync(resultatAttendu);

            var resultatOperation = await _controller.LireDetailDUnMessage(messageId);

            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        }

        [Fact]
        public async Task LireDetailDUnMessage_ReturnBadRequest_QuandLidEstDefault()
        {
            var messageId = Guid.Empty;
            var resultatAttendu = _fixture.Build<ReponseDeRequette<MessageDetailDto>>()
                .With(x => x.StatusCode, (int)HttpStatusCode.BadRequest)
                .Create();
            _service.Setup(x => x.LireDetailDUnMessage(It.IsAny<Guid>())).ReturnsAsync(resultatAttendu);

            var resultatOperation = await _controller.LireDetailDUnMessage(messageId);

            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

        }

        #endregion

        #region Supprimer Une Message

        [Fact]
        public async Task SupprimerUnMessage_ReturnsOkEtTrue_QuandLesParametresSontValides()
        {
            var messageId = Guid.Empty;
            var resultatAttendu = _fixture.Build<ReponseDeRequette<bool>>()
                .With(x => x.Data, true)
                .With(x => x.StatusCode, (int)HttpStatusCode.OK)
                .Create();
            _service.Setup(x => x.SupprimerUnMessage(It.IsAny<Guid>())).ReturnsAsync(resultatAttendu);

            var resultatOperation = await _controller.SupprimerUnMessage(messageId);

            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);

            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeOfType<bool>()
                .And.BeEquivalentTo(true);
        }


        [Fact]
        public async Task SupprimerUnMessage_ReturnsBadRequestFalse_QuandLesParametresNonValides()
        {
            var messageId = Guid.Empty;
            var resultatAttendu = _fixture.Build<ReponseDeRequette<bool>>()
                .With(x => x.Data, false)
                .With(x => x.StatusCode, (int)HttpStatusCode.OK)
                .Create();
            _service.Setup(x => x.SupprimerUnMessage(It.IsAny<Guid>())).ReturnsAsync(resultatAttendu);

            var resultatOperation = await _controller.SupprimerUnMessage(messageId);

            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);

            resultatOperation.Result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeOfType<bool>()
                .And.BeEquivalentTo(false);
        }


        #endregion

    }
}