using System;
using System.Collections.Generic;

namespace task_week_4
{
    class Program
    {
        static void Main(string[] args)
        {
            var accounts = new List<Account>();
            accounts.Add(new Account());
            accounts.Add(new Account("Ganna"));
            accounts.Add(new Account("Ali", 2000));
            accounts.Add(new Account("Sohaila", 5000));

            AccountUtil.Deposit(accounts, 1000);
            AccountUtil.Withdraw(accounts, 2000);

            var savAccounts = new List<SavingsAccount>();
            savAccounts.Add(new SavingsAccount());
            savAccounts.Add(new SavingsAccount("Amal"));
            savAccounts.Add(new SavingsAccount("Omar", 2000));
            savAccounts.Add(new SavingsAccount("Hisham", 5000, 5.0));

            AccountUtil.DepositSavings(savAccounts, 1000);
            AccountUtil.WithdrawSavings(savAccounts, 2000);

            var checAccounts = new List<CheckingAccount>();
            checAccounts.Add(new CheckingAccount());
            checAccounts.Add(new CheckingAccount("Ganna2"));
            checAccounts.Add(new CheckingAccount("Ali2", 2000));
            checAccounts.Add(new CheckingAccount("Sohaila2", 5000));

            AccountUtil.DepositChecking(checAccounts, 1000);
            AccountUtil.WithdrawChecking(checAccounts, 2000);
            AccountUtil.WithdrawChecking(checAccounts, 2000);

            var trustAccounts = new List<TrustAccount>();
            trustAccounts.Add(new TrustAccount());
            trustAccounts.Add(new TrustAccount("Amal2"));
            trustAccounts.Add(new TrustAccount("Omar2", 2000));
            trustAccounts.Add(new TrustAccount("Hisham2", 5000, 5.0));

            AccountUtil.DepositTrust(trustAccounts, 1000);
            AccountUtil.DepositTrust(trustAccounts, 6000);
            AccountUtil.WithdrawTrust(trustAccounts, 2000);
            AccountUtil.WithdrawTrust(trustAccounts, 3000);
            AccountUtil.WithdrawTrust(trustAccounts, 500);

            Console.WriteLine();
        }
    }

    public class Account
    {
        public string Name { get; set; }
        public double Balance { get; set; }

        public Account(string name = "Unnamed Account", double balance = 0.0)
        {
            this.Name = name;
            this.Balance = balance;
        }

        public virtual bool Deposit(double amount)
        {
            if (amount < 0)
                return false;
            else
            {
                Balance += amount;
                return true;
            }
        }

        public virtual bool Withdraw(double amount)
        {
            if (Balance - amount >= 0)
            {
                Balance -= amount;
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return $"[Account: {Name}, Balance: {Balance}]";
        }
    }

    public class SavingsAccount : Account
    {
        public double InterestRate { get; set; }

        public SavingsAccount(string name = "Unnamed Savings Account", double balance = 0.0, double interestRate = 0.0)
            : base(name, balance)
        {
            InterestRate = interestRate;
        }

        public override bool Deposit(double amount)
        {
            if (amount < 0) return false;

            amount += amount * (InterestRate / 100);
            return base.Deposit(amount);
        }

        public override string ToString()
        {
            return $"[SavingsAccount: {Name}, Balance: {Balance}, Interest Rate: {InterestRate}%]";
        }
    }

    public class CheckingAccount : Account
    {
        private const double WithdrawalFee = 1.50;

        public CheckingAccount(string name = "Unnamed Checking Account", double balance = 0.0)
            : base(name, balance)
        {
        }

        public override bool Withdraw(double amount)
        {
            return base.Withdraw(amount + WithdrawalFee);
        }

        public override string ToString()
        {
            return $"[CheckingAccount: {Name}, Balance: {Balance}]";
        }
    }

