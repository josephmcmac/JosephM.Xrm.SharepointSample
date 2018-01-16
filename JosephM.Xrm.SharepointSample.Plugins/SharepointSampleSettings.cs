using JosephM.Xrm.SharepointSample.Plugins.Services;
using JosephM.Xrm.SharepointSample.Plugins.Services.SharePoint;
using JosephM.Xrm.SharepointSample.Plugins.Xrm;
using Microsoft.Xrm.Sdk;
using Schema;
using System;

namespace JosephM.Xrm.SharepointSample.Plugins
{
    /// <summary>
    /// A settings object which loads the first record of a given type for accessing its fields/properties
    /// </summary>
    public class SharepointSampleSettings : ISharepointSettings
    {
        private XrmService XrmService { get; set; }
        private EncryptionService EncryptionService { get; set; }

        public SharepointSampleSettings(XrmService xrmService, EncryptionService encryptionService)
        {
            XrmService = xrmService;
            EncryptionService = encryptionService;
        }

        private const string EntityType = Entities.jmcg_sis_sharepointsettings;

        private Entity _settings;

        public Entity Settings
        {
            get
            {
                if (_settings == null)
                {
                    _settings = XrmService.GetFirst(EntityType);
                    if (_settings == null)
                        throw new NullReferenceException(
                            string.Format(
                                "Error getting the {0} record. It does not exist or you do not have permissions to view it",
                                XrmService.GetEntityLabel(EntityType)));
                }
                return _settings;
            }
            set { _settings = value; }
        }

        string ISharepointSettings.UserName
        {
            get
            {
                var userName = Settings.GetStringField(Fields.jmcg_sis_sharepointsettings_.jmcg_sis_sharepointusername);
                if (string.IsNullOrWhiteSpace(userName))
                    throw new NullReferenceException($"{XrmService.GetFieldLabel(Fields.jmcg_sis_sharepointsettings_.jmcg_sis_sharepointusername, EntityType)} Is Empty In The {XrmService.GetEntityLabel(EntityType)} Record. Contact And Administrator");
                return userName;
            }
        }

        string ISharepointSettings.Password
        {
            get
            {
                var passwordEncrypted = Settings.GetStringField(Fields.jmcg_sis_sharepointsettings_.jmcg_sis_sharepointpasswordencrypted);
                if (string.IsNullOrWhiteSpace(passwordEncrypted))
                    throw new NullReferenceException($"{XrmService.GetFieldLabel(Fields.jmcg_sis_sharepointsettings_.jmcg_sis_sharepointpasswordencrypted, EntityType)} Is Empty In The {XrmService.GetEntityLabel(EntityType)} Record. Contact And Administrator");
                return EncryptionService.Decrypt(passwordEncrypted);
            }
        }
    }
}