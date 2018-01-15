using JosephM.Xrm.SharepointSample.Plugins.Xrm;

namespace JosephM.Xrm.SharepointSample.Plugins
{
    /// <summary>
    /// This is the class for registering plugins in CRM
    /// Each entity plugin type needs to be instantiated in the CreateEntityPlugin method
    /// </summary>
    public class jmcs_sisPluginRegistration : XrmPluginRegistration
    {
        public override XrmPlugin CreateEntityPlugin(string entityType, bool isRelationship)
        {
            switch (entityType)
            {
                //case Entities.account: return new AccountPlugin();
            }
            return null;
        }
    }
}
