using MeterApi.Models;
using MeterApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MeterApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DevicesController : ControllerBase
{
    private readonly IDeviceStore _store;

    public DevicesController(IDeviceStore store) => _store = store;

    [HttpGet]
    public ActionResult<IEnumerable<Device>> GetAll() => Ok(_store.GetAll());

    [HttpGet("{id:guid}")]
    public ActionResult<Device> GetById(Guid id)
    {
        var device = _store.GetById(id);
        return device is null ? NotFound() : Ok(device);
    }

    [HttpPost]
    public ActionResult<Device> Create(CreateDeviceDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            return BadRequest(new { error = "Cihaz adi bos olamaz." });

        var device = _store.Add(dto.Name.Trim(), dto.Location?.Trim() ?? "");
        return CreatedAtAction(nameof(GetById), new { id = device.Id }, device);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id) =>
        _store.Delete(id) ? NoContent() : NotFound();

    [HttpPost("{id:guid}/readings")]
    public ActionResult<Reading> AddReading(Guid id, CreateReadingDto dto)
    {
        if (dto.Value < 0)
            return BadRequest(new { error = "Okuma degeri negatif olamaz." });

        var reading = _store.AddReading(id, dto.Value, dto.ReadAt);
        return reading is null ? NotFound() : Ok(reading);
    }
}
