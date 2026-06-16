using System;
using System.Collections.Generic;
using System.Text;

namespace BankCode;

internal class BankCodeService
{
    // User Story 1: 數字識別
    // 可以知道帳號是甚麼
    /*
        - 從 3x3 字元圖案解析數字 0-9
        - 每個帳號由 9 個數字組成
        - 每個數字佔用 3 個字元寬度
     */

    // 定義數字
    Dictionary<string, char> _digitPatterns = new Dictionary<string, char>
    {
        {" _ " + 
         "| |"+
         "|_|",'0' },

        {"   " + 
         "  |"+
         "  |", '1' },

        {" _ " +
         " _|"+
         "|_ ", '2' },

        {" _ " + 
         " _|"+
         " _|", '3' },

        {" _ " + 
         "|_|"+
         "  |", '4' },

        {" _ " +
         "|_ " +
         " _|", '5' },

        {" _ " +
         "|_ " +
         "|_|", '6' },

        {" _ " +
         "  |" +
         "  |", '7' },

        {" _ " +
         "|_|" +
         "|_|", '8' },

        {" _ " +
         "|_|" +
         " _|", '9'  }
    };

    // 解析帳號
    public string ParseAccount(string[] lines)
    {
        var accountNumber = new StringBuilder();
        for (int i = 0; i < 9; i++)
        {
            string digitPattern = lines[0].Substring(i * 3, 3) +
                                  lines[1].Substring(i * 3, 3) +
                                  lines[2].Substring(i * 3, 3);
            if (_digitPatterns.TryGetValue(digitPattern, out char digit))
            {
                accountNumber.Append(digit);
            }

        }
        return accountNumber.ToString();
    }

    // 測試: 隨便塞數字
    public void TestStory1()
    {
        string[] accountLines = new string[]
        {
            "    _  _     _  _  _  _  _ ",
            "  | _| _||_||_ |_   ||_||_|",
            "  ||_  _|  | _||_|  ||_| _|"
        };
        string accountNumber = ParseAccount(accountLines);
        Console.WriteLine($"Parsed Account Number: {accountNumber}"); // 預期輸出: "123456789"
    }

    // 測試: 隨便塞數字2
    public void TestStory1_2()
    {
        string[] accountLines = new string[]
        {
            " _  _  _  _  _  _  _  _  _ ",
            "| || || || || || || || || |",
            "|_||_||_||_||_||_||_||_||_|"
        };
        string accountNumber = ParseAccount(accountLines);
        Console.WriteLine($"Parsed Account Number: {accountNumber}"); // 預期輸出: "000000000"
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

    // 測試: 校驗和驗證
    public void TestStory2_1()
    {
        string accountNumber = "345882865"; // 可以整除
        bool isValid = IsValidCheckSum(accountNumber);
        Console.WriteLine($"Account Number: {accountNumber}, Checksum Valid: {isValid}"); // 預期輸出: "Account Number: 345882865, Checksum Valid: True"
    }

    // 測試: 校驗和驗證2
    public void TestStory2_2()
    {
        string accountNumber = "111111111"; // 餘數為1
        bool isValid = IsValidCheckSum(accountNumber);
        Console.WriteLine($"Account Number: {accountNumber}, Checksum Valid: {isValid}"); // 預期輸出: "Account Number: 111111111, Checksum Valid: False"
    }
}
