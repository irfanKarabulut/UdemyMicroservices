
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/PhotoStock/UdemyMicroServices.PhotoStock/UdemyMicroServices.PhotoStock.csproj", "Services/PhotoStock/UdemyMicroServices.PhotoStock/"]
COPY ["Shared/UdemyMicroservices.Shared/UdemyMicroservices.Shared.csproj", "Shared/UdemyMicroservices.Shared/"]
RUN dotnet restore "Services/PhotoStock/UdemyMicroServices.PhotoStock/UdemyMicroServices.PhotoStock.csproj"
COPY . .
WORKDIR "/src/Services/PhotoStock/UdemyMicroServices.PhotoStock"
RUN dotnet build "UdemyMicroServices.PhotoStock.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UdemyMicroServices.PhotoStock.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UdemyMicroServices.PhotoStock.dll"]