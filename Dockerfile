FROM mcr.microsoft.com/dotnet/aspnet:5.0
EXPOSE 80


FROM mcr.microsoft.com/dotnet/sdk:5.0
ENV ASPNETCORE_URLS=http://+:80  
RUN apt-get update && apt install -y nodejs npm
RUN apt-get update \
    && apt-get install -y --allow-unauthenticated \
        libc6-dev  \
        libgdiplus \
        libx11-dev \
    && rm -rf /var/lib/apt/lists/*
COPY . Server/
WORKDIR /Server
RUN dotnet publish -c Release 
WORKDIR /Server/bin/Release/net5.0/publish

ENTRYPOINT ["dotnet", "Docker.dll"]
