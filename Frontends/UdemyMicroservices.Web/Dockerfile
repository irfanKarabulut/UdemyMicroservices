
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Frontends/UdemyMicroservices.Web/UdemyMicroservices.Web.csproj", "Frontends/UdemyMicroservices.Web/"]
COPY ["Shared/UdemyMicroservices.Shared/UdemyMicroservices.Shared.csproj", "Shared/UdemyMicroservices.Shared/"]
RUN dotnet restore "Frontends/UdemyMicroservices.Web/UdemyMicroservices.Web.csproj"
COPY . .
WORKDIR "/src/Frontends/UdemyMicroservices.Web"
RUN dotnet build "UdemyMicroservices.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UdemyMicroservices.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UdemyMicroservices.Web.dll"]