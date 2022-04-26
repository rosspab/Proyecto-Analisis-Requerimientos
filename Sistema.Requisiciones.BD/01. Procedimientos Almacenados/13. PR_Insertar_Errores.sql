USE VUELOS
GO

-------------------------------------------------------------------------
-- Se valida la existencia del procedimiento almacenado.
-------------------------------------------------------------------------

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dbo'
			AND SPECIFIC_NAME = N'PR_Insertar_Errores')
   DROP PROCEDURE dbo.PR_Insertar_Errores
GO

CREATE PROCEDURE PR_Insertar_Errores
	@pvDescripcionError		VARCHAR(1000),
	@pvNumero				VARCHAR(100),
	@piCodigo				SMALLINT		OUTPUT,     
	@pvError				VARCHAR(400)	OUTPUT
AS 
BEGIN

	BEGIN TRY

		SET NOCOUNT ON;
		-- Se inicializa el valor de los parámetros OUTPUT
		SET @piCodigo = 0
		SET @pvError = ''
		
		INSERT INTO Errores (date_time, number, error_message_detail)
			VALUES (GETDATE(), @pvNumero, @pvDescripcionError)

	END TRY
	BEGIN CATCH 
		SET @piCodigo = ERROR_NUMBER()    
		SET @pvError = ERROR_MESSAGE()    
	END CATCH

END 