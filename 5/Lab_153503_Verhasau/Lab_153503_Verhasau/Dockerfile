#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Lab_153503_Verhasau/Lab_153503_Verhasau.csproj", "Lab_153503_Verhasau/"]
RUN dotnet restore "Lab_153503_Verhasau/Lab_153503_Verhasau.csproj"
COPY . .
WORKDIR "/src/Lab_153503_Verhasau"
RUN dotnet build "Lab_153503_Verhasau.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Lab_153503_Verhasau.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lab_153503_Verhasau.dll"]