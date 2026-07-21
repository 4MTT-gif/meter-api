namespace MeterApi.Models;

public class Device
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public List<Reading> Readings { get; set; } = new();
}

public class Reading
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public double Value { get; set; }
    public DateTime ReadAt { get; set; } = DateTime.UtcNow;
}
