FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY ./build .

EXPOSE 5190
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:5190

# Configuraciones adicionales para producción
ENV DOTNET_EnableDiagnostics=0
ENV DOTNET_USE_POLLING_FILE_WATCHER=false
ENV ASPNETCORE_LOGGING__CONSOLE__DISABLECOLORS=true

ENTRYPOINT ["dotnet", "TFG-BACK.dll"]
