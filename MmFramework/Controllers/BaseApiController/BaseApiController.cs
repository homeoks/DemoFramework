using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using MmFramework.Auth;
using Repository.BaseRepository;

namespace MFramework.Controllers.BaseApiController
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableCors("AllowAllCorsPolicy")]
    public class BaseApiController : ControllerBase
    {
        protected const string ClientEndpoint = "api/client/";
        private UserJsonPrincipal UserPrincipal { get; set; }

        public BaseApiController()
        {
        }

        protected List<string> GetModelErrors()
        {
            var errors = new List<string>();
            foreach (var value in ModelState.Values)
            {
                foreach (var error in value.Errors)
                {
                    errors.Add(error.ErrorMessage);
                }
            }
            return errors;
        }

        protected string GetHost()
        {
            return $"{Request.Scheme}://{Request.Host.Value}";
        }

        protected IActionResult Result(Func<ApplicationResult> doFunc)
        {
            try
            {
                var result = doFunc();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ApplicationResult.Fail($"{ex.Message} {ex.StackTrace} {ex.InnerException?.Message}"));
            }
        }

        protected IActionResult Result<T>(Func<ApplicationResult<T>> doFunc)
        {
            try
            {
                var result = doFunc();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ApplicationResult.Fail<T>($"{ex.Message} {ex.StackTrace} {ex.InnerException?.Message}"));
            }
        }
        //protected byte[] ResizeImg(Image img)
        //{
        //    var configuration = ApplicationSetting.Get();

        //    var defaultH = Double.Parse(configuration.GetSection("ResizeImg")["Height"]);
        //    var defaultW = Double.Parse(configuration.GetSection("ResizeImg")["Width"]);
        //    var newH = 0;
        //    var newW = 0;
        //    var devide = 1.0;
        //    if (img.Width < defaultW && img.Height < defaultH)
        //    {
        //        newH = img.Height;
        //        newW = img.Width;
        //    }
        //    else
        //    if (img.Height > img.Width)
        //    {
        //        devide = img.Height / defaultH;
        //        newH = (int)defaultH;
        //        newW = (int)(img.Width / devide);
        //    }
        //    else
        //    {
        //        devide = img.Width / defaultW;
        //        newW = (int)defaultW;
        //        newH = (int)(img.Height / devide);
        //    }


        //    using (var b = new Bitmap(img, new Size(newW, newH)))
        //    {
        //        using (var ms2 = new MemoryStream())
        //        {
        //            b.Save(ms2, System.Drawing.Imaging.ImageFormat.Jpeg);
        //            return ms2.ToArray();
        //        }
        //    }
        //}
        protected UserJsonPrincipal CurrentUser
        {
            get
            {
                if (UserPrincipal == null)
                {
                    var token = (HttpContext.Request.Headers.Any(x => x.Key == "Authorization")) ? HttpContext.Request.Headers.Where(x => x.Key == "Authorization")?.FirstOrDefault().Value.SingleOrDefault()?.Replace("Bearer ", "").Replace("bearer ", "") : "";
                    JsonWebToken jwt;
                    try
                    {
                        jwt = new JsonWebToken(token);
                    }
                    catch (Exception e)
                    {
                        ApplicationLogger.Log(e,"CurrentUser");
                        return new UserJsonPrincipal();
                    }
                    var claims = jwt.Claims;

                  
                    //var userId = payload.("sub");
                    //var nameidentifier = payload.Root.Value<string>(xml2005 + "nameidentifier");
                    //var role = payload.Root.Value<string>(xml2008 + "role");
                    //var givenname = payload.Root.Value<string>(xml2005 + "givenname");
                    //var country = payload.Root.Value<string>(xml2005 + "country");
                    //var emailaddress = payload.Root.Value<string>(xml2005 + "emailaddress");


                    //claims.Add(new Claim("nameidentifier", nameidentifier));
                    //claims.Add(new Claim("role", role));
                    //claims.Add(new Claim("givenname", givenname));
                    //claims.Add(new Claim("country", country));
                    //claims.Add(new Claim("emailaddress", emailaddress));

                    UserPrincipal = new UserJsonPrincipal()
                    {
                        Id = claims.First(x=>x.Type== JwtRegisteredClaimNames.Sub).Value,
                        UserName = claims.First(x => x.Type == ClaimTypes.Name).Value,
                        Claims = claims
                    };
                }

                StaticUser.UserId = UserPrincipal.Id;
                return UserPrincipal;
            }
        }

    }
}