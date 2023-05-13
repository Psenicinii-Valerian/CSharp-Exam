using RentACar.controller;
using RentACar.validation;
using RentACar.graphics;
using System.Runtime.ExceptionServices;

namespace RentACar.menu
{
    internal class Menu
    {
        public static void LoadData() 
        {
            EventHistoryController eventHistoryController = EventHistoryController.GetInstance();
            eventHistoryController.LoadEventsHistory();

            CarController carControler = CarController.GetInstance();
            carControler.LoadCars();

            ClientController clientController = ClientController.GetInstance();
            clientController.LoadClients();

            EmployeeController employeeController = EmployeeController.GetInstance();
            employeeController.LoadEmployees();
        }
        public static void SaveData()
        {
            EventHistoryController eventHistoryController = EventHistoryController.GetInstance();
            eventHistoryController.SaveEventsHistory();

            CarController carControler = CarController.GetInstance();
            carControler.SaveCars();
            
            ClientController clientController = ClientController.GetInstance();
            clientController.SaveClients();

            EmployeeController employeeController = EmployeeController.GetInstance();
            employeeController.SaveEmployees();
        }
        public static void WaitForUser()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Press any key to continue: ");
            Console.ReadKey();
        }

        public static void RentACarMenu()
        {
            // load the data from files at the very start of the program
            LoadData();
            bool isProgramRunning = true;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Welcome to our Rent a Car Service!");

            while (isProgramRunning)
            {
                Console.Clear();

                Console.WriteLine($"{Graphics.CYAN}    MENU:{Graphics.NORMAL}");
                Console.WriteLine("1) Manage Cars");
                Console.WriteLine("2) Manage Employees");
                Console.WriteLine("3) Manage Clients");
                Console.WriteLine("X) Exit ");

                // give user the posibility to choose needed functionality
                Console.Write("Enter your choice: ");
                char option = Validation.getValidChar();

                switch (option)
                {
                    case '1':
                    {
                        ManageCarsMenu();
                        break;
                    }
                    case '2':
                    {
                        ManageEmployeeMenu();
                        break;
                    }
                    case '3':
                    {
                        ManageClientsMenu();
                        break;
                    }
                    case 'x':
                    case 'X':
                    {
                        isProgramRunning = false;
                        break;
                    }
                    default:
                    {
                        Console.WriteLine("Incorrect choice! Try again");
                        WaitForUser();
                        break;
                    }
                }
            }

            // save the data to files at the very end of the program
            SaveData();
        }

