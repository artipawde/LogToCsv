
There Are Four Classes 
A) LogToCsv :- 1) It conatin main method And Exceptions.

B) CsvFile :- 1) It Provides Structure of Csv file.
              2) It also creates .csv file if Directory Path Is Correct.

C) CommandArgumentConverter :- 1) It Provides Validation of User Given Inputs And "Help" Will Print To the User If Given Input is Wrong.
                               2) It Accept Paramerter in any order.
                               3) It also Accept With only Source and Destination. In this Case It will Consider all levels.
                               4) User Can Give Multiple Levels as a Paramerter with Source and Destination.
                               5) If User Give two level one is Right and One Is Wrong then "help" will be Print.
                               6) It Accpet Source As a Directory as well as Source ".log" file. 

D) LoggerConverter :- 1) It Check Line by Line data if Other than Format It will Skip that lines.
                      2) It Reads data form Log File and Write in the Csv file.