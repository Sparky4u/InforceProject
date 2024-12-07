using InforceProject.Server.Data;

namespace InforceProject.Server.Models.AboutInfoModel
{
    public class AboutInfoSeed
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<UrlContext>();

                if (!context.AboutInfos.Any())
                {
                    context.AboutInfos.Add(new AboutInfo
                    {
                        Text = "Welcome to the URL Shortener! You can shorten any URL and easily share it with others. Simply paste your long URL into the input field, and we'll give you a short link to use. Enjoy!"
                    });

                    context.SaveChanges();
                }
            }
        }
    }

}
