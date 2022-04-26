USE VUELOS
GO

-------------------------------------------------------------------------
-- Se valida la existencia del procedimiento almacenado.
-------------------------------------------------------------------------

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dbo'
			AND SPECIFIC_NAME = N'PR_Actualizar_Puerta_Aerolinea')
   DROP PROCEDURE dbo.PR_Actualizar_Puerta_Aerolinea
GO

CREATE PROCEDURE PR_Actualizar_Puerta_Aerolinea
	@pvIdPuerta				VARCHAR(10),
	@pvNumeroPuerta			INT,
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
		
		UPDATE Puertas_Aeropuerto
		SET number = @pvNumeroPuerta,
			estado = @pvDisponiblidad
		WHERE door_code = @pvIdPuerta

	END TRY
	BEGIN CATCH 
		SET @piCodigo = ERROR_NUMBER()    
		SET @pvError = ERROR_MESSAGE()    
	END CATCH

END 