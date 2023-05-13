using RentACar.graphics;

namespace RentACar.model
{
    internal class EventHistory
    {
        int id;
        string description;
        string date;
        string eventGravityStatus; // Low, Moderate, High 
        string relatedEntity; // Car, Client, Employee

        public EventHistory() { }

        public EventHistory(int id, string description, string date, string eventGravity, string relatedEntity)
        {
            this.id = id;
            this.description = description;
            this.date = date;
            this.eventGravityStatus = eventGravity;
            this.relatedEntity = relatedEntity;
        }

        public void setId(int id)
        {
            this.id = id;
        }

        public int getId()
        {
            return id;
        }

        public void setDescription(string description)
        {
            this.description = description;
        }

        public string getDescription()
        {
            return description;
        }

        public void setDate(string date)
        {
            this.date = date;
        }

        public string getDate()
        {
            return date;
        }

        public void setEventGravityStatus(string eventGravity)
        {
            this.eventGravityStatus = eventGravity;
        }

        public string getEventGravityStatus()
        {
            return eventGravityStatus;
        }

        public void setRelatedEntity(string relatedEntity)
        {
            this.relatedEntity = relatedEntity;
        }

        public string getRelatedEntity()
        {
            return relatedEntity;
        }

        public string FileSaveString()
        {
            return $"{id} {description} {date} {eventGravityStatus} {relatedEntity}";
        }

        public override string ToString()
        {
            return $"ID: [{id}]" +
                   $"\nDescription: {description}" +
                   $"\nDate: {date}" +
                   $"\nEvent Gravity: {(eventGravityStatus == "Low" ? $"{Graphics.GREEN}Low" : (eventGravityStatus == "Moderate" ? $"{Graphics.BLUE}Moderate" : $"{Graphics.RED}High"))}{Graphics.NORMAL}" +
                   $"\nRelated Entity: {relatedEntity}";
        }

        public string ToStringForObject()
        {
            return $"ID: [{id}]" +
                   $"\nDescription: {description}" +
                   $"\nDate: {date}" +
                   $"\nEvent Gravity: {(eventGravityStatus == "Low" ? $"{Graphics.GREEN}Low" : (eventGravityStatus == "Moderate" ? $"{Graphics.BLUE}Moderate" : $"{Graphics.RED}High"))}{Graphics.NORMAL}";
        }
    }
}
