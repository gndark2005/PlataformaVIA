using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using FirebaseAdmin.Messaging;
using Newtonsoft.Json;

namespace PlataformaVIAOAuth.WebServices.Models
{
    public class MessageFireBase
    {
        public string[] registration_ids { get; set; }
        public Notification notification { get; set; }
        public object data { get; set; }
    }

    public class Notification
    {
        public string title { get; set; }
        public string text { get; set; }

    }

    public class PushNotificationLogic
    {
        public static Uri FireBasePushNotificationsURL { get; set; }


    }
}