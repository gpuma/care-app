CREATE TABLE [dbo].[Usuario]
(
	[Id] INT NOT NULL , 
	[Nombre] NVARCHAR(50) NOT NULL, 
	[Apellido] NVARCHAR(50) NOT NULL, 
	[Username] NVARCHAR(50) NOT NULL, 
	[Password] NVARCHAR(128) NOT NULL, 
	[Tipo] BIT NOT NULL, 
	[Cuidante] NVARCHAR(50) NULL, 
	PRIMARY KEY ([Username]), 
	CONSTRAINT [FK_Usuario_Usuario_Cuidante] FOREIGN KEY ([Cuidante]) REFERENCES [Usuario]([Username])
)
