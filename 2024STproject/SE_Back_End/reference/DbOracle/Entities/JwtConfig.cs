namespace DbOracle.Entities
{
    public class JwtConfig
    {
       public string Key { get; set; } //key
       public int Expres { get; set; } //过期时间（单位秒）
       public string Issuer { get; set; } 
       public string Audience { get; set; }}
    }
