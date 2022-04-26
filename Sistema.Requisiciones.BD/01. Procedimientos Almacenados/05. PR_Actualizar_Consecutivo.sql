USE VUELOS
GO

-------------------------------------------------------------------------
-- Se valida la existencia del procedimiento almacenado.
-------------------------------------------------------------------------

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dbo'
			AND SPECIFIC_NAME = N'PR_Actualizar_Consecutivo')
   DROP PROCEDURE dbo.PR_Actualizar_Consecutivo
GO

CREATE PROCEDURE PR_Actualizar_Consecutivo
	@pvIdConsecutivo		INT,
	@pvNombre				VARCHAR(100),
	@pvPrefijo				VARCHAR(10),
	@pvValor				INT,
	@pvRangoInicial			INT, 
	@pvRangoFinal			INT, 
	@piCodigo				SMALLINT		OUTPUT,     
	@pvError				VARCHAR(400)	OUTPUT
AS 
BEGIN

	BEGIN TRY

		SET NOCOUNT ON;
		-- Se inicializa el valor de los parámetros OUTPUT
		SET @piCodigo = 0
		SET @pvError = ''
		
		UPDATE Consecutivos
		SET description = @pvNombre,
			prefix = @pvPrefijo,
			consecutive_value = @pvValor,
			inf_range = @pvRangoInicial,
			max_range = @pvRangoFinal
		WHERE consecutive_id = @pvIdConsecutivo

	END TRY
	BEGIN CATCH 
		SET @piCodigo = ERROR_NUMBER()    
		SET @pvError = ERROR_MESSAGE()    
	END CATCH

END 