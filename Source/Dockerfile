FROM microsoft/dotnet:2.2-sdk-nanoserver-1709 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY Talking.Clock.Service/*.csproj ./Talking.Clock.Service/
COPY Talking.Clock.Service.Tests/*.csproj ./Talking.Clock.Service.Tests/
RUN dotnet restore

# copy and build everything else
COPY . ./

RUN dotnet build --no-restore -c Release

FROM build AS tests
WORKDIR /app/Talking.Clock.Service.Tests
RUN dotnet test --no-build -c Release

FROM tests AS publish
WORKDIR /app/Talking.Clock.Service
RUN dotnet publish --no-build -c Release -o out

FROM microsoft/dotnet:2.2-aspnetcore-runtime-nanoserver-1709 AS runtime
WORKDIR /app
COPY --from=publish /app/Talking.Clock.Service/out ./
ENTRYPOINT ["dotnet", "Talking.Clock.Service.dll"]