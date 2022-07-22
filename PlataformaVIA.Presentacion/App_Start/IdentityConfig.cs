namespace PlataformaVIA.Presentacion
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;
    using Microsoft.Owin.Security;
    using Presentacion.Models;
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
    : base(store)
        {
        }

        private int PASSWORD_HISTORY_LIMIT;

        private async Task<bool> IsPasswordHistory(string userId, string newPassword)
        {
            using (var context = new ApplicationDbContext())
            {
                string resultParameter = context.Database.SqlQuery<String>("exec Parametro_GetParametro @p0, @p1", 0, "CantidadPasswordNoUsar").FirstOrDefault();
                if (!string.IsNullOrEmpty(resultParameter))
                {
                    PASSWORD_HISTORY_LIMIT = Convert.ToInt32(resultParameter);
                }
                else {
                    PASSWORD_HISTORY_LIMIT = 3;
                }
            }

           var user = await FindByIdAsync(userId);
            if (user.PasswordHistory != null)
            {
                if (user.PasswordHistory.OrderByDescending(o => o.CreatedDate)
                    .Select(s => s.PasswordHash)
                    .Take(PASSWORD_HISTORY_LIMIT)
                    .Where(w => PasswordHasher.VerifyHashedPassword(w, newPassword) != PasswordVerificationResult.Failed).Any())
                    return true;
            }
            return false;
        }

        private async Task<bool> IsPasswordChangedToday(string userId) {
            var user = await FindByIdAsync(userId);
            if (user.PasswordHistory.Where(x=> x.CreatedDate.Date == DateTime.Now.Date).ToList().Count > 0)
                return true;
            return false;
        }

        public Task AddToPasswordHistoryAsync(ApplicationUser user, string password)
        {
            user.PasswordHistory.Add(new PasswordHistory()
            {
                UserId = user.Id,
                PasswordHash = password
            });
            return UpdateAsync(user);
        }

        public override async Task<IdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            using (var context = new ApplicationDbContext())
            {
                string resultParameter = context.Database.SqlQuery<String>("exec Parametro_GetParametro @p0, @p1", 0, "CantidadPasswordNoUsar").FirstOrDefault();
                if (!string.IsNullOrEmpty(resultParameter))
                {
                    PASSWORD_HISTORY_LIMIT = Convert.ToInt32(resultParameter);
                }
                else
                {
                    PASSWORD_HISTORY_LIMIT = 3;
                }
            }

            if (await IsPasswordHistory(userId, newPassword))
                return await Task.FromResult(IdentityResult.Failed( string.Format("No puede volver a usar las ultimas {0} contraseñas", PASSWORD_HISTORY_LIMIT)));
            if (await IsPasswordChangedToday(userId))
                return await Task.FromResult(IdentityResult.Failed(string.Format("Solo es posible cambiar la contraseña una vez al día")));
            var result = await base.ChangePasswordAsync(userId, currentPassword, newPassword); if (result.Succeeded)
            {
                ApplicationUser user = await FindByIdAsync(userId);
                user.PasswordHistory.Add(new PasswordHistory()
                {
                    UserId = user.Id,
                    PasswordHash = PasswordHasher.HashPassword(newPassword)
                });
                return await UpdateAsync(user);
            }
            return result;
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 10,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(10);
            manager.MaxFailedAccessAttemptsBeforeLockout = 3;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                int tiempoExpiracionTokenEnMinutos = 0;
                using (var contextDB = new ApplicationDbContext())
                {
                    string resultParameter = contextDB.Database.SqlQuery<String>("exec Parametro_GetParametro @p0, @p1", 0, "TiempoExpiracionTokenDeCorreo").FirstOrDefault();
                    if (!string.IsNullOrEmpty(resultParameter))
                    {
                        tiempoExpiracionTokenEnMinutos = Convert.ToInt32(resultParameter);
                    }
                    else
                    {
                        tiempoExpiracionTokenEnMinutos = 30;
                    }
                }

                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity")) { TokenLifespan = TimeSpan.FromMinutes(tiempoExpiracionTokenEnMinutos) };
            }
            return manager;
        }
    }

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
