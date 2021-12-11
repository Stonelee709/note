正向代理：VPN，代理客户端

反向代理：代理服务器。

IPHASH：固定IP给同一台服务。不推荐

Nginx 三个作用：反向代理、负载均衡、动静分离

#### 常用命令：

./nginx 启动

./nginx -s stop

./nginx - quit

./nginx -reload

ps aux|grep nginx



配置文件 nginx.conf

/etc/host 文件 如果改成 127.0.0.1 www.baidu，那么访问 baidu 就会访问本机

容器之间要进行直接访问可以通过 --link，就通过 host 映射



#### 自定义网络

自定义网络可以直接通过名称PING通