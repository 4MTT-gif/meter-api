# ---------- 1. Asama: Derleme ----------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY MeterApi/MeterApi.csproj MeterApi/
COPY MeterApi.Tests/MeterApi.Tests.csproj MeterApi.Tests/
RUN dotnet restore MeterApi/MeterApi.csproj

COPY . .
RUN dotnet publish MeterApi/MeterApi.csproj -c Release -o /app/publish

# ---------- 2. Asama: Calistirma ----------
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Production
EXPOSE 8080

ENTRYPOINT ["dotnet", "MeterApi.dll"]
