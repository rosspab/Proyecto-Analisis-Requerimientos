USE VUELOS
GO

-------------------------------------------------------------------------
-- Se valida la existencia del procedimiento almacenado.
-------------------------------------------------------------------------

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dbo'
			AND SPECIFIC_NAME = N'PR_Obtener_Puertas_Aerolineas')
   DROP PROCEDURE dbo.PR_Obtener_Puertas_Aerolineas
GO

CREATE PROCEDURE PR_Obtener_Puertas_Aerolineas
	@pvEstadoPuerta			BIT = NULL,
	@piCodigo				SMALLINT		OUTPUT,     
	@pvError				VARCHAR(400)	OUTPUT
AS 
BEGIN

	BEGIN TRY

		SET NOCOUNT ON;
		-- Se inicializa el valor de los parámetros OUTPUT
		SET @piCodigo = 0
		SET @pvError = ''
		
		SELECT * FROM Puertas_Aeropuerto
		WHERE estado = COALESCE(@pvEstadoPuerta, estado)

	END TRY
	BEGIN CATCH 
		SET @piCodigo = ERROR_NUMBER()    
		SET @pvError = ERROR_MESSAGE()    
	END CATCH

END 