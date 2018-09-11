imageName="english_tasks"
containerName="task_service"
networkName="english-net"

docker run -p 8000:80 --name $containerName --network $networkName $imageName
