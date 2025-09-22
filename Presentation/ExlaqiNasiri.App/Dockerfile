# 1. Build mərhələsi (SDK image istifadə olunur)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# csproj fayllarını kopyala və dependencies restore et
COPY *.csproj ./
RUN dotnet restore

# Bütün layihəni kopyala və build et
COPY . ./
RUN dotnet publish -c Release -o out

# 2. Runtime mərhələsi (yalnız runtime image)
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out ./

# API portunu aç
EXPOSE 80

# Container işə düşəndə bunu çalışdır
ENTRYPOINT ["dotnet", "ExlaqiNasiri.App.dll"]
