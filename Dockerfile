FROM mcr.microsoft.com/dotnet/aspnet:5.0
EXPOSE 80
RUN apt-get update && apt-get install -y nodejs
FROM mcr.microsoft.com/dotnet/sdk:5.0
ENV ASPNETCORE_URLS=http://+:80  
COPY bin/Release/net5.0/publish/ Server/
COPY key.json Server/
WORKDIR /Server

ENTRYPOINT ["dotnet", "Docker.dll"]