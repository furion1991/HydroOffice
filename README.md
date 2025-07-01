# HydroOffice

**HydroOffice** � ��� ���������� WPF-���������� ��� ����� �������, ����������� � ������������. ������ �������� � �������������� **.NET 8**, **WPF**, **NHibernate**, � **MySQL**.

---

## ?? �����������

������ ���������� � ����������� �� �����:

- **Views** � XAML-���������� (UserControl ��� ������ ��������)
- **ViewModels** � MVVM-������, �������, ��������, ��������� � �������� ������
- **Models** � ORM-������ NHibernate
- **Maps** � Fluent NHibernate mappings
- **Repositories** � generic-���������� CRUD-������������
- **DatabaseSessionHelper** � ������������ NHibernate � �������������� ����������� �����

---

## ?? �����������

- [.NET 8](https://dotnet.microsoft.com/)
- [Fluent NHibernate](https://github.com/FluentNHibernate/fluent-nhibernate)
- [MySQL.Data](https://www.nuget.org/packages/MySql.Data)
- [DotNetEnv](https://www.nuget.org/packages/DotNetEnv)

---

## ??? ���������

1. ���������� MySQL ������ � �������� ���� ������ `hydrooffice`.

2. � ����� ������� �������� `.env` ����:

   ```env
   DB_HOST=localhost
   DB_NAME=hydrooffice
   DB_USER=root
   DB_PASS=your_password
