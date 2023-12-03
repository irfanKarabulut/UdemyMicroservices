using UdemyMicroservices.Web.Models.FakePayment;

namespace UdemyMicroservices.Web.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<bool> ReceivePayment(PaymentInfoInput paymentInfoInput);
    }
}
