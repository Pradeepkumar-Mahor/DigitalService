﻿using DigitalService.Domain.DbContext;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DigitalService.Domain.BaseModel
{
    public class AuditableSignInManager<TUser> : SignInManager<TUser> where TUser : class
    {
        private readonly UserManager<TUser> _userManager;

        private readonly ApplicationDbContext _db;

        private readonly IHttpContextAccessor _contextAccessor;

        public AuditableSignInManager(UserManager<TUser> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<TUser> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<TUser>> logger, ApplicationDbContext dbContext)
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, null)
        {
            if (userManager == null)
                throw new ArgumentNullException(nameof(userManager));

            if (dbContext == null)
                throw new ArgumentNullException(nameof(dbContext));

            if (contextAccessor == null)
                throw new ArgumentNullException(nameof(contextAccessor));

            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _db = dbContext;
        }

        public override async Task<SignInResult> PasswordSignInAsync(TUser user, string password, bool isPersistent, bool lockoutOnFailure)
        {
            var result = await base.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);

            var appUser = user as IdentityUser;

            if (appUser != null) // We can only log an audit record if we can access the user object and it's ID
            {
                var ip = _contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

                UserAudit auditRecord = null;

                switch (result.ToString())
                {
                    case "Succeeded":
                        auditRecord = UserAudit.CreateAuditEvent(appUser.Id, UserAuditEventType.Login, ip);
                        break;

                    case "Failed":
                        auditRecord = UserAudit.CreateAuditEvent(appUser.Id, UserAuditEventType.FailedLogin, ip);
                        break;
                }

                if (auditRecord != null)
                {
                    _db.UserAuditEvents.Add(auditRecord);
                    await _db.SaveChangesAsync();
                }
            }

            return result;
        }

        public override async Task SignOutAsync()
        {
            var authFeature = _contextAccessor.HttpContext.Features.Get<IAuthenticationFeature>();
            var authScheme = authFeature?.OriginalPath;

            if (authScheme != null)
            {
                await _contextAccessor.HttpContext.SignOutAsync(authScheme);
            }
            else
            {
                // Handle the case where the authentication scheme is not found
                await _contextAccessor.HttpContext.SignOutAsync();
            }

            //_ = await _contextAccessor.HttpContext.SignOutAsync(_contextAccessor.HttpContext.Features.Get<IAuthenticationFeature>().OriginalPath.HasValue.Ticket?.AuthenticationScheme);

            var user = await _userManager.FindByIdAsync(_userManager.GetUserId(_contextAccessor.HttpContext.User)) as IdentityUser;

            if (user != null)
            {
                var ip = _contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

                var auditRecord = UserAudit.CreateAuditEvent(user.Id, UserAuditEventType.LogOut, ip);
                _db.UserAuditEvents.Add(auditRecord);
                await _db.SaveChangesAsync();
            }
        }
    }
}