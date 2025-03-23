//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.Extensions.Options;
//using Microsoft.IdentityModel.Tokens;
//using RestWithASPNET.Domain.Configuration;
//using System.Text;

namespace RestWithASPNET.WebApi.Configuration
{
    public static class AuthorizationRestUdemyConfiguration
    {
        public static void AddAuthorizationRestUdemyConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            //var tokenConfigurations = new TokenConfiguration();
            //new ConfigureFromConfigurationOptions<TokenConfiguration>(
            //        configuration.GetSection("TokenConfigurations")
            //        ).Configure(tokenConfigurations);

            //services.AddSingleton(tokenConfigurations);

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = "JwtBearer";
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //    .AddJwtBearer(Options =>
            //    {
            //        Options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuer = true,
            //            ValidateAudience = true,
            //            ValidateLifetime = true,
            //            ValidateIssuerSigningKey = true,
            //            ValidIssuer = tokenConfigurations.Issuer,
            //            ValidAudience = tokenConfigurations.Audience,
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfigurations.Secret)),
            //        };
            //    });

            //services.AddAuthorization(auth =>
            //{
            //    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
            //        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
            //        .RequireAuthenticatedUser().Build());
            //});

            services.AddAuthorization(opts =>
            {
                opts.AddPolicy("Vendas", policy =>
                       policy.RequireAssertion(context =>
                            context.User.IsInRole("Backoffice Administrador")
                        || context.User.IsInRole("Backoffice Gerente de Loja")
                            || context.User.IsInRole("Backoffice Vendedor")
                            || context.User.IsInRole("Backoffice Analista de compras")));

                opts.AddPolicy("Financeiro", policy =>
                       policy.RequireAssertion(context =>
                            context.User.IsInRole("Backoffice Administrador")
                            || context.User.IsInRole("Backoffice Analista Financeiro")));

                opts.AddPolicy("Logistica", policy =>
                        policy.RequireAssertion(context =>
                        context.User.IsInRole("Backoffice Administrador")
                        || context.User.IsInRole("Backoffice Analista de Operacoes")
                        || context.User.IsInRole("Backoffice Gerente de Loja")
                        || context.User.IsInRole("Backoffice Analista de compras")));
            });
        }
    }
}