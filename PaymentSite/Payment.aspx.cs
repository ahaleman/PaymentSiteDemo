using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Controllers.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PaymentSite
{
    public partial class Payment : System.Web.UI.Page
    {
        public string Token { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.PRODUCTION;

                ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
                {
                    name = "**********",
                    ItemElementName = ItemChoiceType.transactionKey,
                    Item = "****************"
                };

                var settings = new List<settingType>();

                settings.Add(new settingType()
                {
                    settingName = settingNameEnum.hostedPaymentReturnOptions.ToString(),
                    settingValue = "{\"showReceipt\": false}"
                });

                settings.Add(new settingType()
                {
                    settingName = settingNameEnum.hostedPaymentButtonOptions.ToString(),
                    settingValue = "{\"text\": \"Pay\"}"
                });

                settings.Add(new settingType()
                {
                    settingName = settingNameEnum.hostedPaymentStyleOptions.ToString(),
                    settingValue = "{\"bgColor\": \"blue\"}"
                });


                settings.Add(new settingType
                {
                    settingName = settingNameEnum.hostedPaymentPaymentOptions.ToString(),
                    settingValue = "{\"cardCodeRequired\": false, \"showCreditCard\": true, \"showBankAccount\": true}"
                });

                settings.Add(new settingType
                {
                    settingName = settingNameEnum.hostedPaymentSecurityOptions.ToString(),
                    settingValue = "{\"captcha\": false}"
                });

                settings.Add(new settingType
                {
                    settingName = settingNameEnum.hostedPaymentShippingAddressOptions.ToString(),
                    settingValue = "{\"show\": false, \"required\": false}"
                });

                settings.Add(new settingType
                {
                    settingName = settingNameEnum.hostedPaymentBillingAddressOptions.ToString(),
                    settingValue = "{\"show\": true, \"required\": false}"
                });

                settings.Add(new settingType
                {
                    settingName = settingNameEnum.hostedPaymentCustomerOptions.ToString(),
                    settingValue = "{\"showEmail\": false, \"requiredEmail\": false, \"addPaymentProfile\": true}"
                });

                settings.Add(new settingType
                {
                    settingName = settingNameEnum.hostedPaymentOrderOptions.ToString(),
                    settingValue = "{\"show\": true, \"merchantName\": \"Frisco\"}"
                });

                settings.Add(new settingType
                {
                    settingName = settingNameEnum.hostedPaymentIFrameCommunicatorUrl.ToString(),
                    settingValue = "{\"url\": \"https://localhost:44376/Communicator.html\"}"
                });

                var transactionRequest = new transactionRequestType
                {
                    transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),    // authorize capture only
                    amount = 99.99M,
                    billTo = new customerAddressType
                    {
                        firstName = "Alex",
                        lastName = "Herrera",
                        company = "Avolve",
                        address = "123 Main Street",
                        city = "Phoenix",
                        state = "Arizona",
                        zip = "83000"
                    },
                    refTransId = "RES-ELECTRIC-000",
                    poNumber = DateTime.Now.ToString("yyMMddhhmmss"),
                };

                var request = new getHostedPaymentPageRequest
                {
                    transactionRequest = transactionRequest,
                    hostedPaymentSettings = settings.ToArray(),
                    refId = "RES-ELECTRIC-000"
                };

                var controller = new getHostedPaymentPageController(request);
                controller.Execute();


                var response = controller.GetApiResponse();

                Token = response.token;
            }
        }
    }
}