FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["NuGet.Config", "."]
COPY ["src/Snooker.HttpApi.Host/Snooker.HttpApi.Host.csproj", "src/Snooker.HttpApi.Host/"]
COPY ["src/Snooker.HttpApi/Snooker.HttpApi.csproj", "src/Snooker.HttpApi/"]
COPY ["src/Snooker.Application.Contracts/Snooker.Application.Contracts.csproj", "src/Snooker.Application.Contracts/"]
COPY ["src/Snooker.Domain.Shared/Snooker.Domain.Shared.csproj", "src/Snooker.Domain.Shared/"]
COPY ["src/Snooker.Application/Snooker.Application.csproj", "src/Snooker.Application/"]
COPY ["src/Snooker.Domain/Snooker.Domain.csproj", "src/Snooker.Domain/"]
COPY ["src/Snooker.EntityFrameworkCore.DbMigrations/Snooker.EntityFrameworkCore.DbMigrations.csproj", "src/Snooker.EntityFrameworkCore.DbMigrations/"]
COPY ["src/Snooker.EntityFrameworkCore/Snooker.EntityFrameworkCore.csproj", "src/Snooker.EntityFrameworkCore/"]
RUN dotnet restore "src/Snooker.HttpApi.Host/Snooker.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/src/Snooker.HttpApi.Host"
RUN dotnet build "Snooker.HttpApi.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Snooker.HttpApi.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Snooker.HttpApi.Host.dll"]
