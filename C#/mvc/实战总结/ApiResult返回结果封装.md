### ApiResult 类

```c#
 public class ApiResult
    {
        public string Code { get; set; } = "-1";
        /// <summary>
        /// API调用是否成功
        /// </summary>
        public bool Success { get; set; } = false;
        /// <summary>
        /// 服务器回应消息提示
        /// </summary>
        public string ResultMessage { get; set; }
        /// <summary>
        /// 服务器回应的返回值对象(API调用失败则返回异常对象)
        /// </summary>
        public object ResultObject { get; set; }
        /// <summary>
        /// 服务器回应时间
        /// </summary>
        public string ResponseDatetime { get; set; }

        /// <summary>
        /// 设置API调用结果为成功
        /// </summary>
        /// <returns></returns>
        public ApiResult SetSuccessResult()
        {
            Code = "0";
            ResponseDatetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");
            Success = true;
            ResultMessage = "Success";
            ResultObject = string.Empty;
            return this;
        }
        /// <summary>
        /// 设置API调用结果为成功
        /// </summary>
        /// <param name="resultObject">不需要从Data里面读取返回值对象时，存储简单的值对象或者string</param>
        /// <returns></returns>
        public ApiResult SetSuccessResult(string resultObject)
        {
            Code = "0";
            ResponseDatetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");
            Success = true;
            ResultMessage = "Success";
            ResultObject = resultObject;
            return this;
        }
        /// <summary>
        /// 设置API调用结果为失败
        /// </summary>
        /// <param name="errorCode">错误代码</param>
        /// <param name="errorMessage">错误消息</param>
        /// <returns></returns>
        public ApiResult SetFailedResult(string errorCode, string errorMessage)
        {
            Code = errorCode;
            ResponseDatetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");
            Success = false;
            ResultMessage = errorMessage;
            ResultObject = string.Empty;
            return this;
        }
        /// <summary>
        /// 设置API调用结果为失败
        /// </summary>
        /// <param name="errorCode">错误代码</param>
        /// <param name="errorMessage">错误消息</param>
        /// <param name="e">异常对象</param>
        /// <returns></returns>
        public ApiResult SetFailedResult(string errorCode, string errorMessage, Exception e)
        {
            Code = errorCode;
            ResponseDatetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");
            Success = false;
            ResultMessage = errorMessage;
            ResultObject = e;
            return this;
        }
    }
    /// <summary>
    /// API返回值数据传输对象（泛型版）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResult<T> : ApiResult
    {
        public virtual T Data { get; set; }

        public virtual ApiResult<T> SetSuccessResult(T t)
        {
            var result = new ApiResult<T>();
            result.SetSuccessResult().ResultObject = t.GetType().Name;
            result.Data = t;
            return result;
        }
    }
```



使用

```
public ContentResult Get(int id)
        {
            return new ContentResult { 
            StatusCode=200,
            ContentType="text/html",
            Content=JsonConvert.SerializeObject(new ApiResult<Person>().SetSuccessResult(new Person { PersonID = 1, Name="lee",Age=30 }))
            
            };
        }
```

