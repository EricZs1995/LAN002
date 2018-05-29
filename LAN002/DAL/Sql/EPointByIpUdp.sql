SELECT 
	(CASE WHEN t1.Address IS NOT NULL THEN t1.Address ELSE t2.Address END) AS Address,
	(CASE WHEN t1.Port IS NOT NULL THEN t1.Port ELSE t2.Port END) AS Port,
	(CASE WHEN (t1.Address IS NOT NULL AND t2.Address IS NOT NULL) THEN t1.Packets+t2.Packets 
		WHEN t1.Address IS NOT NULL THEN t1.Packets
		ELSE t2.Packets END) AS Packets,
	(CASE WHEN (t1.Address IS NOT NULL AND t2.Address IS NOT NULL) THEN t1.Bytes+t2.Bytes 
		WHEN t1.Address IS NOT NULL THEN t1.Bytes
		ELSE t2.Bytes END) AS Bytes,
	(CASE WHEN t1.Address IS NOT NULL THEN t1.Packets ELSE 0 END) AS TxPackets,
	(CASE WHEN t1.Address IS NOT NULL THEN t1.Bytes ELSE 0 END) AS TxBytes,
	(CASE WHEN t2.Address IS NOT NULL THEN t2.Packets ELSE 0 END) AS RxPackets,
	(CASE WHEN t2.Address IS NOT NULL THEN t2.Bytes ELSE 0 END) AS RxBytes
FROM
(	
	SELECT 
		[Source] AS Address,
		[SourcePort] AS Port,
		COUNT(*) AS Packets,
		SUM([Framelentgh]) AS Bytes
	FROM dbo.pcap_comp
	WHERE [Protocol] = 'UDP'
	GROUP BY [Source],[SourcePort]
)AS t1 FULL JOIN(
	SELECT 
		[Dest] AS Address,
		[DestPort] AS Port,
		COUNT(*) AS Packets,
		SUM([Framelentgh]) AS Bytes
	FROM dbo.pcap_comp
	WHERE [Protocol] = 'UDP'
	GROUP BY [Dest],[DestPort]
)AS t2 ON t1.Address = t2.Address AND t1.Port = t2.Port

