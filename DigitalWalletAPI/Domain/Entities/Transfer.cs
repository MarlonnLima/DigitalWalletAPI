namespace DigitalWalletAPI.Domain.Entities
{
    public class Transfer
    {
        public int Id { get; set; }
        public int ReceiverWalletId { get; set; }
        public int SenderWalletId { get; set; }
        public DateTime DateTime { get; set; }
    }
}
