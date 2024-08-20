using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using Microsoft.Extensions.Configuration;
using TestTaskVmarmysh.Common.Exceptions;
using TestTaskVmarmysh.DataAccess.Context;
using TestTaskVmarmysh.DataAccess.Entities.TreeEntities;
using TestTaskVmarmysh.DataAccess.Interfaces;
using TestTaskVmarmysh.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace TestTaskVmarmysh.DataAccess.UnitTests
{
    public class TreeRepositoryTests
    {
        Fixture _fixture;
        Mock<ILogger<TreeRepository>> _logger;
        ITreeRepository _treeRepository;
        TreeContext _treeContext;
        ServiceProvider _serviceProvider;

        public TreeRepositoryTests()
        {
            _fixture = new Fixture();
            var configuration = new Mock<IConfiguration>();
            var configurationSection = new Mock<IConfigurationSection>();
            configurationSection.Setup(section => section[It.IsAny<string>()]).Returns("test connection string");
            configuration.Setup(config => config.GetSection(It.IsAny<string>())).Returns(configurationSection.Object);
            var services = new ServiceCollection();
            services.AddScoped<IConfiguration>((provider) => configuration.Object);
            services.AddDbContext<TreeContext>(options =>
                options.UseInMemoryDatabase("TestDb"));
            _serviceProvider = services.BuildServiceProvider();
            _treeContext = _serviceProvider.GetRequiredService<TreeContext>();
            _logger = new Mock<ILogger<TreeRepository>>();
            _treeRepository = new TreeRepository(_treeContext, _logger.Object);
        }

        [Fact]
        public async void TreeRepository_Create_ThrowWrongParameterParentNodeIdException()
        {
            var parentNodeId = -1;
            var nodeName = _fixture.Create<string>();
            var token = _fixture.Create<CancellationToken>();
            var node = _fixture.Build<TreeNode>()
                               .Without(tn => tn.Parent)
                               .Without(tn => tn.Children)
                               .Create();

            var exceptionResult = await Assert.ThrowsAsync<WrongParameterException>(async ()=> await _treeRepository.Create(parentNodeId, nodeName, token));

            Assert.Equal($"Wrong parameter 'parentNodeId'.", exceptionResult.Message);
        }

        [Fact]
        public async void TreeRepository_Create_ThrowWrongParameterNodeNameException()
        {
            var parentNodeId = _fixture.Create<int>();
            var nodeName = string.Empty;
            var token = _fixture.Create<CancellationToken>();
            var node = _fixture.Build<TreeNode>()
                               .Without(tn => tn.Parent)
                               .Without(tn => tn.Children)
                               .Create();

            var exceptionResult = await Assert.ThrowsAsync<WrongParameterException>(async () => await _treeRepository.Create(parentNodeId, nodeName, token));

            Assert.Equal($"Wrong parameter 'nodeName'.", exceptionResult.Message);
        }

        [Fact]
        public async void TreeRepository_Create_ReturnValidResult()
        {
            var parentNodeId = (int?)null;
            var nodeName = _fixture.Create<string>();
            var ctm = new CancellationTokenSource();

            await _treeRepository.Create(parentNodeId, nodeName, ctm.Token);

            var itemsInDatabase = _treeContext.TreeNode.ToList();
            Assert.Single(itemsInDatabase);
            Assert.Equal(nodeName, itemsInDatabase[0].Name);
        }


        ~TreeRepositoryTests()
        {
            var dbContext = _serviceProvider.GetService<TreeContext>();
            dbContext!.Database.EnsureDeleted();
        }
    }
}