using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace UF.AssessmentProject.Helper
{
    public class Helper
    {

        #region Check is null or empty

        /// <summary>
        /// CheckIsNullOrEmpty
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool CheckIsNullOrEmpty(string item)
        {
            var res = string.IsNullOrEmpty(item);

            return res;
        }

        /// <summary>
        /// CheckIsNullOrEmptyInList
        /// </summary>
        /// <param name="Obj"></param>
        /// <returns></returns>
        public static bool CheckIsNullOrEmptyInList(List<object> Obj)
        {
            var count = Obj.Count;
            var res = false;

            for (int i = 0; i < count; i++)
            {

            }

            return res;
        }

        #endregion

        #region Check partner auth

        /// <summary>
        /// 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static bool CheckIsNotAuthPartner(Model.AuthPartnerModel req)
        {
            var res = true;

            if (req.partnerkey == DataDictionary.partnerkey.FAKEGOOGLE.ToString() && req.partnerpassword == SHA256String(DataDictionary.partnerpassword.FAKEPASSWORD1234.ToString()))
            {
                res = false;
            }
            else if (req.partnerkey == DataDictionary.partnerkey.FAKEPEOPLE.ToString() && req.partnerpassword == SHA256String(DataDictionary.partnerpassword.FAKEPASSWORD4578.ToString()))
            {
                res = false;
            }

            return res;
        }

        #endregion

        #region Check validation

        /// <summary>
        /// Business Logic: check
        /// only allow positive value
        /// value in cents - Example 1000 = MYR 10.00
        /// </summary>
        /// <param name="totalamount"></param>
        /// <returns></returns>
        public static Model.ResponseMessage CheckTotalAmountValidation(long totalamount)
        {
            var res = new Model.ResponseMessage();

            if (totalamount <= 0)
            {
                res.result = DataDictionary.responseResult.failed;
                res.errormessage = ResponseMessageConstant.TotalAmountMustBePositive;
            }
            else
            {
                res.result = DataDictionary.responseResult.success;
            }

            return res;
        }

        public static bool CheckTimestampNotValidate(DateTime timestamp)
        {
            var res = true;

            double timeDifference;

            if (timestamp < DateTime.MinValue)
            {
               timeDifference = DateTime.Now.Subtract(timestamp).TotalMinutes;
            }
            else if (timestamp > DateTime.MinValue)
            {
                timeDifference = timestamp.Subtract(DateTime.Now).TotalMinutes;
            }
            else
            {
                timeDifference = 0;
            }

            if (timeDifference <= ConstantUtility.TimestampDifferent)
            {
                res = false;
            }

            return res;

        }

        public static bool CheckSignNotValidate(Model.CheckSignNotValidateModel req)
        {
            var res = false;

            var str = req.timestamp + req.partnerkey + req.partnerrefno + req.totalamount + req.partnerpassword;
            var strHexBase64 = SHA256String(str);

            if (req.sig != strHexBase64)
            {
                res = true;
            }

            return res;

        }

        #endregion

        #region ChangeFormat

        public static string ChangeAmoutFormat(long amount)
        {
            var amountStr = ConstantUtility.Currency + amount.ToString();
            return amountStr;
        }

        #endregion

        #region SHA256
        private static string SHA256String(string randomString)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(randomString));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
        #endregion

    }
}
