﻿using BillingSystemOperational.CustomerService.Domain;

namespace BillingSystemOperational.CustomerService.Application.Dto
{
    public class CustomerListItemDto
    {
        public int CustomerId { get; set; }
        public string Title { get; set; }

        public static CustomerListItemDto GetModel(Customer customer)
        {
            return new CustomerListItemDto
            {
                CustomerId = customer.CustomerId,
                Title = customer.Title,
            };
        }
    }
}
