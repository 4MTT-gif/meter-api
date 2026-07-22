using System.Collections.Concurrent;
using MeterApi.Models;

namespace MeterApi.Services;

public interface IDeviceStore
{
    IEnumerable<Device> GetAll();
    Device? GetById(Guid id);
    Device Add(string name, string location);
    bool Delete(Guid id);
    Reading? AddReading(Guid deviceId, double value, DateTime? readAt);
}

public class DeviceStore : IDeviceStore
{
    private readonly ConcurrentDictionary<Guid, Device> _devices = new();

    public IEnumerable<Device> GetAll() =>
        _devices.Values.OrderByDescending(d => d.CreatedAt);

    public Device? GetById(Guid id) =>
        _devices.TryGetValue(id, out var d) ? d : null;

    public Device Add(string name, string location)
    {
        var device = new Device { Name = name, Location = location };
        _devices[device.Id] = device;
        return device;
    }

    public bool Delete(Guid id) => _devices.TryRemove(id, out _);

    public Reading? AddReading(Guid deviceId, double value, DateTime? readAt)
    {
        if (!_devices.TryGetValue(deviceId, out var device)) return null;

        var reading = new Reading
        {
            Value = value,
            ReadAt = readAt ?? DateTime.UtcNow
        };
        lock (device.Readings) { device.Readings.Add(reading); }
        return reading;
    }
}
