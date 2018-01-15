using JosephM.Xrm.SharepointSample.Plugins.Services;
using JosephM.Xrm.SharepointSample.Plugins.Xrm;

namespace JosephM.Xrm.SharepointSample.Plugins.Workflows
{
    //base class for services or settings used across all workflow activities
    public abstract class jmcs_sisWorkflowActivity<T> : XrmWorkflowActivityInstance<T>
        where T : XrmWorkflowActivityRegistration
    {
        //class for shared services or settings objects for workflow activities
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
