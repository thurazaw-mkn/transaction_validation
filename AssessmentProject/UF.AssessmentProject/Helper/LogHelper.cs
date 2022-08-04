namespace UF.AssessmentProject.Helper
{
    public class LogHelper
    {
        public class LogModel
        {
            public string actionname { get; set; }
            public string route { get; set; }
            public string requestmessage { get; set; }
            public string responsemessage { get; set; }
            public string result { get; set; }
        }

        //public static void WriteLogs(LogModel req)
        //{
        //    log.Info("TransactionController.SubmitTRansaction");
        //    log.Info("Route: " + "api/SubmitTRansaction");
        //    log.Info("RequestMessage: " + JsonConvert.SerializeObject(req));
        //    if (results.result == Helper.DataDictionary.responseResult.success)
        //    {
        //        log.Info("ResponseMessage: " + JsonConvert.SerializeObject(results));
        //    }
        //    else
        //    {
        //        log.Error("ResponseMessage: " + JsonConvert.SerializeObject(results));
        //    }
        //}

    }
}
