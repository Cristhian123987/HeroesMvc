/* Pruebas SSMS del ejercicio Cuarteles–Vehículos */
USE HeroesClase1;
GO

-- 1) Otra placa 2345-ABC: debe fallar por UQ_Vehiculos_Placa
BEGIN TRY
    BEGIN TRANSACTION;
    INSERT INTO dbo.Vehiculos (Nro_Placa, Modelo, CostoMantenimiento)
    VALUES (N'2345-ABC', N'Duplicado', 100.00);
    ROLLBACK TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    SELECT ERROR_MESSAGE() AS Resultado;
END CATCH;

-- 2) Capacidad 0: debe fallar por CK_Cuarteles_Capacidad
BEGIN TRY
    BEGIN TRANSACTION;
    UPDATE dbo.Cuarteles SET Capacidad = 0
    WHERE Nombre = N'Base Oriente';
    ROLLBACK TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    SELECT ERROR_MESSAGE() AS Resultado;
END CATCH;

-- 3) Borrar Base Illimani con ROLLBACK: se permite y sus vehículos quedan con CuartelId NULL
BEGIN TRANSACTION;
DECLARE @Id INT = (SELECT Id FROM dbo.Cuarteles WHERE Nombre = N'Base Illimani');
DELETE FROM dbo.Cuarteles WHERE Id = @Id;
SELECT Nro_Placa, Modelo, CuartelId FROM dbo.Vehiculos;
ROLLBACK TRANSACTION;

-- Comprobación: después del ROLLBACK todo sigue igual
SELECT Nro_Placa, Modelo, CuartelId FROM dbo.Vehiculos;
GO
