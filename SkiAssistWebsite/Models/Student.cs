namespace SkiAssistWebsite.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string TicketNumber { get; set; }
        public string CurrentClass { get; set; }
        public string Teacher { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public int StudentAge { get; set; }
        public string SearchString { get; set; }

        public Student(int studentId, string ticketNum, string currentCurrentClass, string teacher, string studentFirstName, string studentLastName, int studentAge)
        {
            StudentId = studentId;
            TicketNumber = ticketNum; 
            CurrentClass = currentCurrentClass;
            Teacher = teacher;
            StudentFirstName = studentFirstName;
            StudentLastName = studentLastName;
            StudentAge = studentAge;
        }
    }
}