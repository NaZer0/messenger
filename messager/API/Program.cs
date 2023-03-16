using Microsoft.OpenApi.Models;
using Messenger.Domain;
using System.Reflection.Metadata.Ecma335;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "OpenChat API",
    });
});
var app = builder.Build();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});
app.UseSwagger(x => x.SerializeAsV2 = true);

//API

app.MapPost("/api/registeruser", (string login, string passwrd) =>
{
    User user = new User(login, passwrd);
    bool other = Messenger.Application.User.Commands.UserCommand.CreateUser(user);
    return other ? new Text("Create") : new Text("invalid username or password");
});

app.MapPost("/api/loginuser", (string login, string passwrd) =>
{
    User user = new User(login, passwrd);
    int? session = Messenger.Application.User.Commands.UserCommand.LoginUser(user);
    return session != null ? new Text(session.ToString()) : new Text("invalid username or password");
});

app.MapPost("/api/createmessage", (string session, string text) =>
{
    //!!!возможность создать сообщение несуществующему пользователю 
    bool other = Messenger.Application.Messnge.Commands.MessangeCommand.CreateMessnge(session, text);
    return session != null ? new Text("true") : new Text("false");
});

app.MapGet("/api/getmessangetime", (DateTime time) =>
{
    List<Messang> mess = Messenger.Application.Messnge.Commands.MessangeCommand.GetMessanges(time);
    return mess;
});

app.MapGet("/api/getmessangenumber", (int number) =>
{
    List<Messang> mess = Messenger.Application.Messnge.Commands.MessangeCommand.GetMessanges(number);
    return mess;
});

//files
app.MapGet("/html", (context) => {
    var response = context.Response;
    response.ContentType = "text/html; charset=utf-8";
    return response.SendFileAsync("index.html");
});

app.Run();
