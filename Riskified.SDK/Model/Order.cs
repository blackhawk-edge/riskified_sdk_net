﻿using System;
using System.Linq;
using Newtonsoft.Json;
using Riskified.SDK.Model.OrderElements;
using Riskified.SDK.Utils;

namespace Riskified.SDK.Model
{
    
    public class Order : AbstractOrder
    {
        /// <summary>
        /// Creates a new order
        /// </summary>
        /// <param name="merchantOrderId">The unique id of the order at the merchant systems</param>
        /// <param name="email">The email used for contact in the order</param>
        /// <param name="customer">The customer information</param>
        /// <param name="paymentDetails">The payment details</param>
        /// <param name="billingAddress">Billing address</param>
        /// <param name="shippingAddress">Shipping address</param>
        /// <param name="lineItems">An array of all products in the order</param>
        /// <param name="shippingLines">An array of all shipping details for the order</param>
        /// <param name="gateway">The payment gateway that was used</param>
        /// <param name="customerBrowserIp">The customer browser ip that was used for the order</param>
        /// <param name="currency">A three letter code (ISO 4217) for the currency used for the payment</param>
        /// <param name="totalPrice">The sum of all the prices of all the items in the order, taxes and discounts included</param>
        /// <param name="createdAt">The date and time when the order was created</param>
        /// <param name="updatedAt">The date and time when the order was last modified</param>
        /// <param name="discountCodes">An array of objects, each one containing information about an item in the order (optional)</param>
        /// <param name="totalDiscounts">The total amount of the discounts on the Order (optional)</param>
        /// <param name="cartToken">Unique identifier for a particular cart or session that is attached to a particular order. The same ID should be passed in the Beacon JS (optional)</param>
        /// <param name="totalPriceUsd">The price in USD (optional)</param>
        /// <param name="closedAt">The date and time when the order was closed. If the order was closed (optional)</param>
        /// <param name="financialStatus">The financial status of the order (could be paid/voided/refunded/partly_paid/etc.)</param>
        /// <param name="fulfillmentStatus">The fulfillment status of the order</param>
        public Order(int merchantOrderId, string email, Customer customer, IPaymentDetails paymentDetails,
            AddressInformation billingAddress, AddressInformation shippingAddress, LineItem[] lineItems,
            ShippingLine[] shippingLines,
            string gateway, string customerBrowserIp, string currency, double totalPrice, DateTime createdAt,
            DateTime updatedAt,
            DiscountCode[] discountCodes = null, double? totalDiscounts = null, string cartToken = null,
            double? totalPriceUsd = null, DateTime? closedAt = null,string financialStatus = null,string fulfillmentStatus = null) : base(merchantOrderId)
        {
            LineItems = lineItems;
            ShippingLines = shippingLines;
            PaymentDetails = paymentDetails;
            BillingAddress = billingAddress;
            ShippingAddress = shippingAddress;
            Customer = customer;
            Email = email;
            CustomerBrowserIp = customerBrowserIp;
            Currency = currency;
            TotalPrice = totalPrice;
            Gateway = gateway;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            
            // optional fields
            DiscountCodes = discountCodes;
            TotalPriceUsd = totalPriceUsd;
            TotalDiscounts = totalDiscounts;
            CartToken = cartToken;
            ClosedAt = closedAt;
            FinancialStatus = financialStatus;
            FulfillmentStatus = fulfillmentStatus;
        }

        public override void Validate(bool isWeak = false)
        {
            base.Validate(isWeak);
            InputValidators.ValidateObjectNotNull(LineItems, "Line Items");
            LineItems.ToList().ForEach(item => item.Validate(isWeak));
            InputValidators.ValidateObjectNotNull(ShippingLines, "Shipping Lines");
            ShippingLines.ToList().ForEach(item => item.Validate(isWeak));
            InputValidators.ValidateObjectNotNull(PaymentDetails, "Payment Details");
            PaymentDetails.Validate(isWeak);
            InputValidators.ValidateObjectNotNull(BillingAddress, "Billing Address");
            BillingAddress.Validate(isWeak);
            InputValidators.ValidateObjectNotNull(ShippingAddress, "Shipping Address");
            ShippingAddress.Validate(isWeak);
            InputValidators.ValidateObjectNotNull(Customer, "Customer");
            Customer.Validate(isWeak);
            InputValidators.ValidateEmail(Email);
            InputValidators.ValidateIp(CustomerBrowserIp);
            InputValidators.ValidateCurrency(Currency);
            InputValidators.ValidateZeroOrPositiveValue(TotalPrice.Value, "Total Price");
            InputValidators.ValidateValuedString(Gateway, "Gateway");
            InputValidators.ValidateDateNotDefault(CreatedAt.Value, "Created At");
            InputValidators.ValidateDateNotDefault(UpdatedAt.Value, "Updated At");
            
            // optional fields validations
            if(DiscountCodes != null && DiscountCodes.Length > 0)
            {
                DiscountCodes.ToList().ForEach(item => item.Validate(isWeak));
            }
            if(TotalPriceUsd.HasValue)
            {
                InputValidators.ValidateZeroOrPositiveValue(TotalPriceUsd.Value, "Total Price USD");
            }
            if (TotalDiscounts.HasValue)
            {
                InputValidators.ValidateZeroOrPositiveValue(TotalDiscounts.Value, "Total Discounts");
            }
            if (ClosedAt.HasValue)
            {
                InputValidators.ValidateDateNotDefault(ClosedAt.Value, "Closed At");
            }
        }


        [JsonProperty(PropertyName = "cart_token")]
        public string CartToken { get; set; }

        [JsonProperty(PropertyName = "closed_at")]
        public DateTime? ClosedAt { get; set; }

        [JsonProperty(PropertyName = "created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty(PropertyName = "currency")]
        public string Currency { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "gateway")]
        public string Gateway { get; set; }

        [JsonProperty(PropertyName = "total_discounts")]
        public double? TotalDiscounts { get; set; }

        [JsonProperty(PropertyName = "total_price")]
        public double? TotalPrice { get; set; }

        [JsonProperty(PropertyName = "total_price_usd")]
        public double? TotalPriceUsd { get; set; }

        [JsonProperty(PropertyName = "updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty(PropertyName = "browser_ip")]
        public string CustomerBrowserIp { get; set; }
        
        [JsonProperty(PropertyName = "discount_codes")]
        public DiscountCode[] DiscountCodes { get; set; }

        [JsonProperty(PropertyName = "line_items")]
        public LineItem[] LineItems { get; set; }

        [JsonProperty(PropertyName = "shipping_lines")]
        public ShippingLine[] ShippingLines { get; set; }

        [JsonProperty(PropertyName = "payment_details")]
        public IPaymentDetails PaymentDetails { get; set; }

        [JsonProperty(PropertyName = "billing_address")]
        public AddressInformation BillingAddress { get; set; }

        [JsonProperty(PropertyName = "shipping_address")]
        public AddressInformation ShippingAddress { get; set; }

        [JsonProperty(PropertyName = "customer")]
        public Customer Customer { get; set; }

        [JsonProperty(PropertyName = "financial_status")]
        public string FinancialStatus { get; set; }

        [JsonProperty(PropertyName = "fulfillment_status")]
        public string FulfillmentStatus { get; set; }
    }

}
