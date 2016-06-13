CREATE TABLE [dbo].[Usuario]
(
	[Id] INT NOT NULL IDENTITY, 
	[Nombre] NVARCHAR(50) NOT NULL, 
	[Apellido] NVARCHAR(50) NOT NULL, 
	[Username] NVARCHAR(50) NOT NULL, 
	[Password] NVARCHAR(128) NOT NULL, 
	[Tipo] BIT NOT NULL, 
	[Cuidante] NVARCHAR(50) NULL, 
	[Telefono] NVARCHAR(20) NOT NULL, 
	PRIMARY KEY ([Username]), 
	CONSTRAINT [FK_Usuario_Usuario_Cuidante] FOREIGN KEY ([Cuidante]) REFERENCES [Usuario]([Username])
)
