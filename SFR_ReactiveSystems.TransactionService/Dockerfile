#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SFR_ReactiveSystems.TransactionService/SFR_ReactiveSystems.TransactionService.csproj", "SFR_ReactiveSystems.TransactionService/"]
RUN dotnet restore "SFR_ReactiveSystems.TransactionService/SFR_ReactiveSystems.TransactionService.csproj"
COPY . .
WORKDIR "/src/SFR_ReactiveSystems.TransactionService"
RUN dotnet build "SFR_ReactiveSystems.TransactionService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SFR_ReactiveSystems.TransactionService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SFR_ReactiveSystems.TransactionService.dll"]