    public class TrustAccount : SavingsAccount
    {
        private int withdrawalCount = 0;
        private const double BonusThreshold = 5000.0;
        private const double BonusAmount = 50.0;
        private const int MaxWithdrawalsPerYear = 3;

        public TrustAccount(string name = "Unnamed Trust Account", double balance = 0.0, double interestRate = 0.0)
            : base(name, balance, interestRate)
        {
        }

        public override bool Deposit(double amount)
        {
            if (amount >= BonusThreshold)
            {
                amount += BonusAmount;
            }
            return base.Deposit(amount);
        }

        public override bool Withdraw(double amount)
        {
            if (withdrawalCount >= MaxWithdrawalsPerYear || amount >= (Balance * 0.20))
            {
                return false;
            }

            if (base.Withdraw(amount))
            {
                withdrawalCount++;
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"[TrustAccount: {Name}, Balance: {Balance}, Interest Rate: {InterestRate}%, Withdrawals: {withdrawalCount}]";
        }
    }

    public static class AccountUtil
    {
        public static void Deposit(List<Account> accounts, double amount)
        {
            Console.WriteLine("\nDepositing to Accounts");
            foreach (var acc in accounts)
            {
                if (acc.Deposit(amount))
                    Console.WriteLine($"Deposited {amount} to {acc}");
                else
                    Console.WriteLine($"Failed Deposit of {amount} to {acc}");
            }
        }

        public static void Withdraw(List<Account> accounts, double amount)
        {
            Console.WriteLine("\nWithdrawing from Accounts");
            foreach (var acc in accounts)
            {
                if (acc.Withdraw(amount))
                    Console.WriteLine($"Withdrew {amount} from {acc}");
                else
                    Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
            }
        }

        public static void DepositSavings(List<SavingsAccount> accounts, double amount)
        {
            Console.WriteLine("\nDepositing to Savings Accounts");
            foreach (var acc in accounts)
            {
                if (acc.Deposit(amount))
                    Console.WriteLine($"Deposited {amount} to {acc}");
                else
                    Console.WriteLine($"Failed Deposit of {amount} to {acc}");
            }
        }

        public static void WithdrawSavings(List<SavingsAccount> accounts, double amount)
        {
            Console.WriteLine("\nWithdrawing from Savings Accounts");
            foreach (var acc in accounts)
            {
                if (acc.Withdraw(amount))
                    Console.WriteLine($"Withdrew {amount} from {acc}");
                else
                    Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
            }
        }

        public static void DepositChecking(List<CheckingAccount> accounts, double amount)
        {
            Console.WriteLine("\nDepositing to Checking Accounts");
            foreach (var acc in accounts)
            {
                if (acc.Deposit(amount))
                    Console.WriteLine($"Deposited {amount} to {acc}");
                else
                    Console.WriteLine($"Failed Deposit of {amount} to {acc}");
            }
        }

        public static void WithdrawChecking(List<CheckingAccount> accounts, double amount)
        {
            Console.WriteLine("\nWithdrawing from Checking Accounts");
            foreach (var acc in accounts)
            {
                if (acc.Withdraw(amount))
                    Console.WriteLine($"Withdrew {amount} from {acc}");
                else
                    Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
            }
        }

        public static void DepositTrust(List<TrustAccount> accounts, double amount)
        {
            Console.WriteLine("\nDepositing to Trust Accounts");
            foreach (var acc in accounts)
            {
                if (acc.Deposit(amount))
                    Console.WriteLine($"Deposited {amount} to {acc}");
                else
                    Console.WriteLine($"Failed Deposit of {amount} to {acc}");
            }
        }

        public static void WithdrawTrust(List<TrustAccount> accounts, double amount)
        {
            Console.WriteLine("\nWithdrawing from Trust Accounts");
            foreach (var acc in accounts)
            {
                if (acc.Withdraw(amount))
                    Console.WriteLine($"Withdrew {amount} from {acc}");
                else
                    Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
            }
        }
    }
}