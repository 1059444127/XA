using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIH.XA.ViewerToolKit.Interface;

namespace UIH.XA.ViewerToolKit.ViewerToolTest
{
    [TestClass]
    public class ViewerToolFactoryTests
    {
        private const string AppViewerToolNamespace = "UIH.XA.ViewerToolKit.AppViewerToolFactory";
        private const string ViewerToolNamespace = "UIH.XA.ViewerToolKit.ViewerTool";
        private const string App = "App";


        [Import]
        private IViewerToolFactory _toolFactory;

        public ViewerToolFactoryTests()
        {
            var catalog1 = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            var catalog2 = new DirectoryCatalog(@".", AppViewerToolNamespace + ".dll");
            var catalog = new AggregateCatalog(catalog1, catalog2);

            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }

        [TestMethod]
        public void CreateToolTest()
        {
            // Prepare
            var toolName = "Layout";
            var xTool = new XElement(toolName);

            // Act
            var tool = _toolFactory.CreateTool(xTool);
            var actualNamespace = tool.GetType().Namespace;
            var actualToolTypeName = tool.GetType().Name;

            // Assert
            Assert.AreEqual(ViewerToolNamespace, actualNamespace);
            Assert.AreEqual(toolName, actualToolTypeName);
        }

        [TestMethod]
        public void CreateAppToolTest()
        {
            // Prepare
            var toolName = "LLabel";
            var xTool = new XElement(toolName);

            // Act
            var tool = _toolFactory.CreateTool(xTool);
            var actualNamespace = tool.GetType().Namespace;
            var actualToolTypeName = tool.GetType().Name;

            // Assert
            Assert.AreEqual(AppViewerToolNamespace, actualNamespace);
            Assert.AreEqual(App+toolName, actualToolTypeName);
        }


        [TestMethod]
        public void Create_Tool_Not_Exist_Test()
        {
            // Prepare
            var toolName = "NotExist";
            var xTool = new XElement(toolName);

            // Act
            var tool = _toolFactory.CreateTool(xTool);


            // Assert
            Assert.AreEqual(string.Empty, tool.Name);
            Assert.IsFalse(tool.CanAct);
        }

        [TestMethod]
        public void Create_Tool_With_Int_Parameter()
        {
            // Prepare
            var xTool = new XElement("Layout");
            var xRowAttribute = new XAttribute("Row", "3");
            var xColAttribute = new XAttribute("Col", "4");
            xTool.Add(xRowAttribute, xColAttribute);

            // Act
            var tool = _toolFactory.CreateTool(xTool);
           
            // Assert
            Assert.AreEqual("Layout3X4", tool.Name);
        }
    }

}
