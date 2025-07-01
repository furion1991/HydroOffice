# HydroOffice

**HydroOffice** Ч это десктопное WPF-приложение дл€ учета заказов, сотрудников и контрагентов. ѕроект построен с использованием **.NET 8**, **WPF**, **NHibernate**, и **MySQL**.

---

## ?? јрхитектура

ѕроект реализован с разделением по сло€м:

- **Views** Ч XAML-интерфейсы (UserControl дл€ каждой сущности)
- **ViewModels** Ч MVVM-логика, команды, прив€зки, валидаци€ и загрузка данных
- **Models** Ч ORM-модели NHibernate
- **Maps** Ч Fluent NHibernate mappings
- **Repositories** Ч generic-реализаци€ CRUD-репозиториев
- **DatabaseSessionHelper** Ч конфигураци€ NHibernate с автоматическим обновлением схемы

---

## ?? «ависимости

- [.NET 8](https://dotnet.microsoft.com/)
- [Fluent NHibernate](https://github.com/FluentNHibernate/fluent-nhibernate)
- [MySQL.Data](https://www.nuget.org/packages/MySql.Data)
- [DotNetEnv](https://www.nuget.org/packages/DotNetEnv)

---

## ??? Ќастройка

1. ”становите MySQL сервер и создайте базу данных `hydrooffice`.

2. ¬ корне проекта создайте `.env` файл:

   ```env
   DB_HOST=localhost
   DB_NAME=hydrooffice
   DB_USER=root
   DB_PASS=your_password
