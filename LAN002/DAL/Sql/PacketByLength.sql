SELECT TotalNum,
	Num,
	SumLen,
	MaxVal,
	MinVal,
	CAST (Duration AS DECIMAL(18,3)) AS Duration,
	CAST (Num/convert(numeric(18,6),t2.Duration) AS DECIMAL(18,5)) AS Rate,
	CAST (Num*100/convert(numeric(18,3),TotalNum) AS DECIMAL(18,2)) AS PercentNum,
	CAST (SumLen/convert(numeric(18,3),Num) AS DECIMAL(18,2)) AS Average
FROM
(
	SELECT COUNT(*) AS Num,
		SUM(FrameLen) AS SumLen,
		MAX(FrameLen) AS MaxVal,
		MIN(FrameLen) AS MinVal
	FROM dbo.rt_data
	WHERE FrameLen >= @start
	AND FrameLen <=@end
	)AS t1,
(
	SELECT 
		COUNT(*) AS TotalNum,
		MAX(Seconds) AS Duration
	FROM dbo.rt_data
)AS t2