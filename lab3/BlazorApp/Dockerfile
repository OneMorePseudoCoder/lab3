FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
WORKDIR /app

COPY . ./
RUN dotnet publish ./BlazorApp.csproj -c Relese -o output

FROM nginx:alpine
WORKDIR /usr/share/nginx/html
COPY --from=build-env /app/output/wwwroot .

COPY nginx.conf /etc/nginx/nginx.conf

EXPOSE 80