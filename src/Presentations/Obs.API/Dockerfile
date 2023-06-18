FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Presentations/Obs.API/Obs.API.csproj", "src/Presentations/Obs.API/"]
RUN dotnet restore "src/Presentations/Obs.API/Obs.API.csproj"
COPY . .
WORKDIR "/src/src/Presentations/Obs.API"
RUN dotnet build "Obs.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Obs.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Obs.API.dll"]
