/* =====================================================================
   Clase 02 – Ciudades y Villanos (sin migraciones: las tablas se crean por SQL)
   Base: HeroesClase1
   ===================================================================== */
IF DB_ID(N'HeroesClase1') IS NULL
    CREATE DATABASE HeroesClase1;
GO
USE HeroesClase1;
GO

IF OBJECT_ID(N'dbo.Ciudades', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Ciudades (
        Id      INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Ciudades PRIMARY KEY,
        Nombre  NVARCHAR(100)     NOT NULL,
        Pais    NVARCHAR(60)      NOT NULL,
        CONSTRAINT UQ_Ciudades_Nombre UNIQUE (Nombre)
    );
END
GO

IF OBJECT_ID(N'dbo.Villanos', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Villanos (
        Id             INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Villanos PRIMARY KEY,
        Alias          NVARCHAR(100)     NOT NULL,
        Nivel_Amenaza  INT               NOT NULL,
        Recompensa     DECIMAL(10,2)     NOT NULL,
        FechaRegistro  DATETIME2         NOT NULL
            CONSTRAINT DF_Villanos_FechaRegistro DEFAULT (GETDATE()),
        CiudadId       INT               NOT NULL,
        CONSTRAINT CK_Villanos_NivelAmenaza CHECK (Nivel_Amenaza BETWEEN 1 AND 5),
        CONSTRAINT FK_Villanos_Ciudades FOREIGN KEY (CiudadId)
            REFERENCES dbo.Ciudades(Id) ON DELETE NO ACTION
    );
END
GO

/* Datos de prueba: La Paz con dos villanos, Santa Cruz sin villanos */
IF NOT EXISTS (SELECT 1 FROM dbo.Ciudades)
BEGIN
    INSERT INTO dbo.Ciudades (Nombre, Pais)
    VALUES (N'La Paz', N'Bolivia'),
           (N'Santa Cruz', N'Bolivia');

    DECLARE @LaPaz INT = (SELECT Id FROM dbo.Ciudades WHERE Nombre = N'La Paz');

    -- FechaRegistro no se envía: la pone GETDATE()
    INSERT INTO dbo.Villanos (Alias, Nivel_Amenaza, Recompensa, CiudadId)
    VALUES (N'Sombra Roja',   4, 15000.50, @LaPaz),
           (N'Doctor Niebla', 3,  8200.00, @LaPaz);
END
GO

SELECT * FROM dbo.Ciudades;
SELECT * FROM dbo.Villanos;
GO
