using RentACar.graphics;
using RentACar.model;
using RentACar.pagination;
using RentACar.validation;
using System.Diagnostics;
using System.Xml.Linq;

namespace RentACar.controller
{
    internal class CarController
    {
        string filePath = "C:\\Users\\Valerian\\OneDrive\\Desktop\\STEP IT\\C#\\Final_Project\\RentACar\\data\\data_cars.txt";

        List<EventHistory> m_EventsHistory = new List<EventHistory>();

        List<Car> m_Cars = new List<Car>(); // m-member of Cars

        public static CarController s_Instance;

        private CarController() { }
              
        public static CarController GetInstance() 
        {
            if (s_Instance == null)
            {
                s_Instance = new CarController();
            }
            return s_Instance; 
        } 

        public void AddCar()
        {
            Car car = new Car();

            int id, distanceTraveled, year;
            string make, model, producerCountry, color, transmissionType;
            bool isBusyStatus;

            Console.Write("Enter car's id: ");
            id = Validation.getValidInt();
            id = Validation.validateUniqueId(id, m_Cars);
            car.setId(id);

            Console.Write("Enter car's make: ");
            make = Console.ReadLine();
            make = Validation.validateMake(make);
            car.setMake(make);

            Console.Write("Enter car's model: ");
            model = Console.ReadLine();
            model = Validation.validateModel(model);
            car.setModel(model);

            Console.Write("Enter car's producer country: ");
            producerCountry = Console.ReadLine();
            producerCountry = Validation.validateProperNoun(producerCountry, "producer country");
            car.setProducerCountry(producerCountry);

            Console.Write("Enter car's color: ");
            color = Console.ReadLine();
            color = Validation.validateCommonNoun(color, "color");
            car.setColor(color);

            Console.Write("Enter car's transmission type: ");
            transmissionType = Console.ReadLine();
            transmissionType = Validation.validateTransmissionType(transmissionType);
            car.setTransmissionType(transmissionType);

            Console.Write("Enter car's distance traveled(km): ");
            distanceTraveled = Validation.getValidInt();
            distanceTraveled = Validation.validateCarDistanceTraveled(distanceTraveled);
            car.setDistanceTraveled(distanceTraveled);

            Console.Write("Enter car's year: ");
            year = Validation.getValidInt();
            year = Validation.validateCarYear(year);
            car.setYear(year);

            Console.Write("Enter car's busy status (yes/no): ");
            isBusyStatus = Validation.validateStringToBool("car busy status");
            car.setBusyStatus(isBusyStatus);

            AddEventByUser(car, true);

            m_Cars.Add(car);
            Console.WriteLine("Car added successfully!");
        }

        public void AddEventByUser(Car car, bool isCalledFirstTime)
        {
            bool option = true;
            if (isCalledFirstTime)
            {
                Console.Write($"{Graphics.YELLOW}Do you want enter an event for your car? (yes/no):{Graphics.NORMAL} ");
                option = Validation.validateStringToBool("choice");
            }

            while (option)
            {
                EventHistoryController eventHistoryController = EventHistoryController.GetInstance();
                EventHistory eventHistory = eventHistoryController.AddEvent("Car");

                car.addEvent(eventHistory);

                Console.Write($"{Graphics.BLUE}Continue (yes/no):{Graphics.NORMAL} ");
                option = Validation.validateStringToBool("choice");
            }
        }

        public void UpdateEventByUser(Car car)
        {
            EventHistoryController eventHistoryController = EventHistoryController.GetInstance();
            List<EventHistory> eventHistories = car.getEventHistory();

            if (eventHistories.Count > 0)
            {
                foreach (var eventHistory in eventHistories)
                {
                    Console.WriteLine(eventHistory.ToString());
                }
                Console.WriteLine();

                Console.Write($"{Graphics.YELLOW}Enter car's event ID that you want to change:{Graphics.NORMAL} ");
                int userSelectedId = Validation.getValidInt();
                userSelectedId = Validation.validateExistingId(userSelectedId, eventHistories);

                EventHistory selectedEventHistory = eventHistoryController.UpdateEventHistory(userSelectedId);
            }
            else
            {
                Console.WriteLine("There aren't any events set for this car yet so you cannot modify anything!");
            }
        }

        public void LoadCars()
        {
            StreamReader outputCars = new StreamReader(filePath);
            int totalLines = 0;

            while (!outputCars.EndOfStream)
            {
                string readCarLine = outputCars.ReadLine();
                if (readCarLine != null)
                {
                    totalLines++;
                }
            }

            if (totalLines > 0)
            {
                //outputCars.BaseStream.Position = 0; // set the cursor position back to 0
                //outputCars.DiscardBufferedData();
                outputCars.Close();
                outputCars = new StreamReader(filePath);

                while (!outputCars.EndOfStream)
                {
                    string readCarLine = outputCars.ReadLine();

                    string[] elements = readCarLine.Split(' ');

                    Car car = new Car();
                    EventHistoryController eventHistoryController = EventHistoryController.GetInstance();
                    List<EventHistory> eventsHistory = new List<EventHistory>();

                    car.setId(Convert.ToInt32(elements[0]));
                    car.setMake(elements[1]);
                    car.setModel(elements[2]);
                    car.setProducerCountry(elements[3]);
                    car.setColor(elements[4]);
                    car.setTransmissionType(elements[5]);
                    car.setDistanceTraveled(Convert.ToInt32(elements[6]));
                    car.setYear(Convert.ToInt32(elements[7]));
                    car.setBusyStatus(Convert.ToBoolean(elements[8]));

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
                    car.setEventHistory(eventsHistory);
                    m_EventsHistory.AddRange(eventsHistory);

                    m_Cars.Add(car);
                }
            }
            outputCars.Close();
        }

