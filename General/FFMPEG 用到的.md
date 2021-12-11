```bash
创建空白音频
ffmpeg -f lavfi -t 1 -i anullsrc=cl=mono ./dummy.opus
```

CVAT 拆视频为图片

ffmpeg -i 1.mp4 -start_number 0 -b:v 10000k -vsync 0 -an -y -q:v 16 frames_%d.png



ffmpeg -i 1.mp4 -start_number 0  "select=gte(n\, 150)" -b:v 10000k -vsync 0 -an -y -q:v 16 images/%d.jpg



```sh
ffmpeg -i 1.wmv -vf select='eq(n\,100)+eq(n\,184)+eq(n\,213)' -vsync 0 frames%d.png
```

ffmpeg -i 1.wmv -vf select='eq(n\,0)' -vsync 0 frames0.png

```
ffmpeg -i 1.wmv -start_number 0 -vf select='between(n\,10\,20)' -vsync 0 frames%d.png
```

ffmpeg -i 1.wmv -start_number 10 -b:v 10000k -vf select='between(n\,10\,20)' -vsync 0 -an -y -q:v 16 images/%d.jpg



```
ffmpeg -i 1.mp4 -start_number 0 -vf select='eq(n,5)+eq(n,11)+eq(n,15)+eq(n,16)' -vsync 0 images/frames%d.jpg
```

压缩视频：vga 代表 600*480

```text
ffmpeg -i Desktop/1.mov -s vga Desktop/1.mp4
```

 

查看 VFR 的比例 ffmpeg -i input.mp4 -vf vfrdet -f null -

https://www.jianshu.com/p/b9b5bb03becc



```
ffprobe -show_entries packet=pts_time,duration_time,stream_index file.mp4
```