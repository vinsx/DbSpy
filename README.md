# DbSpy

Utility designed to visualize relationships between SQL Server database objects and the C# code referencing those objects. 

DbSpy requires two setting in app.config in order to load data: 
1) DefaulConnection - has to point to SQL Server database. 
2) SolutionPath - has to point to a solution referensing configured database. 
When application is lunched user will have to click Login from the File menu. 
For None Integrated Security DefaultConnection expects User ID={0};Password={1}. These values will be substituted with values from the Login dialog. 
If Integrated Security is set to True in the DefaulConnection user will have to type something in the Login dialog to continue. Entered values will be ignored. 


Please note these utility was build for a company using MyBatis for data access layer and code had significant number of references to the database objects. These tool was intended to facilitate identifying relationships and abandoned references for a subsequent cleanup effort.
