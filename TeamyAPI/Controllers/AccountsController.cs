using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using TeamyAPI.ModelsEF;

namespace TeamyAPI.Controllers;

[Route("api/{action}")]

public class AccountsController : ControllerBase
{
    private const string DATE_Format = "yyyy-MM-dd";

    [HttpPost]
    public ActionResult Login(string uid, string pwd)
    {
        if (string.IsNullOrWhiteSpace(uid) || string.IsNullOrWhiteSpace(pwd))
            return StatusCode(StatusCodes.Status400BadRequest);
        try
        {
            using (var context = new TeamyDBContext())
            {
                User user = context.Users
                    .Where(u => u.LoginName.Equals(uid))
                    .FirstOrDefault<ModelsEF.User>();
                if (user != null)
                {
                    if (CompareHash(pwd, user.PasswordHash, user.Salt))
                        return new JsonResult(new { id = user.Iduser });
                    else
                        return Unauthorized();
                }
                else
                {
                    return StatusCode(StatusCodes.Status401Unauthorized);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost]
    public ActionResult Register(string uid, string pwd)
    {
        if(string.IsNullOrWhiteSpace(uid) || string.IsNullOrWhiteSpace(pwd))
            return StatusCode(StatusCodes.Status400BadRequest);
        try
        {
            var salt = GetSalt();
            byte[] pwdhash = GetHash(pwd, salt);

            using (var context = new TeamyDBContext())
            {
                context.Users.Add(new User()
                {
                    LoginName = uid,
                    PasswordHash = pwdhash,
                    Salt = salt,
                    DateCreated = DateTime.Now.ToString(DATE_Format)
                });
                context.SaveChanges();

                return StatusCode(StatusCodes.Status201Created);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
            return StatusCode(500);
        }
    }

    private static string GetSalt()
    {
        var random = RandomNumberGenerator.Create();

        // Maximum length of salt
        int max_length = 36;

        // Empty salt array
        byte[] salt = new byte[max_length];

        // Build the random bytes
        random.GetNonZeroBytes(salt);

        // Return the string encoded salt
        return Convert.ToBase64String(salt).Substring(0, 31);
    }

    public static byte[] GetHash(string password, string salt)
    {
        byte[] unhashedBytes = Encoding.Unicode.GetBytes(String.Concat(salt, password));

        using (SHA512 sHA512 = SHA512.Create())
        {
            byte[] hashedBytes = sHA512.ComputeHash(unhashedBytes);
            return hashedBytes;
        }
    }

    public static bool CompareHash(string attemptedPassword, byte[] hash, string salt)
    {
        string base64Hash = Convert.ToBase64String(hash);
        string base64AttemptedHash = Convert.ToBase64String(GetHash(attemptedPassword, salt));

        return base64Hash == base64AttemptedHash;
    }
}

