

--select Seconds,SUM(FrameLen) as sumlen
--from(
--select cast(Seconds as int)as Seconds, FrameLen

--from dbo.rt_data ) as t
--group by Seconds
--order by Seconds

select Seconds,Count(*)as count
from(
select cast(Seconds as int)as Seconds, FrameLen

from dbo.rt_data ) as t
group by Seconds
order by Seconds