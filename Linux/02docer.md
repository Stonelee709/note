1创建 Ubuntu 虚拟机

2安装 docker

```
curl -fsSL https://get.docker.com | bash -s docker --mirror Aliyun
```

3 用putty的 pscp 将文件上传。

pscp -P 22 *.* 用户名@192.168.50.5:/home

4 运行 bash 

bash ./build_labelme_image.sh

bash ./start_labelme_container.sh





解决无法用 root 登录的问题：https://blog.csdn.net/qq_39892503/article/details/103478579



### Docker:

镜像是模板 | Container 是小型 Linux

 cat /etc/os-release 查看系统版本

https://docs.docker.com/engine/install/ubuntu/  安装手册

```
 sudo apt-get remove docker docker-engine docker.io containerd runc  删除旧版本
```



```
 sudo apt-get install docker-ce docker-ce-cli containerd.io     ce代表社区 cli客户端
```

启动服务 systemctl start docker

docker images 查看镜像

restart docker 重启 docker 服务



删除 docker

```javascript
 sudo apt-get purge docker-ce docker-ce-cli containerd.io 卸载软件
 sudo rm -rf /var/lib/docker //删除目录
 sudo rm -rf /var/lib/containerd //删除目录
```

docker run hello-world 运行镜像

步骤：1先在本机找镜像 2 如果没有去 docker hub 下载，找不到就返回错误 3 下载镜像到本地



底层原理：

docker 是一个 client-sever 结构的系统，docker 的守护进程运行在主机上，通过 Socket 从客户端访问！

DockerServer 接收到 Docker Client 的指定，就会执行这个命令。

docker version

docker info

docker 命令 --help

docker --help 查看有哪些命令 https://docs.docker.com/reference/

### 镜像

```
root@cvatlinux:/home/cvatlinux/cvat# docker images
REPOSITORY             TAG          IMAGE ID       CREATED         SIZE
postgres               10-alpine    9c86c3f38b5f   9 days ago      72.8MB
hello-world            latest       feb5d9fea6a5   4 weeks ago     13.3kB
openvino/cvat_ui       latest       8453f999b25c   5 weeks ago     49.1MB
openvino/cvat_server   latest       6b28d9526b06   5 weeks ago     4.91GB
traefik                v2.4         de1a7c9d5d63   2 months ago    92MB
redis                  4.0-alpine   e3dd0e49bca5   18 months ago   20.4MB
REPOSITORY 镜像仓库源
Tag 镜像标签
IMAGE ID：镜像ID
```

搜索

docker search mysql --filter=starts=3000 命令或使用网页https://registry.hub.docker.com/

下载

docker pull mysql

docker pull mysql:5.7

删除

docker rmi -f Image ID

docker rmi -f $(docker images -aq) 删除全部



### 容器

有了镜像，才能创建容器

docker run [可选参数] images

--name="Name"

-d 后台运行

-it 使用交互方式运行

-p 指定端口 -这8080:8080

-P 随机指定端口

docker run -it centos /bin/bash 启动并进入内部容器，exist 停止并退出容器, ctrl+p+q 不停止退出容器

docker ps 查看当前正在运行的容器

docker ps -a 查看曾经运行过的容器

docker rm id 删除容器

docker rm -f $(docker ps -aq)

docker start/stop/kill id



### 其它常用命令

docker run -d centos 后台启动

docker logs 日志

docker logs -tf --tail 10 62d62b4cf36b

docker top 62d62b4cf36b 查看容器内部进程信息

docker inspect 62d62b4cf36b  查看容器元数据



**进入容器**

```shell
docker exec -it centos /bin/bash //进入容器后开启一个新的终端
docker attached containerid //进入正在执行的终端
docker cp containerid:container路径 目的主机路径 复制
```



### 安装 Nginx

docker search nginx

docker pull nginx

docker images 查看

docker run -d --name nginx01 -p 3344:80 nginx

docker ps 查看容器

curl localhost: 3344 测试本机

### 安装 Tomcat

docker pull tomcat

docker run -d -p 3355:8080 --name tomacat01 tomcat

进入容器发现容器是阉割的，LINUX 命令少了。因为它默认是最小镜像。将 webapp.dist 目录内容复制到 webapp 中。

cp -r webapp.dist/* webapps //目录文件全部复制



### 部署 es+kibana

docker stats 查看 cpu 的状态



### docker 可视化

portainer



### 分层系统

layer， 共享已有数据，下载时会有很多层，有些应用的层是相同的就不需要下载。

我们对容器进行操作时，会在镜像层加一个容器层，变成两层，如果你想把操作完的结果打包成一个镜像：

docker commit -m "description" -a 作者 容器id 目标镜像名

docker images 查看新的镜像



#### 容器卷

容器的数据持久化、容器间数据共享

docker run -it -v 主机目录:容器目录 

docker run -it -v /home/ceshi: /home centos /bin/bash

docker inspect containerid 查看容器信息, Mounts 就是挂载点。SOURCE 是本机目录，DESTINATION 是容器目录

```shell
"Mounts": [
            {
                "Type": "volume",
                "Name": "cvat_cvat_db",
                "Source": "/var/lib/docker/volumes/cvat_cvat_db/_data",
                "Destination": "/var/lib/postgresql/data",
                "Driver": "local",
                "Mode": "rw",
                "RW": true,
                "Propagation": ""
            }
        ],

```



#### mysql

docker run -d -p 3310:3306 -v /homemysql/conf:/etc/mysql/conf.d -v /home/mysql/data:/var/lib/mysql -e MYSQL_ROOTPASSWORD=123456 --name mysql01 mysql:5.7

docker volume ls 列出卷信息

-v 路径后面跟了 :ro 或 :rw 只读或读写

#### Dockerfile

Dockerfile 就是用来构造 docker 镜像的构建文件。是命令脚本。

docker build -f dockerfile1 -t kuangshen/centsos .

```shell
FROM centos
VOLUME ["volume01", "volume02"]//匿名挂载两个目录通过 docker inspect 可查看挂载具体信息
CMD echo "-----end---"
CMD /bin/bash
```

##### 构造步骤：

1编写 dockerfile

2 docker build

3 docker run

4 docker push



指令：

大写、#表示注释、每一行指令代表一个镜像层

dockerfile是构建文件，定义了一切步骤，源代码

dockerimage是通过dockefile构建生成的镜像，最终发布和运行的产品

dockercontainer运行的镜像。

![img](https://gimg2.baidu.com/image_search/src=http%3A%2F%2Fimg2020.cnblogs.com%2Fblog%2F1869289%2F202005%2F1869289-20200529090758309-649751292.png&refer=http%3A%2F%2Fimg2020.cnblogs.com&app=2002&size=f9999,10000&q=a80&n=0&g=0n&fmt=jpeg?sec=1638186727&t=094dd10ab9845794fd13194474d67f9f)

docker history  containerid 列出image构造过程

ENTRYPOINT 与 CMD 区别：ENTRYPOINT 可以追加命令参数，而 CMD 会被覆盖



#### 数据卷容器

容器之间共享。

--volumes-from

docker run -it --name docker03 --volumes-from docker01 kuangshen/centos:1.0



#### 制作 TOMCAT 镜像

![image-20211030205354294](C:\Users\admin\AppData\Roaming\Typora\typora-user-images\image-20211030205354294.png)



#### 发布

dock login -u username

docker push containername:version