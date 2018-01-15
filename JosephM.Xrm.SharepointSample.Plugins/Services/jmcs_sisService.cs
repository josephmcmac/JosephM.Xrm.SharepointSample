using JosephM.Xrm.SharepointSample.Plugins.Xrm;

namespace JosephM.Xrm.SharepointSample.Plugins.Services
{
    /// <summary>
    /// A service class for performing logic
    /// </summary>
    public class jmcs_sisService
    {
        private XrmService XrmService { get; set; }
        private jmcs_sisSettings jmcs_sisSettings { get; set; }

        public jmcs_sisService(XrmService xrmService, jmcs_sisSettings settings)
        {
            XrmService = xrmService;
            jmcs_sisSettings = settings;
        }
    }
}
