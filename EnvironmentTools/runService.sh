imageName="english_tasks"
containerName="task_service"
networkName="english-net"

docker rm $containerName

docker run -p 8000:8000 --name $containerName --network $networkName $imageName
