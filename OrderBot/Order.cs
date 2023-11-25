using Microsoft.Data.Sqlite;

namespace OrderBot
{
    public class Order : ISQLModel
    {
        private string _intrested = String.Empty;
        private string _prefer = String.Empty;
        private string _reservation = String.Empty;
        private string _rented = String.Empty;
        private string _status = String.Empty;


        public string Intrested
        {
            get => _intrested;
            set => _intrested = value;
        }

        public string Prefer
        {
            get => _prefer;
            set => _prefer = value;
        }
        public string Reservation
        {
            get => _reservation;
            set => _reservation = value;
        }
        public string Rented
        {
            get => _rented;
            set => _rented = value;
        }
        public string Status
        {
            get => _status;
            set => _status = value;
        }

        public void Save()
        {
            using (var connection = new SqliteConnection(DB.GetConnectionString()))
            {
                connection.Open();

                var commandUpdate = connection.CreateCommand();
                commandUpdate.CommandText =
                @"
        UPDATE orders
        SET size = $size
        WHERE phone = $phone
    ";
                //commandUpdate.Parameters.AddWithValue("$size", Size);
                //commandUpdate.Parameters.AddWithValue("$phone", Phone);
                int nRows = commandUpdate.ExecuteNonQuery();
                if (nRows == 0)
                {
                    var commandInsert = connection.CreateCommand();
                    commandInsert.CommandText =
                    @"
            INSERT INTO orders(size, phone)
            VALUES($size, $phone)
        ";
                    //commandInsert.Parameters.AddWithValue("$size", Size);
                    //commandInsert.Parameters.AddWithValue("$phone", Phone);
                    int nRowsInserted = commandInsert.ExecuteNonQuery();

                }
            }

        }
    }
}
