```c#
			List<SplitJson> jsonlist = new List<SplitJson>();
			JsonSerializer serializer = new JsonSerializer();
            StringWriter sw = new StringWriter();
            serializer.Serialize(new JsonTextWriter(sw), jsonlist);
            string result = sw.GetStringBuilder().ToString();
```

