# Chat API

Backend для приложения обмена сообщениями на **ASP.NET Core (.NET 10)**.

## Технологии

- ASP.NET Core, EF Core, MediatR
- PostgreSQL
- SignalR
- Docker, Docker Compose
- OpenTelemetry, Prometheus, Grafana
- Loki, Jaeger

## Возможности

- Регистрация и авторизация пользователей
- Создание и получение чатов
- Управление участниками чатов
- Отправка и получение сообщений
- Мониторинг приложения: метрики, логи и трассировки

## Запуск через Docker

Из корневой директории проекта:

```bash
docker compose up -d --build
```

Команда соберёт образ Chat.Api и запустит сервисы, указанные в `docker-compose.yml`.

Остановка:

```bash
docker compose down
```

## Локальный запуск

Инфраструктуру можно запустить через Docker:

```bash
docker compose up -d
```

Само API - из исходного кода:

```bash
dotnet run --project Chat.Api
```

При локальном запуске необходимо настроить строки подключения в `appsettings.Development.json` или через **User Secrets**.

Пример настройки через User Secrets:

```bash
cd Chat.Api

dotnet user-secrets set "ConnectionStrings:PgConnection" "Host=localhost;Port=5432;Database=chat;Username=postgres;Password=YOUR_PASSWORD"

dotnet user-secrets set "ConnectionStrings:Logs" "http://localhost:3100"

dotnet user-secrets set "ConnectionStrings:Tracing" "http://localhost:4317"
```

Названия ключей должны совпадать с теми, которые используются в конфигурации приложения.

**Важно:** при запуске API на компьютере используются адреса `localhost`, а при запуске внутри Docker Compose — имена соответствующих сервисов (`postgres`, `loki`, `jaeger` и т. д.).

## Миграции БД

Применить миграции EF Core:

```bash
dotnet ef database update -p Chat.Infrastructure -s Chat.Api
```

## Мониторинг

| Сервис | Адрес |
|---|---|
| Chat.Api (локально) | http://localhost:5256 |
| Grafana | http://localhost:3000 |
| Prometheus | http://localhost:9090 |
| Loki | http://localhost:3100 |
| Jaeger | http://localhost:16686 |

Для Loki используется Serilog, а метрики и трассировки собираются через OpenTelemetry.