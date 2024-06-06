using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DbOracle.Entities
{
    public class TokenManager
    {
        private readonly IOptionsSnapshot<JwtConfig> jwtconfig; 
        public TokenManager(IOptionsSnapshot<JwtConfig> jwtconfig) 
        { 
            this.jwtconfig = jwtconfig; 
        }
        public string CreateToken(string id, string name)
        { 
            // 创建声明列表，即 Token 中携带的信息
            List<Claim> claims = new List<Claim>(); 
            claims.Add(new Claim(ClaimTypes.Name, name)); 
            // 添加用户名
            claims.Add(new Claim(ClaimTypes.NameIdentifier, id)); 
            // 添加用户 ID 
            // 设置 Token 的过期时间
            DateTime expres = DateTime.Now.AddSeconds(jwtconfig.Value.Expres); 
            Console.WriteLine($"过期时间{expres}"); 
            // 从配置文件中获取 JWT 密钥并转换为字节数组
            byte[] secbyse = Encoding.UTF8.GetBytes(jwtconfig.Value.Key); 
            // 创建 SymmetricSecurityKey 对象并使用 HmacSha256 算法对密钥进行签名
            var secKey = new SymmetricSecurityKey(secbyse); 
            var credetials = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256); 
            // 创建 JwtSecurityToken 对象并设置声明、过期时间和签名信息
            var tokenDescriptor = new JwtSecurityToken(claims: claims, expires: expres, signingCredentials: credetials); 
            // 生成 JWT Token 字符串并返回
            string jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor); 
            return jwt;
        }
    }

}
