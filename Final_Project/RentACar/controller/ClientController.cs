using RentACar.graphics;
using RentACar.model;
using RentACar.pagination;
using RentACar.validation;
using System.Diagnostics;

namespace RentACar.controller
{

    internal class ClientController
    {
        string filePath = "C:\\Users\\Valerian\\OneDrive\\Desktop\\STEP IT\\C#\\Final_Project\\RentACar\\data\\data_clients.txt";

        Car m_Car = new Car(); 
        List<Client> m_Clients = new List<Client>();
        List<EventHistory> m_EventsHistory = new List<EventHistory>();

        public static ClientController s_Instance;

        private ClientController() { }
        
        public static ClientController GetInstance()
        {
            if (s_Instance == null)
            {
                s_Instance= new ClientController(); 
            }

            return s_Instance;
        }

        public void AddClient()
        {
            Client client = new Client();

            int id, age, drivingExperience;
            string name, surname, gender, phoneNumber, idnp;
            bool isBannedStatus;
            
            Console.Write("Enter client's id: ");
            id = Validation.getValidInt();
            id = Validation.validateUniqueId(id, m_Clients);
            client.setId(id);

            Console.Write("Enter client's name: ");
            name = Console.ReadLine();
            name = Validation.validateProperNoun(name, "name");
            client.setName(name);

            Console.Write("Enter client's surname: ");
            surname = Console.ReadLine();
            surname = Validation.validateProperNoun(surname, "surname");
            client.setSurname(surname);

            Console.Write("Enter client's gender (male/female): ");
            gender = Console.ReadLine();
            gender = Validation.validateGender(gender);
            client.setGender(gender);

            Console.Write("Enter client's phone number(starting with +373): ");
            phoneNumber = Console.ReadLine();
            phoneNumber = Validation.validatePhoneNumber(phoneNumber);
            client.setPhoneNumber(phoneNumber);

            Console.Write("Enter client's idnp: ");
            idnp = Console.ReadLine();
            idnp = Validation.validateIdnp(idnp);
            client.setIdnp(idnp);

            Console.Write("Enter client's age: ");
            age = Validation.getValidInt();
            age = Validation.validateClientAge(age);
            client.setAge(age);

            Console.Write("Enter client's driving experience(years): ");
            drivingExperience = Validation.getValidInt();
            drivingExperience = Validation.validateClientDrivingExperience(drivingExperience, age);
            client.setDrivingExperience(drivingExperience);

            Console.Write("Enter client's banned status (yes/no): ");
            isBannedStatus = Validation.validateStringToBool("client banned status");
            client.setIsBannedStatus(isBannedStatus);

            m_Car = SetClientCarByUser(isBannedStatus);
            client.setCar(m_Car);

            AddEventByUser(client, true);

            m_Clients.Add(client);
        }

        public void AddEventByUser(Client client, bool isCalledFirstTime)
        {
            bool option = true;
            if (isCalledFirstTime)
            {
                Console.Write($"{Graphics.YELLOW}Do you want enter an event for your client (yes/no):{Graphics.NORMAL} ");
                option = Validation.validateStringToBool("choice");
            }

            while (option)
            {
                EventHistoryController eventHistoryController = EventHistoryController.GetInstance();
                EventHistory eventHistory = eventHistoryController.AddEvent("Client");

                client.addEvent(eventHistory);

                if (eventHistory.getEventGravityStatus() == "High")
                {
                    client.setIsBannedStatus(true);

                    if (client.getCar() != null)
                    {
                        UpdateClientCarByUser(client.getCar().getId(), client.getIsBannedStatus());
                        client.setCar(null);
                    }
                }

                Console.Write($"{Graphics.BLUE}Continue (yes/no):{Graphics.NORMAL} ");
                option = Validation.validateStringToBool("choice");
            }
        }

        public void UpdateEventByUser(Client client)
        {
            EventHistoryController eventHistoryController = EventHistoryController.GetInstance();
            List<EventHistory> eventsHistory = client.getEventHistory();

            if (eventsHistory.Count > 0)
            {
                foreach (var eventHistory in eventsHistory)
                {
                    Console.WriteLine(eventHistory.ToString());
                }
                Console.WriteLine();

                Console.Write($"{Graphics.YELLOW}Enter client's event ID that you want to change:{Graphics.NORMAL} ");
                int userSelectedId = Validation.getValidInt();
                userSelectedId = Validation.validateExistingId(userSelectedId, eventsHistory);

                EventHistory selectedEventHistory = eventHistoryController.UpdateEventHistory(userSelectedId);

                if (selectedEventHistory.getEventGravityStatus() == "High")
                {
                    client.setIsBannedStatus(true);
                    UpdateClientCarByUser(client.getCar().getId(), client.getIsBannedStatus());
                    client.setCar(null);
                }
            }
            else
            {
                Console.WriteLine("There aren't any events set for this client yet so you cannot modify anything!");
            }
        }

