namespace BankCode;

internal class BankCodeService
{
    // 解析帳號對外開放方法
    public (bool isValid, string code, string number, bool isAMB, string[] AMBList) ParseNumber(string[] lines)
    {
        var util = new BankCodeUtil();
        var getNumber = util.ParseAccount(lines);
        var isValid = util.IsValidCheckSum(getNumber);
        var code = util.ValidateAccount(getNumber);
        if (util.IsErrorOrILL(code))
        {
            var smartFix = util.SmartFix(lines);
            if (smartFix.isFix)
            {
                return (true, "", smartFix.probablyNumber.First(), smartFix.isAMB, smartFix.probablyNumber);
            }
        }
        return (isValid, code, getNumber, false, []);
    }
}
