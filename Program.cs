var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
List<Child> bd = new List<Child>()
{
    new Child(1,"Логинов Кирилл Денисович",12,"2","Футбол",DateTime.Now,"Подтверждено","+79995"),
};
class Child
{
    private int id;
    private string fullName;
    private int age;
    private string squad;
    private string hobby;
    private DateTime? registrationDateTime;
    private string isConfirmed;
    private string parentContact;

    public Child(int id, string fullName, int age, string squad, string hobby, DateTime? registrationDateTime, string isConfirmed, string parentContact)
    {
        this.id = id;
        this.fullName = fullName;
        this.age = age;
        this.squad = squad;
        this.hobby = hobby;
        this.registrationDateTime = registrationDateTime;
        this.isConfirmed = isConfirmed;
        this.parentContact = parentContact;
    }

    public int Id { get => id; set => id = value; }
    public string FullName { get => fullName; set => fullName = value; }
    public int Age { get => age; set => age = value; }
    public string Squad { get => squad; set => squad = value; }
    public string Hobby { get => hobby; set => hobby = value; }
    public DateTime? RegistrationDateTime { get => registrationDateTime; set => registrationDateTime = value; }
    public string IsConfirmed { get => isConfirmed; set => isConfirmed = value; }
    public string ParentContact { get => parentContact; set => parentContact = value; }
}