USE PROYECTO_VUELOS
GO

-------------------------------------------------------------------------
-- Se valida la existencia del procedimiento almacenado.
-------------------------------------------------------------------------

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dbo'
			AND SPECIFIC_NAME = N'PR_Actualizar_Aerolinea')
   DROP PROCEDURE dbo.PR_Actualizar_Aerolinea
GO

CREATE PROCEDURE PR_Actualizar_Aerolinea
	@pvIdAerolinea			VARCHAR(10),
	@pvNombre				VARCHAR(50),
	@pvIdPais				VARCHAR(10),
	@pvUrlImagen			VARCHAR(200),
	@piCodigo				SMALLINT		OUTPUT,     
	@pvError				VARCHAR(400)	OUTPUT
AS 
BEGIN

	BEGIN TRY

		SET NOCOUNT ON;
		-- Se inicializa el valor de los parámetros OUTPUT
		SET @piCodigo = 0
		SET @pvError = ''
		
		UPDATE Aerolineas
		SET name_agency = @pvNombre,
			country_code = @pvIdPais,
			image = @pvUrlImagen
		WHERE airline_code = @pvIdAerolinea

	END TRY
	BEGIN CATCH 
		SET @piCodigo = ERROR_NUMBER()    
		SET @pvError = ERROR_MESSAGE()    
	END CATCH

END 