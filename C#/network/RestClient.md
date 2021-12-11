```c#
  public JArray GetResourceCount(string programId, string projectId, string chunkId)
        {
            var client = new RestClient(RequestUrl)
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.POST);
            request.AddHeader("Accept", "text/plain");
            request.AddHeader("Authorization", Settings.AccessToken);
            client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.125 Safari/537.36";
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json",
                JsonConvert.SerializeObject(new
                {
                    endpoint = "https://onedoclabeling-be.azurewebsites.net/api",
                    programId,
                    userRoles = new[]{
                        "ocr-vertical-idReadWrite", "cv-face-antispoofingReadWrite"
                    },
                    userEmail = Settings.UserName,
                    action = "FetchChunk",
                    payload = new
                    {
                        projectId,
                        chunkIds = new[] { chunkId }
                    }
                }),
                ParameterType.RequestBody);
            var response = client.Execute(request);

            var result = JsonConvert.DeserializeObject<JArray>(response.Content);
            if (Settings.DownloadJson)
            {
                // download files
                if (!Directory.Exists(programId))
                {
                    Directory.CreateDirectory(programId);
                }
                //File.WriteAllText($@"{programId}\{projectId}_{chunkId}.json", JsonConvert.SerializeObject(result, Formatting.Indented));
                File.WriteAllText("D:\\1.json", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            var arr = result[0]["items"] as JArray;
            return arr;
        }
```

