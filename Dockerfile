#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["4PawsBackend/PawsBackend.csproj", "4PawsBackend/"]
RUN dotnet restore "4PawsBackend/PawsBackend.csproj"
COPY . .
WORKDIR "/src/4PawsBackend"
RUN dotnet build "PawsBackend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PawsBackend.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ./paws-398316-cba858c90847.json /app/paws-398316-cba858c90847.json
ENTRYPOINT ["dotnet", "PawsBackend.dll"]