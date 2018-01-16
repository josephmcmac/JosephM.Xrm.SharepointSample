using JosephM.Xrm.SharepointSample.Plugins.Services;
using JosephM.Xrm.SharepointSample.Plugins.Services.SharePoint;
using JosephM.Xrm.SharepointSample.Plugins.Xrm;

namespace JosephM.Xrm.SharepointSample.Plugins.Workflows
{
    //base class for services or settings used across all workflow activities
    public abstract class SharepointSampleWorkflowActivity<T> : XrmWorkflowActivityInstance<T>
        where T : XrmWorkflowActivityRegistration
    {
        //class for shared services or settings objects for workflow activities
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
    }
}
