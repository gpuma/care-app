CREATE TABLE [dbo].[Configuracion]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[Paciente] NVARCHAR(50) NOT NULL, 
	[Tipo] INT NOT NULL, 
	[BeaconId1] INT NOT NULL, 
	[BeaconId2] INT NOT NULL, 
	[Rango] INT NOT NULL, 
	[Tiempo] INT NOT NULL, 
	[Nombre] NVARCHAR(50) NOT NULL, 
	CONSTRAINT [FK_Configuracion_Usuario_Paciente] FOREIGN KEY ([Paciente]) REFERENCES [Usuario]([Username]), 
	CONSTRAINT [FK_Configuracion_TipoEmergencia_Tipo] FOREIGN KEY ([Tipo]) REFERENCES [TipoEmergencia]([Id])
)
