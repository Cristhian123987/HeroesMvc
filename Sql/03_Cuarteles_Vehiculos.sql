/* Ejercicio – Cuarteles y Vehículos (script del docente, con guardas para re-ejecutar) */
USE HeroesClase1;
GO
IF OBJECT_ID(N'dbo.Cuarteles', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Cuarteles (
        Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        Nombre NVARCHAR(80) NOT NULL,
        Direccion NVARCHAR(150) NOT NULL,
        Capacidad INT NOT NULL,
        CONSTRAINT UQ_Cuarteles_Nombre UNIQUE (Nombre),
        CONSTRAINT CK_Cuarteles_Capacidad CHECK (Capacidad > 0)
    );
END
GO
IF OBJECT_ID(N'dbo.Vehiculos', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Vehiculos (
        Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        Nro_Placa NVARCHAR(10) NOT NULL,
        Modelo NVARCHAR(60) NOT NULL,
        CostoMantenimiento DECIMAL(12,2) NOT NULL,
        FechaAlta DATETIME2 NOT NULL
            CONSTRAINT DF_Vehiculos_FechaAlta DEFAULT (GETDATE()),
        CuartelId INT NULL,
        CONSTRAINT UQ_Vehiculos_Placa UNIQUE (Nro_Placa),
        CONSTRAINT FK_Vehiculos_Cuarteles FOREIGN KEY (CuartelId)
            REFERENCES dbo.Cuarteles(Id) ON DELETE SET NULL
    );
END
GO
IF NOT EXISTS (SELECT 1 FROM dbo.Cuarteles)
BEGIN
    INSERT INTO dbo.Cuarteles (Nombre, Direccion, Capacidad)
    VALUES (N'Base Illimani', N'Av. Arce 123, La Paz', 20),
           (N'Base Oriente', N'Calle Sucre 45, Santa Cruz', 12);

    DECLARE @Illimani INT = (SELECT Id FROM dbo.Cuarteles WHERE Nombre = N'Base Illimani');
    INSERT INTO dbo.Vehiculos (Nro_Placa, Modelo, CostoMantenimiento, CuartelId)
    VALUES (N'2345-ABC', N'Halcón X1', 8500.00, @Illimani),
           (N'9876-XYZ', N'Rayo T2', 4200.75, @Illimani),
           (N'1111-DEF', N'Tortuga M3', 1500.00, NULL);
END
GO
SELECT * FROM dbo.Cuarteles;
SELECT * FROM dbo.Vehiculos;
GO
