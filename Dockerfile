FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY ./build .

EXPOSE 5190
ENV ASPNETCORE_URLS=http://+:5190

ENTRYPOINT ["dotnet", "TFG-BACK.dll"]
