﻿namespace BillingSystemOperational.InvoiceService.Application.Dto
{
    public class InvoiceLineUpdateDto
    {
        public int InvoiceLineId { get; set; } // Zorunlu: Güncellenen veya silinecek satırlar için
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
