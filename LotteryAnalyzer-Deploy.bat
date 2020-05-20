echo Step 1) Build the Docker container for Redis/Talaria/Caduceus
docker-compose --project-name lotteryAnalyzer build 

echo Step 2) Run the Docker container we just created 
start cmd /k docker-compose --project-name lotteryAnalyzer up
pause