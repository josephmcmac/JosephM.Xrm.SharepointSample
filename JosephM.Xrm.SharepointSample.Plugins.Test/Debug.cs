using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JosephM.Xrm.SharepointSample.Plugins.Test
{
    //this class just for general debug purposes
    [TestClass]
    public class DebugTests : SharepointSampleXrmTest
    {
        [TestMethod]
        public void Debug()
        {
            var me = XrmService.WhoAmI();
        }
    }
}