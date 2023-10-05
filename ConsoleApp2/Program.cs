using System.Runtime.CompilerServices;

var teachers = new List<Teacher>(); // Заполнить из файла Учетиля.txt
var students = new List<Student>(); // Заполнить из файла Ученики.txt
var exams = new List<Exams>();

teachers = GetTeacherFromFile(@"C:\Users\IT\source\repos\ConsoleApp1\ConsoleApp2\Teacher.txt");

//1. Найти учителя у которого в классе меньше всего учеников 
//2. Найти средний бал экзамена по Физики за 2023 год.		
//3. Получить количество учиников которые по экзамену Математики получили больше 90 баллов, где учитель AleList
List<Teacher> GetTeacherFromFile(string path)
{
    var teachers = new List<Teacher>();
    string[] lines = File.ReadAllLines(path);
    for (int i = 1; i < lines.Length; i++)
    {
        if (!string.IsNullOrEmpty(lines[i].Split("\t")[0]) || !string.IsNullOrEmpty(lines[i].Split("\t")[1]) || !string.IsNullOrEmpty(lines[i].Split("\t")[2]))
        {
                teachers.Add(new Teacher
                {
                    Name = lines[i].Split("\t")[0],
                    LastName = lines[i].Split("\t")[1],
                    Age = Convert.ToInt16(lines[i].Split("\t")[2])
                });
        }

    } 
    return teachers;
}
public class Person
{
    public long ID { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
}

public class Teacher : Person
{
    public LessonType Lesson { get; set; }
}

public class Student : Person
{

}

public class Exams
{
    public LessonType Lesson { get; set; }

    public long StudentId { get; set; }
    public long TeacherId { get; set; }

    public decimal Score { get; set; }
    public DateTime ExamDate { get; set; }

    public Student Student { get; set; }
    public Teacher Teacher { get; set; }
}

public enum LessonType
{
    Mathematics = 1,
    Physics = 2
}