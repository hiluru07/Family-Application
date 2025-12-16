# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy only the project file
COPY FamilyApplication/*.csproj FamilyApplication/
RUN dotnet restore "FamilyApplication/FamilyApplication.csproj"

# Copy all files
COPY . .
WORKDIR /app/FamilyApplication
RUN dotnet publish -c Release -o /out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "FamilyApplication.dll"]
