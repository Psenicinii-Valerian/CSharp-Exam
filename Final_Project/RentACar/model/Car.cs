using RentACar.graphics;
using System.Text;
using System.Xml.Linq;

namespace RentACar.model
{
    internal class Car
    {
        int id;
        string make;
        string model;
        string producerCountry;
        string color;
        string transmissionType;
        int distanceTraveled;
        int year;
        bool isBusyStatus;
        List<EventHistory> m_EventsHistory = new List<EventHistory>();

        public Car() { }
        public Car(int id, string make, string model, string producerCountry, string color, string transmissionType,
                   int distanceTraveled, int year, bool isBusyStatus, List<EventHistory> eventsHistory)
        {
            this.id = id;
            this.make = make;
            this.model = model;
            this.producerCountry = producerCountry;
            this.color = color;
            this.transmissionType = transmissionType;
            this.distanceTraveled = distanceTraveled;
            this.year = year;
            this.isBusyStatus = isBusyStatus;
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

        public void setMake(string make)
        {
            this.make = make;
        }

        public string getMake()
        {
            return make;
        }

        public void setModel(string model)
        {
            this.model = model;
        }

        public string getModel()
        {
            return model;
        }

        public void setProducerCountry(string producerCountry)
        {
            this.producerCountry = producerCountry;
        }

        public string getProducerCountry()
        {
            return producerCountry;
        }

        public void setColor(string color)
        {
            this.color = color;
        }

        public string getColor()
        {
            return color;
        }

        public void setTransmissionType(string transmissionType)
        {
            this.transmissionType = transmissionType;
        }

        public string getTransmissionType()
        {
            return transmissionType;
        }

        public void setDistanceTraveled(int distanceTraveled)
        {
            this.distanceTraveled = distanceTraveled;
        }

        public int getDistanceTraveled()
        {
            return distanceTraveled;
        }

        public void setYear(int year)
        {
            this.year = year;
        }

        public int getYear()
        {
            return year;
        }

        public void setBusyStatus(bool isBusyStatus)
        {
            this.isBusyStatus = isBusyStatus;
        }

        public bool getBusyStatus()
        {
            return isBusyStatus;
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

            sb.Append($"{id} {make} {model} {producerCountry} {color} {transmissionType} {distanceTraveled} {year} {isBusyStatus}");

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
            sb.AppendLine($"Make: {make}");
            sb.AppendLine($"Model: {model}");
            sb.AppendLine($"Producer Country: {producerCountry}");
            sb.AppendLine($"Color: {color}");
            sb.AppendLine($"Transmission Type: {transmissionType}");
            sb.AppendLine($"Distance Traveled: {distanceTraveled} km");
            sb.AppendLine($"Year: {year}");
            sb.AppendLine($"Is Busy: {(isBusyStatus ? $"{Graphics.RED}Busy" : $"{Graphics.GREEN}Not Busy")}{Graphics.NORMAL}");
            sb.Append($"{Graphics.CYAN}Car events history: ");
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

        public string ToStringForClient()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{Graphics.MAGENTA}ID: [{id}]");
            sb.AppendLine($"Make: {make}");
            sb.AppendLine($"Model: {model}");
            sb.AppendLine($"Producer Country: {producerCountry}");
            sb.AppendLine($"Color: {color}");
            sb.AppendLine($"Transmission Type: {transmissionType}");
            sb.AppendLine($"Distance Traveled: {distanceTraveled} km");
            sb.AppendLine($"Year: {year}");
            sb.Append($"{Graphics.CYAN}Car events history: ");
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

            // Remove last \n from sb if present
            if (sb.Length > 0 && sb[sb.Length - 1] == '\n')
            {
                sb.Length--; // Remove last character from StringBuilder
            }

            sb.Append($"{Graphics.NORMAL}");

            return sb.ToString();
        }
    }
}
