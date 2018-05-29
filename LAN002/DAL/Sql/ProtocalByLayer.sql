SELECT 
	COUNT(*) AS LinkNum,
	SUM([FrameLen]) AS FrameSum,
	SUM([LinkHeadLen]) AS LinkHeadSum , 

	COUNT(CASE WHEN [InterProType] = 'IpV4' THEN 1 ELSE NULL END) AS IpV4Num,
	SUM(CASE WHEN [InterProType] = 'IpV4' THEN [IpHeadLen] ELSE 0 END) AS IpV4HSum,
	SUM(CASE WHEN [InterProType] = 'IpV4' THEN [IpTotalLen] ELSE 0 END) AS IpV4TSum,

	COUNT(CASE WHEN ([InterProType] = 'IpV4' AND [TranType] = 'TCP') THEN 1 ELSE NULL END) AS IpV4TCPNum,
	SUM(CASE WHEN ([InterProType] = 'IpV4' AND [TranType] = 'TCP') THEN [IpHeadLen] ELSE 0 END) AS IpV4TCPHSum,
	SUM(CASE WHEN ([InterProType] = 'IpV4' AND [TranType] = 'TCP') THEN [IpTotalLen] ELSE 0 END) AS IpV4TCPTSum,

	COUNT(CASE WHEN ([InterProType] = 'IpV4' AND [TranType] = 'UDP') THEN 1 ELSE NULL END) AS IpV4UDPNum,
	SUM(CASE WHEN ([InterProType] = 'IpV4' AND [TranType] = 'UDP') THEN [IpHeadLen] ELSE 0 END) AS IpV4UDPHSum,
	SUM(CASE WHEN ([InterProType] = 'IpV4' AND [TranType] = 'UDP') THEN [IpTotalLen] ELSE 0 END) AS IpV4UDPTSum,

	COUNT(CASE WHEN [InterProType] = 'IpV6' THEN 1 ELSE NULL END) AS IpV6Num,
	SUM(CASE WHEN [InterProType] = 'IpV6' THEN [IpHeadLen] ELSE 0 END) AS IpV6HSum,
	SUM(CASE WHEN [InterProType] = 'IpV6' THEN [IpTotalLen] ELSE 0 END) AS IpV6TSum,

	COUNT(CASE WHEN ([InterProType] = 'IpV6' AND [TranType] = 'TCP') THEN 1 ELSE NULL END) AS IpV6TCPNum,
	SUM(CASE WHEN ([InterProType] = 'IpV6' AND [TranType] = 'TCP') THEN [IpHeadLen] ELSE 0 END) AS IpV6TCPHSum,
	SUM(CASE WHEN ([InterProType] = 'IpV6' AND [TranType] = 'TCP') THEN [IpTotalLen] ELSE 0 END) AS IpV6TCPTSum,

	COUNT(CASE WHEN ([InterProType] = 'IpV6' AND [TranType] = 'UDP') THEN 1 ELSE NULL END) AS IpV6UDPNum,
	SUM(CASE WHEN ([InterProType] = 'IpV6' AND [TranType] = 'UDP') THEN [IpHeadLen] ELSE 0 END) AS IpV6UDPHSum,
	SUM(CASE WHEN ([InterProType] = 'IpV6' AND [TranType] = 'UDP') THEN [IpTotalLen] ELSE 0 END) AS IpV6UDPTSum,

	COUNT(CASE WHEN [InterProType] = 'Arp' THEN 1 ELSE NULL END) AS ArpNum,
	SUM(CASE WHEN [InterProType] = 'Arp' THEN [IpHeadLen] ELSE 0 END) AS ArpHSum,
	SUM(CASE WHEN [InterProType] = 'Arp' THEN [IpTotalLen] ELSE 0 END) AS ArpTSum,
	MAX([Seconds]) AS Duration
FROM dbo.stat

