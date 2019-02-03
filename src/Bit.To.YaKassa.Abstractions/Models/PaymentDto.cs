using System;
using System.Globalization;

namespace Bit.To.YaKassa.Abstractions
{
    public class PaymentDto
    {
        /// <summary>
        /// Идентификатор платежа
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Статус платежа
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// Признак оплаты заказа
        /// </summary>
        public bool IsPaid { get; set; }
        /// <summary>
        /// Сумма платежа
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// Код валюты в формате ISO-4217
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// Время подтверждения платежа
        /// </summary>
        public DateTime? CapturedAt { get; set; }
        /// <summary>
        /// Время создания заказа
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Время, до которого вы можете бесплатно отменить или подтвердить платеж
        /// </summary>
        public DateTime ExpiresAt { get; set; }
        /// <summary>
        /// Описание транзакции (не более 128 символов)
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Признак тестовой операции
        /// </summary>
        public bool IsTest { get; set; }
        /// <summary>
        /// Способ оплаты, который был использован для этого платежа.
        /// </summary>
        public string PaymentMethodType { get; set; }
        /// <summary>
        /// Сохраненный способ оплаты 
        /// </summary>
        public bool IsMethodSaved { get; set; }
        /// <summary>
        /// Первые 6 цифр номера банковской карты
        /// </summary>
        public string CardFirst6 { get; set; }
        /// <summary>
        /// Первые 4 цифры номера банковской карты
        /// </summary>
        public string CardLast4 { get; set; }
        /// <summary>
        /// Месяц, в котором истекает время действия банковской карты
        /// </summary>
        public string CardExpiryMonth { get; set; }
        /// <summary>
        /// Год, в котором истекает время действия банковской карты
        /// </summary>
        public string CardExpiryYear { get; set; }
        /// <summary>
        /// Тип банковской карты
        /// </summary>
        public string CardType { get; set; }
        /// <summary>
        /// Название способа оплаты
        /// </summary>
        public string PaymentMethodTitle { get; set; }

        public PaymentDto()
        {
            
        }

        public PaymentDto(string id, string status, bool isPaid, string amount, string currency, DateTime? capturedAt, DateTime createdAt, DateTime expiresAt, string description, bool isTest, string paymentMethodType, bool isMethodSaved, string cardFirst6, string cardLast4, string cardExpiryMonth, string cardExpiryYear, string cardType, string paymentMethodTitle)
        {
            Id = new Guid(id);
            Status = status;
            IsPaid = isPaid;
            Amount = decimal.Parse(amount, CultureInfo.InvariantCulture);
            Currency = currency;
            CapturedAt = capturedAt;
            CreatedAt = createdAt;
            ExpiresAt = expiresAt;
            Description = description;
            IsTest = isTest;
            PaymentMethodType = paymentMethodType;
            IsMethodSaved = isMethodSaved;
            CardFirst6 = cardFirst6;
            CardLast4 = cardLast4;
            CardExpiryMonth = cardExpiryMonth;
            CardExpiryYear = cardExpiryYear;
            CardType = cardType;
            PaymentMethodTitle = paymentMethodTitle;
        }
    }

    //public enum PaymentStatusEnum
    //{
    //    Pending = 1,
    //    WaitingForCapture = 2,
    //    Succeeded = 3,
    //    Canceled = 4
    //}

    //public enum PaymentMethodTypeEnum
    //{
    //    Alfabank = 1,
    //    ApplePay = 2,
    //    B2BSberbank = 3,
    //    BankCard = 4,
    //    Cash = 5,
    //    GooglePay = 6,
    //    Installments = 7,
    //    MobileBalance = 8,
    //    Psb = 9,
    //    Qiwi = 10,
    //    Sberbank = 11,
    //    Webmoney = 12,
    //    YandexMoney = 13
    //}

}
