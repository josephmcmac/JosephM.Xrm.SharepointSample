using JosephM.Xrm.SharepointSample.Plugins.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace JosephM.Xrm.SharepointSample.Plugins.Test
{
    [TestClass]
    public class jmcs_sisXrmTest : XrmTest
    {
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
                return new string[0];
            }
        }

        //class for shared services or settings objects for tests
        //or extending base class logic
        private jmcs_sisSettings _settings;
        public jmcs_sisSettings jmcs_sisSettings
        {
            get
            {
                if (_settings == null)
                    _settings = new jmcs_sisSettings(XrmService);
                return _settings;
            }
        }

        private jmcs_sisService _service;
        public jmcs_sisService jmcs_sisService
        {
            get
            {
                if (_service == null)
                    _service = new jmcs_sisService(XrmService, jmcs_sisSettings);
                return _service;
            }
        }
    }
}
