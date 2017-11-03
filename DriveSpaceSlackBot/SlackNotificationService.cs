using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DriveSpaceSlackBot
{
    public class SlackNotificationService
    {
        public void Send(String message)
        {
            Send(message, new List<String>());
        }

        public void Send(String message, IEnumerable<String> attachments)
        {
            var slackWebhookUrl = ConfigurationManager.AppSettings["slackWebhookUrl"];
            var model = GetModel(message, attachments);

            var json = JsonConvert.SerializeObject(model, Formatting.None, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            var client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
            client.UploadString(slackWebhookUrl, json);
        }

        internal void Send(object message)
        {
            throw new NotImplementedException();
        }

        private SlackNotificationModel GetModel(String message, IEnumerable<String> attachments)
        {
            var model = new SlackNotificationModel();
            var slackAttachments = new List<Attachment>();

            foreach (var attachment in attachments)
            {
                if (File.Exists(attachment))
                {
                    var fileType = GetTermFileType(attachment);
                    var slackAttachment = new Attachment();
                    slackAttachment.Text = String.Format("<File://edward/terms/{0}|{1}>", Path.GetFileName(attachment), fileType);
                    

                    slackAttachments.Add(slackAttachment);
                }
            }

            model.Text = message;
            model.Attachments = slackAttachments;

            return model;
        }

        private String GetTermFileType(String filename)
        {
            if (filename.Contains("Web"))
                return "Web Terms";

            return "HFMUT Terms";
        }
    }

    public class SlackNotificationModel
    {
        public String Text { get; set; }
        public IEnumerable<Attachment> Attachments { get; set; }
    }

    public class Attachment
    {
        public String Text { get; set; }
    }
}
