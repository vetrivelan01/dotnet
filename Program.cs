using System;
using Microsoft.Data.SqlClient;

namespace Com.Wipro.Bank.Util
{
    public class DBUtil
    {
        public static SqlConnection GetDBConnection()
        {
            string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=banksystemDB;Integrated Security=True;TrustServerCertificate=True";
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }

    public class InsufficientBalanceException : Exception
    {
        public override string ToString() => "INSUFFICIENT BALANCE";
    }
}

namespace Com.Wipro.Bank.Bean
{
    public class TransferBean
    {
        public string FromAccount { get; set; }
        public string ToAccount { get; set; }
        public double Amount { get; set; }
    }

    public class AccountBean
    {
        public string AccountNumber { get; set; }
        public string AccountHolder { get; set; }
        public double Balance { get; set; }
    }
}

namespace Com.Wipro.Bank.Dao
{
    using Com.Wipro.Bank.Util;
    using Com.Wipro.Bank.Bean;

    public class BankDAO
    {
        public bool validateAccount(string accountNumber)
        {
            using var con = DBUtil.GetDBConnection();
            using var cmd = con.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM ACCOUNT_TBL WHERE Account_Number = @acc";
            cmd.Parameters.AddWithValue("@acc", accountNumber);
            var result = cmd.ExecuteScalar();
            return result != null && Convert.ToInt32(result) > 0;
        }

        public void createAccount(AccountBean acc)
        {
            using var con = DBUtil.GetDBConnection();
            using var cmd = con.CreateCommand();
            cmd.CommandText = "INSERT INTO ACCOUNT_TBL (Account_Number, Account_Holder, Balance) VALUES (@acc, @holder, @bal)";
            cmd.Parameters.AddWithValue("@acc", acc.AccountNumber);
            cmd.Parameters.AddWithValue("@holder", acc.AccountHolder);
            cmd.Parameters.AddWithValue("@bal", acc.Balance);
            cmd.ExecuteNonQuery();
        }

        public AccountBean searchAccount(string accountNumber)
        {
            using var con = DBUtil.GetDBConnection();
            using var cmd = con.CreateCommand();
            cmd.CommandText = "SELECT Account_Number, Account_Holder, Balance FROM ACCOUNT_TBL WHERE Account_Number = @acc";
            cmd.Parameters.AddWithValue("@acc", accountNumber);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new AccountBean
                {
                    AccountNumber = reader["Account_Number"].ToString(),
                    AccountHolder = reader["Account_Holder"].ToString(),
                    Balance = Convert.ToDouble(reader["Balance"])
                };
            }
            return null; 
        }

        public double getBalance(string accountNumber)
        {
            using var con = DBUtil.GetDBConnection();
            using var cmd = con.CreateCommand();
            cmd.CommandText = "SELECT Balance FROM ACCOUNT_TBL WHERE Account_Number = @acc";
            cmd.Parameters.AddWithValue("@acc", accountNumber);
            var result = cmd.ExecuteScalar();
            return result == null || result == DBNull.Value ? 0.0 : Convert.ToDouble(result);
        }

        public void updateBalance(string accountNumber, double newBalance)
        {
            using var con = DBUtil.GetDBConnection();
            using var cmd = con.CreateCommand();
            cmd.CommandText = "UPDATE ACCOUNT_TBL SET Balance = @bal WHERE Account_Number = @acc";
            cmd.Parameters.AddWithValue("@bal", newBalance);
            cmd.Parameters.AddWithValue("@acc", accountNumber);
            cmd.ExecuteNonQuery();
        }

