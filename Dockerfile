# 1. Build mərhələsi
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# copy csproj files first
COPY ExlaqiNasiri.App/ExlaqiNasiri.App.csproj ./ExlaqiNasiri.App/
COPY ExlaqiNasiri.Application/ExlaqiNasiri.Application.csproj ./ExlaqiNasiri.Application/
COPY ExlaqiNasiri.Infrastructure/ExlaqiNasiri.Infrastructure.csproj ./ExlaqiNasiri.Infrastructure/
COPY ExlaqiNasiri.Persistence/ExlaqiNasiri.Persistence.csproj ./ExlaqiNasiri.Persistence/
COPY ExlaqiNasiri.Domain/ExlaqiNasiri.Domain.csproj ./ExlaqiNasiri.Domain/

# restore all projects
RUN dotnet restore ExlaqiNasiri.App/ExlaqiNasiri.App.csproj


# Bütün layihəni kopyala
COPY Presentation/ExlaqiNasiri.App ./ExlaqiNasiri.App
WORKDIR /app/ExlaqiNasiri.App
RUN dotnet publish -c Release -o out

# 2. Runtime mərhələsi
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/ExlaqiNasiri.App/out ./

EXPOSE 80
ENTRYPOINT ["dotnet", "ExlaqiNasiri.App.dll"]
