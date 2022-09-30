using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using System.Text;
using System;
using System.Security.Claims;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public BasicAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock
        )
: base(options, logger, encoder, clock)
    {
    }
 
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        Response.Headers.Add("WWW-Authenticate", "Basic");
 
        if (!Request.Headers.ContainsKey("Authorization"))
        {
            return Task.FromResult(AuthenticateResult.Fail("Authorization header missing."));
        }
 
        // Get authorization key
        var authorizationHeader = Request.Headers["Authorization"].ToString();
        var authHeaderRegex = new Regex(@"Basic (.*)");
 
        if (!authHeaderRegex.IsMatch(authorizationHeader))
        {
            return Task.FromResult(AuthenticateResult.Fail("Authorization code not formatted properly."));
        }
 
        var authBase64 = Encoding.UTF8.GetString(Convert.FromBase64String(authHeaderRegex.Replace(authorizationHeader, "$1")));
        var authSplit = authBase64.Split(Convert.ToChar(":"), 2);
        var authUsername = authSplit[0];
        var authPassword = authSplit.Length > 1 ? authSplit[1] : throw new Exception("Unable to get password");
        Singleton objectSingleton=Singleton.Instance;
  
        if (authUsername != objectSingleton.Username || authPassword != objectSingleton.Password)
        {
            Console.WriteLine("WRONG : "+objectSingleton.Username);
            return Task.FromResult(AuthenticateResult.Fail("The username or password is not correct."));
        }
 
        Console.WriteLine("RIGHT : "+objectSingleton.Username);
        var authenticatedUser = new AuthenticatedUser("BasicAuthentication", true, "admin");
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(authenticatedUser));
 
        return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
    }
}
