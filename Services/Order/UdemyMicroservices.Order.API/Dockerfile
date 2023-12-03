#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Order/UdemyMicroservices.Order.API/UdemyMicroservices.Order.API.csproj", "Services/Order/UdemyMicroservices.Order.API/"]
COPY ["Services/Order/UdemyMicroservices.Order.Application/UdemyMicroservices.Order.Application.csproj", "Services/Order/UdemyMicroservices.Order.Application/"]
COPY ["Shared/UdemyMicroservices.Shared/UdemyMicroservices.Shared.csproj", "Shared/UdemyMicroservices.Shared/"]
COPY ["Services/Order/UdemyMicroservices.Order.Domain/UdemyMicroservices.Order.Domain.csproj", "Services/Order/UdemyMicroservices.Order.Domain/"]
COPY ["Services/Order/UdemyMicroservices.Order.Domain.Core/UdemyMicroservices.Order.Domain.Core.csproj", "Services/Order/UdemyMicroservices.Order.Domain.Core/"]
COPY ["Services/Order/UdemyMicroservices.Order.Infrastructure/UdemyMicroservices.Order.Infrastructure.csproj", "Services/Order/UdemyMicroservices.Order.Infrastructure/"]
RUN dotnet restore "Services/Order/UdemyMicroservices.Order.API/UdemyMicroservices.Order.API.csproj"
COPY . .
WORKDIR "/src/Services/Order/UdemyMicroservices.Order.API"
RUN dotnet build "UdemyMicroservices.Order.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UdemyMicroservices.Order.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UdemyMicroservices.Order.API.dll"]