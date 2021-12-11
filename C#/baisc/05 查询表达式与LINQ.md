# 查询表达式与LINQ

### 查询表达式

只要实现IEnumerable接口，都可以进行查询

```c#
var x=Directory.GetFiles("c:\\").Select(filepath => {
                 var file = new FileInfo(filepath);
                 return new { file.Name, file.Length };
             });

		IEnumerable<Person> personlist = new Person[]
            {
                new Person {Name="a", Age=10 },
                new Person {Name="b", Age=20 },
                new Person {Name="c", Age=15 },
                new Person {Name="d", Age=18 },
            };
            personlist = personlist.Where<Person>(p => p.Age < 15);
```

模仿数据库查询

1）数据初始化

```c#
 public class Teacher
    {
        public int TeacherID { get; set; }
        public string TName { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }
        public int[] StudentID { get; set; }
        public override string ToString()
        {
            return this.TName + this.Age ;
        }
    }

    public class Student
    {
        public int StudentID { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
        public int TeacherID { get; set; }
        public override string ToString()
        {
            return this.Name + this.Age;
        }
    }

    static public class SchoolData
    {
        

        public static IEnumerable<Teacher> GetTData ()
        {
            return new Teacher[] {
                new Teacher
                {
                    TeacherID=1,
                    TName="1name",
                    Sex="male",
                    Age=30,
                    StudentID=new int[]{1,2 }
                },
                new Teacher
                {
                    TeacherID=2,
                    TName="2name",
                    Sex="female",
                    Age=37,
                    StudentID=new int[]{1}
                },
                new Teacher
                {
                    TeacherID=3,
                    TName="3name",
                    Sex="male",
                    Age=34,
                    StudentID=new int[]{1,2 }
                },
                new Teacher
                {
                    TeacherID=4,
                    TName="4name",
                    Sex="female",
                    Age=29,
                    StudentID=new int[]{1,2 }
                }

            }; 
            
        }
```

2 查询

```c#
class Program
    {
        static void Main(string[] args)
        {

            IEnumerable<Student> studentlist = SchoolData.GetSData();
            IEnumerable<Teacher> teacherlist = SchoolData.GetTData();
            //Where 条件判断
            studentlist =studentlist.Where(student => student.Age > 30).Where(student=> student.Sex=="male");
            //Join 联合主键检查，注意同名时的处理TAge=teacher.Age
            var x = studentlist.Join(teacherlist, student => student.TeacherID, teacher => teacher.TeacherID, (student, teacher) => new { student.Name, student.Age,teacher.TName, TAge=teacher.Age }) ;
            //Select 来重新选择字段
            var y = teacherlist.Select(teacher => new { teacher.TName, teacher.Age});
            //二次排序
            var y = teacherlist.OrderBy(teacher => teacher.Sex).ThenBy(teacher => teacher.Age);

            print(x);
            Console.ReadLine();
            
        }
        static void print<T>(IEnumerable<T> list)
        {
            list.ForEach(p => Console.WriteLine(p));
        }
    }
```



### LINQ

基于上面的数据进行查询



```c#
class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<Student> studentlist = SchoolData.GetSData();
            IEnumerable<Teacher> teacherlist = SchoolData.GetTData();
            var x = from teacher in teacherlist
                    where teacher.Age > 30
                    select teacher;

            var y = from teacher in teacherlist
                    orderby teacher.Sex, teacher.TName
                    select new { teacher.TName, teacher.Sex,teacher.Age };
        print(y);
        Console.ReadLine();
    }
```

