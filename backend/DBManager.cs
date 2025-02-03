using Microsoft.AspNetCore.Hosting.Server;
using MySql.Data.MySqlClient;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Transactions;
using password_manager_API.Models;
using Microsoft.AspNetCore.Http.Metadata;
using BCrypt.Net;
using Org.BouncyCastle.Crypto.Generators;
using password_manager_API.Helpers;
using System.Security.Cryptography;
using Org.BouncyCastle.Bcpg;
namespace password_manager_API
{
    public class DBManager
    {
        private MySqlConnection conn;
        public DBManager() {
            string connectionString =
                $"server=localhost;" +
                $"userid=root;" +
                $"password=Geheim123*;" +
                $"database=passwordmanager;";

            conn = new MySqlConnection(connectionString);
            conn.Open();
        }
        public DBResult IsUserTaken(string username) {

            string query = "SELECT Count(*) FROM users WHERE @Username = username";
            try {
                using (MySqlCommand cmd = new MySqlCommand(query, conn)) {
                    cmd.Parameters.AddWithValue("@Username", username);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());  

                    if(count > 1) {
                        return new DBResult(false, "Benutzername bereits vergeben");
                    }
                    else 
                        return new DBResult(true, "");
                }                       
            }
            catch {
                return new DBResult(false, "Database Failed");       
            }
        }
        public DBResult CreateUser(string username, string hashedPassword) {
            string query = "INSERT INTO users (username, passwordhas) VALUES (@Username, @Password)";

            try {
                using (MySqlCommand cmd = new MySqlCommand(query, conn)) {

                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", hashedPassword);

                    cmd.ExecuteNonQuery();
                }
                return new DBResult(true, "", "Account created");
            }
            catch {
                return new DBResult(false, "Database Failed");
            }
        }
        public DBResult CheckUsername(string username) {
            string query = "SELECT * FROM users WHERE username = @Username";

            try {
                using (MySqlCommand cmd = new MySqlCommand(query, conn)) {
                    cmd.Parameters.AddWithValue("@Username", username);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    if (count == 0) {
                        return new DBResult(false, "No User Found");
                    } else return new DBResult(true, "", "User Found");

                }
            }
            catch {
                return new DBResult(false, "Database Failed");
            }

        }
        public DBResult CheckLogin(string username, string password) {
            string query = "SELECT passwordhash FROM users WHERE username = @Username";

            try {
                using (MySqlCommand cmd = new MySqlCommand(query, conn)) {

                    cmd.Parameters.AddWithValue("@Username", username);

                    using (MySqlDataReader reader = cmd.ExecuteReader()) {

                        if (reader.Read()) {
                            string storedPasswordhash = reader.GetString("passwordhash");

                            if (!HashingHelper.VerifyPassword(password, storedPasswordhash)) {

                                return new DBResult(false, "Password doesnt match");

                            } else
                                return new DBResult(true, "", "Erfolgreiches Login");

                        } else return new DBResult(false, "Database failed");
                    }
                }
            }
            catch { return new DBResult(false, "Database failed"); }     
        }
        public DBResult SaveUserTotpCredentials(string username, string encryptedTotpSecret, string iv) {
            string query  = "UPDATE users SET totp_secret = @TotpSecret, totp_iv = @IV WHERE username = @Username";

            try {
                using (MySqlCommand cmd = new MySqlCommand(query, conn)) {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@TotpSecret", encryptedTotpSecret);
                    cmd.Parameters.AddWithValue("@IV", iv);

                    int affectedRows = cmd.ExecuteNonQuery();
                    Console.WriteLine(affectedRows);

                    if (affectedRows > 0) {
                        return new DBResult(true, "Totp Added");
                    }
                    else {
                        return new DBResult(false, $"No rows were updated.'{username}' might not exist.");
                    }
                }              
            }
            catch { return new DBResult(false, "Database Failed"); }
        }
        public DBResult GetUserTotpCredentials(string username) {
            string query = "SELECT totp_secret, totp_iv FROM users WHERE username = @Username";

            try {
                using (MySqlCommand cmd = new MySqlCommand(query, conn)) {

                    cmd.Parameters.AddWithValue("@Username", username);

                    using (MySqlDataReader reader = cmd.ExecuteReader()) {

                        if (reader.Read()) {
                            string storedTotpSecret = reader.GetString("totp_secret");
                            string storedTotpIV = reader.GetString("totp_iv");

                            var dto = new TotpCredentialsDto {
                                Secret = storedTotpSecret,
                                IV = storedTotpIV
                            };
                          
                            return new DBResult(true, "", dto);

                        } else return new DBResult(false, "Database failed");
                    }
                }
            }
            catch { return new DBResult(false, "Database failed"); }
        }
        public void Close() {
            conn.Close();
        }
       
    }
}
