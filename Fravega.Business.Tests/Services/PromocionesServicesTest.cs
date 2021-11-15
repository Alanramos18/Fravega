using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Data.Entity;
using Fravega.Business.Services;
using Fravega.Data.Repositories.Interfaces;
using Fravega.Dto;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Telerik.JustMock;

namespace Fravega.Business.Tests.Services
{
    [TestFixture]
    public class PromocionesServicesTest
    {
        private PromocionesServices _service;
        private IPromocionesRepository _repository;
        private ILogger<PromocionesServices> _logger;
        private CancellationToken _cancellationToken;

        [OneTimeSetUp]
        public void SetUp()
        {
            _repository = Mock.Create<IPromocionesRepository>(Behavior.Strict);
            _logger = Mock.Create<ILogger<PromocionesServices>>();
            _cancellationToken = new CancellationToken();

            _service = new PromocionesServices(_repository, _logger);
        }

        [Test]
        public async Task GetListAsync_Returns_List()
        {
            //Arrange
            var list = new List<Promocion> { new Promocion() };
            Mock.Arrange(() => _repository.GetAllAsync(_cancellationToken)).Returns(Task.FromResult(list));

            //Act
            var result = await _service.GetListAsync(_cancellationToken);

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<List<Promocion>>());
            Mock.Assert(_repository);
        }

        [Test]
        public async Task GetByIdAsync_Returns_Promotion()
        {
            //Arrange
            var guid = Guid.NewGuid();
            Mock.Arrange(() => _repository.GetByIdAsync(guid, _cancellationToken)).Returns(Task.FromResult(new Promocion()));

            //Act
            var result = await _service.GetByIdAsync(guid, _cancellationToken);

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<Promocion>());
            Mock.Assert(_repository);
        }

        [Test]
        public async Task GetActivePromotions_Returns_List()
        {
            //Arrange
            var date = new DateTime(1993, 4, 18);
            IList<Promocion> list = new List<Promocion> { new Promocion() };
            Mock.Arrange(() => _repository.GetActiveAsync(date, _cancellationToken)).Returns(Task.FromResult(list));

            //Act
            var result = await _service.GetActivePromotionsAsync(date, _cancellationToken);

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<List<Promocion>>());
            Mock.Assert(_repository);
        }

        [Test]
        public async Task GetActiveForSalePromotions_Return_List()
        {
            //Arrange
            var dto = new GetSalePromotionDto();
            IEnumerable<Promocion> list = new List<Promocion> { new Promocion() };
            Mock.Arrange(() => _repository.GetActiveForSaleAsync(dto, _cancellationToken)).Returns(Task.FromResult(list));

            //Act
            var result = await _service.GetActiveForSalePromotionsAsync(dto, _cancellationToken);

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<List<Promocion>>());
            Mock.Assert(_repository);
        }

        [Test]
        public async Task CreateAsync_Return_CreatedPromotion()
        {
            //Arrange
            var dto = new PromotionDto
            { 
                Banks = new List<string> { "Galicia" },
                PaymentMethods = new List<string> { "EFECTIVO" },
                Categories = new List<string> { "Audio" },
                DiscountPorcentage = 40,
                StartDate = new DateTime(2020),
                EndDate = new DateTime(2021)
            };
            Mock.Arrange(() => _repository.CreateAsync(Arg.IsAny<Promocion>(), _cancellationToken)).Returns(Task.CompletedTask);

            //Act
            var result = await _service.CreateAsync(dto, _cancellationToken);

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<Promocion>());
            Mock.Assert(_repository);
        }

        [Test]
        public async Task UpdateAsync_Returns_Ok()
        {
            //Arrange
            var dto = new PromotionDto
            {
                Banks = new List<string> { "Galicia" },
                PaymentMethods = new List<string> { "EFECTIVO" },
                Categories = new List<string> { "Audio" },
                DiscountPorcentage = 40,
                StartDate = new DateTime(2020),
                EndDate = new DateTime(2021)
            };
            var guid = Guid.NewGuid();
            Mock.Arrange(() => _repository.UpdateAsync(guid, Arg.IsAny<Promocion>(), _cancellationToken)).Returns(Task.CompletedTask);

            //Act
            var result = await _service.UpdateAsync(guid, dto, _cancellationToken);

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<Promocion>());
            Mock.Assert(_repository);
        }

        [Test]
        public async Task DeleteAsync()
        {
            //Arrange
            var guid = Guid.NewGuid();
            Mock.Arrange(() => _repository.DeleteAsync(guid, _cancellationToken)).Returns(Task.CompletedTask);

            //Act
            await _service.DeleteAsync(guid, _cancellationToken);

            //Assert
            Mock.Assert(_repository);
        }
    }
}
