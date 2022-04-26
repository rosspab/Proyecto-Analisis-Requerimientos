USE VUELOS
GO

-------------------------------------------------------------------------
-- Se valida la existencia del procedimiento almacenado.
-------------------------------------------------------------------------

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dbo'
			AND SPECIFIC_NAME = N'PR_Obtener_Usuario')
   DROP PROCEDURE dbo.PR_Obtener_Usuario
GO

CREATE PROCEDURE PR_Obtener_Usuario
	@pvIdUsuario			VARCHAR(50) = NULL,
	@pvContrasenia			VARCHAR(50) = NULL,
	@piCodigo				SMALLINT		OUTPUT,     
	@pvError				VARCHAR(400)	OUTPUT
AS 
BEGIN

	BEGIN TRY

		SET NOCOUNT ON;
		-- Se inicializa el valor de los parámetros OUTPUT
		SET @piCodigo = 0
		SET @pvError = ''
		
		SELECT * FROM Usuarios 
		WHERE user_name = COALESCE(@pvIdUsuario, user_name)
		AND password = COALESCE(@pvContrasenia, password)

	END TRY
	BEGIN CATCH 
		SET @piCodigo = ERROR_NUMBER()    
		SET @pvError = ERROR_MESSAGE()    
	END CATCH

END 