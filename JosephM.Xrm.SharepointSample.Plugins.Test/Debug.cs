using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JosephM.Xrm.SharepointSample.Plugins.Test
{
    //this class just for general debug purposes
    [TestClass]
    public class DebugTests : jmcs_sisXrmTest
    {
        [TestMethod]
        public void Debug()
        {
            var me = XrmService.WhoAmI();
        }
    }
}