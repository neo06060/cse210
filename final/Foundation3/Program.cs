using System;
class Address
{
    private string streetAddress;
    private string city;
    private string state;
    private string country;
    public Address(string streetAddress, string city, string state, string country)
    {
        this.streetAddress = streetAddress;
        this.city = city;
        this.state = state;
        this.country = country;
    }
    public string GetFullAddress()
    {
        return $"{streetAddress}, {city}, {state}, {country}";
    }
}
class Event
{
    private string title;
    private string description;
    private DateTime date;
    private TimeSpan time;
    private Address address;
    public Event(string title, string description, DateTime date, TimeSpan time, Address address)
    {
        this.title = title;
        this.description = description;
        this.date = date;
        this.time = time;
        this.address = address;
    }
    public virtual string GetStandardDetails()
    {
        return $"Title: {title}\nDescription: {description}\nDate: {date.ToShortDateString()}\nTime: {time}\nAddress: {address.GetFullAddress()}";
    }
    public virtual string GetFullDetails()
    {
        return GetStandardDetails();
    }
    public virtual string GetShortDescription()
    {
        return $"Event: {title}, Date: {date.ToShortDateString()}";
    }
}
class Lecture : Event
{
    private string speaker;
    private int capacity;
    public Lecture(string title, string description, DateTime date, TimeSpan time, Address address, string speaker, int capacity)
        : base(title, description, date, time, address)
    {
        this.speaker = speaker;
        this.capacity = capacity;
    }
    public override string GetFullDetails()
    {
        return $"{base.GetFullDetails()}\nSpeaker: {speaker}\nCapacity: {capacity}";
    }
    public override string GetShortDescription()
    {
        return $"Lecture: {base.GetShortDescription()}";
    }
}
class Reception : Event
{
    private string rsvpEmail;
    public Reception(string title, string description, DateTime date, TimeSpan time, Address address, string rsvpEmail)
        : base(title, description, date, time, address)
    {
        this.rsvpEmail = rsvpEmail;
    }
    public override string GetFullDetails()
    {
        return $"{base.GetFullDetails()}\nRSVP Email: {rsvpEmail}";
    }
    public override string GetShortDescription()
    {
        return $"Reception: {base.GetShortDescription()}";
    }
}
class OutdoorGathering : Event
{
    private string weatherForecast;
    public OutdoorGathering(string title, string description, DateTime date, TimeSpan time, Address address, string weatherForecast)
        : base(title, description, date, time, address)
    {
        this.weatherForecast = weatherForecast;
    }
    public override string GetFullDetails()
    {
        return $"{base.GetFullDetails()}\nWeather Forecast: {weatherForecast}";
    }
    public override string GetShortDescription()
    {
        return $"Outdoor Gathering: {base.GetShortDescription()}";
    }
}
class Program
{
    static void Main(string[] args)
    {
        Address address1 = new Address("123 Jhon St", "ohio", "san francisco", "polonia");
        Address address2 = new Address("456 Daniel St", "santa monica", "madrid", "austria");
        Lecture lecture = new Lecture("john lecture", "join johns beautifull lecture about cats", new DateTime(2023, 6, 1), new TimeSpan(14, 0, 0), address1, "John Doe", 100);
        Reception reception = new Reception("anual gala", "join our prestigious gala", new DateTime(2023, 6, 2), new TimeSpan(18, 0, 0), address2, "rsvp@example.com");
        OutdoorGathering outdoorGathering = new OutdoorGathering("Outdoor gala", "join our outdoor gala filled with food", new DateTime(2023, 6, 3), new TimeSpan(10, 0, 0), address1, "Sunny");
        List<Event> events = new List<Event> { lecture, reception, outdoorGathering };
        foreach (var evt in events)
        {
            Console.WriteLine(evt.GetStandardDetails());
            Console.WriteLine();
            Console.WriteLine(evt.GetFullDetails());
            Console.WriteLine();
            Console.WriteLine(evt.GetShortDescription());
            Console.WriteLine();
        }
    }
}
