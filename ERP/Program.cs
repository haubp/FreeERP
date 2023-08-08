using FreeERP.Middlewares;

var builder = WebApplication.CreateBuilder(args);
// builder.Services.AddTransient<MyCustomMiddleware>(); -> new instance for every request
builder.Services.AddControllersWithViews();

var app = builder.Build();

// ExceptionHandler -> HSTS -> HttpsRedirection -> StaticFiles -> Routing
// -> CORS -> Authentication -> Authorization -> Custom Middleware -> Endpoint
// app.UseExceptionHandler("/Error");
// App.UseHsts();
// App.UseHttpsRedirection();

app.UseStaticFiles();

// app.UseRouting();
// app.UseCors();
// app.UseAuthentication();
// app.UseAuthorization();
// app.UseSession();

/*
 app.UseMyCustomMiddleware();
 app.UseHelloCustomMiddleware();
 */

/*
 app.Use(async (HttpContext context, RequestDelegate next) => {
    await next(context);
});

 app.Run(async (HttpContext context) => {
    await context.Response.WriteAsync("Hello");
});

 app.UseWhen(
  context => context.Request.Query.ContainsKey("username"),
  app => {
    app.Use(async (context, next) => {
      await context.Response.WriteAsync("");
      await next();
    })
  }
 );
 */

/*
 app.UseEndpoint(endpoints => {
  endpoints.Map("/map1", async (context) => {
    await context.Response.WriteAsync("In Map 1");
  endpoints.Map("files/{filename}.{extension=json}", async (context) => {
    context.Request.RouteValues["filename"]
  });
  });
 });
 */

app.MapControllers();

app.Run(async context => {
    await context.Response.WriteAsync(
        $"Request received at {context.Request.Path}");
});
