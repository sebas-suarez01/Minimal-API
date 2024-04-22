FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base-env
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /src

COPY ["Minimal-API.API/Minimal-API.API.csproj", "Minimal-API.API/"]
COPY ["Minimal-API.Application/Minimal-API.Application.csproj", "Minimal-API.Application/"]
COPY ["Minimal-API.Domain/Minimal-API.Domain.csproj", "Minimal-API.Domain/"]
COPY ["Minimal-API.Infrastructure/Minimal-API.Infrastructure.csproj", "Minimal-API.Infrastructure/"]
COPY ["Minimal-API.Persistance/Minimal-API.Persistance.csproj", "Minimal-API.Persistance/"]

RUN dotnet restore "Minimal-API.API/Minimal-API.API.csproj"

COPY . .

WORKDIR "/src/Minimal-API.API/"

RUN dotnet build "Minimal-API.API.csproj" -c Release -o /app/build


FROM build-env AS publish
RUN dotnet publish "Minimal-API.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base-env AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Minimal-API.API.dll"]