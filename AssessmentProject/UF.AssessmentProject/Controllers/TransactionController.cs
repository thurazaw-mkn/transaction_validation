using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;

namespace UF.AssessmentProject.Controllers
{
    [Produces("application/json"),
        Route("api/[action]"),
        ApiController]
    [SwaggerTag("Transaction Middleware Controller to keep transactional data in Log Files")]
    public class TransactionController : ControllerBase
    {
        private readonly ILogger<TransactionController> _logger;
        private readonly ILog log;

        public TransactionController(ILogger<TransactionController> logger)
        {
            _logger = logger;
            log = LogManager.GetLogger(typeof(TransactionController));
        }

        /// <summary>
        /// Submit Transaction data
        /// </summary>
        /// <remarks>
        /// Ensure all parameter needed and responded as per IDD
        /// Ensure all posible validation is done
        /// API purpose: To ensure all data is validated and only valid partner with valid signature are able to access to this API
        /// </remarks>
        /// <param name="req">language:en-US(English), ms-MY(BM)</param>  
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, "Submit Transaction Message successfully", typeof(Model.Transaction.ResponseMessage))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized, Request")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Oops! Can't get your Post right now")]
        public ActionResult<Model.Transaction.ResponseMessage> SubmitTRansaction(Model.Transaction.RequestMessage req)
        {
            Model.Transaction.ResponseMessage results = new Model.Transaction.ResponseMessage();

            try
            {
                #region required field
                if (Helper.Helper.CheckIsNullOrEmpty(req.partnerkey))
                {
                    results.result = Helper.DataDictionary.responseResult.failed;
                    results.errormessage = Helper.ResponseMessageConstant.IsRequiredResopnse("partnerkey");
                    goto result;
                }

                if (Helper.Helper.CheckIsNullOrEmpty(req.partnerrefno))
                {
                    results.result = Helper.DataDictionary.responseResult.failed;
                    results.errormessage = Helper.ResponseMessageConstant.IsRequiredResopnse("partnerrefno");
                    goto result;
                }

                if (Helper.Helper.CheckIsNullOrEmpty(req.totalamount.ToString()))
                {
                    results.result = Helper.DataDictionary.responseResult.failed;
                    results.errormessage = Helper.ResponseMessageConstant.IsRequiredResopnse("totalamount");
                    goto result;
                }

                var itemsCount = req.items.Count;
                for (int i = 0; i < itemsCount; i++)
                {
                    if (Helper.Helper.CheckIsNullOrEmpty(req.items[i].partneritemref))
                    {
                        results.result = Helper.DataDictionary.responseResult.failed;
                        results.errormessage = Helper.ResponseMessageConstant.IsRequiredResopnse("partneritemref");
                        goto result;
                    }
                    if (Helper.Helper.CheckIsNullOrEmpty(req.items[i].name))
                    {
                        results.result = Helper.DataDictionary.responseResult.failed;
                        results.errormessage = Helper.ResponseMessageConstant.IsRequiredResopnse("name");
                        goto result;
                    }
                    if (Helper.Helper.CheckIsNullOrEmpty(req.items[i].qty.ToString()))
                    {
                        results.result = Helper.DataDictionary.responseResult.failed;
                        results.errormessage = Helper.ResponseMessageConstant.IsRequiredResopnse("qty");
                        goto result;
                    }
                    if (Helper.Helper.CheckIsNullOrEmpty(req.items[i].unitprice.ToString()))
                    {
                        results.result = Helper.DataDictionary.responseResult.failed;
                        results.errormessage = Helper.ResponseMessageConstant.IsRequiredResopnse("unitprice");
                        goto result;
                    }
                }
                #endregion

                #region Auth partner

                var authPartnerModel = new Model.AuthPartnerModel()
                {
                    partnerkey = req.partnerkey,
                    partnerpassword = req.partnerpassword,
                };
                if (Helper.Helper.CheckIsNotAuthPartner(authPartnerModel))
                {
                    results.result = Helper.DataDictionary.responseResult.failed;
                    results.errormessage = Helper.ResponseMessageConstant.AccessDenied;
                    goto result;
                }

                #endregion

                #region Message signature validation

                var checkSignNotValidateModel = new Model.CheckSignNotValidateModel()
                {
                    timestamp = req.timestamp,
                    partnerkey = req.partnerkey,
                    partnerrefno = req.partnerrefno,
                    totalamount = req.totalamount.ToString(),
                    partnerpassword = req.partnerpassword,
                    sig = req.sig,
                };
                if (Helper.Helper.CheckSignNotValidate(checkSignNotValidateModel))
                {
                    results.result = Helper.DataDictionary.responseResult.failed;
                    results.errormessage = Helper.ResponseMessageConstant.AccessDenied;
                    goto result;
                }

                #endregion

                #region value validation

                // Total amount validation
                var isValidateTotalAmount = Helper.Helper.CheckTotalAmountValidation(req.totalamount);
                if (isValidateTotalAmount.result == Helper.DataDictionary.responseResult.failed)
                {
                    results.result = isValidateTotalAmount.result;
                    results.errormessage = isValidateTotalAmount.errormessage;
                    goto result;
                }
                var totalamountStr = req.totalamount.ToString();

                // Total amount from items validation
                long totalamountfromitem = 0;
                for (int i = 0; i < itemsCount; i++)
                {
                    totalamountfromitem += req.items[i].unitprice * req.items[i].qty;
                }
                if (req.totalamount != totalamountfromitem)
                {
                    results.result = Helper.DataDictionary.responseResult.failed;
                    results.errormessage = Helper.ResponseMessageConstant.InvalidTotalAmount;
                    goto result;
                }

                //Timestamp validation
                if (Helper.Helper.CheckTimestampNotValidate(req.RealTimeStamp))
                {
                    results.result = Helper.DataDictionary.responseResult.failed;
                    results.errormessage = Helper.ResponseMessageConstant.Expired;
                    goto result;
                }

                #endregion

                results.result = Helper.DataDictionary.responseResult.success;
                results.errormessage = Helper.ResponseMessageConstant.Success;
                goto result;
            }
            catch (Exception ex)
            {
                results.result = Helper.DataDictionary.responseResult.failed;
                results.errormessage = ex.Message;
                goto result;
            }
            finally
            {
                //var logModel = new Helper.LogHelper.LogModel()
                //{
                //    actionname = "TransactionController.SubmitTRansaction"
                //};
                //Helper.LogHelper.WriteLogs(logModel);

                log.Info("TransactionController.SubmitTRansaction");
                log.Info("Route: " + "api/SubmitTRansaction");
                log.Info("RequestMessage: " + JsonConvert.SerializeObject(req));
                if (results.result == Helper.DataDictionary.responseResult.success)
                {
                    log.Info("ResponseMessage: " + JsonConvert.SerializeObject(results));
                }
                else
                {
                    log.Error("ResponseMessage: " + JsonConvert.SerializeObject(results));
                }
            }

        result:
            return Ok(results);

        }

        /// <summary>
        /// Test this controller is active
        /// </summary>
        /// <remarks>
        /// Test API to check API is Alive or not
        /// </remarks>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<string> TestAPI()
        {
            string result = "Hello World!";
            return Ok(result);
        }
    }
}
