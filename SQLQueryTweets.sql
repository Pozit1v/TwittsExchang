create table Tweets (
Id uniqueidentifier not null default newid() PRIMARY KEY,
Author nvarchar(max) not null,
Text nvarchar(max) null,
Media nvarchar(max) null,
CreateDate datetime null)