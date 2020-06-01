#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["PriceCalculator/PriceCalculator.csproj", "PriceCalculator/"]
COPY ["PriceCalculator.Tests/PriceCalculator.Tests.csproj", "PriceCalculator.Tests/"]
RUN dotnet restore "PriceCalculator.Tests/PriceCalculator.Tests.csproj"
COPY . .
WORKDIR "/src/PriceCalculator.Tests"
RUN dotnet test "PriceCalculator.Tests.csproj" 
WORKDIR "/src/PriceCalculator"

FROM build AS publish
RUN dotnet publish "PriceCalculator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PriceCalculator.dll"]
