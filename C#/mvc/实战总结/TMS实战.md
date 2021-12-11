### TMS

#### 配置无需修改就可以更新视图

```
1安装 Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
2在ConfigureService中配置：
#if DEBUG
            services.AddRazorPages().AddRazorRuntimeCompilation();
#endif
```



Model

```c#
    public class TranslationStatus
    {
        public int TranslationStatusID { get; set; }
        public string status { get; set; }
        public ICollection<TranslationUnit> TranslationUnits { get; set; }

    }
    public class TranslationUnit
    {
        public int TranslationUnitID { get; set; }
        [Required]
        public string SourceString { get; set; }
        public string TargetString { get; set; }
        public int TranslationStatusID { get; set; }
        
        public TranslationStatus TranslationStatus { get; set; }
    }
```

Controller

```c#
 public IActionResult Create()
        {
            //ViewData["TranslationStatusID"] = new SelectList(_context.TranslationStatus, "TranslationStatusID", "TranslationStatusID");
            ViewData["status"] = new SelectList(_context.TranslationStatus, "TranslationStatusID", "status");
            return View();
        }
```

View

Form 不能放到 Table 内。

经验：如果想提交 ID，但显示其他字符串，可以做两个 INPUT，一个显示，一个隐藏。



#### 问题：上传文件时一直无法将文件上传。

答案：需要使用 asp-action 而不是 action

```http
<form asp-action="ImportFile" method="post" enctype="multipart/form-data">
    <p>选择文件<input type="file" name="files"></p>
    <p><input type="submit" value="submit"></p>
</form>
```

上传的 Controller

```c#
[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ImportFile(List<IFormFile> files)
        {
            foreach (var formFile in files)
            {
                var filePath = @"D:\UploadingFiles\" + formFile.FileName.Substring(formFile.FileName.LastIndexOf("\\") + 1);//注意formFile.FileName包含上传文件的文件路径，所以要进行Substring只取出最后的文件名
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                    
                }
                var lines = System.IO.File.ReadLines(filePath);
                lines.ForEach(line =>
                {
                    var text = line.Split(new string[] { "\t" }, StringSplitOptions.None);
                    for (int i = 0; i < text.Length; i++)
                    {
                        _context.Add(new TranslationUnit { SourceString = text[0],TargetString= text[1],TranslationStatusID= int.Parse(text[2]) });
                    }
                   
                    
                });
                await _context.SaveChangesAsync();
                
            }
            return RedirectToAction(nameof(Index));
        }
```



#### 分页查询

Controller: 用 take 来代替 limit

```c#
public async Task<IActionResult> IndexPage(int pagesize, int page)
        {
            int pagecount = (int)Math.Ceiling((double)_context.TranslationUnits.Count() / pagesize)+1; 
            var tMSContext = _context.TranslationUnits.Skip(pagesize* (page-1)).Take(pagesize).Include(t => t.TranslationStatus);
            ViewBag.pagecount = pagecount;
            ViewBag.pagesize = pagesize;
            return View(await tMSContext.ToListAsync());
        }

//View
@for (int i = 1; i < ViewBag.pagecount; i++)
{
    @Html.ActionLink(@i.ToString(),"IndexPage",new{pagesize=ViewBag.pagesize, page=@i }, new{ @class="btn btn-default" })
 }
```

#### 异步提交翻译：

注意 Form 的 onsubmit="return false"

```html
@foreach (var item in Model)
{

    <form id="@item.TranslationUnitID" method="post" onsubmit="return false">
        <div>
            <input name="TranslationUnitID" value="@item.TranslationUnitID" readonly />
            <input name="SourceString" value="@item.SourceString" readonly />
            <input name="TranslationStatusID" value="@item.TranslationStatusID" style="display:none" readonly />
            <input value="@item.TranslationStatus.status" readonly />
            <input name="TargetString" value="@item.TargetString" />
            <button id="btnSubmit" type="submit" onclick="saveTranslation(@item.TranslationUnitID)">Save</button>
            <label id="message"></label>
        </div>
    </form>

}
<script>
    function saveTranslation(formID) {
        $.ajax({
            type: 'post',
            url: 'Save',
            data: $("#"+formID).serialize(),
            success: function (data) {formID
                showSuccess(formID)
            },
            error: function (data) {
                showError(formID)
            }
        });
    }
    function showSuccess(formID) {
        $("#"+formID+" #message").text("success!");
        setTimeout(function () { $("#" + formID +" #message").text("") }, 1000);
        
    }
    function showError(formID) {
        $("#" + formID +" #message").text("Failed!");
        setTimeout(function () { $("#" + formID +" #message").text("") }, 1000);

    }
</script>
```



