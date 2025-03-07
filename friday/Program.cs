using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

List<Videocard> repo = [];

app.MapGet("/", () => repo);
app.MapPost("/post", (Videocard dto) => repo.Add(dto));
app.MapPut("/update", ([FromQuery]int id, UpdateVcDTO dto) =>
{
    Videocard buffer = repo.Find(v => v.Id == id);
    if (buffer != null)
    {
        if (buffer.Id != dto.id)
            buffer.Id = dto.id;
        if (buffer.Brand != dto.brand)
            buffer.Brand = dto.brand;
        if (buffer.Series != dto.series)
            buffer.Series = dto.series;
        if (buffer.Model != dto.model)
            buffer.Model = dto.model;
        if (buffer.Memory != dto.memory)
            buffer.Memory = dto.memory;
        return Results.Json(buffer);
    }
    else return Results.NotFound();
});
app.MapDelete("/delete", ([FromQuery]int id) =>
{
    Videocard buffer = repo.Find(v => v.Id == id);
    return repo.Remove(buffer);
});



app.Run();

public class Videocard
{
    public int Id { get; set; }
    public string Brand { get; set; }
    public string Series { get; set; }
    public string Model { get; set; }
    public string Memory { get; set; }
    public DateOnly ReleaseDate { get; set; }
};

record class UpdateVcDTO(int id, string brand, string series, string model, string memory);