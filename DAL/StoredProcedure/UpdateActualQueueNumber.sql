
BEGIN WITH cte AS (SELECT A.RequestId, A.Year, A.ActualQueueNumber, ROW_NUMBER() 
OVER(ORDER BY A.RequestId ASC) AS Number FROM AlmatyDDO_DB.dbo.[DayCareQueueRequest]
AS A JOIN AlmatyDDO_DB.dbo.[DayCareQueueState] AS B ON A.DayCareStateId = B.Id INNER JOIN Request AS C ON A.RequestId=C.Id  WHERE 
Year = @Year and B.StateInQueue <> 3 and B.StateInQueue <> 5 AND C.State<>5) UPDATE cte SET ActualQueueNumber = Number END 


--CREATE PROCEDURE [dbo].[UpdateActualQueueNumber] @Year int AS BEGIN WITH cte AS
-- (SELECT A.RequestId, A.Year, A.ActualQueueNumber, ROW_NUMBER() OVER(ORDER BY A.RequestId ASC)
--  AS Number FROM AlmatyDDO_DB.dbo.[DayCareQueueRequest] AS A JOIN AlmatyDDO_DB.dbo.[DayCareQueueState] 
--  AS B ON A.DayCareStateId = B.Id INNER JOIN Request AS C ON A.RequestId=C.Id WHERE Year = @Year and B.StateInQueue <> 5 AND C.State<>5 ) UPDATE cte SET ActualQueueNumber = Number END 