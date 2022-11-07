FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["LocationsApi/LocationsApi.csproj", "LocationsApi/"]
RUN dotnet restore "LocationsApi/LocationsApi.csproj"
COPY . .
WORKDIR "/src/LocationsApi"
RUN dotnet build "LocationsApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LocationsApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LocationsApi.dll"]