        public void SaveCars()
        {
            StreamWriter inputCars = new StreamWriter(filePath);

            foreach (var car in m_Cars)
            {
                inputCars.WriteLine(car.FileSaveString());
            }

            inputCars.Close();
        }

        public void ShowCars()
        {
            int totalLines = 0;

            foreach (var car in m_Cars)
            {
                totalLines++;
            }

            Pagination.ShowOnPage(totalLines, m_Cars);
        }

        public void ShowCarById(int carId)
        {
            foreach (var car in m_Cars)
            {
                if (car.getId() == carId)
                {
                    Console.WriteLine(car.ToString());
                    return;
                }
            }
            Console.WriteLine($"Car with ID[{carId}] not found!");
        }

        public Car FindCarById(int carId)
        {
            foreach (var car in m_Cars)
            {
                if (car.getId() == carId)
                    return car;
            }

            // Debug.Assert(false, "someObject is null! this could totally be a bug!");
            return null;
        }

        public Car GetAvailableCar(int carId)
        {
            carId = Validation.validateExistingId(carId, m_Cars);

            if (carId != 0)
            {
                foreach (var car in m_Cars)
                {
                    if (car.getId() == carId && car.getBusyStatus() == false)
                    {
                        car.setBusyStatus(true);
                        return car;
                    }
                }
                Console.WriteLine($"Cannot provide this car to client because it is {Graphics.RED}busy{Graphics.NORMAL}!");
                return null;
            }
            Console.WriteLine($"Cannot provide car to client because " +
                              $"{Graphics.RED}there aren't any registered cars yet{Graphics.NORMAL}!");
            return null;
        }

        public void RemoveCarBusyStatus(int carId)
        {
            foreach (var car in m_Cars)
            {
                if (car.getId() == carId)
                {
                    car.setBusyStatus(false);
                    Console.WriteLine($"Seted busy status for car with ID[{carId}] - {Graphics.GREEN}false{Graphics.NORMAL}");
                }
            }
        }

        public int GetRegisteredCarsCount()
        {
           return m_Cars.Count > 0 ? m_Cars.Count : 0;
        }

        public void UpdateCar(int carId)
        {
            carId = Validation.validateExistingId(carId, m_Cars);

            if (carId != 0)
            {
                Car car = FindCarById(carId);
                while (true)
                {
                    Console.WriteLine();
                    Console.WriteLine("Enter what you want to change:");
                    Console.WriteLine("1. Change make");
                    Console.WriteLine("2. Change model");
                    Console.WriteLine("3. Change producer country");
                    Console.WriteLine("4. Change color");
                    Console.WriteLine("5. Change transmission type");
                    Console.WriteLine("6. Change distance traveled");
                    Console.WriteLine("7. Change year");
                    Console.WriteLine("8. Change current car event history");
                    Console.WriteLine("9. Add new event for car");
                    Console.WriteLine("X. All done");
                    string choice;
                    Console.Write("\nEnter your choice:: ");
                    choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                        {
                            Console.Write("Enter car's new make:: ");
                            string newMake = Console.ReadLine();
                            newMake = Validation.validateMake(newMake);

                            car.setMake(newMake);
                            break;
                        }
                        case "2":
                        {
                            Console.Write("Enter car's new model:: ");
                            string newModel = Console.ReadLine();
                            newModel = Validation.validateModel(newModel);

                            car.setModel(newModel);
                            break;
                        }
                        case "3":
                        {
                            Console.Write("Enter car's new producer country:: ");
                            string newProducerCountry = Console.ReadLine();
                            newProducerCountry = Validation.validateProperNoun(newProducerCountry, "producer country");
                            car.setProducerCountry(newProducerCountry);
                            break;
                        }
                        case "4":
                        {
                            Console.Write("Enter car's new color:: ");
                            string newColor = Console.ReadLine();
                            newColor = Validation.validateCommonNoun(newColor, "color");
                            car.setColor(newColor);
                            break;
                        }
                        case "5":
                        {
                            Console.Write("Enter car's new transmission type:: ");
                            string newTransmissionType = Console.ReadLine();

                            newTransmissionType = Validation.validateTransmissionType(newTransmissionType);
                            car.setTransmissionType(newTransmissionType);
                            break;
                        }
                        case "6":
                        {
                            Console.Write("Enter car's new distance traveled(km):: ");
                            int newDistanceTraveled = Validation.getValidInt();
                            newDistanceTraveled = Validation.validateCarDistanceTraveled(newDistanceTraveled);
                            car.setDistanceTraveled(newDistanceTraveled);
                            break;
                        }
                        case "7":
                        {
                            Console.Write("Enter car's new year:: ");
                            int newYear = Validation.getValidInt();
                            newYear = Validation.validateCarYear(newYear);
                            car.setYear(newYear);
                            break;
                        }
                        case "8":
                        {
                            // updating event for car
                            UpdateEventByUser(car);
                            break;
                        }
                        case "9":
                        {
                            Console.WriteLine($"{Graphics.GREEN}Adding new event for car:{Graphics.NORMAL}");
                            AddEventByUser(car, false);

                            break;
                        }
                        case "x":
                        case "X":
                        {
                            Console.WriteLine("Car updated successfully!");
                            return;
                        }
                        default:
                        {
                            Console.WriteLine("Invalid choice! Please try again");
                            break;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("There aren't any registered cars yet!");
            }
        }

        public void DeleteCar(int carId)
        {
            int i = 0;
            foreach(var car in m_Cars)
            {
                if (car.getId() == carId)
                {
                    m_Cars.RemoveAt(i);
                    Console.WriteLine($"Employee with ID [{carId}] has been deleted successfully");
                    return;
                }
                i++;
            }
            Console.WriteLine($"Car with ID[{carId}] not found!");
        }
    }
}