#### 将翻译界面改造成不在 Form 中

```html
@foreach (var item in Model) { 
    <tr id="@item.TranslationUnitID">

    <td><textarea id="TranslationUnitID" name="TranslationUnitID" readonly>@item.TranslationUnitID </textarea></td>
    <td><textarea id="SourceString" name="SourceString" readonly>@item.SourceString</textarea></td>
    <td><textarea readonly>@item.TranslationStatus.status</textarea></td>
    <td><textarea id="TargetString" name="TargetString" contenteditable>@item.TargetString</textarea></td>
    <td><textarea id="TranslationStatusID" name="TranslationStatusID" style="display:none" readonly>@item.TranslationStatusID</textarea></td>
    <td>
        <button id="btnSubmit" type="submit" onclick="saveTranslation(@item.TranslationUnitID)">Save</button>
        <label id="message"></label>
    </td>
</tr>
        }
    <tbody>
    </tbody>
</table>
<script>
    function saveTranslation(formID) {
        var TranslationUnitID = $("#" + formID + " #TranslationUnitID").val();
        var SourceString = $("#" + formID + " #SourceString").val();
        var TranslationStatusID = $("#" + formID + " #TranslationStatusID").val();
        var TargetString = $("#" + formID + " #TargetString").val();
        $.ajax({
            type: 'post',
            url: 'Save',
            data: {
                TranslationUnitID: TranslationUnitID,
                SourceString: SourceString,
                TranslationStatusID: TranslationStatusID,
                TargetString: TargetString,

            },
            success: function (data) {
                showSuccess(formID)
            },
            error: function (data) {
                showError(formID)
            }
        });
    }
    function showSuccess(formID) {
        $("#"+formID+" #message").text("success!");
        setTimeout(function () { $("#" + formID +" #message").text("") }, 1000);
        
    }
    function showError(formID) {
        $("#" + formID +" #message").text("Failed!");
        setTimeout(function () { $("#" + formID +" #message").text("") }, 1000);

    }
</script>
```

#### 增加过滤器，只有登录才能访问

```
public class CheckLoginAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if(context.HttpContext.Request.Cookies["TUserName"]==null)
            {
               context.Result= (context.Controller as Controller)
                .RedirectToAction("Index", "Home");
            }
        }
 //后面只要在需要的控制器上面添加[CheckLogin]特性
```

#### 对于有下拉菜单的页面应该要使用 AJAX 进行异步拉取数据

```javascript
<script>
    window.onload = function () {

        $.ajax({
            type: "GET",
            url: "/SourceLangs/Droplist",
            success: function (data) {

                showSourceLangList(data)
            },
            error: function (data) {
                //showError(formID)
            }
        });

        $.ajax({
            type: "GET",
            url: "/TargetLangs/Droplist",
            success: function (data) {

                showTargetLangList(data)
            },
            error: function (data) {
                //showError(formID)
            }

        });
    }

    function showSourceLangList(data) {
        var obj = jQuery.parseJSON(data);
        var appendstring = "";
        for (var i = 0; i < obj.length; i++) {
            appendstring += "<option value=" + obj[i].SourceLangID + ">" + obj[i].SourceLangName+"</option>";
        }
        $("#selectSourceLangID").html(appendstring);
    }

    function showTargetLangList(data) {
        var obj = jQuery.parseJSON(data);
        var appendstring = "";
        for (var i = 0; i < obj.length; i++) {
            appendstring += "<option value=" + obj[i].TargetLangID + ">" + obj[i].TargetLangName + "</option>";
        }
        $("#selectTargetLangID").html(appendstring);
    }

</script>

```

#### 有两个外键的查询

```c#
public async Task<IActionResult> Index()
        {
            var projectContext= _context.Project.Include(s=> s.SourceLang).Include(s=>s.TargetLang);
            var result = await projectContext.ToListAsync();
            return View(result);
        }
```

