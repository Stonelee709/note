sudo -i 切换成 root用户

139.219.8.244 linux1 linux@123456

[root@linux1 home]# : # 表示超级管理员 $表示其他, home 表示当前位置；root 是用户名; linux 是服务器名

### 关机

shutdown -h  now 正常关机

halt 关闭内存

init 0 也是关机



### 目录

bin: 可执行二进制文件

Dev: 放外接设备，在其中的外接设备是不能直接被使用的，需要挂载（类似分配盘符）。

etc: 该目录主要存储配置文件

home: root 用户以外的用户目录

proc: 表示进程，存linux运行的进程

root: 表示 root 用户自己的用户目录。

sbin: super binary，存放可执行二进制文件，但必须要用超级管理员权限。

tmp: 临时文件。不需要自己删除，系统会自动清除

Usr: 用户自己安装的软件，类似 Programme Files

var: 存放的是日志目录。

mnt: 当外接设备需要挂载的时候，就需要挂载到 mnt 目录。



### 基础指令

| 命令               | 作用                                                         |
| ------------------ | ------------------------------------------------------------ |
| ls                 | ls 列出目录和文件<br/>ls -l 以列表形式显示<br/>ls -a 包含隐藏文件<br/>ls -lh 显示文件大小时选择合适单位<br />dr-xr-xr-x.   5 root root  278 Nov 18  2020 boot<br/>**d**r-xr-xr-x.  ： d 表示目录 - 表示文件<br/>root root : 表示用户和用户组<br/>隐藏文件以 . 开头<br/>目录都占 4k大小<br /><br />![img](https://www.runoob.com/wp-content/uploads/2014/06/file-llls22.jpg)![image-20211029000011984](C:\Users\admin\AppData\Roaming\Typora\typora-user-images\image-20211029000011984.png) |
| pwd                | 当前目录位置                                                 |
| mkdir              | -p 参数可以连同子目录一起创建                                |
| touch              | 创建文件                                                     |
| cp                 | 复制,-r 复制目录，表示递归                                   |
| mv                 | 移动                                                         |
| rm                 | 删除。-f 强制不提示。-r删除目录<br />rm -f linux*  通配符    |
| vim                | 在编辑器打开文件。shift+冒号打开命令行，输入 q 回车。<br />没有权限文件使用 ctrl+z 退出 |
| > 和 >> 输出重定向 | 将命令行的输出的结果保存到文件。> 覆盖输出 >>追加输出。 ls -la > ls.txt |
| cat                | 作用1：在命令行直接打开文件。<br />作用2：合并多个文件。cat 1.txt 2.txt 3.txt >4.txt |
| head               | 查看文件前几行 head -5 1.log                                 |
| tail               | 查看文件后几行 tail -10 1.log。<br />tail -F 查看动态更新    |
| less               | 分页显示                                                     |
| **进阶命令**       |                                                              |
| df                 | 查看磁盘空间。df -h                                          |
| free               | 查看内存使用情况。free -m。<br />Swap:内存不够时将硬盘转为内存用的交换空间 |
| wc                 | 统计文件信息，包含行数、单词数、字节数。wc -lwc 1.log        |
| date               | date +%F                                                     |
| clear /ctrl+L      | 清屏                                                         |
| \| 管道符          | 和有输出的命令搭配使用，用于过滤。ls \|grep y 过滤包含 y 的文件或目录。<br />ls \|wc -l |
| id                 | 用户信息                                                     |
| ps -ef             | 查看进程信息。-e显示所有的进程。 -f 表示列出全部的列。<br />UID          PID（进程ID）    PPID（父进程 ID）  C（CPU使用占比） STIME（启动时间） TTY（终端设备）          TIME（进程执行时间） CMD（进程对应的名称或路径）<br />如果一个程序的父级进程找不到，就是僵尸进程。 |
| top                | 查看服务器进程占的资源，按 q 退出<br />PID USER      PR(优先级)  NI    VIRT（虚拟内存）    RES（常驻内存）    SHR（共享内存） S  （睡眠的状态）%CPU  %MEM     TIME+ COMMAND |
| du -sh             | 查看目录真实大小。du -sh bin/                                |
| find               | 查找文件。find / -name httpd.conf<br />find / -name *.config \| wc -l 统计有多少个名称<br />find / -type f 查看文件个数<br />find / -type d 查看文件夹个数 |
| service            | 用于控制一些软件服务的启动/停止/重启 start/stop/restart.<br />servcie httpd restart |
| killall            | killall 进程名称                                             |
| ifconfig           | 获取网卡信息                                                 |
| reboot             | 重新启动计算机                                               |
| shutdown           | 关机<br />shutdonw -c 取消关机                               |
| uptime             | 运行时间                                                     |
| uname              | 获取计算机系统相关信息 -a                                    |
| netstat -tnlp      | 查看网络的连接状态                                           |
| man                | 手册，查看命令用法 man netstat                               |

![image-20211029000129200](C:\Users\admin\AppData\Roaming\Typora\typora-user-images\image-20211029000129200.png)

### VIM 编辑器

Vim 存在三种模式：命令模式、编辑模式、尾行模式

打开文件：

1 vim 文件名

2 vim 数字 文件名 //将光标移动到指定行

3 vim +关键词 文件名 //高亮显示



模式切换

（1）进入vim：vim test.c  （刚进入是命令模式，不可输入文字）

（2）命令模式 --> 插入模式

    1.输入a   （进入后，是从目前光标所在位置的下一位置开始输入文字）
    
    2.输入i    （进入后，是从光标当前所在位置开始输入文字）
    
    3.输入o   （进入后，是插入新的一行，从行首开始输入文字）

（3）命令模式  --> 底行模式

    输入  ：

（4）不管当前是插入模式，还是底行模式，都要按 Esc 退入到命令模式才能进入其它模式

（5）退出vim 切换到底行模式 输入 q 退出

    1.输入：w（保存当前文件）
    
    2.输入：wq（保存并退出）
    
    3.输入：q!（强制退出）
**命令模式**

^ 回到行首

gg回到首行

G回到尾行

数字 G 光标跳到指定行

yy 复制

数字+yy:复制多行

p 粘贴

ctrl+v 进入可视块，通过上下箭头选择区域，按 yy 复制，到位置按 p 粘贴

dd: 删除/剪切

:u 撤销

ctrl+r:  取消之前的撤销

### 未行模式

:w 保存

:q 退出

:wq: 保存退出

:q! 强制退出

:! ls 执行外部命令

/关键词：搜索

:nohl: 取消高亮

:s/some/other/g  替换



**硬链接和软链接**

硬链接为了保护重要文件不被删除。

软链接类似快捷键



**文件下上传下载**

安装 lrzsz

rz 上传

ip addr 获取 IP 地址



LINUX 挂载硬盘：

https://blog.51cto.com/u_12348890/2092339

lsblk 查看分区挂载情况

df -h 查看磁盘空间



查看有哪些IP连接本机
netstat -an

统计80端口连接数
netstat -nat | grep “80” | wc -l

统计已连接上的，状态为 “established”
netstat -na | grep ESTABLISHED | wc -l

查看80端口 “TIME_WAIT” 数
netstat -nat | grep “80” | grep TIME_WAIT | wc -l

查看80端口 “ESTABLISHED” 数
netstat -nat | grep “80” | grep ESTABLISHED | wc -l

统计httpd协议连接数
ps -ef | grep httpd | wc -l
