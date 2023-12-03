FROM mcr.microsoft.com/dotnet/sdk:7.0-bookworm-slim as build
WORKDIR /app
EXPOSE 80
COPY Shared/UdemyMicroservices.Shared/*.csporj Shared/UdemyMicroservices.Shared/
COPY Services/Basket/UdemyMicroservices.Basket/*.csproj Services/Basket/UdemyMicroservices.Basket/
RUN dotnet restore Services/Basket/UdemyMicroservices.Basket/*.csproj
COPY . .
RUN dotnet publish Services/Basket/UdemyMicroservices.Basket/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:7.0-bookworm-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","UdemyMicroservices.Basket.dll" ]