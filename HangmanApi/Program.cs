var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var polishWords = new[]
{
    "kot", "pies", "samochód", "telefon", "dom", "krzesło", "telewizor", "komputer", "ogórek", "dzwonek"
};

var englishWords = new[]
{
    "cat", "dog", "car", "phone", "house", "chair", "television", "computer", "cucumber", "bell"
};

app.MapGet("/randomword", (string lang) =>
{
    var random = new Random();

    switch (lang.ToLower())
    {
        case "eng":
            var englishRandomIndex = random.Next(0, englishWords.Length);
            return englishWords[englishRandomIndex];
        case "pl":
            var polishRandomIndex = random.Next(0, polishWords.Length);
            return polishWords[polishRandomIndex];
        default:
            throw new ArgumentException("Invalid language specified. Please use 'english' or 'polish'.");
    }
})
.WithName("GetRandomWord")
.WithOpenApi();

app.Run();
