using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreNew
{
    public class CaseTransaction : BaseClass
    {
        public static List<CaseTransaction> CaseTransactions = new List<CaseTransaction>();

        public double Amount { get; set; }
        public TransactionTypeEnums TransactionType { get; set; }
        public CaseTransaction(double _amount, TransactionTypeEnums _transactionType)
        {
            Amount = _amount;
            TransactionType = _transactionType;
        }
        public static void saveCaseTransaction(CaseTransaction caseTransaction)
        {
            CaseTransactions.Add(caseTransaction);
        }
        public static double calculateAmount(double price, int qty)
        {
            return price * qty;
        }
        public static void listCaseTransactions()
        {
            Console.WriteLine("CASE TRANSACTION LIST");
            double caseTotal = 0;

            foreach (CaseTransaction caseTransaction in CaseTransactions)
            {
                Console.WriteLine("--------------------");

                if (caseTransaction.TransactionType == TransactionTypeEnums.EXPENSE)
                {
                    caseTotal = caseTotal - caseTransaction.Amount;
                }
                else
                {
                    caseTotal = caseTotal + caseTransaction.Amount;
                }
                Console.WriteLine(caseTransaction.ToString());
            }
            Console.WriteLine("Total Case Amount " + caseTotal);
        }
        public override string ToString()
        {
            return String.Format("Id: {0} - Type: {1} - Amount: {2} - Created Time: {3}", Id, TransactionType, Amount, CreatedTime);
        }
    }
}
