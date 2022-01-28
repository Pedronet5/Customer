using CustomerAccount.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CustomerAccount.Domain.Models.Entities
{

    public class CustomerCardEntity
    {
        [Key]
        [DisplayName("Customer ID")]
        public int CustomerId { get; set; }

        [DisplayName("Card Number")]
        public long CardNumber { get; set; }

        [DisplayName("security Code")]
        public int CVV { get; set; }

        [DisplayName("Card ID")]
        public Guid CardId { get; set; }

        [DisplayName("Token")]
        public string Token { get; set; }

        [DisplayName("Creation Date")]
        public DateTime CreateDate { get; set; }

        public CustomerCardEntity(int custumeId, long cardNumber, int cvv)
        {
            CustomerId = custumeId;
            CardNumber = cardNumber;
            CVV = cvv;
            CreateDate = DateTime.UtcNow;
            CardId = Guid.NewGuid();

            GenerateCardToken();
        }

        public CustomerCardEntity()
        {

        }

        public void GenerateCardToken()
        {
            var cardPositionArray = new List<int>();
            int rotationTimes = CVV;
            var lastFourPosition = CardNumber.ToString().ToCharArray(12, 4).ToList();

            lastFourPosition.ForEach(x => cardPositionArray.Add(int.Parse(x.ToString())));
            Token = RotationHelper.Rotate(cardPositionArray.ToArray(), rotationTimes);
        }
    }
}
