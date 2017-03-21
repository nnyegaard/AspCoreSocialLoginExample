namespace AspNetCoreSocialLoginExample.Infrastructure
{
    using AspNet.Security.OAuth.LinkedIn;
    using Microsoft.AspNetCore.Authentication.OAuth;
    using Microsoft.AspNetCore.Builder;
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public static class AuthenticationSettings
    {
        public static FacebookOptions FacebookOptions(string appId, string appSecret)
        {
            if (string.IsNullOrEmpty(appId))
            {
                throw new ArgumentNullException($"{nameof(appId)} is null or empty");
            }

            if (string.IsNullOrEmpty(appSecret))
            {
                throw new ArgumentNullException($"{nameof(appSecret)} is null or empty");
            }

            FacebookOptions options = new FacebookOptions
            {
                AppId = appId,
                AppSecret = appSecret,
                SaveTokens = true,
                Scope = { "email", "user_friends" },
                Events = new OAuthEvents
                {
                    OnCreatingTicket = ctx =>
                    {
                        // Get a Facebook specific claim (This will be added to the Name claim
                        string facebookFirstname = ctx.User.GetValue("first_name").ToObject<string>();

                        ctx.Identity.AddClaim(new Claim("access_token", ctx.AccessToken));
                        ctx.Identity.AddClaim(new Claim("refresh_token", ctx.AccessToken));
                        ctx.Identity.AddClaim(new Claim("ExpiresIn", ctx.ExpiresIn.Value.Seconds.ToString()));
                        ctx.Identity.AddClaim(new Claim("FBFirstname", facebookFirstname)); // Add custom claim to the identity

                        return Task.FromResult(0);
                    },
                    OnRemoteFailure = ctx =>
                    {
                        // Handle remote errors

                        return Task.FromResult(0);
                    }
                }
            };

            return options;
        }

        public static LinkedInAuthenticationOptions LinkedInOptions(string clientId, string clientSecret)
        {
            if (string.IsNullOrEmpty(clientId))
            {
                throw new ArgumentNullException($"{nameof(clientId)} is null or empty");
            }

            if (string.IsNullOrEmpty(clientSecret))
            {
                throw new ArgumentNullException($"{nameof(clientSecret)} is null or empty");
            }

            LinkedInAuthenticationOptions options = new LinkedInAuthenticationOptions
            {
                ClientId = clientId,
                ClientSecret = clientSecret,
                SaveTokens = true,
                Events = new OAuthEvents
                {
                    OnCreatingTicket = ctx =>
                    {
                        ctx.Identity.AddClaim(new Claim("access_token", ctx.AccessToken));
                        ctx.Identity.AddClaim(new Claim("refresh_token", ctx.AccessToken));
                        ctx.Identity.AddClaim(new Claim("ExpiresIn", ctx.ExpiresIn.Value.Seconds.ToString()));

                        return Task.FromResult(0);
                    },
                    OnRemoteFailure = ctx =>
                    {
                        // Handle remote errors

                        return Task.FromResult(0);
                    }
                }
            };

            return options;
        }
    }
}
