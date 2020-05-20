# LotteryApp

This is a backend project that is intended to work with 2 different front end applications

First half of the application is building a databse full of various lotteries winning numbers. The lotteries being analyzed are tracked in the db and managed by users. 
There is a daily background thread that is running through the list of lotteries and pulling the data from the lotteries via a provided url. The data is stripped from the html via specialized service per website domain.

The second half of the application is intended to be an accessible website for all to see past winning numbers. Whenever a user searches for a supported lottery, a statistic is tracked to see what lotteries are the most popular to search for.


Set up:
You will be required to run migrations against the initial postgres database
After that all that is required to start everything is to click the deploy .bat file

Currently only way to see analyzed data is through pgAdmin"# LotteryAnalyzerService" 
