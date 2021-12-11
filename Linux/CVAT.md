django
qiang.li@pacteraedge.com
pass@123
export CVAT_HOST=139.219.8.12
docker-compose up -d  // 运行

docker-compose down // 删除

```//
docker-compose down --volumes//连 VOLUME 一起删除
```

```
docker-compose stop
```

dgs/Pass@123
cvat.chinanorth.cloudapp.chinacloudapi.cn:8080
export CVAT_HOST=cvat.chinanorth.cloudapp.chinacloudapi.cn
docker-compose up -d
cvat.chinanorth.cloudapp.chinacloudapi.cn:8080
http://cvat.chinanorth.cloudapp.chinacloudapi.cn:8080/

重启docker服务 sudo service docker restart

关闭docker  sudo service docker stop  

/var/lib/docker/

lsblk

```sh
docker exec -it cvat bash -ic 'python3 ~/manage.py createsuperuser'
```



step 1

export CVAT_HOST=cvat.chinanorth.cloudapp.chinacloudapi.cn

step2

sudo service docker restart



查看工具版本

docker image inspect openvino/cvat_server:latest|grep -i version

查看 CPU