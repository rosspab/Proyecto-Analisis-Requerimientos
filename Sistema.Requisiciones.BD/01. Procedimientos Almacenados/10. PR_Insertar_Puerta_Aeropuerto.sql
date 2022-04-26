USE VUELOS
GO

-------------------------------------------------------------------------
-- Se valida la existencia del procedimiento almacenado.
-------------------------------------------------------------------------

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dbo'
			AND SPECIFIC_NAME = N'PR_Insertar_Puerta_Aeropuerto')
   DROP PROCEDURE dbo.PR_Insertar_Puerta_Aeropuerto
GO

CREATE PROCEDURE PR_Insertar_Puerta_Aeropuerto
	@pvIdPuerta				VARCHAR(10),
	@pvNumeroPuerta			INT,
	@pvDetalle				VARCHAR(100),
	@pvDisponiblidad		BIT,
	@piCodigo				SMALLINT		OUTPUT,     
	@pvError				VARCHAR(400)	OUTPUT
AS 
BEGIN

	BEGIN TRY

		SET NOCOUNT ON;
		-- Se inicializa el valor de los parámetros OUTPUT
		SET @piCodigo = 0
		SET @pvError = ''
		
		INSERT INTO Puertas_Aeropuerto VALUES (@pvIdPuerta, @pvNumeroPuerta, @pvDetalle, @pvDisponiblidad)

	END TRY
	BEGIN CATCH 
		SET @piCodigo = ERROR_NUMBER()    
		SET @pvError = ERROR_MESSAGE()    
	END CATCH

END 