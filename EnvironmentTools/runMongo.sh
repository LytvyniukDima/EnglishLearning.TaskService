networkName="english-net"
containerName="mongodb"

docker kill $containerName
docker rm $containerName

docker network create english-net

# for linux
# docker run -d -p 27017:27017 -v ~/data:/data/db --name $containerName --network $networkName mongo

# for windows
docker run -d -p 27017:27017 --name $containerName --network $networkName mongo

# connect to db
# mongo localhost/<mydb>
# db.getCollection("<myCollection>")
# db.<myCollection>.fin()