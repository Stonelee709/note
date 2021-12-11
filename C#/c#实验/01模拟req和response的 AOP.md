req 处理需要一种先进先处理的数据结构，而 response需要一种先进后处理的结构。

将 IFilter 定义两个方法，分别加到委托当中，分别对 req 和 response 进行处理：

```c#
 public class Program
    {
        static void Main(string[] args)
        {
            Filterchain<Request> filterReq = null ;

            Filterchain<Response> filterResponse=null;
            Request req1 = new Request() { RequestInfo = "req 1" };
            EncodingFilter encodingfilter = new EncodingFilter();
            filterReq += encodingfilter.Requestfilter;
            var req=filterReq(req1);
            Console.WriteLine(req.RequestInfo);
            Console.ReadLine();
        }
    }

    public delegate T Filterchain<T>(T req);
    

    public class Filter
    {

    }
    public class Request
    {
        public string RequestInfo { get; set; }
    }
    public class Response
    {
        public string ResponseInfo { get; set; }
    }
```

