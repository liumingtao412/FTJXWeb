using System;
using System.Text;
using System.Security.Cryptography;

/// <summary>
/// MD5Provider 的摘要说明
/// </summary>
public class MD5Provider
{
	public MD5Provider()
	{
		
	}
    /// <summary>
    /// 用md5算法进行哈希加密
    /// </summary>
    /// <param name="message">待加密的内容</param>
    /// <returns>string，加密后的结果</returns>
    public static string Hash(string message)
    {
        if (string.IsNullOrEmpty(message))
        {
            return string.Empty;
        }
        else
        {
            MD5 md5 = MD5.Create();
            byte[] source = Encoding.UTF8.GetBytes(message);
            byte[] result = md5.ComputeHash(source);
            StringBuilder buffer = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                buffer.Append(result[i].ToString("X"));
            }
            return buffer.ToString();
        }
    }
}
