using AhDai.Core.Extensions;
using AhDai.Integration.Models;
using System.Linq;

namespace AhDai.Integration.Baidu.Models.Ocr;

/// <summary>
/// VatInvoiceOutput
/// </summary>
public class VatInvoiceOutput : BaseDocumentOutput<VatInvoiceWordsResult>
{
    /// <summary>
    /// GetFriendlyOutput
    /// </summary>
    /// <returns></returns>
    public OcrVatInvoiceFriendlyOutput? GetFriendlyOutput()
    {
        if (WordsResultNum == 0 || WordsResult == null) return null;
        var output = new OcrVatInvoiceFriendlyOutput()
        {
            Number = WordsResult.InvoiceNum,
            Type = WordsResult.InvoiceType,
            SellerName = WordsResult.SellerName,
            SellerTaxNumber = WordsResult.SellerRegisterNum,
            BuyerName = WordsResult.PurchaserName,
            BuyerTaxNumber = WordsResult.PurchaserRegisterNum,
            Date = WordsResult.InvoiceDate?.ToDateOnlyExactOrNull("yyyy年MM月dd日"),
            Amount = WordsResult.AmountInFiguers,
            TaxRate = WordsResult.CommodityTaxRate.FirstOrDefault()?.Word[..^1]?.ToDecimalOrNull(),
            Tax = WordsResult.CommodityTax.FirstOrDefault()?.Word?.ToDecimalOrNull(),
            AmountExcludingTax = WordsResult.CommodityAmount.FirstOrDefault()?.Word?.ToDecimalOrNull(),
            Remark = "",
        };
        return output;
    }
}
