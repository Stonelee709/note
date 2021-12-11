#### Controller 功能

1. Index(): Overview Page
2. ImportOne()
3. ImportBatch()
4. Label()--GetTask
5. QA()---GetQATask
6. Submit()
7. Save()
8. QueryDataByProject()
9. QueryDataByID()



#### Json 必需属性:

id:项目唯一性

projectcode:每个项目一个，自动生成，overviewpage使用。UUID。PartitionKey

taskname: 拆分任务使用



#### WebApps:

text labeling

image labeling

audio transcription

voice recording looptalk

webapp 列表放在内存中

