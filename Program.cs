using System;
using System.IO;

namespace BookStoreNew
{
    public class Program
    {
        static void Main(string[] args)
        {
            readBooksTextFile();
            readCaseTransactionsTextFile();


            int choice = -1;
            Console.WriteLine("\t\tWelcome To The Book Store Console App !\n");
            while (choice != 9)
            {
                choice = -1;
                Console.WriteLine("1- Add Book");
                Console.WriteLine("2- Delete Book");
                Console.WriteLine("3- Update Book");
                Console.WriteLine("4- Sell Book");
                Console.WriteLine("5- List Books");
                Console.WriteLine("6- Search Book");
                Console.WriteLine("7- List Case Transactions");
                Console.WriteLine("8- Delete Lists");
                Console.WriteLine("9- Exit");


                

                while (!(choice > 0 && choice < 10))
                {
                    try
                    {
                        choice = Convert.ToInt32(Console.ReadLine());


                        
                            while (!(choice >= 0 && choice < 10))
                            {
                                Console.WriteLine("You entered an incorrect value, please re-enter");
                                choice = Convert.ToInt32(Console.ReadLine());
                                
                            }
                        
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine("\nERROR MESSAGE: " + ex.Message);
                        Console.WriteLine("You entered an incorrect value, please enter a number...");
                    }
                }

                switch (choice)
                {
                    case 1:

                        addBookCase();
                        break;

                    case 2:

                        listBook();
                        Console.WriteLine("\nID of the book to be deleted: ");
                        int bookId = Convert.ToInt32(Console.ReadLine());
                        Book.removeBook(bookId);
                        break;

                    case 3:

                        listBook();
                        Console.WriteLine("\nID of the book to be updated: ");
                        int updatedBookId = Convert.ToInt32(Console.ReadLine());

                        Book.updateBook(updatedBookId);
                        Console.WriteLine("The updated book list is shown below ");
                        listBook();
                        break;

                    case 4:

                        listBook();
                        Console.Write("ID of the book to be sold");
                        int soldBookId = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Quantity of the book to be sold");
                        int soldBookQty = Convert.ToInt32(Console.ReadLine());

                        Book.sellBook(soldBookId, soldBookQty);
                        Console.WriteLine("The updated book list is shown below ");
                        listBook();
                        break;

                    case 5:

                        listBook();
                        break;

                    case 6:

                        Console.WriteLine("Which word do you want to search in the book names ? ");
                        Book.searchbook();
                        break;

                    case 7:

                        CaseTransaction.listCaseTransactions();
                        break;
                    case 8:
                        Console.WriteLine("Which list do you want to delete ?");
                        Console.WriteLine("1- Book List");
                        Console.WriteLine("2- Case Transaction List");


                        int choiceDelete  = 3;

                        while (!(choiceDelete >= 0 && choiceDelete < 3))
                        {
                            try
                            {
                                choiceDelete = Convert.ToInt32(Console.ReadLine());

                                if (choiceDelete == 1)
                                {
                                    deleteBookTextFile();
                                }
                                else if (choiceDelete == 2)
                                {
                                    deleteCaseTransactionsTextFile();
                                }
                                else
                                {
                                    while (!(choiceDelete > 0 && choiceDelete < 3))
                                    {
                                        Console.WriteLine("You entered an incorrect value, please re-enter");
                                        choice = Convert.ToInt32(Console.ReadLine());                                        
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("\nERROR MESSAGE: " + ex.Message);
                                Console.WriteLine("You entered an incorrect value, please enter a number...");
                            }
                        }                                                
                        break;

                    default:
                        

                        break;

                }
                saveBooksTextFile();
                saveCaseTransactionsTextFile();
            }
            

        }
        public static void addBookCase()
        {
            Console.WriteLine("Name of the book:");
            string bookName = Console.ReadLine();

            Console.WriteLine("Cost price of the book:");
            double costPrice = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Type of book (0-4):");

            int UserBookType = 5;
            BookTypeEnums bookType = 0;

            while (!(UserBookType >= 0 && UserBookType < 5))
            {
                try
                {
                    UserBookType = Convert.ToInt32(Console.ReadLine());


                    if (UserBookType >= 0 && UserBookType < 5)
                    {
                        bookType = (BookTypeEnums)UserBookType;
                    }
                    else
                    {
                        while (!(UserBookType >= 0 && UserBookType < 5))
                        {
                            Console.WriteLine("You entered an incorrect value, please re-enter");
                            UserBookType = Convert.ToInt32(Console.ReadLine());
                            bookType = (BookTypeEnums)UserBookType;
                        }
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine("\nERROR MESSAGE: " + ex.Message);
                    Console.WriteLine("You entered an incorrect value, please enter a number...");
                }
            }

            Console.WriteLine("Percentage of tax: %");
            int taxPercentage = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Percentage of profit margin: %");
            int profitMargin = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Quantity of book");
            int qty = Convert.ToInt32(Console.ReadLine());


            Book newBook = new Book(bookName, costPrice, bookType, taxPercentage, profitMargin, qty);
            Book.addBook(newBook);
        }
        public static void listBook()
        {



            Console.WriteLine("BOOK LIST");

            foreach (Book item in Book.Books)
            {
                Console.WriteLine("--------------------");
                Console.WriteLine(item.ToString());
            }
        }
        public static void listCaseTransaction()
        {


            Console.WriteLine("CASE TRANSACTION LIST");
            foreach (CaseTransaction item in CaseTransaction.CaseTransactions)
            {
                Console.WriteLine("--------------------");
                Console.WriteLine(item.ToString());
            }
        }
        public static void saveBooksTextFile()
        {
            FileStream fS = new FileStream("BookList.txt", FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
            foreach (Book item in Book.Books)
            {

                StreamWriter sW = new StreamWriter(fS);

                sW.WriteLine(item);

                sW.Close();



            }
            fS.Close();
        }

        public static void readBooksTextFile()
        {
            FileStream fS = new FileStream("BookList.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamReader sR = new StreamReader(fS);

            string lineInText = sR.ReadLine();

            while (lineInText != null)
            {

                // ID
                int idStart = lineInText.IndexOf("Id: ") + ("Id: ".Length);
                int idEnd = lineInText.IndexOf(" - Name: ", idStart) - idStart;
                int iD = Convert.ToInt32(lineInText.Substring(idStart, idEnd));

                // Name
                int nameStart = lineInText.IndexOf("Name: ") + ("Name: ".Length);
                int nameEnd = lineInText.IndexOf(" - Type: ", nameStart) - nameStart;
                string namE = lineInText.Substring(nameStart, nameEnd);

                //Book Type
                int typeStart = lineInText.IndexOf("Type: ") + ("Type: ".Length);
                int typeEnd = lineInText.IndexOf(" - Cost Price: ", typeStart) - typeStart;
                string typEE = lineInText.Substring(typeStart, typeEnd);
                Enum.TryParse(typEE, out BookTypeEnums booktypEE);

                //Cost Price
                int costpriceStart = lineInText.IndexOf(" - Cost Price: ") + (" - Cost Price: ".Length);
                int costpriceEnd = lineInText.IndexOf(" - Tax Percentage: ", costpriceStart) - costpriceStart;
                double costpricE = Convert.ToDouble(lineInText.Substring(costpriceStart, costpriceEnd));

                //Tax Percentage
                int taxpercentageStart = lineInText.IndexOf(" - Tax Percentage: ") + (" - Tax Percentage: ".Length);
                int taxpercentageEnd = lineInText.IndexOf(" - Profit Margin: ", taxpercentageStart) - taxpercentageStart;
                double taxpercentagE = Convert.ToDouble(lineInText.Substring(taxpercentageStart, taxpercentageEnd));

                //Profit Margin
                int profitmarginStart = lineInText.IndexOf(" - Profit Margin: ") + (" - Profit Margin: ".Length);
                int profitmarginEnd = lineInText.IndexOf(" - Price: ", profitmarginStart) - profitmarginStart;
                double profitmargiN = Convert.ToDouble(lineInText.Substring(profitmarginStart, profitmarginEnd));

                //Price 
                int priceStart = lineInText.IndexOf(" - Price: ") + (" - Price: ".Length);
                int priceEnd = lineInText.IndexOf(" - Quantity: ", priceStart) - priceStart;
                double pricE = Convert.ToDouble(lineInText.Substring(priceStart, priceEnd));

                //Quantity
                int quantityStart = lineInText.IndexOf(" - Quantity: ") + (" - Quantity: ".Length);
                int quantityEnd = lineInText.IndexOf(" - Created Time: ", quantityStart) - quantityStart;
                int quantitY = Convert.ToInt32(lineInText.Substring(quantityStart, quantityEnd));

                //Created Time
                int createdtimeStart = lineInText.IndexOf(" - Created Time: ") + (" - Created Time: ".Length);
                int createdtimeEnd = lineInText.IndexOf(" - Updated Time: ", createdtimeStart) - createdtimeStart;
                DateTime createdtimE = Convert.ToDateTime(lineInText.Substring(createdtimeStart, createdtimeEnd));

                //Updated Time
                int updatedtimeStart = lineInText.IndexOf(" - Updated Time: ") + (" - Updated Time: ".Length);
                // int updatedtimeEnd = lineInText.IndexOf(" - ", updatedtimeStart) - updatedtimeStart;
                DateTime updatedtimE = Convert.ToDateTime(lineInText.Substring(updatedtimeStart));


                // Read the Lines and Creates new book for each line
                Book newBook = new Book(namE, costpricE, booktypEE, pricE, quantitY);
                newBook.Id = iD;
                newBook.CreatedTime = createdtimE;
                newBook.UpdatedTime = updatedtimE;

                Book.addBook(newBook);

                lineInText = sR.ReadLine();

            }
            sR.Close();

            fS.Close();



        }
        public static void deleteBookTextFile()
        {
            FileStream fS = new FileStream("BookList.txt", FileMode.Truncate, FileAccess.Write, FileShare.ReadWrite);

            fS.Close();



        }
        public static void saveCaseTransactionsTextFile()
        {
            FileStream fS = new FileStream("CaseTransactionList.txt", FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
            foreach (CaseTransaction item in CaseTransaction.CaseTransactions)
            {

                StreamWriter sW = new StreamWriter(fS);

                sW.WriteLine(item);

                sW.Close();



            }
            fS.Close();
        }

        public static void readCaseTransactionsTextFile()
        {
            FileStream fS = new FileStream("CaseTransactionList.txt", FileMode.Open, FileAccess.ReadWrite);
            StreamReader sR = new StreamReader(fS);

            string lineInText = sR.ReadLine();

            while (lineInText != null)
            {

                

                int idStart = lineInText.IndexOf("Id: ") + 1;
                int idEnd = lineInText.IndexOf(" - Type: ", idStart) - idStart;
                int iD = Convert.ToInt32(lineInText.Substring(idStart, idEnd));

                int typeStart = lineInText.IndexOf(" - Type: ") + (" - Type: ".Length);
                int typeEnd = lineInText.IndexOf(" - Amount: ", typeStart) - typeStart;
                string typEE = lineInText.Substring(typeStart, typeEnd);
                Enum.TryParse(typEE, out TransactionTypeEnums transactiontypEE);


                int amountStart = lineInText.IndexOf(" - Amount: ") + (" - Amount: ".Length);
                int amountEnd = lineInText.IndexOf(" - Created Time: ", amountStart) - amountStart;
                int amounT = Convert.ToInt32(lineInText.Substring(amountStart, amountEnd));
               


                int createdtimeStart = lineInText.IndexOf(" - Created Time: ") + (" - Created Time: ".Length);
                // int costpriceEnd = lineInText.IndexOf("Price: ", costpriceStart) - costpriceStart;
                DateTime createdtimE = Convert.ToDateTime(lineInText.Substring(createdtimeStart));

                CaseTransaction newCaseTransaction = new CaseTransaction(amounT,transactiontypEE);
                newCaseTransaction.Id = iD;
                newCaseTransaction.CreatedTime = createdtimE;

                CaseTransaction.saveCaseTransaction(newCaseTransaction);


                lineInText = sR.ReadLine();

            }
            sR.Close();

            fS.Close();





        }

        public static void deleteCaseTransactionsTextFile()
        {
            FileStream fS = new FileStream("CaseTransactionList.txt", FileMode.Truncate, FileAccess.Write, FileShare.ReadWrite);

            fS.Close();


            
        }
    }

}


