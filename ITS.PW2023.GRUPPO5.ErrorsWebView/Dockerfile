#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ITS.PW2023.GRUPPO5.ErrorsWebView/ITS.PW2023.GRUPPO5.ErrorsWebView.csproj", "ITS.PW2023.GRUPPO5.ErrorsWebView/"]
RUN dotnet restore "ITS.PW2023.GRUPPO5.ErrorsWebView/ITS.PW2023.GRUPPO5.ErrorsWebView.csproj"
COPY . .
WORKDIR "/src/ITS.PW2023.GRUPPO5.ErrorsWebView"
RUN dotnet build "ITS.PW2023.GRUPPO5.ErrorsWebView.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ITS.PW2023.GRUPPO5.ErrorsWebView.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ITS.PW2023.GRUPPO5.ErrorsWebView.dll"]