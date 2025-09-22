# --- Stage 1: Build ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# İş qovluğunu təyin edirik
WORKDIR /src

# Bütün csproj-ları kopyalayırıq
COPY Core/ExlaqiNasiri.Application/ExlaqiNasiri.Application.csproj Core/ExlaqiNasiri.Application/
COPY Core/ExlaqiNasiri.Domain/ExlaqiNasiri.Domain.csproj Core/ExlaqiNasiri.Domain/
COPY Infrastructure/ExlaqiNasiri.Infrastructure/ExlaqiNasiri.Infrastructure.csproj Infrastructure/ExlaqiNasiri.Infrastructure/
COPY Infrastructure/ExlaqiNasiri.Persistence/ExlaqiNasiri.Persistence.csproj Infrastructure/ExlaqiNasiri.Persistence/
COPY Presentation/ExlaqiNasiri.App/ExlaqiNasiri.App.csproj Presentation/ExlaqiNasiri.App/

# Asılılıqları restore edirik
RUN dotnet restore Presentation/ExlaqiNasiri.App/ExlaqiNasiri.App.csproj

# Layihəni kopyalayırıq
COPY . .

# Layihəni release üçün publish edirik
RUN dotnet publish Presentation/ExlaqiNasiri.App/ExlaqiNasiri.App.csproj -c Release -o /app/publish /p:UseAppHost=false

# --- Stage 2: Runtime ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

# Publish edilmiş faylları gətiririk
COPY --from=build /app/publish .

# API default port
EXPOSE 5000
EXPOSE 5001

# API-ni işə salırıq
ENTRYPOINT ["dotnet", "ExlaqiNasiri.App.dll"]
