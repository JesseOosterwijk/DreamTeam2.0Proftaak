using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text.RegularExpressions;
using Data.Interfaces;
using Data.Memory;
using Models;

namespace Data.Contexts
{
    public class UserContextSQL : IUserContext
    {
        private readonly SqlConnection _conn = Connection.GetConnection();

        public void CreateUser(User newUser)
        {
            try
            {
                _conn.Open();
                using (SqlCommand cmd = new SqlCommand("CreateUser", _conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BSN", SqlDbType.Int).Value = newUser.BSN;
                    cmd.Parameters.AddWithValue("@FirstName", SqlDbType.NVarChar).Value = newUser.FirstName;
                    cmd.Parameters.AddWithValue("@LastName", SqlDbType.NVarChar).Value = newUser.LastName;
                    cmd.Parameters.AddWithValue("@Email", SqlDbType.NVarChar).Value = newUser.EmailAddress;
                    cmd.Parameters.AddWithValue("@Address", SqlDbType.NVarChar).Value = newUser.Address;
                    cmd.Parameters.AddWithValue("@DateOfBirth", SqlDbType.DateTime).Value = newUser.BirthDate;
                    cmd.Parameters.AddWithValue("@Residence", SqlDbType.NVarChar).Value = newUser.Residence;
                    cmd.Parameters.AddWithValue("@Weight", SqlDbType.Int).Value = newUser.Weight;
                    cmd.Parameters.AddWithValue("@Gender", SqlDbType.Bit).Value = newUser.UserGender;
                    cmd.Parameters.AddWithValue("@Password", SqlDbType.NVarChar).Value = newUser.Password;
                    cmd.Parameters.AddWithValue("@AccountType", SqlDbType.NVarChar).Value = newUser.UserAccountType.ToString();
                    cmd.Parameters.AddWithValue("@Status", SqlDbType.Bit).Value = true;
                    cmd.Parameters.AddWithValue("@InfoSharing", SqlDbType.Bit).Value = false;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw new ArgumentException("User not Created");
            }
            finally
            {
                _conn.Close();
            }
        }

        public bool CheckIfUserAlreadyExists(string email)
        {
            try
            {
                _conn.Open();

                SqlCommand cmd = new SqlCommand("CheckIfUserAlreadyExists", _conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@email", email);

                int numberofAccounts = (int)cmd.ExecuteScalar();

                if (numberofAccounts != 0)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw new ArgumentException("Check failed");
            }
            finally
            {
                _conn.Close();
            }
            return true;
        }

        public bool CheckIfAccountIsActive(string email)
        {
            try
            {
                _conn.Open();

                SqlParameter emailParam = new SqlParameter
                {
                    ParameterName = "@email"
                };

                SqlCommand cmd = new SqlCommand("CheckIfAccountIsActive", _conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                emailParam.Value = email;
                cmd.Parameters.Add(emailParam);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (reader.GetBoolean(0))
                        {
                            _conn.Close();
                            return true;
                        }
                    }
                }
                else
                {
                    return false;
                }
                reader.Close();
            }
            finally
            {
                _conn.Close();
            }
            return false;
        }

        public bool CheckIfEmailIsValid(string userEmail)
        {
            if (string.IsNullOrWhiteSpace(userEmail))
            {
                return false;
            }
            try
            {
                userEmail = Regex.Replace(userEmail, @"(@)(.+)$", DomainMapper,
                    RegexOptions.None, TimeSpan.FromMilliseconds(200));

                string DomainMapper(Match match)
                {
                    IdnMapping idn = new IdnMapping();

                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            try
            {
                return Regex.IsMatch(userEmail,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public User GetUserInfo(string email)
        {
            try
            {
                string query =
                    "SELECT UserID, AccountType, FirstName, LastName, Birthdate, Sex, Email, Address, PostalCode, City, Status, Password, Weight " +
                    "FROM [User] " +
                    "WHERE Email = @email";

                _conn.Open();
                SqlParameter emailParam = new SqlParameter
                {
                    ParameterName = "@email"
                };

                SqlCommand cmd = new SqlCommand(query, _conn);
                emailParam.Value = email;
                cmd.Parameters.Add(emailParam);
                User currentUser = new CareRecipient(1, "a", "b", "c,", "d", "f", Convert.ToDateTime("1988/12/20"), Enums.Gender.Male, true, Enums.AccountType.CareRecipient, 85, "");
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string accountType = reader.GetString(1);
                        Enums.Gender gender = (Enums.Gender)Enum.Parse(typeof(Enums.Gender), reader.GetString(5));


                        //if (accountType == "Administrator")
                        //{
                        //    currentUser = new Administrator(reader.GetInt32(0), reader.GetString(2), reader.GetString(3), reader.GetString(9), reader.GetString(8), email,
                        //        reader.GetDateTime(4), gender, reader.GetBoolean(10), Enums.AccountType.Administrator, reader.GetInt32(12), reader.GetString(11));
                        //}
                        //else if (accountType == "Doctor")
                        //{
                        //    currentUser = new Doctor(reader.GetInt32(0), reader.GetString(2), reader.GetString(3), reader.GetString(9), reader.GetString(8), email,
                        //        reader.GetDateTime(4), gender, reader.GetBoolean(10), Enums.AccountType.Doctor, reader.GetInt32(12), reader.GetString(11));
                        //}
                        //else
                        //{
                        //    currentUser = new CareRecipient(reader.GetInt32(0), reader.GetString(2), reader.GetString(3), reader.GetString(9), reader.GetString(8), email,
                        //        reader.GetDateTime(4), gender, reader.GetBoolean(10), Enums.AccountType.CareRecipient, reader.GetInt32(12), reader.GetString(11));
                        //}

                        return currentUser;
                    }
                    return currentUser;
                }
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                _conn.Close();
            }
        }

        public User GetUserById(int id)
        {
            var user = new User();
            try
            {
                string query =
                    "SELECT * " +
                    "FROM [User] " +
                    "WHERE [UserID] = @UserId";
                _conn.Open();

                using (SqlCommand cmd = new SqlCommand(query, _conn))
                {
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = id;

                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        user.FirstName = (string)rdr["FirstName"];
                        user.LastName = (string)rdr["LastName"];
                        user.Weight = (int)rdr["Weight"];
                        user.EmailAddress = (string)rdr["Email"];
                    }
                }
                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }

        public User CheckValidityUser(string emailAdress, string password)
        {
            try
            {
                string query =
                    "SELECT UserID, BSN, AccountType, FirstName, LastName, DateOfBirth, Gender, Address, Residence, Status, Password, Weight " +
                    "FROM [User] " +
                    "WHERE [Email] = @Email";
                _conn.Open();

                SqlDataAdapter cmd = new SqlDataAdapter
                {
                    SelectCommand = new SqlCommand(query, _conn)
                };

                cmd.SelectCommand.Parameters.Add("@Email", SqlDbType.VarChar).Value = emailAdress;
                cmd.SelectCommand.Parameters.Add("@Password", SqlDbType.VarChar).Value = password;

                DataTable dt = new DataTable();
                cmd.Fill(dt);

                int userId = Convert.ToInt32(dt.Rows[0].ItemArray[0]);
                int BSN = Convert.ToInt32(dt.Rows[0].ItemArray[1]);
                string accountType = dt.Rows[0].ItemArray[2].ToString();
                string firstName = dt.Rows[0].ItemArray[3].ToString();
                string lastName = dt.Rows[0].ItemArray[4].ToString();
                DateTime birthDate = Convert.ToDateTime(dt.Rows[0].ItemArray[5]);
                Enums.Gender gender = (Enums.Gender)Enum.Parse(typeof(Enums.Gender), dt.Rows[0].ItemArray[6].ToString());
                string address = dt.Rows[0].ItemArray[7].ToString();
                string city = dt.Rows[0].ItemArray[8].ToString();
                bool status = Convert.ToBoolean(dt.Rows[0].ItemArray[9]);
                int weight = Convert.ToInt32(dt.Rows[0].ItemArray[11]);
                string hashedPassword = dt.Rows[0].ItemArray[10].ToString();


                if (!Hasher.SecurePasswordHasher.Verify(password, hashedPassword))
                    throw new ArgumentException("Password invalid");

                switch (accountType)
                {
                    case "Administrator":
                        return new Administrator(userId, firstName, lastName, address, city, emailAdress,
                            birthDate, gender, status, Enums.AccountType.Administrator, weight, hashedPassword);
                    case "CareRecipient":
                        return new CareRecipient(userId, BSN, Enums.AccountType.CareRecipient, firstName, lastName, emailAdress, hashedPassword, address, city,
                            gender, birthDate, weight, status);
                    case "Doctor":
                        return new Doctor(userId, BSN, Enums.AccountType.Doctor, firstName, lastName, emailAdress, hashedPassword, address, city,
                            gender, birthDate, weight, status);
                    default:
                        throw new AggregateException("User not found");
                }

            }
            catch (Exception)
            {
                throw new ArgumentException("User cannot be checked");
            }
            finally
            {
                _conn.Close();
            }
        }
    }

}
