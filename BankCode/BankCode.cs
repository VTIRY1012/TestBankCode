using System;
using System.Collections.Generic;
using System.Text;

namespace BankCode;

internal class BankCode
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
}
