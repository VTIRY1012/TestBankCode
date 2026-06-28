using System.Text;

namespace BankCode;

internal class BankCodeService
{
    // 解析帳號對外開放方法
    public string ParseNumber(string[] lines)
    {
        var util =  new BankCodeUtil();
        var getNumber = util.ParseAccount(lines);
        return getNumber;
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

    // User Story 4: 智能修正
    /*
        - 嘗試通過添加/移除單一管線 (`|`) 或底線 (`_`) 來修正錯誤
        - 單一候選 - 直接顯示正確的帳號
        - 多個候選 - 標記為 `AMB` 並列出所有可能的帳號
        - 無法修正 - 保持原狀態 (ERR 或 ILL)
     */


    // 嘗試通過添加/移除單一管線 (`|`) 或底線 (`_`) 來修正錯誤
    Dictionary<char, string[]> _digitLikePatterns = new Dictionary<char, string[]>
    {
        //  " _ " + "| |" + "|_|", // 原始
        { '0',
            new string []
            {
                "   " + "| |" + "|_|", // 移除頂部底線
                " _ " + "  |" + "|_|", // 移除左側管線
                " _ " + "| |" + "   ", // 移除底部底線
                " _ " + "|  " + "|_|"  // 移除右側管線
            }
        },
        { '1',
            new string[]
            {
                "   " + "   " + "  |", // 移除上方管線
                "   " + "  |" + "   ", // 移除下方管線
                "   " + "   " + "   ", // 移除兩條管線
            }
        },
        { '2',
            new string[]
            {
                "   " + " _|" + "|_ ", // 移除頂部底線
                " _ " + "   " + "|_ ", // 移除右側管線
                " _ " + " _|" + "   ", // 移除底部底線
                " _ " + "  |" + "|_ "  // 移除中間底線
            }
        },
        { '3',
            new string[]
            {
                "   " + " _|" + " _|", // 移除頂部底線
                " _ " + "   " + " _|", // 移除上方右側管線
                " _ " + " _|" + "   ", // 移除底部底線
                " _ " + "  |" + " _|"  // 移除中間底線
            }
        },
        { '4',
            new string[]
            {
                "   " + "|_|" + "  |", // 移除頂部底線
                " _ " + "| |" + "  |", // 移除中間底線
                " _ " + "|_ " + "  |", // 移除右側上方管線
                " _ " + "  |" + "  |"  // 移除左側管線
            }
        },
        { '5',
            new string[]
            {
                "   " + "|_ " + " _|", // 移除頂部底線
                " _ " + "   " + " _|", // 移除左側管線
                " _ " + "|_ " + "   ", // 移除底部底線
                " _ " + "| |" + " _|"  // 添加左下管線
            }
        },
        { '6',
            new string[]
            {
                "   " + "|_ " + "|_|", // 移除頂部底線
                " _ " + "   " + "|_|", // 移除左上管線
                " _ " + "|_ " + "| |", // 移除底部底線
                " _ " + "| |" + "|_|"  // 移除中間底線
            }
        },
        { '7',
            new string[]
            {
                "   " + "  |" + "  |", // 移除頂部底線
                " _ " + "   " + "  |", // 移除上方管線
                " _ " + "  |" + "   "  // 移除下方管線
            }
        },
        { '8',
            new string[]
            {
                "   " + "|_|" + "|_|", // 移除頂部底線
                " _ " + "| |" + "|_|", // 移除中間底線
                " _ " + "|_|" + "| |", // 移除底部底線
                " _ " + "  |" + "|_|", // 移除左上管線
                " _ " + "|  " + "|_|"  // 移除右上管線
            }
        },
        { '9',
            new string[]
            {
                "   " + "|_|" + " _|", // 移除頂部底線
                " _ " + "| |" + " _|", // 移除中間底線
                " _ " + "|_|" + "   ", // 移除底部底線
                " _ " + "|_ " + " _|", // 移除右下管線
                " _ " + "  |" + " _|"  // 移除左上管線
            }
        }
    };
}
