ffmpeg [global_options] {[input_file_options] -i input_url} ... {[output_file_options] output_url} ...



```
ffmpeg -i input.avi -r 24 output.avi //强制将文件 frame rate 转为 24
ffmpeg -i input.avi -b:v 64k -bufsize 64k output.avi // 将 bitrate 转为64KBS
```

输入文件 ->经过拆分器（libavformat 库）拆分成编码的数据包-》经过解码器成为解码的未压缩的帧-》过滤器处理（ libavfilter library）---》编码-》输出文件

简单过滤器：

-vf: video filter/-af:audio filter

复杂过滤器：

-filter_complex。 -lavfi 选项相对于-filter_complex。

overlay 选项是将两个视频叠放在一起。而音频叠放在一起的选项是amix选项。



Stream copy 模式只进行拆分和合并，不进行解码和编码，因而快，不能应用过滤器。

#### 流选择

-vn （忽略视频流）/ -an（忽略音频流） / -sn（忽略字幕流） / -dn（忽略数字流） 

使用 -map 选项时，只会包含用户选择的流



示例

```javascript
input file 'A.avi'
      stream 0: video 640x360
      stream 1: audio 2 channels

input file 'B.mp4'
      stream 0: video 1920x1080
      stream 1: audio 2 channels
      stream 2: subtitles (text)
      stream 3: audio 5.1 channels
      stream 4: subtitles (text)

input file 'C.mkv'
      stream 0: video 1280x720
      stream 1: audio 2 channels
      stream 2: subtitles (image)

ffmpeg -i A.avi -i B.mp4 out1.mkv out2.wav -map 1:a -c:a copy out3.mov
前两个 output 文件没有指定 -map 选项，所以工具自动选择流
out1.mkv is a Matroska container file and accepts video, audio and subtitle streams, so ffmpeg will try to select one of each type.
For video, it will select stream 0 from B.mp4, which has the highest resolution among all the input video streams.
For audio, it will select stream 3 from B.mp4, since it has the greatest number of channels.
For subtitles, it will select stream 2 from B.mp4, which is the first subtitle stream from among A.avi and B.mp4.

out2.wav accepts only audio streams, so only stream 3 from B.mp4 is selected.

For out3.mov, since a -map option is set, no automatic stream selection will occur. The -map 1:a option will select all audio streams from the second input B.mp4. No other streams will be included in this output file.

For the first two outputs, all included streams will be transcoded. The encoders chosen will be the default ones registered by each output format, which may not match the codec of the selected input streams.

For the third output, codec option for audio streams has been set to copy, so no decoding-filtering-encoding operations will occur, or can occur. Packets of selected streams shall be conveyed from the input file and muxed within the output file.
```

