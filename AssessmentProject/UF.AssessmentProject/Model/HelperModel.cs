namespace UF.AssessmentProject.Model
{
    public class HelperModel
    {
    }

    public class CheckSignNotValidateModel
    {
        public string timestamp { get; set; }
        public string partnerkey { get; set; }
        public string partnerrefno { get; set; }
        public string totalamount { get; set; }
        public string partnerpassword { get; set; }
        public string sig { get; set; }
    }

    public class AuthPartnerModel
    {
        public string partnerkey { get; set; }
        public string partnerpassword { get; set; }
    }
}
