FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src

COPY [".", "WebApi/SimpleDemo01/"]
RUN dotnet restore "WebApi/SimpleDemo01/SimpleDemo01.csproj"
COPY . .
WORKDIR "/src/WebApi/SimpleDemo01"
RUN dotnet build "SimpleDemo01.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "SimpleDemo01.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SimpleDemo01.dll"]