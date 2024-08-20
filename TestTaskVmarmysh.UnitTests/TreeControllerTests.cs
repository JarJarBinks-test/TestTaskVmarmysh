using AutoFixture;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Moq;
using TestTaskVmarmysh.Common.Exceptions;
using TestTaskVmarmysh.Controllers;
using TestTaskVmarmysh.Services.Entities;
using TestTaskVmarmysh.Services.Interfaces;

namespace TestTaskVmarmysh.UnitTests
{
    public class TreeControllerTests
    {
        Fixture _fixture;
        Mock<ITreeService> _service;
        Mock<ILogger<TreeController>> _logger;
        TreeController _treeController;

        public TreeControllerTests()
        {
            _fixture = new Fixture();
            _service = new Mock<ITreeService>();
            _logger = new Mock<ILogger<TreeController>>();
            _treeController = new TreeController(_service.Object, _logger.Object);
        }

        [Fact]
        public async void TreeService_Get_ReturnValidResult()
        {
            var treeName = _fixture.Create<string>();
            var token = _fixture.Create<CancellationToken>();
            var subChildNode = _fixture.Build<TreeNodeView>()
                                       .Without(tn => tn.Children)
                                       .Create();
            var childNode = _fixture.Build<TreeNodeView>()
                                    .With(tn => tn.Children, new List<TreeNodeView>() { subChildNode })
                                    .Create();
            var node = _fixture.Build<TreeNodeView>()
                               .With(tn => tn.Children, new List<TreeNodeView>() { childNode })
                               .Create();

            _service.Setup(repo => repo.GetTree(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(node);
            var result = await _treeController.Get(treeName, token);

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