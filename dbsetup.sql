create table Department(
DepartmentId int identity(1,1),
Department nvarchar(500)
)

insert into Department values('IT')
insert into Department values('Support')

select * from Department

create table Employee(
EmployeeId int identity(1,1),
EmployeeName nvarchar(500),
Department nvarchar(500),
DateOfJoining datetime,
PhotoFileName nvarchar(500)
)

insert into Employee values('Bob', 'IT', GETDATE(), 'anonymous.png')

select * from Employee

select EmployeeId, EmployeeName, Department, CONVERT(varchar,DateOfJoining,105) as DateOfJoining, PhotoFileName from employee