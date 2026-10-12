/* Prueba D – Restricciones en SSMS. Cada bloque debe devolver un error y no cambiar datos. */
USE HeroesClase1;
GO

-- Nombre de ciudad repetido: debe fallar por UQ_Ciudades_Nombre
BEGIN TRY
    BEGIN TRANSACTION;
    INSERT INTO dbo.Ciudades (Nombre, Pais) VALUES (N'La Paz', N'Bolivia');
    ROLLBACK TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    SELECT ERROR_MESSAGE() AS Resultado;
END CATCH;

-- Nivel fuera de rango: debe fallar por CK_Villanos_NivelAmenaza
BEGIN TRY
    BEGIN TRANSACTION;
    UPDATE dbo.Villanos SET Nivel_Amenaza = 9
    WHERE Id = (SELECT MIN(Id) FROM dbo.Villanos);
    ROLLBACK TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    SELECT ERROR_MESSAGE() AS Resultado;
END CATCH;

-- Borrar una ciudad con villanos: debe fallar por FK_Villanos_Ciudades
BEGIN TRY
    BEGIN TRANSACTION;
    DELETE FROM dbo.Ciudades WHERE Nombre = N'La Paz';
    ROLLBACK TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    SELECT ERROR_MESSAGE() AS Resultado;
END CATCH;
GO
