using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AMRConnector
{
    public class DbConnector
    {
        private readonly string connectionString;

        public DbConnector()
        {
            // Update this with your machine name & instance if different
            connectionString = "Data Source=DESKTOP-Q3UC528\\SQLEXPRESS01;Initial Catalog=Hotel_Management_Systems;Integrated Security=True;MultipleActiveResultSets=true;TrustServerCertificate=True;";
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        // ✅ Login check
        public bool IsValidNamePass(string username, string password)
        {
            bool isValid = false;

            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();

                    string query = "SELECT COUNT(*) FROM User_Table WHERE User_Name = @Username AND User_Password = @Password";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);

                        int count = (int)cmd.ExecuteScalar();
                        isValid = (count > 0);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database connection failed: " + ex.Message);
                }
            }

            return isValid;
        }

        // ✅ Add user
        public bool AddUser(string username, string password)
        {
            bool success = false;

            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO User_Table (User_Name, User_Password) VALUES (@Username, @Password)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("User added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            success = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to add user: " + ex.Message);
                }
            }

            return success;
        }

        // ✅ Update user
        public bool UpdateUser(int userId, string username, string password)
        {
            bool success = false;

            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE User_Table SET User_Name = @Username, User_Password = @Password WHERE User_ID = @UserID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);
                        cmd.Parameters.AddWithValue("@UserID", userId);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("User updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            success = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to update user: " + ex.Message);
                }
            }

            return success;
        }

        // ✅ Delete user
        public bool DeleteUser(int userId)
        {
            bool success = false;

            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM User_Table WHERE User_ID = @UserID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userId);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("User deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            success = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to delete user: " + ex.Message);
                }
            }

            return success;
        }

        // ✅ Display/search user (for DataGridView)
        public void DisplayAndSearchUser(string query, DataGridView dgv)
        {
            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgv.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to retrieve users: " + ex.Message);
                }
            }
        }

        // ✅ Add client
        public bool AddClient(string firstName, string lastName, string phone, string address)
        {
            bool success = false;

            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Client_Table (Client_FirstName, Client_LastName, Client_Phone, Client_Address) " +
                                   "VALUES (@FirstName, @LastName, @Phone, @Address)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@FirstName", firstName);
                        cmd.Parameters.AddWithValue("@LastName", lastName);
                        cmd.Parameters.AddWithValue("@Phone", phone);
                        cmd.Parameters.AddWithValue("@Address", address);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Client added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            success = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to add client: " + ex.Message);
                }
            }

            return success;
        }

        // ✅ Update client
        public bool UpdateClient(int clientId, string firstName, string lastName, string phone, string address)
        {
            bool success = false;

            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE Client_Table SET Client_FirstName = @FirstName, Client_LastName = @LastName, " +
                                   "Client_Phone = @Phone, Client_Address = @Address WHERE Client_ID = @ClientID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@FirstName", firstName);
                        cmd.Parameters.AddWithValue("@LastName", lastName);
                        cmd.Parameters.AddWithValue("@Phone", phone);
                        cmd.Parameters.AddWithValue("@Address", address);
                        cmd.Parameters.AddWithValue("@ClientID", clientId);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Client updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            success = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to update client: " + ex.Message);
                }
            }

            return success;
        }

        // ✅ Delete client
        public bool DeleteClient(int clientId)
        {
            bool success = false;

            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM Client_Table WHERE Client_ID = @ClientID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ClientID", clientId);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Client deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            success = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to delete client: " + ex.Message);
                }
            }

            return success;
        }

        // ✅ Add room
        public bool AddRoom(string roomType, string roomPhone, string roomFree)
        {
            bool success = false;
            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Room_Table (Room_Type, Room_Phone, Room_Free) VALUES (@Type, @Phone, @Free)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Type", roomType);
                        cmd.Parameters.AddWithValue("@Phone", roomPhone);
                        cmd.Parameters.AddWithValue("@Free", roomFree);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Room added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            success = true;
                        }
                    }
                }
                catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601) // unique key violation
                {
                    MessageBox.Show("Room phone must be unique. A room with this phone already exists.", "Duplicate value", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to add room: " + ex.Message);
                }
            }
            return success;
        }

        // ✅ Update room
        public bool UpdateRoom(int roomNumber, string roomType, string roomPhone, string roomFree)
        {
            bool success = false;
            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"UPDATE Room_Table 
                             SET Room_Type = @Type, Room_Phone = @Phone, Room_Free = @Free 
                             WHERE Room_Number = @No";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Type", roomType);
                        cmd.Parameters.AddWithValue("@Phone", roomPhone);
                        cmd.Parameters.AddWithValue("@Free", roomFree);
                        cmd.Parameters.AddWithValue("@No", roomNumber);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Room updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            success = true;
                        }
                    }
                }
                catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
                {
                    MessageBox.Show("Room phone must be unique. A room with this phone already exists.", "Duplicate value", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to update room: " + ex.Message);
                }
            }
            return success;
        }

        // ✅ Delete room
        public bool DeleteRoom(int roomNumber)
        {
            bool success = false;
            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM Room_Table WHERE Room_Number = @No";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@No", roomNumber);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Room deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            success = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to delete room: " + ex.Message);
                }
            }
            return success;
        }


        // ------------------------ Reservation methods ------------------------

        /// <summary>
        /// Create a reservation and return true on success.
        /// Expects roomNumberStr and clientIdStr that can be parsed to int.
        /// </summary>
        public bool Reservation(string roomType, string roomNumberStr, string clientIdStr, string dateIn, string dateOut)
        {
            bool success = false;

            if (!int.TryParse(roomNumberStr, out int roomNumber))
            {
                MessageBox.Show("Invalid room number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!int.TryParse(clientIdStr, out int clientId))
            {
                MessageBox.Show("Invalid client ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"INSERT INTO Reservation_Table 
                             (Reservation_Room_Type, Reservation_Room_Number, Reservation_Client_ID, Reservation_In, Reservation_Out)
                             VALUES (@Type, @RoomNo, @ClientID, @InDate, @OutDate)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Type", roomType);
                        cmd.Parameters.AddWithValue("@RoomNo", roomNumber);
                        cmd.Parameters.AddWithValue("@ClientID", clientId);
                        cmd.Parameters.AddWithValue("@InDate", dateIn);
                        cmd.Parameters.AddWithValue("@OutDate", dateOut);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Reservation created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            success = true;
                        }
                    }
                }
                catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601) // unique constraint violation
                {
                    MessageBox.Show("This client already has a reservation (unique constraint).", "Duplicate reservation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to create reservation: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return success;
        }

        /// <summary>
        /// Update an existing reservation by reservation id.
        /// </summary>
        public bool UpdateReservation(int reservationId, string roomType, string roomNumberStr, string clientIdStr, string dateIn, string dateOut)
        {
            bool success = false;

            if (!int.TryParse(roomNumberStr, out int roomNumber))
            {
                MessageBox.Show("Invalid room number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!int.TryParse(clientIdStr, out int clientId))
            {
                MessageBox.Show("Invalid client ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"UPDATE Reservation_Table
                             SET Reservation_Room_Type = @Type,
                                 Reservation_Room_Number = @RoomNo,
                                 Reservation_Client_ID = @ClientID,
                                 Reservation_In = @InDate,
                                 Reservation_Out = @OutDate
                             WHERE Reservation_ID = @ResID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Type", roomType);
                        cmd.Parameters.AddWithValue("@RoomNo", roomNumber);
                        cmd.Parameters.AddWithValue("@ClientID", clientId);
                        cmd.Parameters.AddWithValue("@InDate", dateIn);
                        cmd.Parameters.AddWithValue("@OutDate", dateOut);
                        cmd.Parameters.AddWithValue("@ResID", reservationId);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Reservation updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            success = true;
                        }
                        else
                        {
                            MessageBox.Show("No reservation found with the provided ID.", "Not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
                {
                    MessageBox.Show("Update failed due to duplicate client reservation (unique constraint).", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to update reservation: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return success;
        }

        /// <summary>
        /// Delete reservation by reservation id.
        /// </summary>
        public bool DeleteReservation(int reservationId)
        {
            bool success = false;

            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM Reservation_Table WHERE Reservation_ID = @ResID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ResID", reservationId);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Reservation deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            success = true;
                        }
                        else
                        {
                            MessageBox.Show("No reservation found with the provided ID.", "Not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to delete reservation: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return success;
        }

        /// <summary>
        /// Update the Room_Free status for a room (roomNumberStr expected to be an integer string).
        /// </summary>
        public bool UpdateReservationRoom(string roomNumberStr, string roomFree)
        {
            bool success = false;

            if (!int.TryParse(roomNumberStr, out int roomNumber))
            {
                MessageBox.Show("Invalid room number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE Room_Table SET Room_Free = @Free WHERE Room_Number = @No";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Free", roomFree);
                        cmd.Parameters.AddWithValue("@No", roomNumber);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            // optional: don't show message every time to avoid spam
                            success = true;
                        }
                        else
                        {
                            MessageBox.Show("No room found with the provided number.", "Not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to update room status: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return success;
        }

        // ✅ Load room numbers by type into ComboBox
        public void RoomTypeAndNo(string roomType, ComboBox comboBox)
        {
            using (SqlConnection conn = GetConnection())
            {
                string query = "SELECT Room_Number FROM Room_Table WHERE Room_Type = @RoomType AND Room_Free = 'Yes' ORDER BY Room_Number";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@RoomType", roomType);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        comboBox.Items.Clear();
                        comboBox.Items.Add("Select room");

                        while (reader.Read())
                        {
                            comboBox.Items.Add(reader["Room_Number"].ToString());
                        }

                        if (comboBox.Items.Count > 0)
                            comboBox.SelectedIndex = 0;
                    }
                }
            }
        }


        // Add this inside your DbConnector class
        public int Count(string query)
        {
            int result = 0;
            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        result = (int)cmd.ExecuteScalar();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to execute count query: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return result;
        }





    }
}
