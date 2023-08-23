using FreeERP.Middlewares;
using FreeERP.Options;
using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);
// builder.Services.AddTransient<MyCustomMiddleware>(); -> new instance for every request
// builder.Services.AddRouting(option => {
//  option.ConstraintMap.Add("months", typeof(MonthsCustomConstraint));
// });
// var builder = WebApllication.CreateBuilder(new WebApplicationOptions() { WebRootPath="myroot" });
//builder.Services.AddControllers(option => {
//    OptionsBuilderConfigurationExtensions.ModelBinderProviders.Insert(0, new TestBinderProvider())
// });
builder.Services.AddControllersWithViews();
builder.Services.Add(new ServiceDescriptor(
    typeof(ICitiesService),
    typeof(CitiesService),
    ServiceLifetime.Transient
));
builder.Services.Configure<WeatherApiOptions>(builder.Configuration.GetSection("weatherapi"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// ExceptionHandler -> HSTS -> HttpsRedirection -> StaticFiles -> Routing
// -> CORS -> Authentication -> Authorization -> Custom Middleware -> Endpoint
// app.UseExceptionHandler("/Error");
// App.UseHsts();
// App.UseHttpsRedirection();

app.UseStaticFiles();
// app.UseStaticFiles(new StaticFileOptions(){ FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "\\mywebroot")) });

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
  });
  endpoints.Map("files/{filename}.{extension=json}", async (context) => {
    context.Request.RouteValues["filename"]
  });
  endpoints.Map("files/{id:minlength(2):int?}", async (context) => {
    context.Request.RouteValues["filename"]
  });
  endpoints.Map("files/{reportDate:datetime}", async (context) => {
    context.Request.RouteValues["filename"]
  });
  endpoints.Map("files/{name:minlength(3)}", async (context) => {
    context.Request.RouteValues["filename"]
  });
  endpoints.Map("files/{custom:months}", async (context) => {
    context.Request.RouteValues["filename"]
  });
  endpoints.Map("/", async (context) => {
    await context.Response.WriteAsync(app.Configuration.GetValue<string>("myKey", "default value"));
  });
 });
 */

app.MapControllers();

app.Run(async context => {
    await context.Response.WriteAsync(
        $"Request received at {context.Request.Path}");
});
