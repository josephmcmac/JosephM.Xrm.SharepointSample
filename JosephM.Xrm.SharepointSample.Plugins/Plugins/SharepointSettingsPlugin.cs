using JosephM.Xrm.SharepointSample.Plugins.Xrm;
using Schema;

namespace JosephM.Xrm.SharepointSample.Plugins.Plugins
{
    public class SharepointSettingsPlugin : SharepointSampleEntityPluginBase
    {
        public override void GoExtention()
        {
            EncryptSharepointPassword();
        }

        /// <summary>
        /// Dont stored passwords in plain text - store them in encrypted field
        /// </summary>
        private void EncryptSharepointPassword()
        {
            if (IsMessage(PluginMessage.Create, PluginMessage.Update) && IsStage(PluginStage.PreOperationEvent))
            {
                if (FieldChanging(Fields.jmcg_sis_sharepointsettings_.jmcg_sis_sharepointpasswordreset)
                && GetField(Fields.jmcg_sis_sharepointsettings_.jmcg_sis_sharepointpasswordreset) != null)
                {
                    //encrypt the password into these fields
                    var encrypt = EncryptionService.Encrypt(GetStringField(Fields.jmcg_sis_sharepointsettings_.jmcg_sis_sharepointpasswordreset));
                    SetField(Fields.jmcg_sis_sharepointsettings_.jmcg_sis_sharepointpasswordencrypted, encrypt);
                    SetField(Fields.jmcg_sis_sharepointsettings_.jmcg_sis_sharepointpasswordreset, null);
                }
            }
        }
    }
}
