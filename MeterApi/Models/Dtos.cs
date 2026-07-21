namespace MeterApi.Models;

public record CreateDeviceDto(string Name, string Location);

public record CreateReadingDto(double Value, DateTime? ReadAt);
