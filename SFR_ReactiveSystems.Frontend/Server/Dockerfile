#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SFR_ReactiveSystems.Frontend/Server/SFR_ReactiveSystems.Frontend.Server.csproj", "SFR_ReactiveSystems.Frontend/Server/"]
COPY ["SFR_ReactiveSystems.Frontend/Client/SFR_ReactiveSystems.Frontend.Client.csproj", "SFR_ReactiveSystems.Frontend/Client/"]
RUN dotnet restore "SFR_ReactiveSystems.Frontend/Server/SFR_ReactiveSystems.Frontend.Server.csproj"
COPY . .
WORKDIR "/src/SFR_ReactiveSystems.Frontend/Server"
RUN dotnet build "SFR_ReactiveSystems.Frontend.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SFR_ReactiveSystems.Frontend.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SFR_ReactiveSystems.Frontend.Server.dll"]
