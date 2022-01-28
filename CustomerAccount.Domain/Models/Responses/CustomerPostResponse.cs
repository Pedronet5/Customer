using System;

namespace CustomerAccount.Domain.Responses
{
    public class CustomerPostResponse
    {
        /// <summary>
        /// The cardId generated during the process
        /// </summary>
        public Guid CardId { get; set; }

        /// <summary>
        /// The token generated during the process
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// The UTC date of stored card
        /// </summary>
        public DateTime CreateDate { get; set; }
    }
}
