# Use the official .NET SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

# Copy the solution file and restore NuGet packages
COPY *.sln .
COPY ASPNETPlus/ ASPNETPlus/
COPY ASPNETPlus.Contracts/ ASPNETPlus.Contracts/
COPY ASPNETPlus.Entities/ ASPNETPlus.Entities/
COPY ASPNETPlus.LoggerService/ ASPNETPlus.LoggerService/
COPY ASPNETPlus.Repository/ ASPNETPlus.Repository/
COPY ASPNETPlus.Presentation/ ASPNETPlus.Presentation/
COPY ASPNETPlus.Service/ ASPNETPlus.Service/
COPY ASPNETPlus.Service.Contracts/ ASPNETPlus.Service.Contracts/
COPY ASPNETPlus.Shared/ ASPNETPlus.Shared/

# Restore NuGet packages for the solution
RUN dotnet restore

# Copy the remaining source code and build
# COPY . .

# Build the application
RUN dotnet build

# Publish the application
RUN dotnet publish -c Release -o out

# Build the final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Expose port 80 for the API
EXPOSE 80

# Run the API when the container starts
ENTRYPOINT ["dotnet", "ASPNETPlus.dll"]
