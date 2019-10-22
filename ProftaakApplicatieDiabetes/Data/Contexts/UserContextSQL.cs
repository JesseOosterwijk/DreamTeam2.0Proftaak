using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text.RegularExpressions;
using Data.Interfaces;
using Models;

namespace Data.Contexts
{
    public class UserContextSQL : IUserContext
    {
        private readonly SqlConnection _conn = Connection.GetConnection();

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
                    "SELECT UserID, AccountType, FirstName, LastName, Birthdate, Sex, Email, Address, PostalCode, City, Status, Password " +
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
                User currentUser = new CareRecipient(1, "a", "b", "c,", "d", "f", Convert.ToDateTime("1988/12/20"), Models.Enums.Gender.Male, true, Models.Enums.AccountType.CareRecipient, "");
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string accountType = reader.GetString(1);
                        Models.Enums.Gender gender = (Models.Enums.Gender)Enum.Parse(typeof(Models.Enums.Gender), reader.GetString(5));


                        if (accountType == "Administrator")
                        {
                            currentUser = new CareRecipient(reader.GetInt32(0), reader.GetString(2), reader.GetString(3), reader.GetString(9), reader.GetString(8), email,
                                reader.GetDateTime(4), gender, reader.GetBoolean(10), Models.Enums.AccountType.Administrator, reader.GetString(11));
                        }
                        else if (accountType == "CareRecipient")
                        {
                            currentUser = new CareRecipient(reader.GetInt32(0), reader.GetString(2), reader.GetString(3), reader.GetString(9), reader.GetString(8), email,
                                reader.GetDateTime(4), gender, reader.GetBoolean(10), Models.Enums.AccountType.CareRecipient, reader.GetString(11));
                        }
                        else
                        {
                            currentUser = new CareRecipient(reader.GetInt32(0), reader.GetString(2), reader.GetString(3), reader.GetString(9), reader.GetString(8), email,
                                reader.GetDateTime(4), gender, reader.GetBoolean(10), Models.Enums.AccountType.Doctor, reader.GetString(11));
                        }

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

        public User GetUserById(int bsn)
        {
            try
            {
                string query =
                    "SELECT AccountType, FirstName, LastName, Birthdate, Sex, Email, Address, PostalCode, City, Status, Password " +
                    "FROM [User] " +
                    "WHERE [UserID] = @UserId";
                _conn.Open();

                SqlDataAdapter cmd = new SqlDataAdapter
                {
                    SelectCommand = new SqlCommand(query, _conn)
                };

                cmd.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = bsn;

                DataTable dt = new DataTable();
                cmd.Fill(dt);

                string accountType = dt.Rows[0].ItemArray[0].ToString();
                string firstName = dt.Rows[0].ItemArray[1].ToString();
                string lastName = dt.Rows[0].ItemArray[2].ToString();
                DateTime birthDate = Convert.ToDateTime(dt.Rows[0].ItemArray[3].ToString());
                Models.Enums.Gender gender = (Models.Enums.Gender)Enum.Parse(typeof(Models.Enums.Gender), dt.Rows[0].ItemArray[4].ToString());
                string email = dt.Rows[0].ItemArray[5].ToString();
                string address = dt.Rows[0].ItemArray[6].ToString();
                string city = dt.Rows[0].ItemArray[8].ToString();
                bool status = Convert.ToBoolean(dt.Rows[0].ItemArray[9].ToString());
                string password = dt.Rows[0].ItemArray[10].ToString();

                if (accountType == "Administrator")
                {
                    return new CareRecipient(bsn, firstName, lastName, address, city, email,
                        birthDate, gender, status, Models.Enums.AccountType.Administrator, password);
                }

                else if (accountType == "CareRecipient")
                {
                    return new CareRecipient(bsn, firstName, lastName, address, city, email,
                        birthDate, gender, status, Models.Enums.AccountType.CareRecipient, password);
                }
                return null;
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
                    "SELECT UserID, AccountType, FirstName, LastName, Birthdate, Sex, Address, PostalCode, City, Status, Password " +
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
                string accountType = dt.Rows[0].ItemArray[1].ToString();
                string firstName = dt.Rows[0].ItemArray[2].ToString();
                string lastName = dt.Rows[0].ItemArray[3].ToString();
                DateTime birthDate = Convert.ToDateTime(dt.Rows[0].ItemArray[4]);
                Models.Enums.Gender gender = (Models.Enums.Gender)Enum.Parse(typeof(Models.Enums.Gender), dt.Rows[0].ItemArray[5].ToString());
                string address = dt.Rows[0].ItemArray[6].ToString();
                string city = dt.Rows[0].ItemArray[8].ToString();
                bool status = Convert.ToBoolean(dt.Rows[0].ItemArray[9]);
                string hashedPassword = dt.Rows[0].ItemArray[10].ToString();


                if (!Hasher.SecurePasswordHasher.Verify(password, hashedPassword))
                    throw new ArgumentException("Password invalid");

                switch (accountType)
                {
                    case "Administrator":
                        return new CareRecipient(userId, firstName, lastName, address, city, emailAdress,
                            birthDate, gender, status, Models.Enums.AccountType.Administrator, hashedPassword);
                    case "CareRecipient":
                        return new CareRecipient(userId, firstName, lastName, address, city, emailAdress,
                            birthDate, gender, status, Models.Enums.AccountType.CareRecipient, hashedPassword);
                    case "Doctor":
                        return new CareRecipient(userId, firstName, lastName, address, city, emailAdress,
                            birthDate, gender, status, Models.Enums.AccountType.Doctor, hashedPassword);
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
