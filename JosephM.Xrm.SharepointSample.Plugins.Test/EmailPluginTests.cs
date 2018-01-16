using JosephM.Xrm.SharepointSample.Plugins.Xrm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Schema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JosephM.Xrm.SharepointSample.Plugins.Test
{
    [TestClass]
    public class EmailPluginTests : SharepointSampleXrmTest
    {
        [DeploymentItem("Files/Test Document.txt")]
        [TestMethod]
        public void EmailPluginTestMoveDocumentsToSharepointWhenRegardingCase()
        {
            var fileNames = new[]
            { "Sample Attachment.PNG", "Sample Attachment.PDF", "Sample Attachment.DOCX" };
            var documentContent = Convert.ToBase64String(File.ReadAllBytes("Test Document.txt"));

            //okay lets create a case, then an email regarding it with attachments and verify
            //the ones with our valid extensions are created in sharepoint
            var aCase = CreateTestRecord(Entities.incident, new Dictionary<string, object>
            {
                { Fields.incident_.customerid, TestContact.ToEntityReference() }
            });

            var email = CreateTestRecord(Entities.email, new Dictionary<string, object>()
            {
                { Fields.email_.regardingobjectid, aCase.ToEntityReference() },
                { Fields.email_.subject, "TESTATTACHMENTSCRIPT" },
                { Fields.email_.directioncode, false }
            });

            foreach (var filename in fileNames)
            {
                CreateTestRecord(Entities.activitymimeattachment, new Dictionary<string, object>()
                {
                    { Fields.activitymimeattachment_.activityid, email.ToEntityReference() },
                    { Fields.activitymimeattachment_.filename, filename },
                    { Fields.activitymimeattachment_.body, documentContent }
                });
            }

            WaitTillTrue(() =>
            {
                return SharepointService.GetDocumentFolderRelativeUrl(aCase.LogicalName, aCase.Id) != null;
            }, 60);
            var sharePointFolder = SharepointService.GetDocumentFolderRelativeUrl(aCase.LogicalName, aCase.Id);

            WaitTillTrue(() =>
            {
                return SharepointService.GetDocuments(sharePointFolder).Count() == 2;
            }, 60);

            var sharePointDocuments = SharepointService.GetDocuments(sharePointFolder);
            Assert.IsTrue(sharePointDocuments.Count(sd => sd.Name.EndsWith(".PDF")) == 1);
            Assert.IsTrue(sharePointDocuments.Count(sd => sd.Name.EndsWith(".DOCX")) == 1);

            //okay lets do the similar scenario but where an existing received email is set regarding the case
            //this is actually the scenario when created by case creation rules
            email = CreateTestRecord(Entities.email, new Dictionary<string, object>()
            {
                { Fields.email_.subject, "TESTATTACHMENTSCRIPT" },
                { Fields.email_.directioncode, false }
            });

            foreach (var filename in fileNames)
            {
                CreateTestRecord(Entities.activitymimeattachment, new Dictionary<string, object>()
                {
                    { Fields.activitymimeattachment_.activityid, email.ToEntityReference() },
                    { Fields.activitymimeattachment_.filename, filename },
                    { Fields.activitymimeattachment_.body, documentContent }
                });
            }

            email.SetLookupField(Fields.email_.regardingobjectid, aCase);
            email = UpdateFieldsAndRetreive(email, Fields.email_.regardingobjectid);

            WaitTillTrue(() =>
            {
                return SharepointService.GetDocumentFolderRelativeUrl(aCase.LogicalName, aCase.Id) != null;
            }, 60);
            sharePointFolder = SharepointService.GetDocumentFolderRelativeUrl(aCase.LogicalName, aCase.Id);

            //this time we should have twice as many attachments for each thing
            //because the first email already added some
            WaitTillTrue(() =>
            {
                return SharepointService.GetDocuments(sharePointFolder).Count() == 4;
            }, 60);

            sharePointDocuments = SharepointService.GetDocuments(sharePointFolder);
            Assert.IsTrue(sharePointDocuments.Count(sd => sd.Name.EndsWith(".PDF")) == 2);
            Assert.IsTrue(sharePointDocuments.Count(sd => sd.Name.EndsWith(".DOCX")) == 2);


            DeleteMyToday();
        }
    }
}