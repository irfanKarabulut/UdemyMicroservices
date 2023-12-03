using System.ComponentModel.DataAnnotations;

namespace UdemyMicroservices.Web.Models.Order
{
    public class CheckoutInfoInput
    {
        [Display(Name ="İl")]
        public string Province { get; set; }

        [Display(Name = "İlçe")]
        public string District { get; set; }

        [Display(Name = "Cadde")]
        public string Street { get; set; }

        [Display(Name = "Posta Kodu")]
        public string ZipCode { get; set; }

        [Display(Name = "Açık Adres")]
        public string Line { get; set; }



        [Display(Name = "Kart Sahibinin Adı")]
        public string CardName { get; set; }

        [Display(Name = "K.Kartı Numarası")]
        public string CardNumber { get; set; }

        [Display(Name = "Son Kullanma Tarihi")]
        public string Expiration { get; set; }

        [Display(Name = "Güvenlik Numarası")]
        public string CVV { get; set; }
     
    }
}
