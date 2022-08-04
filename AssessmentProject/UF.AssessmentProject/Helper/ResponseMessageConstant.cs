namespace UF.AssessmentProject.Helper
{
    public class ResponseMessageConstant
    {
        // static response
        public const string Success = "Success."; //Success

        public const string AccessDenied = "Access Denied!"; //Unauthorized partner or Signature Mismatch
        public const string InvalidTotalAmount = "Invalid Total Amount."; //Only applicable when itemDetails is provided. The total value stated in itemDetails array not equal to value in totalamount.
        public const string Expired = "Expired."; //Provided timestamp exceed server time +-5min Example: server time is 13 Dec 2021 15:04:02 The valid time will be +-5 Min of the server time. Other than this will consider the API is expired.
        public const string TotalAmountMustBePositive = "Total amount must be positive.";

        // dynamic response
        private const string Required = " is Required."; //Mandatory Parameter is not provided. Example: partnerrefno is required.


        #region required response message

        public static string IsRequiredResopnse(string item)
        {
            var result = item + Required;
            return result;
        }

        #endregion
    }
}
