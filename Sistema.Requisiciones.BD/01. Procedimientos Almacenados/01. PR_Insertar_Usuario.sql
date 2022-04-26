USE VUELOS
GO

-------------------------------------------------------------------------
-- Se valida la existencia del procedimiento almacenado.
-------------------------------------------------------------------------

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dbo'
			AND SPECIFIC_NAME = N'PR_Insertar_Usuario')
   DROP PROCEDURE dbo.PR_Insertar_Usuario
GO

CREATE PROCEDURE PR_Insertar_Usuario 
	@pvuser_name			VARCHAR(9),
	@pvIDRol				VARCHAR(50),
	@pvpassword		VARCHAR(50),
	@pvemail		VARCHAR(50),
	@pvsecurity_question				VARCHAR(150),
	@pvsecurity_answer			VARCHAR(15), 
	@piCodigo				SMALLINT		OUTPUT,     
	@pvError				VARCHAR(400)	OUTPUT
AS 
BEGIN

	BEGIN TRY

		SET NOCOUNT ON;
		-- Se inicializa el valor de los parámetros OUTPUT
		SET @piCodigo = 0
		SET @pvError = ''
		
		INSERT INTO USUARIOS (user_name, IDRol, password, email, security_question, security_answer) 
			VALUES (@pvuser_name, @pvIDROL, @pvpassword, @pvemail, @pvsecurity_question, @pvsecurity_answer)

	END TRY
	BEGIN CATCH 
		SET @piCodigo = ERROR_NUMBER()    
		SET @pvError = ERROR_MESSAGE()    
	END CATCH

END 