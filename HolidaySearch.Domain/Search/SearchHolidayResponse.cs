namespace HolidaySearch.Domain.Search
{
    public class SearchHolidayResponse
    {
        public int TotalResults { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<HolidayResult> Results { get; set; }
    }

    public class HolidayResult
    {
        public Flight Flight { get; set; }
        public Hotel Hotel { get; set; }
        public decimal TotalPrice => Flight?.Price + Hotel?.TotalPrice ?? 0;
    }

    public class Flight
    {
        public int Id { get; set; }
        public string Airline { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public decimal Price { get; set; }
        public DateTime DepartureDate { get; set; }
    }

    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ArrivalDate { get; set; }
        public decimal PricePerNight { get; set; }
        public int Nights { get; set; }
        public List<string> LocalAirports { get; set; }

        public decimal TotalPrice => PricePerNight * Nights;
    }
}
