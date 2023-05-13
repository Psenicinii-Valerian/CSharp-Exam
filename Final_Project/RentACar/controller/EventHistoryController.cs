using RentACar.model;
using RentACar.pagination;
using RentACar.validation;
using System.Diagnostics;
using RentACar.graphics;
namespace RentACar.controller
{
    internal class EventHistoryController
    {
        string filePath = "C:\\Users\\Valerian\\OneDrive\\Desktop\\STEP IT\\C#\\Final_Project\\RentACar\\data\\data_events_history.txt";

        List<EventHistory> m_EventsHistory = new List<EventHistory>();

        public static EventHistoryController s_Instance;

        private EventHistoryController() { }

        public static EventHistoryController GetInstance()
        {
            if (s_Instance == null)
            {
                s_Instance = new EventHistoryController();
            }
            return s_Instance;
        }

        public EventHistory AddEvent(string targetUser = "")
        {
            EventHistory newEvent = new EventHistory();

            int id;
            string description, eventGravity, relatedEntity, date;

            Console.Write("Enter event's id: ");
            id = Validation.getValidInt();
            id = Validation.validateUniqueId(id, m_EventsHistory);
            newEvent.setId(id);

            Console.Write("Enter event's description: ");
            description = Console.ReadLine();
            description = Validation.validateDescription(description);
            newEvent.setDescription(description);

            Console.Write("Enter event's date (dd/MM/yyyy): ");
            date = Validation.getValidStringDate();
            newEvent.setDate(date);

            Console.Write("Enter event's gravity (Low/Moderate/High): ");
            eventGravity = Console.ReadLine();
            Validation.validateGravityStatus(eventGravity);
            newEvent.setEventGravityStatus(eventGravity);

            if(targetUser == "")
            {
                Console.WriteLine($"{Graphics.RED}Pay attention! You will cannot change the following information later{Graphics.NORMAL}");
                Console.Write("Enter event's related entity (Car/Client/Employee): ");
                relatedEntity = Console.ReadLine();
                relatedEntity = Validation.validateRelatedEntity(relatedEntity);
                newEvent.setRelatedEntity(relatedEntity);
            }
            else
            {
                newEvent.setRelatedEntity(targetUser);
            }

            m_EventsHistory.Add(newEvent);

            return newEvent;
        }

        public void LoadEventsHistory()
        {
            StreamReader outputEvents = new StreamReader(filePath);
            int totalLines = 0;

            while (!outputEvents.EndOfStream)
            {
                string readEventLine = outputEvents.ReadLine();
                if (readEventLine != null)
                {
                    totalLines++;
                }
            }

            if (totalLines > 0)
            {
                outputEvents.Close();
                outputEvents = new StreamReader(filePath);

                while (!outputEvents.EndOfStream)
                {
                    string readEventLine = outputEvents.ReadLine();

                    string[] elements = readEventLine.Split(' ');

                    EventHistory newEvent = new EventHistory();

                    newEvent.setId(Convert.ToInt32(elements[0]));
                    newEvent.setDescription(elements[1]);
                    newEvent.setDate(elements[2]);
                    newEvent.setEventGravityStatus(elements[3]);
                    newEvent.setRelatedEntity(elements[4]);

                    m_EventsHistory.Add(newEvent);
                }
            }
            outputEvents.Close();
        }

        public void SaveEventsHistory()
        {
            StreamWriter inputEvents = new StreamWriter(filePath);

            foreach (var eventsHistory in m_EventsHistory)
            {
                inputEvents.WriteLine(eventsHistory.FileSaveString());
            }

            inputEvents.Close();
        }

        public void ShowEventsHistory()
        {
            int totalLines = 0;

            foreach (var eventsHistory in m_EventsHistory)
            {
                totalLines++;
            }

            Pagination.ShowOnPage(totalLines, m_EventsHistory);
        }

        public void ShowEventById(int eventId)
        {
            foreach (var eventsHistory in m_EventsHistory)
            {
                if (eventsHistory.getId() == eventId)
                {
                    Console.WriteLine(eventsHistory.ToString());
                    return;
                }
            }
            Console.WriteLine($"Event with ID[{eventId}] not found!");
        }

        public EventHistory FindEventById(int eventId)
        {
            foreach (var eventsHistory in m_EventsHistory)
            {
                if (eventsHistory.getId() == eventId)
                {
                    return eventsHistory;
                }
            }
            Debug.Assert(false, "eventsHistory is null! This could totally be a bug!");
            return new EventHistory();
        }

        public EventHistory UpdateEventHistory(int eventId)
        {
            eventId = Validation.validateExistingId(eventId, m_EventsHistory);
            if (eventId != 0)
            {
                EventHistory eventHistory = FindEventById(eventId);

                while (true)
                {
                    Console.WriteLine();
                    Console.WriteLine("Enter what you want to change:");
                    Console.WriteLine("1. Change event description");
                    Console.WriteLine("2. Change event date");
                    Console.WriteLine("3. Change event gravity status");
                    Console.WriteLine("X. All done");
                    char choice;
                    Console.Write("\nEnter your choice:: ");
                    choice = Validation.getValidChar();

                    switch (choice)
                    {
                        case '1':
                        {
                            Console.Write("Enter new event description: ");
                            string newDescription = Console.ReadLine();
                            newDescription = Validation.validateDescription(newDescription);

                            eventHistory.setDescription(newDescription);
                            break;
                        }
                        case '2':
                        {
                            Console.Write("Enter new event date: ");
                            string newDate = Validation.getValidStringDate();

                            eventHistory.setDate(newDate);
                            break;
                        }
                        case '3':
                        {
                            Console.Write("Enter new event gravity status: ");
                            string newGravityStatus = Console.ReadLine();
                            newGravityStatus = Validation.validateGravityStatus(newGravityStatus);

                            eventHistory.setEventGravityStatus(newGravityStatus);
                            break;
                        }
                        case 'x':
                        case 'X':
                        {
                            Console.WriteLine("Event history updated successfully!");
                            return eventHistory;
                        }
                        default:
                        {
                            Console.WriteLine("You've entered a wrong option!");
                            break;
                        }
                    }
                }
            }
            return null; // we won't get here never
        }

        public void DeleteEvent(int eventId)
        {
            int i = 0;
            foreach (var ev in m_EventsHistory)
            {
                if (ev.getId() == eventId)
                {
                    m_EventsHistory.RemoveAt(i);
                    Console.WriteLine($"Event with ID [{eventId}] has been deleted successfully");
                    return;
                }
                i++;
            }
            Console.WriteLine($"Event with ID [{eventId}] not found!");
        }
    }
}
