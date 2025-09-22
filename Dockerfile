# 1. Build mərhələsi
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# csproj faylını kopyala (tam yol göstəririk)
COPY Presentation/ExlaqiNasiri.App/ExlaqiNasiri.App.csproj ./ExlaqiNasiri.App.csproj
RUN dotnet restore ./ExlaqiNasiri.App.csproj

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
