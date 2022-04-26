USE VUELOS
GO

-------------------------------------------------------------------------
-- Se valida la existencia del procedimiento almacenado.
-------------------------------------------------------------------------

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dbo'
			AND SPECIFIC_NAME = N'PR_Insertar_Bitacora')
   DROP PROCEDURE dbo.PR_Insertar_Bitacora
GO

CREATE PROCEDURE PR_Insertar_Bitacora
	@pvTipoAccion			INT,
	@pvTabla				VARCHAR(50),
	@pvDescripcion			VARCHAR(1000),
	@pvIdUsuario			VARCHAR(9),
	@piCodigo				SMALLINT		OUTPUT,     
	@pvError				VARCHAR(400)	OUTPUT
AS 
BEGIN

	BEGIN TRY

		SET NOCOUNT ON;
		-- Se inicializa el valor de los parámetros OUTPUT
		SET @piCodigo = 0
		SET @pvError = ''
		
		INSERT INTO Bitacora (date_time, user_name, action_description, detail) 
			VALUES (GETDATE(), @pvIdUsuario, @pvTipoAccion, @pvDescripcion)

	END TRY
	BEGIN CATCH 
		SET @piCodigo = ERROR_NUMBER()    
		SET @pvError = ERROR_MESSAGE()    
	END CATCH

END 