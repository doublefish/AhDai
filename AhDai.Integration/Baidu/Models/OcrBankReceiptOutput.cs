using AhDai.Core.Extensions;
using AhDai.Integration.Models;
using System.Collections.Generic;
using System.Linq;

namespace AhDai.Integration.Baidu.Models;

/// <summary>
/// OcrBankReceiptOutput
/// </summary>
public class OcrBankReceiptOutput : BaseDocumentOcrOutput<Dictionary<string, OcrBankReceiptWordsResult[]>>
{
    /// <summary>
    /// GetFriendlyOutput
    /// </summary>
    /// <returns></returns>
    public OcrBankReceiptFriendlyOutput? GetFriendlyOutput()
    {
        if (WordsResultNum == 0 || WordsResult == null) return null;
        var output = new OcrBankReceiptFriendlyOutput()
        {
            Number = WordsResult.GetValueOrDefault("回单编号")?.FirstOrDefault()?.Word,
            Title = WordsResult.GetValueOrDefault("标题")?.FirstOrDefault()?.Word,
            Summary = WordsResult.GetValueOrDefault("摘要")?.FirstOrDefault()?.Word,
            Purpose = WordsResult.GetValueOrDefault("用途")?.FirstOrDefault()?.Word,
            Date = WordsResult.GetValueOrDefault("交易日期")?.FirstOrDefault()?.Word?.ToDateOnlyExactOrNull(["yyyyMMdd"]),
            Amount = WordsResult.GetValueOrDefault("小写金额")?.FirstOrDefault()?.Word?.ToDecimalOrNull(),
            AmountInWords = WordsResult.GetValueOrDefault("大写金额")?.FirstOrDefault()?.Word,
            PayerBankName = WordsResult.GetValueOrDefault("付款人开户银行")?.FirstOrDefault()?.Word,
            PayerAccountName = WordsResult.GetValueOrDefault("付款人户名")?.FirstOrDefault()?.Word,
            PayerAccountNumber = WordsResult.GetValueOrDefault("付款人账号")?.FirstOrDefault()?.Word,
            PayeeBankName = WordsResult.GetValueOrDefault("收款人开户银行")?.FirstOrDefault()?.Word,
            PayeeAccountName = WordsResult.GetValueOrDefault("收款人户名")?.FirstOrDefault()?.Word,
            PayeeAccountNumber = WordsResult.GetValueOrDefault("收款人账号")?.FirstOrDefault()?.Word,
            TransactionId = WordsResult.GetValueOrDefault("流水号")?.FirstOrDefault()?.Word,
        };
        return output;
    }
}
