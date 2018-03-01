SELECT Year, Count_BIG(A.RequestId) AS'FullCount',
 Count_BIG(
 CASE WHEN Privilage <> 0 AND StateInQueue=1 THEN 1 END
 ) as 'PrivilagedInQueue',
 Count_BIG(
 CASE WHEN Privilage=0 AND StateInQueue=1 THEN 1 END
 ) AS 'CommonInQueue',
  Count_BIG(
 CASE WHEN Privilage=2 AND StateInQueue=1 THEN 1 END
 ) AS 'AlternativesInQueue',
 COUNT_BIG(
 CASE WHEN StateInQueue = 3 THEN 1 END
 ) AS 'Directed'
FROM DayCareQueueRequest AS A INNER JOIN DayCareQueueState AS B ON A.RequestId=B.RequestId  GROUP BY A.Year OPTION 	(MAXDOP 1)  