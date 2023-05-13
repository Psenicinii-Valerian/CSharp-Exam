using RentACar.graphics;
using RentACar.validation;
using System.Text;

namespace RentACar.model
{
    internal class Employee
    {
        int id;
        string name;
        string surname;
        string gender;
        string position;
        string email;
        int age;
        int workExperience;
        int salary;
        List<EventHistory> m_EventsHistory = new List<EventHistory>();
        public Employee() { }
        public Employee(int id, string name, string surname, string gender, string position, string email, 
                        int age, int workExperience, int salary, List<EventHistory> eventsHistory)
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.gender = gender;
            this.position = position;
            this.email = email;
            this.age = age;
            this.workExperience = workExperience;
            this.salary = salary;
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

        public void setPosition(string position)
        {
            this.position = position;
        }

        public string getPosition()
        {
            return position;
        }

        public void setEmail(string email)
        {
            this.email = email;
        }

        public string getEmail()
        {
            return email;
        }

        public void setAge(int age)
        {
            this.age = age;
        }

        public int getAge()
        {
            return age;
        }

        public void setWorkExperience(int workExperience)
        {
            this.workExperience = workExperience;
        }

        public int getWorkExperience()
        {
            return workExperience;
        }

        public void setSalary(int salary)
        {
            this.salary = salary;
        }

        public int getSalary()
        {
            return salary;
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

            sb.Append($"{id} {name} {surname} {getGender()} {position} {email} {age} {workExperience} {salary}");
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
            sb.AppendLine($"ID: [{id}]");
            sb.AppendLine($"Name: {name}");
            sb.AppendLine($"Surname: {surname}");
            sb.AppendLine($"Gender: {getGender()}");
            sb.AppendLine($"Position: {position}");
            sb.AppendLine($"Email: {email}");
            sb.AppendLine($"Age: {age} years old");
            sb.AppendLine($"Work experience: {workExperience} year(s)");
            sb.AppendLine($"Salary: {salary}$$");
            sb.Append($"{Graphics.CYAN}Employee events history: ");
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
                sb.AppendLine("not any events have been registered yet!");
            }
            sb.Append($"{Graphics.NORMAL}");

            return sb.ToString();
        }
    }
}
