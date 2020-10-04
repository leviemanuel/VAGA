/* Para impedir possíveis problemas de perda de dados, analise este script detalhadamente antes de executá-lo fora do contexto do designer de banco de dados.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.TBFinanciamento SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.TBFinanciamentoParcela
	(
	idFinanciamentoParcela uniqueidentifier NOT NULL,
	idFinanciamento uniqueidentifier NOT NULL,
	numParcela int NOT NULL,
	vlParcela decimal(18, 3) NOT NULL,
	dtVencimento datetime NOT NULL,
	dtPagamento datetime NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.TBFinanciamentoParcela ADD CONSTRAINT
	DF_TBFinanciamentoParcela_idFinanciamentoParcela DEFAULT newid() FOR idFinanciamento
GO
ALTER TABLE dbo.TBFinanciamentoParcela ADD CONSTRAINT
	PK_TBFinanciamentoParcela PRIMARY KEY CLUSTERED 
	(
	idFinanciamentoParcela
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.TBFinanciamentoParcela ADD CONSTRAINT
	FK_TBFinanciamentoParcela_TBFinanciamento FOREIGN KEY
	(
	idFinanciamento
	) REFERENCES dbo.TBFinanciamento
	(
	idFinanciamento
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.TBFinanciamentoParcela SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
GO