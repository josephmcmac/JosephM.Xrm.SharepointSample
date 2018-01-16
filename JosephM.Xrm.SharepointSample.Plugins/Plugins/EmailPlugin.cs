using System;
using System.Linq;
using System.Threading;
using JosephM.Xrm.SharepointSample.Plugins.Core;
using JosephM.Xrm.SharepointSample.Plugins.Xrm;
using Microsoft.Xrm.Sdk.Query;
using Schema;

namespace JosephM.Xrm.SharepointSample.Plugins.Plugins
{
    public class EmailPlugin : SharepointSampleEntityPluginBase
    {
        public override void GoExtention()
        {
            MoveAttachmentsToSharepoint();
        }

        private void MoveAttachmentsToSharepoint()
        {
            if (IsMessage(PluginMessage.Create, PluginMessage.Update) && IsStage(PluginStage.PostEvent) && IsMode(PluginMode.Asynchronous))
            {
                if (!GetBoolean(Fields.email_.directioncode) && FieldChanging(Fields.email_.regardingobjectid))
                {
                    if(GetLookupType(Fields.email_.regardingobjectid) == Entities.incident)
                    {
                        if (IsMessage(PluginMessage.Create))
                        {
                            //lets just snooze for 10 seconds if this is a create message
                            //in case attachments are added after the initial create message
                            Thread.Sleep(10000);
                        }

                        var documentExtentions = new[] { "pdf", "doc", "docx" };

                        var attachmentQuery = XrmService.BuildQuery(Entities.activitymimeattachment, null, new[] {
                            new ConditionExpression(Fields.activitymimeattachment_.activityid, ConditionOperator.Equal, TargetId)
                        });
                        var typeFilter = attachmentQuery.Criteria.AddFilter(LogicalOperator.Or);
                        typeFilter.Conditions.AddRange(documentExtentions.Select(de => new ConditionExpression(Fields.activitymimeattachment_.filename, ConditionOperator.EndsWith, de)));

                        var attachmentsToMove = XrmService.RetrieveAll(attachmentQuery);
                        if (attachmentsToMove.Any())
                        {
                            var regardingObjectId = GetLookupGuid(Fields.email_.regardingobjectid);
                            var regardingType = GetLookupType(Fields.email_.regardingobjectid);
                            var regardingName = XrmEntity.GetLookupName(XrmService.LookupField(TargetType, TargetId, Fields.email_.regardingobjectid));

                            //get a sharepoint folder for the target location record 
                            var documentFolder = SharepointService.GetOrCreateDocumentFolderRelativeUrl(regardingType, regardingObjectId.Value, regardingName);

                            foreach (var item in attachmentsToMove)
                            {
                                var fileName = item.GetStringField(Fields.activitymimeattachment_.filename);
                                //if a txt file could potentially have already been moved so lets just leave txt files in crm
                                if (fileName != null && !fileName.EndsWith(".txt"))
                                {
                                    var fileContent = item.GetStringField(Fields.activitymimeattachment_.body);
                                    //load the attachment to sharepoint
                                    var documentUrl = SharepointService.UploadDocument(documentFolder, fileName, fileContent, "[EmailFile]", item.Id.ToString().ToUpper().Replace("{", "").Replace("}", "").Replace("-", ""));

                                    ////update the attachment to a text file with the sharepoint url
                                    //item.SetField(Fields.activitymimeattachment_.filename, fileName + ".txt");
                                    //item.SetField(Fields.activitymimeattachment_.body, Convert.ToBase64String(("Moved to sharepoint - " + documentUrl).ToByteArray()));
                                    //XrmService.Update(item, new[] { Fields.activitymimeattachment_.filename, Fields.activitymimeattachment_.body });
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}