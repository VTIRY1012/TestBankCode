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
        foreach (var pattern in GetDigitPatterns(lines))
        {
            if (BankCodeDictionary.DigitPatterns.TryGetValue(pattern, out char digit))
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
    
    public string[] GetDigitPatterns(string[] lines)
    {
        var patterns = new string[9];
        for (int i = 0; i < 9; i++)
        {
            // 從 3 行裡各取「第 i 格的那 3 個字元」，疊成 9 字元的圖案
            patterns[i] = lines[0].Substring(i * 3, 3) +
                          lines[1].Substring(i * 3, 3) +
                          lines[2].Substring(i * 3, 3);
        }
        return patterns;
    }
    
    // User Story 2: 校驗和驗證
    /*
        - 實作校驗和計算: `(d1 + 2*d2 + 3*d3 + ... + 9*d9) mod 11 = 0`
        - 驗證帳號的有效性
     */
    public bool IsValidCheckSum(string accountNumber)
    {
        var sum = 0;
        // i = 0, 1, 2, 3, 4, 5, 6, 7, 8 因為帳號有 9 個數字
        for (int i = 0; i < 9; i++)
        {
            //i=0 → position = 9-0 = 9  (第1個數字，權重9)
            //i=1 → position = 9-1 = 8  (第2個數字，權重8) 以此類推
            int digit = accountNumber[i] - '0';
            int position = 9 - i;
            sum += digit * position;
        }
        return sum % 11 == 0;
    }
    
    
    // User Story 3: 錯誤處理
    /*
        457508000
        664371495 ERR
        86110??36 ILL
     */
    public string ValidateAccount(string accountNumber)
    {
        if (accountNumber.Contains('?'))
        {
            return "ILL"; // 無法識別的數字
        }
        else if (!IsValidCheckSum(accountNumber))
        {
            return "ERR"; // 校驗和錯誤
        }
        else
        {
            return accountNumber; // 有效帳號
        }
    }

    public bool IsErrorOrILL(string code)
    {
        return code  == "ILL" || code == "ERR";
    }

    // User Story 4: 智能修正
    /*
        - 嘗試通過添加/移除單一管線 (`|`) 或底線 (`_`) 來修正錯誤
        - 單一候選 - 直接顯示正確的帳號
        - 多個候選 - 標記為 `AMB` 並列出所有可能的帳號
        - 無法修正 - 保持原狀態 (ERR 或 ILL)
     */

    public (bool isFix, bool isAMB, string[] probablyNumber) SmartFix(string[] lines)
    {
        var original = ParseAccount(lines);              // 可能含 '?'，例如 "0?0000051"
        var patterns = GetDigitPatterns(lines);          // 每格的原始圖案 (關鍵：保留 '?' 格的圖案)
        var valid = new HashSet<string>();
        for (int i = 0; i < 9; i++)
        {
            // 用「原始圖案」算候選，'?' 格也照樣能補回來
            foreach (var altDigit in GetAlternativeDigits(patterns[i]))
            {
                var chars = original.ToCharArray();
                chars[i] = altDigit;
                var candidate = new string(chars);
                if (candidate.Contains('?')) continue;   // 還有別的 '?' → 一條線救不了
                if (IsValidCheckSum(candidate)) valid.Add(candidate);
            }
        }
        var probably = valid.OrderBy(n => n).ToArray();
        return (probably.Length >= 1, probably.Length > 1, probably);
    }
    
    // 3x3 格子裡「可放線段」的槽位 (索引 -> 該槽位應有的線段字元)
    // 索引: 0 1 2 / 3 4 5 / 6 7 8；其餘 (0,2) 永遠空白不需嘗試
    private static readonly Dictionary<int, char> SegmentSlots = new()
    {
        { 1, '_' }, { 4, '_' }, { 7, '_' }, // 上中、中中、下中
        { 3, '|' }, { 5, '|' }, { 6, '|' }, { 8, '|' } // 中左、中右、下左、下右
    };

    // 嘗試通過添加/移除單一管線 (|) 或底線 (_) 來修正錯誤
    // 對單一格子翻轉每個槽位 (空白<->線段)，回傳所有「相差一條線段」的合法候選數字
    private IEnumerable<char> GetAlternativeDigits(string pattern)
    {
        BankCodeDictionary.DigitPatterns.TryGetValue(pattern, out char originalDigit);

        var alternatives = new List<char>();
        foreach (var (index, segmentChar) in SegmentSlots)
        {
            var chars = pattern.ToCharArray();
            // 有線段就移除，沒有就補上
            chars[index] = chars[index] == ' ' ? segmentChar : ' ';
            var candidate = new string(chars);

            if (BankCodeDictionary.DigitPatterns.TryGetValue(candidate, out char digit)
                && digit != originalDigit)
            {
                alternatives.Add(digit);
            }
        }
        return alternatives;
    }
}