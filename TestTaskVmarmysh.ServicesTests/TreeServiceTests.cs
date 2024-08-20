using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using TestTaskVmarmysh.Common.Exceptions;
using TestTaskVmarmysh.DataAccess.Entities.TreeEntities;
using TestTaskVmarmysh.DataAccess.Interfaces;
using TestTaskVmarmysh.Services.Interfaces;
using TestTaskVmarmysh.Services.Services;

namespace TestTaskVmarmysh.Services.UnitTests
{
    public class TreeServiceTests
    {
        Fixture _fixture;
        Mock<ITreeRepository> _respository;
        Mock<ILogger<TreeService>> _logger;
        ITreeService _treeService;

        public TreeServiceTests()
        {
            _fixture = new Fixture();
            _respository = new Mock<ITreeRepository>();
            _logger = new Mock<ILogger<TreeService>>();
            _treeService = new TreeService(_respository.Object, _logger.Object);
        }

        [Fact]
        public async void TreeService_GetTree_ThrowWrongParameterException()
        {
            var treeName = string.Empty;
            var token = _fixture.Create<CancellationToken>();
            var node = _fixture.Build<TreeNode>()
                               .Without(tn => tn.Parent)
                               .Without(tn => tn.Children)
                               .Create();

            _respository.Setup(repo => repo.GetTree(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(node);
            var exceptionResult = await Assert.ThrowsAsync<WrongParameterException>(async ()=> await _treeService.GetTree(treeName, token));
            _respository.Verify(repo => repo.GetTree(treeName, token), Times.Never);

            Assert.Equal($"Wrong parameter 'name'.", exceptionResult.Message);
        }

        [Fact]
        public async void TreeService_GetTree_ThrowException()
        {
            var treeName = _fixture.Create<string>();
            var token = _fixture.Create<CancellationToken>();
            TreeNode node = null!;

            _respository.Setup(repo => repo.GetTree(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(node);
            var exceptionResult = await Assert.ThrowsAsync<Exception>(async () => await _treeService.GetTree(treeName, token));
            _respository.Verify(repo => repo.GetTree(treeName, token), Times.Once);

            Assert.Equal($"Tree '{treeName}' not found.", exceptionResult.Message);
        }

        [Fact]
        public async void TreeService_GetTree_ReturnValidResult()
        {
            var treeName = _fixture.Create<string>();
            var token = _fixture.Create<CancellationToken>();
            var subChildNode = _fixture.Build<TreeNode>()
                                       .Without(tn => tn.Parent)
                                       .Without(tn => tn.Children)
                                       .Create();
            var childNode = _fixture.Build<TreeNode>()
                                    .Without(tn => tn.Parent)
                                    .With(tn => tn.Children, new List<TreeNode>() { subChildNode })
                                    .Create();
            var node = _fixture.Build<TreeNode>()
                               .Without(tn => tn.Parent)
                               .With(tn => tn.Children, new List<TreeNode>() { childNode })
                               .Create();

            _respository.Setup(repo => repo.GetTree(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(node);
            var result = await _treeService.GetTree(treeName, token);
            _respository.Verify(repo => repo.GetTree(treeName, token), Times.Once);

            Assert.Equal(node.Id, result.Id);
            Assert.Equal(node.Name, result.Name);
            Assert.Single(node.Children);
            Assert.Equal(childNode.Id, node.Children[0].Id);
            Assert.Equal(childNode.Name, node.Children[0].Name);
            Assert.Single(node.Children[0].Children);
            Assert.Equal(subChildNode.Id, node.Children[0].Children[0].Id);
            Assert.Equal(subChildNode.Name, node.Children[0].Children[0].Name);
        }
    }
}