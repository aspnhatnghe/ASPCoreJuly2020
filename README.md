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

# DI - Life cycle
## services.AddSingleton(): 1 instance duy nhất toàn project
## services.AddScoped(): 1 instance tương ứng với scope đang dùng (trong controller có inject service thì các request gọi action sẽ chung 1 instance)

## services.AddTransient() : mỗi request sẽ tạo 1 instance