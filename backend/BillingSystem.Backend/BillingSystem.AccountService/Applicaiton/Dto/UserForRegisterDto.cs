﻿using BillingSystem.Shared.Abstract;

namespace BillingSystem.AccountService.Applicaiton
{
    public class UserForRegisterDto : IDto
    {
        public string Password { get; set; }
        public string UserName { get; set; }
        public CustomerAddDto Customer { get; set; }
    }
    public class CustomerAddDto
    {
        public string TaxNumber { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string EMail { get; set; }
        public int UserId { get; set; }
    }
    public class CustomerAddResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}