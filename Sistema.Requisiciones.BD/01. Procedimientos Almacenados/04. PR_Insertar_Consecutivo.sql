USE VUELOS
GO

-------------------------------------------------------------------------
-- Se valida la existencia del procedimiento almacenado.
-------------------------------------------------------------------------

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dbo'
			AND SPECIFIC_NAME = N'PR_Insertar_Consecutivo')
   DROP PROCEDURE dbo.PR_Insertar_Consecutivo
GO

CREATE PROCEDURE PR_Insertar_Consecutivo
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
		
		INSERT INTO Consecutivos (consecutive_id, description, prefix, consecutive_value, inf_range, max_range) 
			VALUES (@pvIdConsecutivo, @pvNombre, @pvPrefijo, @pvValor, @pvRangoInicial, @pvRangoFinal)

	END TRY
	BEGIN CATCH 
		SET @piCodigo = ERROR_NUMBER()    
		SET @pvError = ERROR_MESSAGE()    
	END CATCH

END 