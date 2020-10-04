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
ALTER TABLE dbo.TBCliente SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.TBFinanciamento
	(
	idFinanciamento uniqueidentifier NOT NULL,
	idCliente uniqueidentifier NOT NULL,
	tpFinanciamento char(5) NOT NULL,
	vlTotal decimal(18, 3) NOT NULL,
	dtVencimento datetime NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.TBFinanciamento ADD CONSTRAINT
	DF_TBFinanciamento_idFinanciamento DEFAULT newid() FOR idFinanciamento
GO
ALTER TABLE dbo.TBFinanciamento ADD CONSTRAINT
	PK_TBFinanciamento PRIMARY KEY CLUSTERED 
	(
	idFinanciamento
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.TBFinanciamento ADD CONSTRAINT
	FK_TBFinanciamento_TBCliente FOREIGN KEY
	(
	idCliente
	) REFERENCES dbo.TBCliente
	(
	idCliente
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.TBFinanciamento SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
GO