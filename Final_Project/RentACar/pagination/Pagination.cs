using RentACar.validation;


namespace RentACar.pagination
{
    internal class Pagination
    {
        public static void ShowOnPage<T>(int totalLines, List<T> items) where T : class
        {
            // initialization of the pagination
            int currentPage = 1;
            int pageLineSize = 2;
            int totalPages = Convert.ToInt32(Math.Ceiling((double)totalLines / pageLineSize));
            bool show = true;

            do
            {
                // Display the information for the current page
                int startLine = (currentPage - 1) * pageLineSize;

                for (int i = startLine; i < startLine + 2 && i < totalLines; i++) {
                    Console.WriteLine(items[i].ToString());
                    Console.WriteLine();
                }

                // user menu
                Console.WriteLine($"Page {currentPage} of {(totalPages > 0 ? totalPages : 1)}");
                Console.WriteLine("1. Next page");
                Console.WriteLine("2. Previous page");
                Console.WriteLine("X. Exit");
                Console.Write("Enter your choice: ");
                char choice = Validation.getValidChar();

                switch (choice)
                {
                    case '1':
                    {
                        if (currentPage < Math.Ceiling((double)totalLines / pageLineSize))
                        {
                            currentPage++;
                        }
                        else
                        {
                            Console.WriteLine("Already on the last page");
                        }
                        break;
                    }
                    case '2':
                    {
                        if (currentPage > 1)
                        {
                            currentPage--;
                        }
                        else
                        {
                            Console.WriteLine("Already on the first page");
                        }
                        break;
                    }
                    case 'x':
                    case 'X':
                    {
                        show = false;
                        break;
                    }
                    default:
                    {
                        Console.WriteLine("Invalid choice. Please try again");
                        break;
                    }
                }
                Console.Clear();
            } while (show);
        }
    }
}
