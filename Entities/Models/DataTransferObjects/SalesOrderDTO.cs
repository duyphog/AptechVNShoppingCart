﻿using System;
namespace Entities.Models.DataTransferObjects
{
    public class SalesOrderDTO
    {
        public string Id { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid? AppUserId { get; set; }
        public int? OrderStatusId { get; set; }
        public int? PaymentTypeId { get; set; }
        public string DeliveryTypeId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string Contry { get; set; }
        public string StreetAddress { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string OrderNote { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public bool? Status { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public decimal TotalAmount { get => Quantity*Price; }
    }
}
