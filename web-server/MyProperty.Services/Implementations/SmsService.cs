using MyProperty.Services.Interfaces;

namespace MyProperty.Services.Implementations
{
    public class SmsService : ISmsService
    {
        public string SmsApiUrl { private set; get; }
        public SmsService(ISmsConfig smsConfig)
        {
            SmsApiUrl = string.Format("http://ptechsms.com/sendSMS?username={0}&sendername={1}&smstype={2}&apikey={3}", smsConfig.UserName, smsConfig.SenderName, smsConfig.SmsType, smsConfig.ApiKey);
            SmsApiUrl += "&numbers={0}&message={1}";
        }
        public void SendTextMessage(string mobileNumbers, string messageText)
        {
            //HostingEnvironment.QueueBackgroundWorkItem(cancelationToken =>
            //{
            //    HttpWebRequest req = (HttpWebRequest)WebRequest.Create(string.Format(SmsApiUrl, mobileNumbers, messageText));
            //    using (HttpWebResponse myResp = (HttpWebResponse)req.GetResponse())
            //    using (StreamReader respStreamReader = new StreamReader(myResp.GetResponseStream()))
            //    {
            //        string responseString = respStreamReader.ReadToEnd();
            //    }
            //});
        }
    }

    public class SmsConfig : ISmsConfig
    {
        public string UserName { get; set; }
        public string SenderName { get; set; }
        public string SmsType { get; set; } = "TRANS";
        public string ApiKey { get; set; }
    }
    public interface ISmsConfig
    {
        string UserName { get; set; }
        string SenderName { get; set; }
        string SmsType { get; set; }
        string ApiKey { get; set; }
    }
}
