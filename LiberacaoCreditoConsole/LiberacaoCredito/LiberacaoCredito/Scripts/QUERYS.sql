-- #1
WITH CTE_ClientesSP
AS
(
	SELECT CLI.nmCliente, FIN.idFinanciamento, FIN.dtVencimento,
	CAST((SELECT COUNT(*) FROM TBFinanciamentoParcela WHERE dtPagamento IS NOT NULL AND idFinanciamento = FIN.idFinanciamento) AS FLOAT)/
	(SELECT COUNT(*) FROM TBFinanciamentoParcela where idFinanciamento = FIN.idFinanciamento) * 100 AS PercParcelasPagas
	FROM TBFinanciamento FIN 
	INNER JOIN TBCliente CLI ON CLI.idCliente = FIN.idCliente
	WHERE  CLI.UFCliente = 'SP'
)
SELECT nmCliente FROM CTE_ClientesSP
WHERE PercParcelasPagas > 60
ORDER BY dtVencimento



-- #2
SELECT DISTINCT TOP 4 CLI.nmCliente
FROM TBFinanciamento FIN 
INNER JOIN TBCliente CLI ON CLI.idCliente = FIN.idCliente
INNER JOIN TBFinanciamentoParcela PAR ON PAR.idFinanciamento = FIN.idFinanciamento
WHERE
CONVERT(DATE, PAR.dtVencimento)< DATEADD(day, -5, CONVERT(DATE, GETDATE())) AND PAR.dtPagamento IS NULL

-- #3
SELECT CLI.nmCliente--, FIN.idFinanciamento
FROM TBFinanciamento FIN 
INNER JOIN TBCliente CLI ON CLI.idCliente = FIN.idCliente
WHERE FIN.vlTotal > 10000.00 AND
(	SELECT COUNT(*) 
	FROM TBFinanciamentoParcela 
	WHERE 
	((dtPagamento IS NULL AND DATEDIFF(DAY, dtVencimento, GETDATE()) > 10) OR DATEDIFF(DAY, dtVencimento, dtPagamento) > 10) AND dtVencimento < CONVERT(DATE, GETDATE())
	AND idFinanciamento = FIN.idFinanciamento
	GROUP BY idFinanciamento
) >=2