using RentACar.graphics;
using System.Text;

namespace RentACar.model
{
    internal class Client
    {
        int id;
        string name;
        string surname;
        string gender;
        string phoneNumber;
        string idnp;
        int age;
        int drivingExperience;
        bool isBannedStatus;
        Car car = new Car();
        List<EventHistory> m_EventsHistory = new List<EventHistory>();

        public Client() { }

        public Client(int id, string name, string surname, string gender, string phoneNumber, string idnp,
                      int age, int drivingExperience, bool isBannedStatus, Car car, List<EventHistory> eventsHistory)
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.gender = gender;
            this.phoneNumber = phoneNumber;
            this.idnp = idnp;
            this.age = age;
            this.drivingExperience = drivingExperience;
            this.isBannedStatus = isBannedStatus;
            this.car = car;
            this.m_EventsHistory = eventsHistory;
        }

        public void setId(int id)
        {
            this.id = id;
        }

        public int getId()
        {
            return id;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public string getName()
        {
            return name;
        }

        public void setSurname(string surname)
        {
            this.surname = surname;
        }

        public string getSurname()
        {
            return surname;
        }

        public void setGender(string gender)
        {
            this.gender = gender;
        }

        public string getGender()
        {
            return gender.StartsWith("M", StringComparison.OrdinalIgnoreCase) ? "Male" : "Female";
        }
        public void setIdnp(string idnp)
        {
            this.idnp = idnp;
        }

        public string getIdnp()
        {
            return idnp;
        }
        public void setPhoneNumber(string phoneNumber)
        {
            this.phoneNumber = phoneNumber;
        }

        public string getPhoneNumber()
        {
            return phoneNumber;
        }

        public void setAge(int age)
        {
            this.age = age;
        }

        public int getAge()
        {
            return age;
        }

        public void setDrivingExperience(int drivingExperience)
        {
            this.drivingExperience = drivingExperience;
        }

        public int getDrivingExperience()
        {
            return drivingExperience;
        }   

        public void setIsBannedStatus(bool isBannedStatus)
        {
            this.isBannedStatus = isBannedStatus;
        }

        public bool getIsBannedStatus()
        {
            return isBannedStatus;
        }

        public void setCar(Car car)
        {
            this.car = car;
        }   
        
        public Car getCar()
        {
            return car;
        }

        public void setEventHistory(List<EventHistory> eventHistory)
        {
            if (eventHistory != null)
            {
                m_EventsHistory.AddRange(eventHistory);
            }
        }

        public void addEvent(EventHistory eventHistory)
        {
            if (eventHistory != null)
            {
                m_EventsHistory.Add(eventHistory);
            }
        }

        public List<EventHistory> getEventHistory() 
        {
            return m_EventsHistory;
        }

        public string FileSaveString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{id} {name} {surname} {getGender()} {phoneNumber} {idnp} {age} {drivingExperience}");
            sb.Append($" {isBannedStatus} {(car != null ? car.getId() : 0)}");

            if (m_EventsHistory != null && m_EventsHistory.Count > 0)
            {
                // Loop through all EventHistory objects in the collection
                foreach (var eventHistory in m_EventsHistory)
                {
                    int id = eventHistory.getId(); 
                    sb.Append($" {id}");
                }
            }
            return sb.ToString();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Id: [{id}]");
            sb.AppendLine($"Name: {name}");
            sb.AppendLine($"Surname: {surname}");
            sb.AppendLine($"Gender: {getGender()}");
            sb.AppendLine($"Phone Number: {phoneNumber}");
            sb.AppendLine($"IDNP: {idnp}");
            sb.AppendLine($"Age: {age}");
            sb.AppendLine($"Driving Experience: {drivingExperience} years");
            sb.AppendLine($"Is Banned Status: {(isBannedStatus ? $"{Graphics.RED}Banned" : $"{Graphics.GREEN}Not Banned")}{Graphics.NORMAL}");
            sb.AppendLine($"{Graphics.CYAN}Car: {(car != null ? "\n" + car.ToStringForClient() : "currently not renting any")}{Graphics.NORMAL}");
            sb.Append($"{Graphics.CYAN}Client events history: ");
            if (m_EventsHistory != null && m_EventsHistory.Count > 0)
            {
                sb.AppendLine();
                foreach (var eventHistory in m_EventsHistory)
                {
                    sb.AppendLine($"{Graphics.YELLOW}{eventHistory.ToStringForObject()}");
                }
            }
            else
            {
                sb.Append($"not any events have been registered yet!");
            }
            sb.Append($"{Graphics.NORMAL}");

            return sb.ToString();
        }
    }
}
