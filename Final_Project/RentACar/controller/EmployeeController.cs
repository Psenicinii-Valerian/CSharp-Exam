using RentACar.validation;
using RentACar.pagination;
using RentACar.model;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Xml.Linq;
using System.Collections.Generic;
using RentACar.graphics;

namespace RentACar.controller
{
    internal class EmployeeController
    {
        string filePath = "C:\\Users\\Valerian\\OneDrive\\Desktop\\STEP IT\\C#\\Final_Project\\RentACar\\data\\data_employees.txt";

        List<Employee> m_Employees = new List<Employee>();

        List<EventHistory> m_EventsHistory = new List<EventHistory>();

        public static EmployeeController s_Instance;

        private EmployeeController() { }

        public static EmployeeController GetInstance()
        {
            if (s_Instance == null)
            {
                s_Instance = new EmployeeController();
            }
            return s_Instance;
        }

        public void AddEmployee()
        {
            Employee emp = new Employee();

            int id, age, workExperience, salary;
            string name, surname, gender, position, email;

            Console.Write("Enter employee's id: ");
            id = Validation.getValidInt();
            id = Validation.validateUniqueId(id, m_Employees);
            emp.setId(id);

            Console.Write("Enter employee's name: ");
            name = Console.ReadLine();
            name = Validation.validateProperNoun(name, "name");
            emp.setName(name);

            Console.Write("Enter employee's surname: ");
            surname = Console.ReadLine();
            surname = Validation.validateProperNoun(surname, "surname");
            emp.setSurname(surname);

            Console.Write("Enter employee's gender (male/female): ");
            gender = Console.ReadLine();
            gender = Validation.validateGender(gender);
            emp.setGender(gender);

            Console.Write("Enter employee's position: ");
            position = Console.ReadLine();
            position = Validation.validateEmployeePosition(position);
            emp.setPosition(position);

            Console.Write("Enter employee's email: ");
            email = Console.ReadLine();
            email = Validation.validateEmail(email);
            emp.setEmail(email);

            Console.Write("Enter employee's age: ");
            age = Validation.getValidInt();
            age = Validation.validateAge(age);
            emp.setAge(age);

            Console.Write("Enter employee's work experience(years): ");
            workExperience = Validation.getValidInt();
            workExperience = Validation.validateEmployeeWorkExperience(workExperience, age);
            emp.setWorkExperience(workExperience);

            Console.Write("Enter employee's salary($$): ");
            salary = Validation.getValidInt();
            salary = Validation.validateEmployeeSalary(salary);
            emp.setSalary(salary);

            AddEventByUser(emp, true);

            m_Employees.Add(emp);
            Console.WriteLine("Employee added successfully!");
        }

        public void AddEventByUser(Employee emp, bool isCalledFirstTime)
        {
            bool option = true;
            if (isCalledFirstTime)
            {
                Console.Write($"{Graphics.YELLOW}Do you want to enter an event for your employee? (yes/no):{Graphics.NORMAL} ");
                option = Validation.validateStringToBool("choice");
            }

            while (option)
            {
                EventHistoryController eventHistoryController = EventHistoryController.GetInstance();
                EventHistory eventHistory = eventHistoryController.AddEvent("Employee");

                emp.addEvent(eventHistory);

                Console.Write($"{Graphics.BLUE}Continue? (yes/no):{Graphics.NORMAL} ");
                option = Validation.validateStringToBool("choice");
            }
        }

        public void UpdateEventByUser(Employee emp)
        {
            EventHistoryController eventHistoryController = EventHistoryController.GetInstance();
            List<EventHistory> eventHistories = emp.getEventHistory();

            if (eventHistories.Count > 0)
            {
                foreach (var eventHistory in eventHistories)
                {
                    Console.WriteLine(eventHistory.ToString());
                }
                Console.WriteLine();

                Console.Write($"{Graphics.YELLOW}Enter the event ID for the employee's history that you want to change:{Graphics.NORMAL} ");
                int userSelectedId = Validation.getValidInt();
                userSelectedId = Validation.validateExistingId(userSelectedId, eventHistories);

                EventHistory selectedEventHistory = eventHistoryController.UpdateEventHistory(userSelectedId);
            }
            else
            {
                Console.WriteLine("There aren't any events set for this employee yet so you cannot modify anything!");
            }
        }

        public void LoadEmployees()
        {
            StreamReader outputEmployees = new StreamReader(filePath);
            int totalLines = 0;

            while (!outputEmployees.EndOfStream)
            {
                string readCarLine = outputEmployees.ReadLine();
                if (readCarLine != null)
                {
                    totalLines++;
                }
            }

            if (totalLines > 0)
            {
                outputEmployees.Close();
                outputEmployees = new StreamReader(filePath);

                while (!outputEmployees.EndOfStream)
                {
                    string readEmployeeLine = outputEmployees.ReadLine();

                    string[] elements = readEmployeeLine.Split(' ');

                    Employee emp = new Employee();
                    EventHistoryController eventHistoryController = EventHistoryController.GetInstance();
                    List<EventHistory> eventsHistory = new List<EventHistory>();

                    emp.setId(Convert.ToInt32(elements[0]));
                    emp.setName(elements[1]);
                    emp.setSurname(elements[2]);
                    emp.setGender(elements[3]);
                    emp.setPosition(elements[4]);
                    emp.setEmail(elements[5]);
                    emp.setAge(Convert.ToInt32(elements[6]));
                    emp.setWorkExperience(Convert.ToInt32(elements[7]));
                    emp.setSalary(Convert.ToInt32(elements[8]));
                    if (elements.Length > 9)
                    {
                        for (int i = 9; i < elements.Length; i++)
                        {
                            eventsHistory.Add(eventHistoryController.FindEventById(Convert.ToInt32(elements[i])));
                        }
                    }
                    else
                    {
                        m_EventsHistory = new List<EventHistory>();
                    }
                    emp.setEventHistory(eventsHistory);
                    m_EventsHistory.AddRange(eventsHistory);

                    m_Employees.Add(emp);
                }
            }
            outputEmployees.Close();
        }
        public void SaveEmployees()
        {
            StreamWriter inputEmployees = new StreamWriter(filePath);

            foreach (var emp in m_Employees)
            {
                inputEmployees.WriteLine(emp.FileSaveString());
            }

            inputEmployees.Close();
        }

