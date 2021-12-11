强制推送

git config --global http.sslVerify "false"

**git push -f origin master**

===================

查看配置: git config --l

查看全局配置: git config --global --l

配置文件：C:\Program Files\Git\etc\gitconfig 文件

安装好 git 后需要设置用户名

git config --global user.name "xxxx"

git config --global user.email "xxxx"



#### 目录

1. 工作区：本地工作目录
2. 暂存区：
3. Repository 本地代码库
4. 远程仓库



#### 步骤

1. 在工作目录添加修改文件
2. 将需要进行版本管理 的文件放入缓存区 git add
3. 将暂存区的文件提交到 git 仓库 git commit
4. 推到远程. git push



![image-20211211191151457](C:\Users\admin\AppData\Roaming\Typora\typora-user-images\image-20211211191151457.png)

#### 仓库搭建

1本地仓库创建：在目录中 git init

2 远程克隆：git clone url

![image-20211211224625273](C:\Users\admin\AppData\Roaming\Typora\typora-user-images\image-20211211224625273.png)

#### 文件状态

1. Untracked未跟踪,通过 git add 将其状态变成 Staged
2. Unmodified:文件内容与快照相同。git rm 将其移出变成 untracked 状态
3. Modified
4. Staged:暂存状态



git status 命令查看文件状态

```bash
git add . 添加所有文件
git commit -m  "提交消息"
```

.gitignore 忽略的文件

```bash
*.txt 忽略所有 txt
!lib.txt 这个文件除外
target/ 这个目录下面的忽略
```



####  分支

git branch 查看本地分支

git branch -r 查看远程分支

git branch dev 新建 dev 分支

git checkout -b [branch] 新建分支并切换到分支

git merge [branch] 将指定分支合并到当前分支

git branch -d [branch-name] 删除分支

git push origin --delete [branch-name] 删除远程分支



一般操作需要保证master稳定运行，开发时建一个 Dev 分支，然后将 Dev 分支合并

git log 查看提交日志

git reset --hard commitID

git reflog 可以在回退后查看全部操作

git fetch 获取远程服务器