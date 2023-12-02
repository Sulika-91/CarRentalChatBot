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
      

        public string Intrested{
            get => _intrested;
            set => _intrested = value;
        }

        public string Prefer{
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

        public void Save(){
           using (var connection = new SqliteConnection(DB.GetConnectionString()))
            {
                connection.Open();

                var commandUpdate = connection.CreateCommand();
                commandUpdate.CommandText =
               @"
                UPDATE preferences
                SET 
                intrested = $intrested, 
                prefer = $prefer, 
                reservation = $reservation, 
                rented = $rented, 
                status = $status
                WHERE intrested = $intrested
            ";
                commandUpdate.Parameters.AddWithValue("$intrested", Intrested);
                commandUpdate.Parameters.AddWithValue("$prefer", Prefer);
                commandUpdate.Parameters.AddWithValue("$reservation", Reservation);
                commandUpdate.Parameters.AddWithValue("$rented", Rented);
                commandUpdate.Parameters.AddWithValue("$status", Status);

                int nRows = commandUpdate.ExecuteNonQuery();
                if(nRows == 0){
                    var commandInsert = connection.CreateCommand();
                    commandInsert.CommandText =
                        @"
                    INSERT INTO preferences(intrested, prefer, reservation, rented, status)
                    VALUES($intrested, $prefer, $reservation, $rented, $status)
                ";
                    commandInsert.Parameters.AddWithValue("$intrested", Intrested);
                    commandInsert.Parameters.AddWithValue("$prefer", Prefer);
                    commandInsert.Parameters.AddWithValue("$reservation", Reservation);
                    commandInsert.Parameters.AddWithValue("$rented", Rented);
                    commandInsert.Parameters.AddWithValue("$status", Status);

                    int nRowsInserted = commandInsert.ExecuteNonQuery();

                }
            }

        }
    }
}
