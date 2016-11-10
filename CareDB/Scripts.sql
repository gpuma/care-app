INSERT [dbo].[Usuario] ([Nombre], [Apellido], [Username], [Password], [Tipo], [Cuidante], [Telefono]) VALUES (N'Rocío', N'Puma', N'chio', N'chio', 0, N'vash', N'+51054470160')
GO
INSERT [dbo].[Usuario] ([Nombre], [Apellido], [Username], [Password], [Tipo], [Cuidante], [Telefono]) VALUES (N'Elvis', N'Puma', N'elvis', N'elvis', 0, N'vash', N'+51054470160')
GO
INSERT [dbo].[Usuario] ([Nombre], [Apellido], [Username], [Password], [Tipo], [Cuidante], [Telefono]) VALUES (N'Hernán', N'Puma', N'hernan', N'hernan', 0, N'vash', N'+51054470160')
GO
INSERT [dbo].[Usuario] ([Nombre], [Apellido], [Username], [Password], [Tipo], [Cuidante], [Telefono]) VALUES (N'juanito', N'perez', N'juan', N'juan', 1, NULL, N'12312')
GO
INSERT [dbo].[Usuario] ([Nombre], [Apellido], [Username], [Password], [Tipo], [Cuidante], [Telefono]) VALUES (N'pedrito', N'yepez', N'pedro', N'pdero', 0, N'juan', N'69')
GO
INSERT [dbo].[Usuario] ([Nombre], [Apellido], [Username], [Password], [Tipo], [Cuidante], [Telefono]) VALUES (N'Silvia', N'Tejada', N'silvia', N'silvia', 0, N'vash', N'+51958554460')
GO
INSERT [dbo].[Usuario] ([Nombre], [Apellido], [Username], [Password], [Tipo], [Cuidante], [Telefono]) VALUES (N'hector', N'beltran', N'thor', N'thor', 1, NULL, N'666')
GO
INSERT [dbo].[Usuario] ([Nombre], [Apellido], [Username], [Password], [Tipo], [Cuidante], [Telefono]) VALUES (N'Gustavo', N'Puma', N'vash', N'vash', 1, NULL, N'+51921302769')
GO

select * from TipoEmergencia
delete from TipoEmergencia
insert into TipoEmergencia values(0,'ProximidadPorPeriodo','Emergencia por proximidad')
insert into TipoEmergencia values(1,'ProximidadPorPeriodoAHora','Emergencia por proximidad a hora')
insert into TipoEmergencia values(2,'CruceRapido','Emergencia por cruce rápido')
insert into TipoEmergencia values(3,'CruceIncompleto','Emergencia por cruce incompleto')

insert into Usuario values ('Gustavo','Puma','vash','vash',1,'vash','+51921302769')
insert into Usuario values ('Elvis','Puma','elvis','elvis',0,'vash','+51054470160')
insert into Usuario values ('Rocío','Puma','chio','chio',0,'vash','+51054470160')
insert into Usuario values ('Hernán','Puma','hernan','hernan',0,'vash','+51054470160')
insert into Usuario values ('Silvia','Tejada','silvia','silvia',0,'vash','+51958554460')

select * from Configuracion

insert into Configuracion values (1,'chio',0,28927,null,1,2000,'baño')
insert into Configuracion values (2,'chio',2,40796,53847,1,10000,'escaleras')

declare @p1 int
set @p1=59
exec sp_prepexec @p1 output,N'@P1 nvarchar(19),@P2 int,@P3 nvarchar(3),@P4 int,@P5 int,@P6 int,@P7 int,@P8 nvarchar(6)',N'SELECT TOP 1 [Configuracion].[Id] AS [Configuracion_Id], [Configuracion].[Paciente] AS [Configuracion_Paciente], [Configuracion].[Tipo] AS [Configuracion_Tipo], [Configuracion].[BeaconId1] AS [Configuracion_BeaconId1], [Configuracion].[BeaconId2] AS [Configuracion_BeaconId2], [Configuracion].[Rango] AS [Configuracion_Rango], [Configuracion].[Tiempo] AS [Configuracion_Tiempo], [Configuracion].[Nombre] AS [Configuracion_Nombre], [Configuracion].[Hora] AS [Configuracion_Hora] FROM [Configuracion] WHERE [Configuracion].[Hora] = @P1 AND [Configuracion].[Tiempo] = @P2 AND [Configuracion].[Paciente] = @P3 AND [Configuracion].[Tipo] = @P4 AND [Configuracion].[BeaconId1] = @P5 AND [Configuracion].[BeaconId2] = @P6 AND [Configuracion].[Rango] = @P7 AND [Configuracion].[Nombre] = @P8',N'0001-01-01T00:00:00',10000,N'emi',0,40796,0,1,N'prueba'
select @p1
go

select * from Configuracion
delete from Configuracion
select * from Usuario
update usuario set Password= 'juan' where username='juan'
delete from Usuario
insert into Configuracion values ('emi',0,40796,null,0,10,'prueba', null)
