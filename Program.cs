var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        policy => policy.AllowAnyOrigin()
                         .AllowAnyMethod()
                         .AllowAnyHeader());
});

var app = builder.Build();

app.UseCors("AllowAnyOrigin");


List<Child> bd = new List<Child>()
{
    new Child(1,"Логинов Кирилл Денисович",12,"2","Футбол",DateTime.Now,"Подтверждено","+79999999999"),
    new Child(2,"Паролев Кирилл Денисович",12,"2","Футбол",DateTime.Now,"Подтверждено","+79999999999"),
};

app.MapGet("/children", () => bd);

app.MapGet("/children/{id}", (int id) =>
{
    Child child = bd.Find(ch => ch.Id == id);
    if (child == null)
        return Results.NotFound("Ребенок не найден");

    return Results.Json(child);
});

app.MapPost("/children", (Child child) =>
{
    child.Id = bd.Any() ? bd.Max(c => c.Id) + 1 : 1;
    child.RegistrationDateTime = DateTime.Now;
    bd.Add(child);
    return Results.Created($"/children/{child.Id}", child);
});

app.MapDelete("/children/{id}", (int id) =>
{
    var child = bd.FirstOrDefault(c => c.Id == id);
    if (child is null) return Results.NotFound("Ребенок не найден");

    bd.Remove(child);
    return Results.Ok("Ребенок удален");
});

app.MapPut("/children/{id}", (int id, ChildUpdateDto updatedChildDto) =>
{
    var child = bd.FirstOrDefault(c => c.Id == id);

    if (child == null)
        return Results.NotFound("Ребенок не найден");

    if (!string.IsNullOrEmpty(updatedChildDto.FullName))
        child.FullName = updatedChildDto.FullName;
    if (updatedChildDto.Age > 0)
        child.Age = updatedChildDto.Age;
    if (!string.IsNullOrEmpty(updatedChildDto.Squad))
        child.Squad = updatedChildDto.Squad;
    if (!string.IsNullOrEmpty(updatedChildDto.Hobby))
        child.Hobby = updatedChildDto.Hobby;
    if (!string.IsNullOrEmpty(updatedChildDto.IsConfirmed))
        child.IsConfirmed = updatedChildDto.IsConfirmed;
    if (!string.IsNullOrEmpty(updatedChildDto.ParentContact))
        child.ParentContact = updatedChildDto.ParentContact;


    if (updatedChildDto.RegistrationDateTime.HasValue)
        child.RegistrationDateTime = updatedChildDto.RegistrationDateTime;

    return Results.Json(child);
});



app.Run();

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
public class ChildUpdateDto
{
    public string FullName { get; set; }
    public int Age { get; set; }
    public string Squad { get; set; }
    public string Hobby { get; set; }
    public DateTime? RegistrationDateTime { get; set; }
    public string IsConfirmed { get; set; }
    public string ParentContact { get; set; }
}