        public bool updateAccount(AccountBean acc)
        {
            using var con = DBUtil.GetDBConnection();
            using var cmd = con.CreateCommand();
            cmd.CommandText = "UPDATE ACCOUNT_TBL SET Account_Holder = @holder, Balance = @bal WHERE Account_Number = @acc";
            cmd.Parameters.AddWithValue("@holder", acc.AccountHolder);
            cmd.Parameters.AddWithValue("@bal", acc.Balance);
            cmd.Parameters.AddWithValue("@acc", acc.AccountNumber);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool deleteAccount(string accountNumber)
        {
            using var con = DBUtil.GetDBConnection();
            using var cmd = con.CreateCommand();
            cmd.CommandText = "DELETE FROM ACCOUNT_TBL WHERE Account_Number = @acc";
            cmd.Parameters.AddWithValue("@acc", accountNumber);
            return cmd.ExecuteNonQuery() > 0;
        }

       
        public void recordTransaction(TransferBean t)
        {
            using var con = DBUtil.GetDBConnection();
            using var cmd = con.CreateCommand();
            cmd.CommandText = "INSERT INTO TRANSACTION_TBL (Transaction_ID, From_Account, To_Account, Amount, Transaction_Date) VALUES (@id, @from, @to, @amt, @date)";
            cmd.Parameters.AddWithValue("@id", new Random().Next(1000, 9999));
            cmd.Parameters.AddWithValue("@from", t.FromAccount);
            cmd.Parameters.AddWithValue("@to", t.ToAccount);
            cmd.Parameters.AddWithValue("@amt", t.Amount);
            cmd.Parameters.AddWithValue("@date", DateTime.Now);
            cmd.ExecuteNonQuery();
        }

        public bool deleteTransaction(int transactionId)
        {
            using var con = DBUtil.GetDBConnection();
            using var cmd = con.CreateCommand();
            cmd.CommandText = "DELETE FROM TRANSACTION_TBL WHERE Transaction_ID = @id";
            cmd.Parameters.AddWithValue("@id", transactionId);
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}

namespace Com.Wipro.Bank.Service
{
    using Com.Wipro.Bank.Dao;
    using Com.Wipro.Bank.Bean;
    using Com.Wipro.Bank.Util;

    class Program
    {
        static void Main()
        {
            BankMain bank = new BankMain();
            int choice;

            do
            {
                Console.WriteLine("\n--- BANK MENU ---");
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. Check Balance");
                Console.WriteLine("3. Transfer Money");
                Console.WriteLine("4. Delete Account");
                Console.WriteLine("5. Delete Transaction");
                Console.WriteLine("6. Exit");
                Console.WriteLine("7. Search Account");
                Console.WriteLine("8. Update Account");
                Console.Write("Enter your choice: ");

                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.Write("Enter Account Number: ");
                        string accNo = Console.ReadLine();
                        Console.Write("Enter Account Holder Name: ");
                        string holder = Console.ReadLine();
                        Console.Write("Enter Initial Balance: ");
                        double bal = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine(bank.openAccount(new AccountBean { AccountNumber = accNo, AccountHolder = holder, Balance = bal }));
                        break;

                    case 2:
                        Console.Write("Enter Account Number: ");
                        string checkAcc = Console.ReadLine();
                        Console.WriteLine(bank.checkBalance(checkAcc));
                        break;

                    case 3:
                        Console.Write("From Account: ");
                        string fromAcc = Console.ReadLine();
                        Console.Write("To Account: ");
                        string toAcc = Console.ReadLine();
                        Console.Write("Amount: ");
                        double amt = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine(bank.transfer(new TransferBean { FromAccount = fromAcc, ToAccount = toAcc, Amount = amt }));
                        break;

                    case 4:
                        Console.Write("Enter Account Number to Delete: ");
                        string delAcc = Console.ReadLine();
                        Console.WriteLine(bank.closeAccount(delAcc));
                        break;

                    case 5:
                        Console.Write("Enter Transaction ID to Delete: ");
                        int transId = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine(bank.deleteTransaction(transId) ? "Transaction deleted" : "Transaction not found");
                        break;

                    case 7:
                        Console.Write("Enter Account Number to Search: ");
                        string searchAcc = Console.ReadLine();
                        Console.WriteLine(bank.searchAccount(searchAcc));
                        break;

                    case 8:
                        Console.Write("Enter Account Number to Update: ");
                        string updAccNo = Console.ReadLine();
                        Console.Write("Enter New Account Holder Name: ");
                        string newHolder = Console.ReadLine();
                        Console.Write("Enter New Balance: ");
                         double newBal = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine(bank.updateAccount(new AccountBean { AccountNumber = updAccNo, AccountHolder = newHolder, Balance = newBal }));
                        break;

                    case 6:
                        Console.WriteLine("Exiting...");
                        break;

                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            } while (choice != 6);
        }
    }

    public class BankMain
    {
        BankDAO dao = new BankDAO();

        public string openAccount(AccountBean acc)
        {
            if (dao.validateAccount(acc.AccountNumber))
                return "ACCOUNT ALREADY EXISTS";

            dao.createAccount(acc);
            return "ACCOUNT CREATED";
        }

        public string checkBalance(string acc)
        {
            if (!dao.validateAccount(acc))
                return "ACCOUNT NUMBER INVALID";

            double bal = dao.getBalance(acc);
            return $"BALANCE IS: {bal}";
        }

        public string closeAccount(string acc)
        {
            if (!dao.validateAccount(acc))
                return "ACCOUNT NOT FOUND";

            return dao.deleteAccount(acc) ? "ACCOUNT CLOSED" : "DELETE FAILED";
        }

        public string searchAccount(string acc)
        {
            var account = dao.searchAccount(acc);
            if (account == null)
                return "ACCOUNT NOT FOUND";

            return $"ACCOUNT NUMBER: {account.AccountNumber}\nHOLDER: {account.AccountHolder}\nBALANCE: {account.Balance}";
        }

        public string updateAccount(AccountBean acc)
        {
            if (!dao.validateAccount(acc.AccountNumber))
                return "ACCOUNT NOT FOUND";

            return dao.updateAccount(acc) ? "ACCOUNT UPDATED" : "UPDATE FAILED";
        }

   
        public string transfer(TransferBean t)
        {
            if (t == null)
                return "INVALID";

            if (!dao.validateAccount(t.FromAccount) || !dao.validateAccount(t.ToAccount))
                return "INVALID ACCOUNT";

            try
            {
                double fromBal = dao.getBalance(t.FromAccount);
                if (fromBal < t.Amount)
                    throw new InsufficientBalanceException();

                dao.updateBalance(t.FromAccount, fromBal - t.Amount);
                dao.updateBalance(t.ToAccount, dao.getBalance(t.ToAccount) + t.Amount);
                dao.recordTransaction(t);
                return "SUCCESS";
            }
            catch (InsufficientBalanceException e)
            {
                return e.ToString();
            }
        }

        public bool deleteTransaction(int transactionId)
        {
            return dao.deleteTransaction(transactionId);
        }
    }
}
