using JosephM.Xrm.SharepointSample.Plugins.Plugins;
using JosephM.Xrm.SharepointSample.Plugins.Xrm;
using Schema;

namespace JosephM.Xrm.SharepointSample.Plugins
{
    /// <summary>
    /// This is the class for registering plugins in CRM
    /// Each entity plugin type needs to be instantiated in the CreateEntityPlugin method
    /// </summary>
    public class SharepointSamplePluginRegistration : XrmPluginRegistration
    {
        public override XrmPlugin CreateEntityPlugin(string entityType, bool isRelationship)
        {
            switch (entityType)
            {
                case Entities.jmcg_sis_sharepointsettings: return new SharepointSettingsPlugin();
                case Entities.email: return new EmailPlugin();
            }
            return null;
        }
    }
}
