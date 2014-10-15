﻿using Newtonsoft.Json;
using Riskified.SDK.Exceptions;
using Riskified.SDK.Utils;

namespace Riskified.SDK.Model.OrderElements
{
    public class PaypalPaymentDetails : IPaymentDetails
    {
        /// <summary>
        /// The payment information for the order in case of a paypal payment
        /// </summary>
        /// <param name="paymentStatus">The payment status as recieved from paypal</param>
        /// <param name="authorizationId">The authorization Id received from paypal</param>
        /// <param name="payerEmail">The payer email assigned to his paypal accout as received from paypal</param>
        /// <param name="payerStatus">The payer status as received from paypal</param>
        /// <param name="payerAddressStatus">The payer address status as received from paypal</param>
        /// <param name="protectionEligibility">The merchants protection eligibility for the order as received from paypal</param>
        /// <param name="pendingReason">The pending reason received from paypal</param>
        /// <exception cref="OrderFieldBadFormatException">throws an exception if one of the parameters doesn't match the expected format</exception>
        public PaypalPaymentDetails(string paymentStatus, string authorizationId = null, string payerEmail = null, string payerStatus = null, string payerAddressStatus = null , 
            string protectionEligibility = null , string pendingReason = null)
        {
            AuthorizationId = authorizationId;
            PayerEmail = payerEmail;
            PayerStatus = payerStatus;
            PayerAddressStatus = payerAddressStatus;
            ProtectionEligibility = protectionEligibility;
            PaymentStatus = paymentStatus;
            PendingReason = PendingReason;
        }

        public void Validate(bool isWeak = false)
        {
            if (!isWeak)
            {
                InputValidators.ValidateValuedString(PaymentStatus, "Payment Status");
            }
        }

        [JsonProperty(PropertyName = "authorization_id")]
        public string AuthorizationId { get; set; }

        [JsonProperty(PropertyName = "payer_email")]
        public string PayerEmail { get; set; }

        [JsonProperty(PropertyName = "payer_status")]
        public string PayerStatus { get; set; }

        [JsonProperty(PropertyName = "payer_address_status")]
        public string PayerAddressStatus { get; set; }

        [JsonProperty(PropertyName = "protection_eligibility")]
        public string ProtectionEligibility { get; set; }

        [JsonProperty(PropertyName = "payment_status")]
        public string PaymentStatus { get; set; }

        [JsonProperty(PropertyName = "pending_reason")]
        public string PendingReason { get; set; }

        
    }
}
