using System.Security.Cryptography;
using System.Text;

namespace DbOracle.Entities
{
    public class Encrypter
    {
        public static string encrypt(string strPwd)
        {

            string str = "";
            MD5 md5 = MD5.Create();
            // 初始化MD5对象
            //MD5 md5 = MD5CryptoServiceProvider();
            // 将字符编码为一个字节数组
            byte[] data = Encoding.Default.GetBytes(strPwd);
            // 计算data字节数组的哈希值
            byte[] md5Data = md5.ComputeHash(data);
            // 清空md5
            md5.Clear();
            // 遍历md5Data哈希数组
            for (int i = 0; i < md5Data.Length - 1; i++)
            {
                str += md5Data[i].ToString("x").PadLeft(2, '0');
            }

            return str;
        }
    }
}
