using BankCode;

namespace BankCode.Tests;

// 測試輔助: 把 9 位數帳號渲染成 3 行的 3x3 圖案 (string[])
// 反查產品端字典，避免手寫 ASCII 圖案出錯
internal static class AccountRenderer
{
    private static readonly Dictionary<char, string> Glyphs =
        BankCodeDictionary.DigitPatterns.ToDictionary(kv => kv.Value, kv => kv.Key);

    public static string[] Render(string account)
    {
        var rows = new string[3];
        for (int row = 0; row < 3; row++)
        {
            rows[row] = string.Concat(account.Select(c => Glyphs[c].Substring(row * 3, 3)));
        }
        return rows;
    }
}
