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
CREATE TABLE dbo.TBCliente
	(
	idCliente uniqueidentifier NOT NULL,
	nmCliente varchar(100) NOT NULL,
	UFCliente char(2) NOT NULL,
	numCelular nchar(10) NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.TBCliente ADD CONSTRAINT
	DF_TBCliente_idCliente DEFAULT (newid()) FOR idCliente
GO
ALTER TABLE dbo.TBCliente ADD CONSTRAINT
	PK_TBCliente PRIMARY KEY CLUSTERED 
	(
	idCliente
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.TBCliente SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
GO