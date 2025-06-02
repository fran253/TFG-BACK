# Dockerfile para TFG-BACK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY TFG-BACK.csproj ./
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 3000
ENV ASPNETCORE_URLS=http://+:3000

ENTRYPOINT ["dotnet", "TFG-BACK.dll"]