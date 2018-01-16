using JosephM.Xrm.SharepointSample.Plugins.Services;
using JosephM.Xrm.SharepointSample.Plugins.Services.SharePoint;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Schema;
using System.Collections.Generic;

namespace JosephM.Xrm.SharepointSample.Plugins.Test
{
    [TestClass]
    public class SharepointSampleXrmTest : XrmTest
    {
        private EncryptionService _encryptionService;
        public EncryptionService EncryptionService
        {
            get
            {
                if (_encryptionService == null)
                    _encryptionService = new EncryptionService();
                return _encryptionService;
            }
        }

        private SharepointService _sharepointService;
        public SharepointService SharepointService
        {
            get
            {
                if (_sharepointService == null)
                    _sharepointService = new SharepointService(SharepointSampleSettings, XrmService);
                return _sharepointService;
            }
        }

        //USE THIS IF NEED TO VERIFY SCRIPTS FOR A PARTICULAR SECURITY ROLE
        //private XrmService _xrmService;
        //public override XrmService XrmService
        //{
        //    get
        //    {
        //        if (_xrmService == null)
        //        {
        //            var xrmConnection = new XrmConfiguration()
        //            {
        //                AuthenticationProviderType = XrmConfiguration.AuthenticationProviderType,
        //                DiscoveryServiceAddress = XrmConfiguration.DiscoveryServiceAddress,
        //                OrganizationUniqueName = XrmConfiguration.OrganizationUniqueName,
        //                Username = "",
        //                Password = ""
        //            };
        //            _xrmService = new XrmService(xrmConnection);
        //        }
        //        return _xrmService;
        //    }
        //}

        protected override IEnumerable<string> EntitiesToDelete
        {
            get
            {
                return new[] { Entities.email, Entities.incident };
            }
        }

        //class for shared services or settings objects for tests
        //or extending base class logic
        private SharepointSampleSettings _settings;
        public SharepointSampleSettings SharepointSampleSettings
        {
            get
            {
                if (_settings == null)
                    _settings = new SharepointSampleSettings(XrmService, EncryptionService);
                return _settings;
            }
        }
    }
}
