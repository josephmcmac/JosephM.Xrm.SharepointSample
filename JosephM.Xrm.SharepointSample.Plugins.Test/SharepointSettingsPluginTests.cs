using JosephM.Xrm.SharepointSample.Plugins.Services.SharePoint;
using JosephM.Xrm.SharepointSample.Plugins.Xrm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Schema;
using System.Collections.Generic;

namespace JosephM.Xrm.SharepointSample.Plugins.Test
{
    [TestClass]
    public class SharepointSettingsTests : SharepointSampleXrmTest
    {
        [TestMethod]
        public void SharepointSettingsEncryptAndDecryptPasswordTest()
        {
            //verify the sharepoint password is cleared and encrypted when entered

            var testPassword1 = "TESTPASSWORD1";
            //create
            var settings = CreateTestRecord(Entities.jmcg_sis_sharepointsettings, new Dictionary<string, object>()
            {
                { Fields.jmcg_sis_sharepointsettings_.jmcg_sis_sharepointpasswordreset, testPassword1 }
            }, XrmServiceAdmin);
            //verify cleared
            Assert.IsTrue(settings.GetField(Fields.jmcg_sis_sharepointsettings_.jmcg_sis_sharepointpasswordreset) == null);
            //verify settings object correctly decrypts it
            var settingsObject = new SharepointSampleSettings(XrmService, EncryptionService);
            settingsObject.Settings = settings;
            Assert.AreEqual(testPassword1, ((ISharepointSettings)settingsObject).Password);

            //update
            settings.SetField(Fields.jmcg_sis_sharepointsettings_.jmcg_sis_sharepointpasswordreset, testPassword1 + testPassword1);
            settings = UpdateFieldsAndRetreive(XrmServiceAdmin, settings, Fields.jmcg_sis_sharepointsettings_.jmcg_sis_sharepointpasswordreset);
            //verify cleared
            Assert.IsTrue(settings.GetField(Fields.jmcg_sis_sharepointsettings_.jmcg_sis_sharepointpasswordreset) == null);
            //verify settings object correctly decrypts it
            settingsObject = new SharepointSampleSettings(XrmService, EncryptionService);
            settingsObject.Settings = settings;
            Assert.AreEqual(testPassword1 + testPassword1, ((ISharepointSettings)settingsObject).Password);


            Delete(settings);
        }
    }
}