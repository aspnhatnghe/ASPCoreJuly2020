# ASPCoreJuly2020

# Khai giảng 11/07/2020

# ORM Drapper
* Cài Drapper
## Select
* Lấy nhiều:
var allEvents = connection.Query<Loai>("SELECT MaLoai, TenLoai FROM Event");
connection.Query<T>(sql [,parameters])

* Insert/Update/Delete
connection.Execute(sql [,parameters])