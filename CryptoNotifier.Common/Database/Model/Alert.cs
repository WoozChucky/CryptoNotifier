using SQLite;

namespace CryptoNotifier.Common.Database.Model
{
    public class Alert
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Currency { get; set; }

        public float Amount { get; set; }

        public float TransactionAmount { get; set; }

        public ActionType Action { get; set; }

        public AlertStatus Status { get; set; }
    }
}
