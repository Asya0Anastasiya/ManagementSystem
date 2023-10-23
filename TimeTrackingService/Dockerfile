FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /source
COPY . .

RUN dotnet restore "./TimeTrackingService.csproj" --disable-parallel
RUN dotnet publish "./TimeTrackingService.csproj" -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal 
WORKDIR /app
COPY --from=build /app ./

ENV ASPNETCORE_URLS=http://+:5002
#ENV GOOGLE_APPLICATION_CREDENTIALS=./application_default_credentials.json
EXPOSE 5002

ENTRYPOINT ["dotnet", "TimeTrackingService.dll"]