        public Car SetClientCarByUser(bool isBannedStatus)
        {
            if (!isBannedStatus)
            {
                CarController carController = CarController.GetInstance();
                Console.Write($"{Graphics.MAGENTA}Enter the ID of the car that the client is currently " +
                              $"renting(0 if not renting any car):{Graphics.NORMAL} ");
  
                int carId = Validation.getValidInt();
                if (carId != 0) 
                {
                    m_Car = carController.GetAvailableCar(carId);
                    return m_Car;
                }
            }
            else
            {
                Console.WriteLine($"Cannot provide a car to this user because he/she is {Graphics.RED}banned{Graphics.NORMAL}!");
            }
            return null;
        }

        public Car UpdateClientCarByUser(int carId, bool isBannedStatus)
        {
            CarController carController = CarController.GetInstance();

            if (carId != 0) 
            {
                carController.RemoveCarBusyStatus(carId);
            }

            if (!isBannedStatus)
            {
                Console.Write($"{Graphics.MAGENTA}Enter client's new rented car (0 if not renting any car anymore):{Graphics.NORMAL} ");
                int newCarId = Validation.getValidInt();

                if (newCarId != 0)
                {
                    m_Car = carController.GetAvailableCar(newCarId);
                    return m_Car;
                }
            }
            else
            {
                Console.WriteLine($"Cannot provide a car to this user because he/she is {Graphics.RED}banned{Graphics.NORMAL}!");
            }
            return null;
        }

        public void LoadClients()
        {
            StreamReader outputClients = new StreamReader(filePath);
            int totalLines = 0;

            while(!outputClients.EndOfStream)
            {
                string readClientLine = outputClients.ReadLine();
                if (readClientLine != null)
                {
                    totalLines++;
                }
            }

            if (totalLines > 0)
            {
                outputClients.Close();
                outputClients = new StreamReader(filePath);

                while (!outputClients.EndOfStream)
                {
                    string readClientLine = outputClients.ReadLine();

                    if (readClientLine != null)
                    {
                        string[] elements = readClientLine.Split(' ');
                        if (elements.Length > 0) 
                        {
                            Client client = new Client();
                            CarController carController = CarController.GetInstance();
                            EventHistoryController eventHistoryController = EventHistoryController.GetInstance();
                            List<EventHistory> eventsHistory = new List<EventHistory>();

                            client.setId(Convert.ToInt32(elements[0]));
                            client.setName(elements[1]);
                            client.setSurname(elements[2]);
                            client.setGender(elements[3]);
                            client.setPhoneNumber(elements[4]);
                            client.setIdnp(elements[5]);
                            client.setAge(Convert.ToInt32(elements[6]));
                            client.setDrivingExperience(Convert.ToInt32(elements[7]));
                            client.setIsBannedStatus(Convert.ToBoolean(elements[8]));
                            m_Car = carController.FindCarById(Convert.ToInt32(elements[9]));
                            client.setCar(m_Car);

                            if (elements.Length > 10)
                            {
                                for(int i = 10; i < elements.Length; i++) 
                                { 
                                    eventsHistory.Add(eventHistoryController.FindEventById(Convert.ToInt32(elements[i])));
                                }
                            }
                            else
                            {
                                m_EventsHistory = new List<EventHistory>();
                            }
                            client.setEventHistory(eventsHistory);
                            m_EventsHistory.AddRange(eventsHistory);

                            m_Clients.Add(client);
                        }
                    }
                }
            }
            outputClients.Close();
        }

        public void SaveClients()
        {
            StreamWriter inputClients = new StreamWriter(filePath);
            foreach (var client in m_Clients)
            {
                inputClients.WriteLine(client.FileSaveString());
            }
            inputClients.Close();
        }

        public void ShowClients() 
        {
            int totaLines = 0;

            foreach(var client in m_Clients)
            {
                totaLines++;
            }
            Pagination.ShowOnPage(totaLines, m_Clients);
        }

        public void ShowClientById(int clientId) 
        {
            foreach (var client in m_Clients)
            {
                if (client.getId() == clientId)
                {
                    Console.WriteLine(client.ToString());
                    return;
                }
            }
            Console.WriteLine($"Client with ID[{clientId}] not found!");
        }

