# Meter API (Backend)

Cihaz/sayac kayit uygulamasinin backend servisi. .NET Web API ile yazildi, Docker ile paketlendi, Render uzerinde calisiyor.

## Canli Adres

- API: https://meter-api-t1lq.onrender.com
- Ornek: https://meter-api-t1lq.onrender.com/api/devices

## Teknolojiler

- .NET 9 Web API (C#)
- Veri: in-memory (List/Dictionary) - kalici degil
- Docker (multi-stage build)
- GitHub Actions (build + test)
- Render (hosting)

## Endpointler

| Metot | Yol | Aciklama |
|-------|-----|----------|
| GET | /api/devices | Tum cihazlari listele |
| GET | /api/devices/{id} | Tek cihaz getir |
| POST | /api/devices | Cihaz ekle |
| DELETE | /api/devices/{id} | Cihaz sil |
| POST | /api/devices/{id}/readings | Cihaza okuma ekle |
| GET | /health | Servis durumu |

## Onemli Notlar

### Render uyku davranisi
Ucretsiz pakette API 15 dakika istek almazsa uykuya gecer. Uyandiktan sonra ilk istek servisin yeniden baslamasi nedeniyle ~50 saniyeye kadar surebilir. Bu bir hata degildir, normal davranistir. Demo oncesi API bir kez uyandirilmalidir.

### Veri kaliciligi
Veriler bellekte (in-memory) tutulur. Servis uykuya gectiginde veya her yeni deploy sirasinda tum veriler sifirlanir. Bu beklenen ve kabul edilen davranistir; projenin amaci veri saklama degil, uctan uca zincirin calismasidir.

## Yerel Calistirma
API http://localhost:8080 adresinde calisir. Swagger yerine test icin /health ve /api/devices endpointleri kullanilabilir.

## CORS

Frontend farkli bir origin oldugu icin (github.io), backend CORS ile o adrese izin verir. Izin verilen origin, `AllowedOrigins` environment variable ile yapilandirilir.

## TestlerDeviceStore icin birim testleri mevcuttur (ekleme, silme, okuma, hata durumlari).

## Ilgili Repo

Frontend (React arayuz): https://github.com/4MTT-gif/meter-frontend

Canli arayuz: https://4mtt-gif.github.io/meter-frontend/
