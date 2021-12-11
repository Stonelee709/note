https://www.entityframeworktutorial.net/

# Code First

建立 Model 类

```c#
public class Album
    {
        public int AlbumID { get; set; }
        public string AlbumName { get; set; }
        public List<Song> Songlist { get; set; }
    }
public class Song
    {
        public int SongID { set; get; }
        public string SongName { get; set; }
        public string SongSinger { get; set; }
        public int price { get; set; }
        public int AlbumID { get; set; }
        public Album Album { get; set; }
    }
```

建立 Data 文件夹并添加 Context 类

```c#
 public class MusicStoreContext:DbContext
    {
        public MusicStoreContext(DbContextOptions<MusicStoreContext> options)
            : base(options)
        {
        }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Song> Songs { get; set; }
    }
```

在 AppSetting 中添加数据库连接字符串：

```
"ConnectionStrings": { "MusicStoreContext": "Server=tcp:dgstest.database.chinacloudapi.cn,1433;Initial Catalog=DGSTest;Persist Security Info=False;User ID=dgstest;Password=pass@123456789;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;" }
```

在 Startup.cs  的ConfigureServices中添加：：这步实际上是向 IOC 容器添加依赖注入 MusicStoreContext，所以后续可以通过构建器来初始化：  public SongsController(MusicStoreContext context)    { _context = context;}

```
services.AddDbContext<MusicStoreContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("MusicStoreContext")));
```

依赖包：

1. EntityFrameworkCore
2. EntityFrameworkCore.SQLSERVER
3. EntityFrameworkCore.TOOLS

在程序包管理器控制台，进行迁移

```
EntityFrameworkCore\Add-Migration 1st
//执行此命令前，如果数据库是在 AZURE 上，需要设置数据库的防火墙
EntityFrameworkCore\Update-Database
```

**注意：如果出现 build Failed 时，可能是代码有问题，需要选编译运行一下代码看看错误在哪里**

Controller

```c#
public class SongsController : Controller
{
    private readonly MusicStoreContext _context;

    public SongsController(MusicStoreContext context)
    {
        _context = context;
    }

    // GET: Songs
    public async Task<IActionResult> Index()
    {
        var musicStoreContext = _context.Songs.Include(s => s.Album);
        return View(await musicStoreContext.ToListAsync());
    }
    public async Task<IActionResult> Search(string condition)
    {
        var musicStoreContext = _context.Songs.Where(s=>s.SongName.Contains(condition));
        return View(await musicStoreContext.ToListAsync());
    }

    // GET: Songs/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var song = await _context.Songs
            .Include(s => s.Album)
            .FirstOrDefaultAsync(m => m.SongID == id);
        if (song == null)
        {
            return NotFound();
        }

        return View(song);
    }

    // GET: Songs/Create
    public IActionResult Create()
    {
        ViewData["AlbumID"] = new SelectList(_context.Albums, "AlbumID", "AlbumID");
        ViewData["AlbumName"] = new SelectList(_context.Albums, "AlbumID", "AlbumName");
        return View();
    }

    // POST: Songs/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("SongID,SongName,SongSinger,price,AlbumID")] Song song)
    {
        if (ModelState.IsValid)
        {
            _context.Add(song);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["AlbumID"] = new SelectList(_context.Albums, "AlbumID", "AlbumID", song.AlbumID);
        return View(song);
    }

    // GET: Songs/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var song = await _context.Songs.FindAsync(id);
        if (song == null)
        {
            return NotFound();
        }
        ViewData["AlbumID"] = new SelectList(_context.Albums, "AlbumID", "AlbumID", song.AlbumID);
        ViewData["AlbumName"] = new SelectList(_context.Albums, "AlbumID", "AlbumName");
        return View(song);
    }

    // POST: Songs/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("SongID,SongName,SongSinger,price,AlbumID")] Song song)
    {
        if (id != song.SongID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(song);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SongExists(song.SongID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["AlbumID"] = new SelectList(_context.Albums, "AlbumID", "AlbumID", song.AlbumID);
        return View(song);
    }

    // GET: Songs/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var song = await _context.Songs
            .Include(s => s.Album)
            .FirstOrDefaultAsync(m => m.SongID == id);
        if (song == null)
        {
            return NotFound();
        }

        return View(song);
    }

    // POST: Songs/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var song = await _context.Songs.FindAsync(id);
        _context.Songs.Remove(song);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool SongExists(int id)
    {
        return _context.Songs.Any(e => e.SongID == id);
    }
}
```