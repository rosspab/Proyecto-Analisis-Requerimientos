USE VUELOS
GO

-------------------------------------------------------------------------
-- Se valida la existencia del procedimiento almacenado.
-------------------------------------------------------------------------

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dbo'
			AND SPECIFIC_NAME = N'PR_Cambiar_Contrasenia')
   DROP PROCEDURE dbo.PR_Cambiar_Contrasenia
GO

CREATE PROCEDURE PR_Cambiar_Contrasenia
	@pvUsuario				VARCHAR(9),
	@pvContrasenia			VARCHAR(50),
	@piCodigo				SMALLINT		OUTPUT,     
	@pvError				VARCHAR(400)	OUTPUT
AS 
BEGIN

	BEGIN TRY

		SET NOCOUNT ON;
		-- Se inicializa el valor de los parámetros OUTPUT
		SET @piCodigo = 0
		SET @pvError = ''
		
		UPDATE Usuarios 
		SET password = @pvContrasenia
		WHERE user_name = @pvUsuario

	END TRY
	BEGIN CATCH 
		SET @piCodigo = ERROR_NUMBER()    
		SET @pvError = ERROR_MESSAGE()    
	END CATCH

END 