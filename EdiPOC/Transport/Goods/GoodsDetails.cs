namespace EdiPOC.Transport.Goods;

public record GoodsDetails
{
    // TODO pridat "IM_ZB_PHVE"
    public List<Good> Goods { get; private set; }
    public int? QuantityTotal { get; private set; }
    public int? GrossWeightTotal { get; private set; }
    public Temperature? Temperature { get; internal set; }
    public string? AdrClass { get; private set; }

    private GoodsDetails() // Parameterless constructor for EF Core
    {
    }
    
    private GoodsDetails(List<Good> goods, int? quantityTotal, int? grossWeightTotal, Temperature? temperature, string? adrClass)
    {
        Goods = goods;
        QuantityTotal = quantityTotal;
        GrossWeightTotal = grossWeightTotal;
        Temperature = temperature;
        AdrClass = adrClass;
    }
    
    public static GoodsDetails Create(List<Good> goods, int? quantityTotal, int? grossWeightTotal, Temperature? temperature, string? adrClass) =>
        new(goods, quantityTotal, grossWeightTotal, temperature, adrClass);

    internal bool HasDangerousGoods() => AdrClass is not null || Goods.Exists(x => x.DangerousGoodSpecifications is not null);
    
    internal string GetMergedDescription() => Goods.Count > 0 ? string.Join("; ", Goods.Where(x => x.Description != null).Select(x => x.Description)) : string.Empty;

    internal bool ExistsGoodWithAtaCode() => Goods.Any(good => good.AtaCode is not null);
    
    internal bool ExistsLimitedQuantityGood() => Goods.Any(good => good.DangerousGoodSpecifications is not null && good.DangerousGoodSpecifications.IsLimitedQuantity);
    
    internal bool ExistsImdgGood() => Goods.Any(good => good.DangerousGoodSpecifications is not null && good.DangerousGoodSpecifications.IsImdg);
}