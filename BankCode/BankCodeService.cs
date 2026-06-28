namespace BankCode;

internal class BankCodeService
{
    // 解析帳號對外開放方法
    public (bool isValid, string number) ParseNumber(string[] lines)
    {
        var util = new BankCodeUtil();
        var getNumber = util.ParseAccount(lines);
        var isValid = util.IsValidCheckSum(getNumber);
        getNumber = util.ValidateAccount(getNumber);
        return (isValid, getNumber);
    }
}
