using System.Text;

namespace BankCode;

public class BankCodeUtil
{
    // User Story 1: 數字識別
    // 可以知道帳號是甚麼
    /*
        - 從 3x3 字元圖案解析數字 0-9
        - 每個帳號由 9 個數字組成
        - 每個數字佔用 3 個字元寬度
     */

    // 解析帳號
    public string ParseAccount(string[] lines)
    {
        var accountNumber = new StringBuilder();
        for (int i = 0; i < 9; i++)
        {
            string digitPattern = lines[0].Substring(i * 3, 3) +
                                  lines[1].Substring(i * 3, 3) +
                                  lines[2].Substring(i * 3, 3);
            if (BankCodeDictionary.DigitPatterns.TryGetValue(digitPattern, out char digit))
            {
                accountNumber.Append(digit);
            }
            else
            {
                accountNumber.Append('?'); // 無法識別的數字用 '?' 表示
            }

        }
        return accountNumber.ToString();
    }
}