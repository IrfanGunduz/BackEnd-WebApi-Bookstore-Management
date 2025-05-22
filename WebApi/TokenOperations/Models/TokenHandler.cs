using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.TokenOperations.Models;

namespace WebApi.TokenOperations{

    public class TokenHandler{
        public IConfiguration Configuration {get; set;}
        private readonly IBookStoreDbContext _context;
        public TokenHandler(IConfiguration configuration, IBookStoreDbContext context)
        {
            Configuration = configuration;
            _context = context;
        }

        public Token CreateAccessToken(User user){
            Token tokenModel = new Token();
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"]));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            tokenModel.Expiration = DateTime.Now.AddMinutes(15);

            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: Configuration["Token:Issuer"],
                audience: Configuration["Token:Audience"],
                expires: tokenModel.Expiration,
                notBefore: DateTime.Now,
                signingCredentials: credentials
            );
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            //Token Yaratılıyor..
            tokenModel.AccessToken = tokenHandler.WriteToken(securityToken);
            tokenModel.RefreshToken = CreateRefreshToken();

            //User sınıfında database'e tokenı kaydediyoruz ama clean architecture bozuyor!
            user.RefreshToken = tokenModel.RefreshToken;
            user.RefreshTokenExpireDate = tokenModel.Expiration.AddMinutes(5);
            _context.Users.Update(user);
            _context.SaveChanges();

            return tokenModel;

        }

        public string CreateRefreshToken(){
            return Guid.NewGuid().ToString();
        }
    }
}