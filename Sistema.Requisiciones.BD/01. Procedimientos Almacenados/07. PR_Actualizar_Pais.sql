USE PROYECTO_VUELOS
GO

-------------------------------------------------------------------------
-- Se valida la existencia del procedimiento almacenado.
-------------------------------------------------------------------------

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dbo'
			AND SPECIFIC_NAME = N' PR_Actualizar_Pais')
   DROP PROCEDURE dbo. PR_Actualizar_Pais
GO

CREATE PROCEDURE  PR_Actualizar_Pais
	@pvIdPais				VARCHAR(10),
	@pvNombre				VARCHAR(50),
	@pvUrlImagen			VARCHAR(200)  = NULL,
	@piCodigo				SMALLINT		OUTPUT,     
	@pvError				VARCHAR(400)	OUTPUT
AS 
BEGIN

	BEGIN TRY

		SET NOCOUNT ON;
		-- Se inicializa el valor de los par�metros OUTPUT
		SET @piCodigo = 0
		SET @pvError = ''
		
		UPDATE Paises
		SET country_name = @pvNombre,
			image = @pvUrlImagen
		WHERE country_code = @pvIdPais

	END TRY
	BEGIN CATCH 
		SET @piCodigo = ERROR_NUMBER()    
		SET @pvError = ERROR_MESSAGE()    
	END CATCH

END 