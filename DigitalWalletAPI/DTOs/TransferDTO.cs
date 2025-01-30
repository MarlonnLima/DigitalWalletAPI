namespace DigitalWalletAPI.DTOs
{
    public class TransferDTO
    {
        public int SenderWalletId { get; set; }
        public int ReceiverWalletId { get; set; }
        public decimal Amount { get; set; }
    }
}
