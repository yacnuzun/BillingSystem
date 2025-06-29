// InvoiceDeleteRequestDto
export interface InvoiceDeleteRequestDto {
  invoiceId: number; // C#'taki 'int' TypeScript'te 'number' olur
}

// InvoiceDeleteResponseDto
export interface InvoiceDeleteResponseDto {
  success: boolean;
  message: string;
}

export interface InvoiceRequestResult<T> {
    success: boolean;
    message: string;
    data?: T; // İsteğe bağlı olarak dönecek veri
}

// ---- Diğer İhtiyaç Duyulacak DTO'lar (Örnekler) ----

// InvoiceListQueryResponseDto (Fatura listesini getirmek için)
export interface InvoiceListResponseDto {
  invoiceId: number;
  invoiceNumber: string;
  customerTitle: string;
  totalAmount: number;
  invoiceDate: string; // ISO 8601 formatında string
  
}

export interface InvoiceLineDto {
  itemName: string;
  quantity: number;
  price: number;
}

export interface InvoiceCreateRequestDto {
  customerId: number;
  invoiceNumber: string;
  invoiceDate: string;
  totalAmount: number;
  userId: number;
  invoiceLines: InvoiceItemDto[];
}

export interface InvoiceCreateResponseDto {
  invoiceId: number;
  message: string;
}

// InvoiceDetailResponseDto (Tek bir faturanın detaylarını getirmek için)
export interface InvoiceDetailResponseDto {
  invoiceId: number;
  invoiceNumber: string;
  customerId: number;
  totalAmount: number;
  invoiceDate: string;
  userId: number;
  invoiceLines: InvoiceLineItemsDto[];
}

export interface InvoiceItemDto {
  itemName: string;
  quantity: number;
  price: number;
}

export interface InvoiceLineItemsDto{
  invoiceLineId: number;
  itemName: string;
  quantity: number;
  price: number;
}

// InvoiceCreateRequestDto (Yeni fatura oluşturmak için)
export interface InvoiceCreateRequestDto {
  customerName: string;
  customerAddress: string;
  issueDate: string; // Tarih string olarak gönderilebilir
  dueDate: string;
  items: InvoiceItemCreateDto[];
}

export interface InvoiceItemCreateDto {
  productName: number;
  quantity: number;
  price: number;
}

// InvoiceUpdateRequestDto (Fatura güncellemek için)
export interface InvoiceUpdateRequestDto {
  invoiceId: number;
  customerId: number;
  invoiceNumber: string;
  invoiceDate: string;
  totalAmount: number;
  userId: number;
  invoiceLines: InvoiceLineItemsDto[];
}
