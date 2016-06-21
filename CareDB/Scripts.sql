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

insert into TipoEmergencia values('ProximidadPorPeriodo','Emergencia por proximidad')
insert into TipoEmergencia values('ProximidadPorPeriodoAHora','Emergencia por proximidad a hora')
insert into TipoEmergencia values('CruceRapido','Emergencia por cruce rápido')
insert into TipoEmergencia values('CruceIncompleto','Emergencia por cruce incompleto')

insert into Usuario values ('Gustavo','Puma','vash','vash',1,'vash','+51921302769')
insert into Usuario values ('Elvis','Puma','elvis','elvis',0,'vash','+51054470160')
insert into Usuario values ('Rocío','Puma','chio','chio',0,'vash','+51054470160')
insert into Usuario values ('Hernán','Puma','hernan','hernan',0,'vash','+51054470160')
insert into Usuario values ('Silvia','Tejada','silvia','silvia',0,'vash','+51958554460')

select * from Usuario