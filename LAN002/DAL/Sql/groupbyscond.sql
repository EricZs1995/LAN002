select Seconds,COUNT(*)as count, SUM(rt.FrameLen) as sumlen
from dbo.rt_data as rt
group by rt.Seconds
order by rt.Seconds