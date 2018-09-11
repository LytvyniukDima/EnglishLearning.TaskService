networkName="english-net"
containerName="mongodb"

sudo docker network create english-net
sudo docker run -d -p 27017:27017 -v ~/data:/data/db --name $containerName --network $networkName mongo

# connect to db
# mongo localhost/<mydb>
# db.getCollection("<myCollection>")
# db.<myCollection>.fin()