        public static void ManageCarsMenu()
        {
            CarController carControler = CarController.GetInstance();

            bool isCarManagerRunning = true;

            while (isCarManagerRunning)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Welcome to the Car Management System!");

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("1) Add Car");
                Console.WriteLine("2) Show one car");
                Console.WriteLine("3) Show all cars");
                Console.WriteLine("4) Update car");
                Console.WriteLine("5) Delete car");
                Console.WriteLine("X) Exit ");

                Console.Write("Enter your choice: ");
                char userInput = Validation.getValidChar();

                switch (userInput)
                {
                    case '1':
                    {
                        Console.Clear();

                        carControler.AddCar();
                        WaitForUser();
                        break;
                    }
                    case '2':
                    {
                        Console.Clear();

                        if (carControler.GetRegisteredCarsCount() > 0) 
                        {
                            Console.Write("Enter the ID of the car whose information you want to show: ");
                            int userCarId = Validation.getValidInt();

                            carControler.ShowCarById(userCarId);
                        }
                        else
                        {
                            Console.WriteLine("There aren't any registered cars yet!");
                        }
                        WaitForUser();

                        break;
                    }
                    case '3':
                    {
                        Console.Clear();

                        if (carControler.GetRegisteredCarsCount() > 0)
                        {
                            carControler.ShowCars();
                        }
                        else
                        {
                            Console.WriteLine("There aren't any registered cars yet!");
                            WaitForUser();
                        }
                        break;
                    }
                    case '4':
                    {
                        Console.Clear();
                        if (carControler.GetRegisteredCarsCount() > 0)
                        {
                            Console.Write("Enter the ID of the car whose information you want to update: ");
                            int userCarId = Validation.getValidInt();

                            carControler.UpdateCar(userCarId);
                        }
                        else
                        {
                            Console.WriteLine("There aren't any registered cars yet!");
                        }
                        WaitForUser();
                        break;
                    }
                    case '5':
                    {
                        Console.Clear();

                        if (carControler.GetRegisteredCarsCount() > 0)
                        {
                            Console.Write("Enter the ID of the car whose information you want to delete: ");
                            int userCarId = Validation.getValidInt();

                            carControler.DeleteCar(userCarId);
                        }
                        else
                        {
                            Console.WriteLine("There aren't any registered cars yet!");
                        }
                        WaitForUser();
                        break;
                    }
                    case 'x':
                    case 'X':
                    {
                        isCarManagerRunning = false;
                        break;
                    }
                    default:
                    {
                        Console.WriteLine("Incorrect choice! Try again");
                        WaitForUser();
                        break;
                    }
                }
            }
        }

        public static void ManageEmployeeMenu()
        {
            EmployeeController employeeController = EmployeeController.GetInstance();

            bool isEmployeeManagerRunning = true;

            while (isEmployeeManagerRunning)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Welcome to the Employee Management System!");

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("1) Add Employee");
                Console.WriteLine("2) Show one employee");
                Console.WriteLine("3) Show all employees");
                Console.WriteLine("4) Update employee");
                Console.WriteLine("5) Delete employee");
                Console.WriteLine("X) Exit");

                Console.Write("Enter your choice: ");
                char userInput = Validation.getValidChar();

                switch (userInput)
                {
                    case '1':
                    {
                        Console.Clear();

                        employeeController.AddEmployee();
                        WaitForUser();
                        break;
                    }
                    case '2':
                    {
                        Console.Clear();

                        if (employeeController.GetRegisteredEmployeesCount() > 0)
                        {
                            Console.Write("Enter the ID of the employee whose information you want to show: ");
                            int userEmployeeId = Validation.getValidInt();

                            employeeController.ShowEmployeeById(userEmployeeId);
                        }
                        else
                        {
                            Console.WriteLine("There aren't any registered employees yet!");
                        }
                        WaitForUser();
                        break;
                    }
                    case '3':
                    {
                        Console.Clear();

                        if (employeeController.GetRegisteredEmployeesCount() > 0)
                        {
                            employeeController.ShowEmployees();
                        }
                        else
                        {
                            Console.WriteLine("There aren't any registered employees yet!");
                            WaitForUser();
                        }
                        break;
                    }
                    case '4':
                    {
                        Console.Clear();

                        if (employeeController.GetRegisteredEmployeesCount() > 0)
                        {
                            Console.Write("Enter the ID of the employee whose information you want to update: ");
                            int userEmployeeId = Validation.getValidInt();

                            employeeController.UpdateEmployee(userEmployeeId);
                        }
                        else
                        {
                            Console.WriteLine("There aren't any registered employees yet!");
                        }
                        WaitForUser();
                        break;
                    }
                    case '5':
                    {
                        Console.Clear();

                        if (employeeController.GetRegisteredEmployeesCount() > 0)
                        {
                            Console.Write("Enter the ID of the employee whose information you want to delete: ");
                            int userEmployeeId = Validation.getValidInt();

                            employeeController.DeleteEmployee(userEmployeeId);
                        }
                        else
                        {
                            Console.WriteLine("There aren't any registered employees yet!");
                        }
                        WaitForUser();
                        break;
                    }
                    case 'x':
                    case 'X':
                    {
                        isEmployeeManagerRunning = false;
                        break;
                    }
                    default:
                    {
                        Console.WriteLine("Incorrect choice! Try again");
                        WaitForUser();
                        break;
                    }
                }
            }
        }

        public static void ManageClientsMenu()
        {
            ClientController clientController = ClientController.GetInstance();

            bool isClientManagerRunning = true;

            while (isClientManagerRunning)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Welcome to the Client Management System!");

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("1) Add Client");
                Console.WriteLine("2) Show one client");
                Console.WriteLine("3) Show all clients");
                Console.WriteLine("4) Update client");
                Console.WriteLine("5) Delete client");
                Console.WriteLine("X) Exit");

                Console.Write("Enter your choice: ");
                char userInput = Validation.getValidChar();

                switch (userInput)
                {
                    case '1':
                    {
                        Console.Clear();

                        clientController.AddClient();
                        WaitForUser();
                        break;
                    }
                    case '2':
                    {
                        Console.Clear();

                        if (clientController.GetRegisteredClientsCount() > 0)
                        {
                            Console.Write("Enter the ID of the client whose information you want to show: ");
                            int userClientId = Validation.getValidInt();

                            clientController.ShowClientById(userClientId);
                        }
                        else
                        {
                            Console.WriteLine("There aren't any registered clients yet!");
                        }
                        WaitForUser();
                        break;
                    }
                    case '3':
                    {
                        Console.Clear();
                        if (clientController.GetRegisteredClientsCount() > 0)
                        {
                            clientController.ShowClients();
                        }
                        else
                        {
                            Console.WriteLine("There aren't any registered clients yet!");
                            WaitForUser();
                        }
                        break;
                    }
                    case '4':
                    {
                        Console.Clear();

                        if (clientController.GetRegisteredClientsCount() > 0)
                        {
                            Console.Write("Enter the ID of the client whose information you want to update: ");
                            int userClientId = Validation.getValidInt();

                            clientController.UpdateClient(userClientId);
                        }
                        else
                        {
                            Console.WriteLine("There aren't any registered clients yet!");
                        }
                        WaitForUser();
                        break;
                    }
                    case '5':
                    {
                        Console.Clear();

                        if (clientController.GetRegisteredClientsCount() > 0)
                        {
                            Console.Write("Enter the ID of the client whose information you want to delete: ");
                            int userClientId = Validation.getValidInt();

                            clientController.DeleteClient(userClientId);
                        }
                        else
                        {
                            Console.WriteLine("There aren't any registered clients yet!");
                        }
                        WaitForUser();
                        break;
                    }
                    case 'x':
                    case 'X':
                    {
                        isClientManagerRunning = false;
                        break;
                    }
                    default:
                    {
                        Console.WriteLine("Incorrect choice! Try again");
                        WaitForUser();
                        break;
                    }
                }
            }
        }
    }
}
