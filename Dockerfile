FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore
COPY EvaCase/EvaCase.Domain/EvaCase.Domain.csproj EvaCase.Domain/
COPY EvaCase/EvaCase.Application/EvaCase.Application.csproj EvaCase.Application/
COPY EvaCase/EvaCase.Infrastructure/EvaCase.Infrastructure.csproj EvaCase.Infrastructure/
COPY EvaCase/EvaCase.WebAPI/EvaCase.WebAPI.csproj EvaCase.WebAPI/
RUN dotnet restore EvaCase.WebAPI/EvaCase.WebAPI.csproj

# Copy everything and build
COPY EvaCase/EvaCase.Domain/ EvaCase.Domain/
COPY EvaCase/EvaCase.Application/ EvaCase.Application/
COPY EvaCase/EvaCase.Infrastructure/ EvaCase.Infrastructure/
COPY EvaCase/EvaCase.WebAPI/ EvaCase.WebAPI/
RUN dotnet build EvaCase.WebAPI/EvaCase.WebAPI.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish EvaCase.WebAPI/EvaCase.WebAPI.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EvaCase.WebAPI.dll"]
