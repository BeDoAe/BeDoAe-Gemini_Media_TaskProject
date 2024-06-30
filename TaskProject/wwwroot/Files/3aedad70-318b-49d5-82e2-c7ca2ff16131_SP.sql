CREATE PROCEDURE [dbo].[SoftDeleteProject]
    @ProjectId INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Soft delete the project
    UPDATE Projects
    SET IsDeleted = 1
    WHERE Id = @ProjectId;

    -- Soft delete related tasks
    UPDATE Tasks
    SET IsDeleted = 1
    WHERE ProjectId = @ProjectId;

    -- Soft delete related subtasks
    UPDATE Subtasks
    SET IsDeleted = 1
    WHERE TaskId IN (SELECT Id FROM Tasks WHERE ProjectId = @ProjectId);

    -- Optionally, soft delete related user tasks (many-to-many table)
    UPDATE UserTasks
    SET IsDeleted = 1
    WHERE TaskId IN (SELECT Id FROM Tasks WHERE ProjectId = @ProjectId);
END
