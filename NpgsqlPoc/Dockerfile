#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["NpgsqlPoc/NpgsqlPoc.csproj", "NpgsqlPoc/"]
RUN dotnet restore "NpgsqlPoc/NpgsqlPoc.csproj"
RUN dotnet dev-certs https
COPY . .
WORKDIR "/src/NpgsqlPoc"

RUN dotnet build "NpgsqlPoc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NpgsqlPoc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
COPY --from=build /root/.dotnet/corefx/cryptography/x509stores/my/* /root/.dotnet/corefx/cryptography/x509stores/my/
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NpgsqlPoc.dll"]