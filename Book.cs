using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreNew
{
    public class Book : BaseClass
    {
        public static List<Book> Books = new List<Book>();
        public string Name { get; set; }
        public BookTypeEnums BookType { get; set; }
        public double CostPrice { get; set; }
        public int TaxPercentage { get; set; }
        public int ProfitMargin { get; set; }
        public double Price { get; set; }
        public int QTY { get; set; }


        ///Constructor
        public Book(string _name, double _costPrice, BookTypeEnums _bookType, int _taxPercentage, int _profitMargin = 10, int _qty = 1)
        {
            Name = _name;
            CostPrice = _costPrice;
            BookType = _bookType;
            TaxPercentage = _taxPercentage;
            ProfitMargin = _profitMargin;
            QTY = _qty;

            Price = calculatePrice(_costPrice,
                _taxPercentage,
                _profitMargin);

        }
        public Book(string _name, double _costPrice, BookTypeEnums _bookType, double _price, int _qty = 1)
        {

            Name = _name;
            CostPrice = _costPrice;
            BookType = _bookType;
            Price = _price;
            QTY = _qty;

        }
        public static double calculatePrice(double costPrice, int tax, int profitMargin)
        {
            double taxPrice = (costPrice * tax) / 100;
            double profitPrice = (costPrice * profitMargin) / 100;
            double price = costPrice + taxPrice + profitPrice;
            return price;
        }
        public static void addBook(Book book)
        {
            try
            {
                Books.Add(book);
                double amount = CaseTransaction.calculateAmount(book.CostPrice, book.QTY);

                CaseTransaction caseTransaction = new CaseTransaction(amount, TransactionTypeEnums.EXPENSE);
                CaseTransaction.saveCaseTransaction(caseTransaction);

            }
            catch (Exception ex)
            {
                Console.WriteLine("An Error Occured" + ex.Message);
            }

        }

        public static void removeBook(int Id)
        {

            foreach (Book book in Books)
            {
                if (book.Id == Id)
                {
                    Books.Remove(book);
                    Console.WriteLine("The book is successfuly deleted ");
                    break;
                }
            }
        }

        public static void sellBook(int bookId, int bookQTY)
        {
            foreach (Book soldbook in Books)
            {
                if (soldbook.Id == bookId)
                {
                    soldbook.QTY = soldbook.QTY - bookQTY;
                    double soldPrice = CaseTransaction.calculateAmount(soldbook.Price, bookQTY);
                    CaseTransaction newSellCase = new CaseTransaction(soldPrice, TransactionTypeEnums.INCOMING);
                    CaseTransaction.saveCaseTransaction(newSellCase);
                }
            }
        }

        public static void updateBook(int bookId)
        {
            foreach (Book updatedBook in Books)
            {
                if (updatedBook.Id == bookId)
                {
                    Console.WriteLine("Which property of the book do you want to change ");
                    Console.WriteLine("1-Name 2-Book Type 3- Cost Price 4- Tax Percentage 5- Profit Margin 6- Quantity");
                    int choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("Name: ");
                            updatedBook.Name = Console.ReadLine();
                            break;
                        case 2:
                            Console.WriteLine("Type(0-4): ");
                            updatedBook.BookType = (BookTypeEnums)Convert.ToInt32(Console.ReadLine());
                            updatedBook.Price = calculatePrice(updatedBook.CostPrice, updatedBook.TaxPercentage, updatedBook.ProfitMargin);
                            break;
                        case 3:
                            Console.WriteLine("Cost Price: ");
                            updatedBook.CostPrice = Convert.ToInt32(Console.ReadLine());

                            break;
                        case 4:
                            Console.WriteLine("Tax Percentage: ");
                            updatedBook.TaxPercentage = Convert.ToInt32(Console.ReadLine());
                            updatedBook.Price = calculatePrice(updatedBook.CostPrice, updatedBook.TaxPercentage, updatedBook.ProfitMargin);
                            break;
                        case 5:
                            Console.WriteLine("Profit Margin: ");
                            updatedBook.ProfitMargin = Convert.ToInt32(Console.ReadLine());
                            updatedBook.Price = calculatePrice(updatedBook.CostPrice, updatedBook.TaxPercentage, updatedBook.ProfitMargin);
                            break;
                        
                        case 6:
                            Console.WriteLine("Quantity:");
                            updatedBook.QTY = Convert.ToInt32(Console.ReadLine());
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public static void searchbook()
        {
            string searchedWord = Console.ReadLine();

            Console.WriteLine("\nThe book list of the word you have searced is below");
            Console.WriteLine("BOOK LIST");

            foreach (Book book in Books)
            {
                string bookName = book.Name;
                string UpperCaseBookName = bookName.ToUpper();
                string UpperCaseSearchedWord = searchedWord.ToUpper();

                if (UpperCaseBookName.Contains(UpperCaseSearchedWord))
                {
                    Console.WriteLine("--------------------");
                    Console.WriteLine(book.ToString());
                }
            }

        }

        public override string ToString()
        {
            return String.Format("Id: {0} - " + "Name: {1} - " + "Type: {2} - " + "Cost Price: {3} - " + "Tax Percentage: {4} - " + "Profit Margin: {5} - " + "Price: {6} - " + "Quantity: {7} - "+ "Created Time: {8} - " + "Updated Time: {9}",
                Id,
                Name,
                BookType,
                CostPrice,
                TaxPercentage,
                ProfitMargin,
                Price,
                QTY,
                CreatedTime,
                UpdatedTime);
        }
    }
}


