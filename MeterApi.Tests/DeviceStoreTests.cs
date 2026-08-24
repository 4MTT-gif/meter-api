using MeterApi.Services;

namespace MeterApi.Tests;

public class DeviceStoreTests
{
    [Fact]
    public void Add_CihaziDepoyaEkler()
    {
        var store = new DeviceStore();

        var device = store.Add("Sayac-1", "Bodrum");

        Assert.Single(store.GetAll());
        Assert.Equal("Sayac-1", device.Name);
        Assert.Equal("Bodrum", device.Location);
    }

    [Fact]
    public void GetById_YoksaNullDoner()
    {
        var store = new DeviceStore();

        var result = store.GetById(Guid.NewGuid());

        Assert.Null(result);
    }

    [Fact]
    public void Delete_VarOlanCihaziSiler()
    {
        var store = new DeviceStore();
        var device = store.Add("Sayac-2", "Cati");

        var silindi = store.Delete(device.Id);

        Assert.True(silindi);
        Assert.Empty(store.GetAll());
    }

    [Fact]
    public void Delete_OlmayanCihazIcinFalseDoner()
    {
        var store = new DeviceStore();

        var silindi = store.Delete(Guid.NewGuid());

        Assert.False(silindi);
    }

    [Fact]
    public void AddReading_CihazaOkumaEkler()
    {
        var store = new DeviceStore();
        var device = store.Add("Sayac-3", "Giris");

        var reading = store.AddReading(device.Id, 42, null);

        Assert.NotNull(reading);
        Assert.Equal(42, reading!.Value);
        Assert.Single(device.Readings);
    }

    [Fact]
    public void AddReading_OlmayanCihazIcinNullDoner()
    {
        var store = new DeviceStore();

        var reading = store.AddReading(Guid.NewGuid(), 10, null);

        Assert.Null(reading);
    }
    
    [Fact]
    public void GetAll_BosDepodaBosDoner()
    {
        var store = new DeviceStore();

        var count = store.GetAll().Count();

        Assert.Equal(0, count);
    }

    [Fact]
    public void GetAll_EklenenCihazSayisiniDondurur()
    {
        var store = new DeviceStore();
        store.Add("Sayac-1", "Bodrum");
        store.Add("Sayac-2", "Cati");
        store.Add("Sayac-3", "Giris");

        var count = store.GetAll().Count();

        Assert.Equal(3, count);
    }
}
