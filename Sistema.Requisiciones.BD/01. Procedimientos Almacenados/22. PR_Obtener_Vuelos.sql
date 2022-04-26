USE VUELOS
GO

-------------------------------------------------------------------------
-- Se valida la existencia del procedimiento almacenado.
-------------------------------------------------------------------------

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dbo'
			AND SPECIFIC_NAME = N'Insertar_Reservacion')
   DROP PROCEDURE dbo.Insertar_Reservacion
GO

CREATE PROCEDURE Insertar_Reservacion
	@BookingId				VARCHAR(36),
	@vueloId				VARCHAR(10),
	@Username				VARCHAR(50),
	@Quantity				SMALLINT,
	@isReservation			BIT,
	@cardId					INT,
	@piCodigo				SMALLINT		OUTPUT,     
	@pvError				VARCHAR(400)	OUTPUT
AS 
BEGIN

	BEGIN TRY

		SET NOCOUNT ON;
		-- Se inicializa el valor de los parámetros OUTPUT
		SET @piCodigo = 0
		SET @pvError = ''
		
		INSERT INTO Compra VALUES (@BookingId, @vueloId, @Username, @Quantity, @isReservation, @cardId)

	END TRY
	BEGIN CATCH 
		SET @piCodigo = ERROR_NUMBER()    
		SET @pvError = ERROR_MESSAGE()    
	END CATCH

END 