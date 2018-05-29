SELECT
	AddressA,
	PortA,
	AddressB,
	PortB,
	Packets,
	( CASE WHEN Duration!=0 THEN CAST (Packets/Duration AS DECIMAL(18,3)) ELSE 0 END) AS pps,
	Bytes,
	( CASE WHEN Duration!=0 THEN CAST (Bytes*8/Duration AS DECIMAL(18,3)) ELSE 0 END) AS bps,
	PacketAtB,
	( CASE WHEN Duration!=0 THEN CAST (PacketAtB/Duration AS DECIMAL(18,3)) ELSE 0 END) AS AtBpps,
	BytesAtB,
	( CASE WHEN Duration!=0 THEN CAST (BytesAtB*8/Duration AS DECIMAL(18,3)) ELSE 0 END) AS AtBbbs,
	PacketBtA ,
	( CASE WHEN Duration!=0 THEN CAST (PacketBtA/Duration AS DECIMAL(18,3)) ELSE 0 END) AS BtApps,
	BytesBtA,
	( CASE WHEN Duration!=0 THEN CAST (BytesBtA*8/Duration AS DECIMAL(18,3)) ELSE 0 END) AS BtAbbs,
	CAST (RelStart AS DECIMAL(18,3)) AS RelStart,
	CAST (Duration AS DECIMAL(18,3)) AS Duration
FROM(
	SELECT 
		SourceIp AS AddressA,
		SourcePort AS PortA,
		DestinationIp AS AddressB,
		DestinationPort AS PortB,
		COUNT(*) AS Packets,
		SUM(FrameLen) AS Bytes,
		COUNT( CASE WHEN CompIp=0 THEN 1 ELSE NULL END)AS PacketAtB ,
		SUM( CASE WHEN CompIp=0 THEN FrameLen ELSE 0 END) AS BytesAtB,
		COUNT( CASE WHEN CompIp=1 THEN 1 ELSE NULL END)AS PacketBtA ,
		SUM( CASE WHEN CompIp=1 THEN FrameLen ELSE 0 END) AS BytesBtA,
		MIN(Seconds) AS RELStart,
		(MAX(Seconds)-MIN(Seconds)) AS Duration
	FROM dbo.stat
	WHERE TranType = 'TCP'
	GROUP BY SourceIp,DestinationIp,SourcePort,DestinationPort
	) AS t