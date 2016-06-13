CREATE TABLE [dbo].[Emergencia]
(
	[Id] INT NOT NULL PRIMARY KEY, 
	[Tipo] INT NOT NULL, 
	[Timestamp] DATETIME NOT NULL, 
	[Estado] INT NOT NULL, 
	[Paciente] NVARCHAR(50) NOT NULL, 
	CONSTRAINT [FK_Emergencia_EstadoEmergencia_Estado] FOREIGN KEY ([Estado]) REFERENCES [EstadoEmergencia]([Id]), 
	CONSTRAINT [FK_Emergencia_TipoEmergencia_Tipo] FOREIGN KEY ([Tipo]) REFERENCES [TipoEmergencia]([Id]), 
	CONSTRAINT [FK_Emergencia_Usuario_Paciente] FOREIGN KEY ([Paciente]) REFERENCES [Usuario]([Username])
)
