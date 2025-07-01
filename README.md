# HydroOffice

**HydroOffice** — это десктопное WPF-приложение для учета заказов, сотрудников и контрагентов. Проект построен с использованием **.NET 8**, **WPF**, **NHibernate**, и **MySQL**.

---

## ?? Архитектура

Проект реализован с разделением по слоям:

- **Views** — XAML-интерфейсы (UserControl для каждой сущности)
- **ViewModels** — MVVM-логика, команды, привязки, валидация и загрузка данных
- **Models** — ORM-модели NHibernate
- **Maps** — Fluent NHibernate mappings
- **Repositories** — generic-реализация CRUD-репозиториев
- **DatabaseSessionHelper** — конфигурация NHibernate с автоматическим обновлением схемы

---

## ?? Зависимости

- [.NET 8](https://dotnet.microsoft.com/)
- [Fluent NHibernate](https://github.com/FluentNHibernate/fluent-nhibernate)
- [MySQL.Data](https://www.nuget.org/packages/MySql.Data)
- [DotNetEnv](https://www.nuget.org/packages/DotNetEnv)

---

## ??? Настройка

1. Установите MySQL/MariaDB сервер и создайте базу данных `hydrooffice`.

2. В корне проекта создайте `.env` файл:

   ```env
   DB_HOST=localhost
   DB_NAME=hydrooffice
   DB_USER=root
   DB_PASS=your_password