        public Client FindClientById(int clientId)
        {
            foreach (var client in m_Clients)
            {
                if (client.getId() == clientId)
                {
                    return client;
                }
            }
            Debug.Assert(false, "someObject is null! this could totally be a bug!");
            return new Client();
        }

        public int GetRegisteredClientsCount()
        {
            return m_Clients.Count > 0 ? m_Clients.Count : 0;
        }

        public void UpdateClient(int clientId)
        {
            clientId = Validation.validateExistingId(clientId, m_Clients);

            if (clientId != 0) 
            {
                Client client = FindClientById(clientId);
                while (true)
                {
                    Console.WriteLine();
                    Console.WriteLine("Enter what you want to change:");
                    Console.WriteLine("1. Change name");
                    Console.WriteLine("2. Change surname");
                    Console.WriteLine("3. Change gender");
                    Console.WriteLine("4. Change phone number");
                    Console.WriteLine("5. Change IDNP");
                    Console.WriteLine("6. Change age");
                    Console.WriteLine("7. Change driving experience");
                    Console.WriteLine("8. Change is banned status");
                    Console.WriteLine("9. Change currently rented car(or add if not renting any)");
                    Console.WriteLine("10. Change current client event history");
                    Console.WriteLine("11. Add new event for client");
                    Console.WriteLine("X. All done");
                    string choice;
                    Console.Write("\nEnter your choice: ");
                    choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                        {
                            Console.Write("Enter client's new name: ");
                            string newName = Console.ReadLine();
                            newName = Validation.validateProperNoun(newName, "name");

                            client.setName(newName);
                            break;
                        }
                        case "2":
                        {
                            Console.Write("Enter client's new surname: ");
                            string newSurname = Console.ReadLine();
                            newSurname = Validation.validateProperNoun(newSurname, "surname");

                            client.setSurname(newSurname);
                            break;
                        }
                        case "3":
                        {
                            Console.Write("Enter client's new gender: ");
                            string newGender = Console.ReadLine();
                            newGender = Validation.validateGender(newGender);

                            client.setGender(newGender);
                            break;
                        }
                        case "4":
                        {
                            Console.Write("Enter client's new phone number(starting with +373): ");
                            string newPhoneNumber = Console.ReadLine();
                            newPhoneNumber = Validation.validatePhoneNumber(newPhoneNumber);

                            client.setPhoneNumber(newPhoneNumber);
                            break;
                        }
                        case "5":
                        {
                            Console.Write("Enter client's new IDNP: ");
                            string newIdnp = Console.ReadLine();
                            newIdnp = Validation.validateIdnp(newIdnp);

                            client.setIdnp(newIdnp);
                            break;
                        }
                        case "6":
                        {
                            Console.Write("Enter client's new age: ");
                            int newAge = Validation.getValidInt();
                            newAge = Validation.validateClientAge(newAge);

                            client.setAge(newAge);
                            break;
                        }
                        case "7":
                        {
                            Console.Write("Enter client's new driving experience(years): ");
                            int newDrivingExperience = Validation.getValidInt();
                            newDrivingExperience = Validation.validateClientDrivingExperience(newDrivingExperience, client.getAge());

                            client.setDrivingExperience(newDrivingExperience);
                            break;
                        }
                        case "8":
                        {
                            Console.Write("Enter client's new ban status (yes/no): ");
                            bool newIsBannedStatus = Validation.validateStringToBool("client's ban status");

                            if (newIsBannedStatus)
                            {
                                m_Car = UpdateClientCarByUser(client.getCar() != null ? client.getCar().getId() : 0,
                                                              newIsBannedStatus);
                                client.setCar(null);
                            }

                            client.setIsBannedStatus(newIsBannedStatus);
                            break;
                        }
                        case "9":
                        {
                            m_Car = UpdateClientCarByUser(client.getCar() != null ? client.getCar().getId() : 0,
                                                          client.getIsBannedStatus());

                            client.setCar(m_Car);
                            break;
                        }
                        case "10":
                        {
                            // updating event for client
                            UpdateEventByUser(client);
                            break;
                        }
                        case "11":
                        {
                            Console.WriteLine($"{Graphics.GREEN}Adding new event for client:{Graphics.NORMAL}");
                            AddEventByUser(client, false);

                            break;
                        }
                        case "x":
                        case "X":
                        {
                            Console.WriteLine($"Client updated successfully!");
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
                Console.WriteLine("There aren't any registered clients yet!");
            }
        }

        public void DeleteClient(int clientId)
        {
            foreach (var client in m_Clients)
            {
                if (client.getId() == clientId)
                {
                    m_Clients.Remove(client);
                    return;
                }
            }
            Console.WriteLine($"Client with ID[{clientId}] not found!");
        }
    }
}