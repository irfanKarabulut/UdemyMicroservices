FROM mcr.microsoft.com/dotnet/sdk:3.1 as build
WORKDIR /app
EXPOSE 80
COPY Shared/UdemyMicroservices.Shared/*.csporj Shared/UdemyMicroservices.Shared/
COPY IdentityServer/UdemyMicroservices.IdentityServer/*.csproj IdentityServer/UdemyMicroservices.IdentityServer/
RUN dotnet restore IdentityServer/UdemyMicroservices.IdentityServer/*.csproj
COPY . .
RUN dotnet publish IdentityServer/UdemyMicroservices.IdentityServer/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:3.1 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","UdemyMicroservices.IdentityServer.dll" ]