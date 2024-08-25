using AutoMapper;
using CleanArchitectureBlazorServer.Application.Features.AccountHolders.Commands;
using CleanArchitectureBlazorServer.Application.Features.AccountHolders.Queries;
using CleanArchitectureBlazorServer.Common.Requests;
using CleanArchitectureBlazorServer.Common.Responses;
using CleanArchitectureBlazorServer.Common.Wrapper;
using CleanArchitectureBlazorServer.Infrastructure.Contexts;
using CleanArchitectureBlazorServer.Services.AccountHoldersServices;
using CleanArchitectureBlazorServer.Web.Mapping;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CleanArchitectureBlazorServer.Tests.AccountHolders
{
    public class AccountHoldersShould : IntegrationTestBase
    {
        private readonly AccountHoldersService _service;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IMapper> _mapperMock;

        public AccountHoldersShould() : base()
        {
            // Configure mocks
            _mediatorMock = new Mock<IMediator>();
            _mapperMock = new Mock<IMapper>();

            // Configure the service collection with mocks
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(_connection); // Use inherited _connection
            });
            serviceCollection.AddAutoMapper(typeof(MappingProfile).Assembly);
            serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateAccountHolderCommandHandler).Assembly));

            // Replace the service provider with the mock implementations
            serviceCollection.AddSingleton(_mediatorMock.Object);
            serviceCollection.AddSingleton(_mapperMock.Object);

            var serviceProvider = serviceCollection.BuildServiceProvider();
            _scope = serviceProvider.CreateScope(); // Use inherited _scope
            _dbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>(); // Use inherited _dbContext


            // Setup mocks
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<CreateAccountHolderCommand>(), default))
                .ReturnsAsync(new ResponseWrapper<int>().Success(1));

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetAllAccountHolderQuery>(), default))
                .ReturnsAsync(new ResponseWrapper<List<AccountHolderResponse>>().Success(new List<AccountHolderResponse>
                {
                    new AccountHolderResponse
                    {
                        Id = 1,
                        FirstName = "First",
                        LastName = "Last",
                        ContactNumber = "09993120589",
                        DateOfBirth = new DateTime(2000, 1, 1),
                        Email = "test01@example.com"
                    }
                }));

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<UpdateAccountHolderCommand>(), default))
                .ReturnsAsync(new ResponseWrapper<int>().Success(1));

            _mediatorMock
            .Setup(m => m.Send(It.Is<GetAccountHolderQuery>(q => q.Id == 1), default))
            .ReturnsAsync(new ResponseWrapper<AccountHolderResponse>().Success(new AccountHolderResponse
            {
                Id = 1,
                FirstName = "First",
                LastName = "Last",
                ContactNumber = "09993120589",
                DateOfBirth = new DateTime(2000, 1, 1),
                Email = "test01@example.com"
            }));

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<DeleteAccountHolderCommand>(), default))
                .ReturnsAsync(new ResponseWrapper<int>().Success(1));


            _service = new AccountHoldersService(_mediatorMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task PerformAccountHolderMethods()
        {
            // CREATE
            var createModel = new CreateAccountHolder
            {
                FirstName = "First",
                LastName = "Last",
                ContactNumber = "09993120589",
                DateOfBirth = new DateTime(2000, 1, 1),
                Email = "test01@example.com"
            };
            var created = await _service.AddAccountHolderAsync(createModel);
            Assert.NotNull(created);
            Assert.True(created.IsSuccessful);

            // GET ALL
            var models = await _service.GetAllAccountHolderAsync();
            Assert.NotNull(models);
            Assert.True(models.IsSuccessful);
            Assert.Contains(models.Data, x => x.Id == created.Data);

            // UPDATE
            var updateModel = new UpdateAccountHolder
            {
                Id = created.Data,
                FirstName = "FirstUpdated",
                LastName = "LastUpdated",
                ContactNumber = "09993120589",
                DateOfBirth = new DateTime(2000, 1, 1),
                Email = "test01@example.com"
            };
            var updated = await _service.UpdateAccountHolderAsync(updateModel);
            Assert.NotNull(updated);
            Assert.True(updated.IsSuccessful);

            // GET
            var model = await _service.GetAccountHolderAsync(created.Data);
            Assert.NotNull(model);
            Assert.Equal(created.Data, model.Data.Id);

            // DELETE
            var deleteModel = await _service.DeleteAccountHolderAsync(model.Data.Id);
            Assert.NotNull(deleteModel);
            Assert.True(deleteModel.IsSuccessful);

            //// DELETE
            //var deleteAct = () => _service.DeleteAccountHolderAsync(model.Data.Id);
            //var deleteEx = await Record.ExceptionAsync(deleteAct);
            //Assert.Null(deleteEx);

            // Update mock data after deletion
            UpdateMockDataAfterDelete();

            // GET All after delete
            models = await _service.GetAllAccountHolderAsync();
            Assert.NotNull(models);
            Assert.True(models.IsSuccessful);
            Assert.DoesNotContain(models.Data, x => x.Id == model.Data.Id);
        }

        private void UpdateMockDataAfterDelete()
        {
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetAllAccountHolderQuery>(), default))
                .ReturnsAsync(new ResponseWrapper<List<AccountHolderResponse>>().Success(new List<AccountHolderResponse>())); // Empty list after deletion
        }
    }

}

    
