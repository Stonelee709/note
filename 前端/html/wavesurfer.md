```html
<div id="waveform"></div>
<script>
    var wavesurfer = WaveSurfer.create({
        container: '#waveform',
        mediaControls: true,
        preload: true,
        backgroundColor:'black'
    });
    wavesurfer.load('../audio/1.wav');
    //wavesurfer.loadBlob('https://speechaz.blob.core.windows.net/speech/1Lecture/20210623/20210623_en0001_don.wav?sv=2020-04-08&st=2021-09-25T05%3A01%3A37Z&se=2021-09-26T05%3A01%3A37Z&sr=b&sp=r&sig=11YWHFmnmVAWap7Pwiq5BQYcMHa7VlTztGwJ2S1cOCQ%3D');
    wavesurfer.on('ready', function () {
        wavesurfer.play();
    });
</script>
```

