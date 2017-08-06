INSERT INTO RFS_TxType(EnvironmentId, WorkflowID)
SELECT EnvironmentID, WorkflowID
FROM RFS_TxHeader
GROUP BY EnvironmentID, WorkflowID