        public void ShowEmployees()
        {
            int totalLines = 0;

            foreach (var emp in m_Employees)
            {
                totalLines++;
            }
            Pagination.ShowOnPage(totalLines, m_Employees);
        }

        public void ShowEmployeeById(int empId)
        {
            foreach (var emp in m_Employees)
            {
                if (emp.getId() == empId)
                {
                    Console.WriteLine(emp.ToString());
                    return;
                }
            }
            Console.WriteLine($"Employee with ID[{empId}] not found!");
        }

        public Employee FindEmployeeById(int employeeId)
        {
            foreach (var emp in m_Employees)
            {
                if (emp.getId() == employeeId)
                {
                    return emp;
                }
            }

            Debug.Assert(false, "someObject is null! this could totally be a bug!");
            return new Employee();
        }

        public int GetRegisteredEmployeesCount()
        {
            return m_Employees.Count > 0 ? m_Employees.Count : 0;
        }

        public void UpdateEmployee(int employeeId)
        {
            employeeId = Validation.validateExistingId(employeeId, m_Employees);
            if (employeeId != 0)
            {
                Employee emp = FindEmployeeById(employeeId);
                while (true)
                {
                    Console.WriteLine();
                    Console.WriteLine("Enter what you want to change:");
                    Console.WriteLine("1. Change name");
                    Console.WriteLine("2. Change surname");
                    Console.WriteLine("3. Change gender");
                    Console.WriteLine("4. Change position");
                    Console.WriteLine("5. Change email");
                    Console.WriteLine("6. Change age");
                    Console.WriteLine("7. Change work experience");
                    Console.WriteLine("8. Change salary");
                    Console.WriteLine("9. Change current employee event history");
                    Console.WriteLine("10. Add new event for employee");
                    Console.WriteLine("X. All done");
                    string choice;
                    Console.Write("\nEnter your choice: ");
                    choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                        {
                            Console.Write("Enter employee's new name: ");
                            string newName = Console.ReadLine();
                            newName = Validation.validateProperNoun(newName, "name");

                            emp.setName(newName);
                            break;
                        }
                        case "2":
                        {
                            Console.Write("Enter employee's new surname: ");
                            string newSurname = Console.ReadLine();
                            newSurname = Validation.validateProperNoun(newSurname, "surname");

                            emp.setSurname(newSurname);
                            break;
                        }
                        case "3":
                        {
                            Console.Write("Enter employee's new gender: ");
                            string newGender = Console.ReadLine();
                            newGender = Validation.validateGender(newGender);

                            emp.setGender(newGender);
                            break;
                        }
                        case "4":
                        {
                            Console.Write("Enter employee's new position: ");
                            string newPosition = Console.ReadLine();
                            newPosition = Validation.validateEmployeePosition(newPosition);

                            emp.setPosition(newPosition);
                            break;
                        }
                        case "5":
                        {
                            Console.Write("Enter employee's new email: ");
                            string newEmail = Console.ReadLine();
                            newEmail = Validation.validateEmail(newEmail);

                            emp.setEmail(newEmail);
                            break;
                        }
                        case "6":
                        {
                            Console.Write("Enter employee's new age: ");
                            int newAge = Validation.getValidInt();
                            newAge = Validation.validateAge(newAge);

                            emp.setAge(newAge);
                            break;
                        }
                        case "7":
                        {
                            Console.Write("Enter employee's new work experience(years): ");
                            int newWorkExperience = Validation.getValidInt();
                            newWorkExperience = Validation.validateEmployeeWorkExperience(newWorkExperience, emp.getAge());

                            emp.setWorkExperience(newWorkExperience);
                            break;
                        }
                        case "8":
                        {
                            Console.Write("Enter employee's new salary($$): ");
                            int newSalary = Validation.getValidInt();
                            newSalary = Validation.validateEmployeeSalary(newSalary);

                            emp.setSalary(newSalary);
                            break;
                        }
                        case "9":
                        {
                            // updating event for employee
                            UpdateEventByUser(emp);
                            break;
                        }
                        case "10":
                        {
                            Console.WriteLine($"{Graphics.GREEN}Adding new event for employee:{Graphics.NORMAL}");
                            AddEventByUser(emp, false);

                            break;
                        }
                        case "x":
                        case "X":
                        {
                            Console.WriteLine("Employee updated successfully!");
                            return;
                        }
                        default:
                        {
                            Console.WriteLine("You've entered a wrong option!");
                            break;
                        }
                    }
                }
            }      
            else
            {
                Console.WriteLine("There aren't any registered employees yet!");
            }
        }

        public void DeleteEmployee(int employeeId)
        {
            int i = 0;
            foreach (var emp in m_Employees)
            {
                if (emp.getId() == employeeId)
                {
                    m_Employees.RemoveAt(i);
                    Console.WriteLine($"Employee with ID [{employeeId}] has been deleted successfully");
                    return;
                }
                i++;
            }
            Console.WriteLine($"Employee with ID[{employeeId}] not found!");
        }
    }
}