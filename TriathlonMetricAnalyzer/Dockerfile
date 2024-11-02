# Use Microsoft's .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy the .csproj file and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the application and build it
COPY . ./
RUN dotnet publish -c Release -o /out

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /out .

# Expose the port for Render
EXPOSE 8080
ENV ASPNETCORE_URLS=https://+:8080

# Run the application
ENTRYPOINT ["dotnet", "TriathlonMetricAnalyzer.dll"]

