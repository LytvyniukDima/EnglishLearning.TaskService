FROM microsoft/dotnet:2.1-sdk AS build

# copy everything else and build app
COPY . /app
WORKDIR /app/EnglishLearning.TaskService.Host
RUN dotnet publish -c Release -o /app/output

FROM microsoft/dotnet:2.1-aspnetcore-runtime AS runtime
COPY --from=build /app/output /app/host
WORKDIR /app/host
ENTRYPOINT ["dotnet", "EnglishLearning.TaskService.Host.dll"]
