using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Reflection;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UIH.XA.ViewerToolKit.Interface;
using UIH.XA.ViewerToolKit.ViewModel;

namespace UIH.XA.ViewerToolKit.ViewModelTest
{
    [TestClass]
    public class ViewerToolBoxViewModelTests
    {

        [Export("ViewerToolBoxConfig", typeof (string))] private const string ConfigPath = @"config\ViewerToolBox.xml";
        [Import] private ToolsToolBoxViewModel _viewModel;
        private readonly Mock<IViewerTool> _viewerToolMock = new Mock<IViewerTool>();
        private readonly Mock<IViewerToolFactory> _toolBoxModelMock = new Mock<IViewerToolFactory>();
        [Export] private IViewerToolFactory _model;// = new Mock<IViewerToolFactory>().Setup(itbm=>itbm.CreateTool(It.IsAny<XElement>())).Returns()

        public ViewerToolBoxViewModelTests ()
        {
            _viewerToolMock.Setup(vtm => vtm.Name).Returns("FooTool");
            _toolBoxModelMock.Setup(itbm => itbm.CreateTool(It.IsAny<XElement>())).Returns(_viewerToolMock.Object);

            _model = _toolBoxModelMock.Object;


            var catalog1 = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            var catalog2 = new DirectoryCatalog(@".", "UIH.XA.ViewerToolKit.ViewModel.dll");
            var catalog = new AggregateCatalog(catalog1, catalog2);
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);

        }

        [TestMethod]
        public void ToolsTest()
        {
            // Arrange & Act
            var toolCount = _viewModel.Tools.Count;

            // Assert
            Assert.IsTrue(toolCount>0);
        }
    }
}