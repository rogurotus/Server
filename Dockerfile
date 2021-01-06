FROM mcr.microsoft.com/dotnet/aspnet:5.0
COPY bin/Release/net5.0/publish/ Server/
WORKDIR /Server

EXPOSE 80

ENTRYPOINT ["dotnet", "Docker.dll"]