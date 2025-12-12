using System.Text;

namespace EdiPOC.Converter.Pdf.Html;

internal static partial class HtmlEdiConverter
{
       private static StringBuilder AppendBody(this StringBuilder builder, PdfEdiElements data)
    {
        var bodyData = data.BodyData;
      
        builder
            .Append($"""
                     <body>
                     <main id="pdf-root">

                     <div class="pdf-wrapper">

                     <div class="logo-and-address-wrapper">
                       <div class="logo">
                         <img src="https://upload.wikimedia.org/wikipedia/commons/3/3a/Metrans_Rail_logo.jpg" alt="metrans-logo"/>
                       </div>

                       <div class="address">
                         <div class="from">
                           <div>Von: METRANS A.S.</div>
                           <div>Podleská 926/5</div>
                           <div>CZ 10400 Praha 10</div>
                         </div>

                         <div class="to">
                           <div{MarkHighlighted(bodyData, PdfEdiElements.CarrierName)}>An: {InsertValue(bodyData, PdfEdiElements.CarrierName)}</div>
                           <div{MarkHighlighted(bodyData, PdfEdiElements.CarrierStreet)}>{InsertValue(bodyData, PdfEdiElements.CarrierStreet)}</div>
                           <div{MarkHighlighted(bodyData, PdfEdiElements.CarrierCountry)}>{InsertValue(bodyData, PdfEdiElements.CarrierCountry)}</div>
                         </div>
                       </div>
                     </div>

                     <div class="transport-text-warning-text-wrapper">
                        <div{MarkHighlighted(bodyData, PdfEdiElements.OrderHeader)}>
                          <div class="transport-text">{InsertValue(bodyData, PdfEdiElements.OrderHeader)}</div>
                        </div>
                        <div class="warning-text">{InsertValue(bodyData, PdfEdiElements.WarningText)}</div>
                     </div>

                     <div class="notes-wrapper">
                        <div class="notes-header">Bemerkungen:</div>
                        <div{MarkHighlighted(bodyData, PdfEdiElements.Notes)}>
                          <div class="notes-text">{InsertValue(bodyData, PdfEdiElements.Notes)}</div>
                        </div>
                     </div>

                     <div class="additional-info-wrapper">
                        <div{MarkHighlighted(bodyData, PdfEdiElements.NoteOutsideBorder)}>
                          {InsertValue(bodyData, PdfEdiElements.NoteOutsideBorder)}
                        </div>
                     </div>

                     </div>
                     <div class="page-break"></div>
                     <div class="pdf-wrapper">

                     <div class="reservation-wrapper">
                       <div class="shipment-details-wrapper">
                         <div class="port-ship-reeder-port-of-loading-waster-wrapper">
                           <div class="port-ship-reeder-wrapper">
                             <div{MarkHighlighted(bodyData, PdfEdiElements.Harbour)}>
                               <div class="name">Hafen:</div> {InsertValue(bodyData, PdfEdiElements.Harbour)}
                             </div>
                             <div{MarkHighlighted(bodyData, PdfEdiElements.ShipName)}>
                               <div class="name">Schiff:</div> {InsertValue(bodyData, PdfEdiElements.ShipName)}
                             </div>
                             <div{MarkHighlighted(bodyData, PdfEdiElements.ShippingCompany)}>
                               <div class="name">Reeder:</div> {InsertValue(bodyData, PdfEdiElements.ShippingCompany)}
                             </div>
                           </div>

                           <div class="port-of-loading-waste-wrapper">
                             <div{MarkHighlighted(bodyData, PdfEdiElements.HarbourOfLoading)}>
                               <div class="name">Abgangshafen:</div>
                               {InsertValue(bodyData, PdfEdiElements.HarbourOfLoading)}
                             </div>

                             <div{MarkHighlighted(bodyData, PdfEdiElements.IsWeighingRequest)}>
                               <div class="name">Abfall:</div>
                               {InsertValue(bodyData, PdfEdiElements.IsWeighingRequest)}
                             </div>
                           </div>
                         </div>

                         <div class="reservation-waybill-type-weight-wrapper">
                           <div class="reservation-waybill">
                             <div{MarkHighlighted(bodyData, PdfEdiElements.ReferenceNumber)}>
                               <div class="name"><b>BuchNr. Ref.:</b></div>
                               {InsertValue(bodyData, PdfEdiElements.ReferenceNumber)}
                             </div>

                             <div{MarkHighlighted(bodyData, PdfEdiElements.CmrNumber)}>
                               <div class="name"><b>Frachtbrief#:</b></div>
                               {InsertValue(bodyData, PdfEdiElements.CmrNumber)}
                             </div>
                           </div>

                           <div class="type-weight">
                             <div{MarkHighlighted(bodyData, PdfEdiElements.ContainerType)}>
                               <div class="name"><b>Type:</b></div>
                               {InsertValue(bodyData, PdfEdiElements.ContainerType)}
                             </div>

                             <div{MarkHighlighted(bodyData, PdfEdiElements.Weight)}>
                               <div class="name"><b>Gewicht:</b></div>
                               {InsertValue(bodyData, PdfEdiElements.Weight)}
                             </div>
                           </div>
                         </div>
                       </div>

                       <div class="delivery-details">
                         <div{MarkHighlighted(bodyData, PdfEdiElements.DeliveryDateTime)}>
                           <div class="name"><b>Zustellungstermin:</b></div>
                           {InsertValue(bodyData, PdfEdiElements.DeliveryDateTime)}
                         </div>

                         <div{MarkHighlighted(bodyData, PdfEdiElements.TerminalReturnDateTime)}>
                           <div class="name"><b>Terminal Rückgabe:</b></div>
                           {InsertValue(bodyData, PdfEdiElements.TerminalReturnDateTime)}
                         </div>
                       </div>
                     """
            );

        builder.AppendDangerousGoods(data);
        builder.Append($"""
                        </div>
                        <div class="middle-part"></div>

                            <div class="container-wrapper">
                              <div class="item">
                                 <div{MarkHighlighted(bodyData, PdfEdiElements.ContainerNumber)}>
                                   <div class="name"><b>Container:</b></div>
                                   {InsertValue(bodyData, PdfEdiElements.ContainerNumber)}
                                 </div>
                              </div>

                              <div class="item">
                                 <div{MarkHighlighted(bodyData, PdfEdiElements.Seal)}>
                                   <div class="name"><b>Siegel:</b></div>
                                   {InsertValue(bodyData, PdfEdiElements.Seal)}
                                 </div>
                              </div>

                              <div class="item">
                                 <div{MarkHighlighted(bodyData, PdfEdiElements.CustomsDocumentType)}>
                                   <div class="name"><b>Art zollverfahren:</b></div>
                                   {InsertValue(bodyData, PdfEdiElements.CustomsDocumentType)}
                                 </div>
                              </div>

                              <div class="item">
                                 <div{MarkHighlighted(bodyData, PdfEdiElements.ContainerNotes)}>
                                   <div class="name"><b>Bemerkungen:</b></div>
                                   {InsertValue(bodyData, PdfEdiElements.ContainerNotes)}
                                 </div>
                              </div>
                            </div>
                          </div>
                        </div>
                        """
        );

        builder.AppendStops(data)
            .Append("</div>")
            .Append("</main>")
            .Append("</body>");

        return builder;
    }

    private static void AppendDangerousGoods(this StringBuilder builder, PdfEdiElements data)
    {
      var goodsData = data.GoodsData;

        builder.Append($"""
                        <div class="content-details">
                          <div{MarkHighlighted(goodsData.GoodsDescription)} style="display: flex;">
                            <div class="name"><b>Inhalt:</b></div>
                            <div style="white-space: pre-line; transform: translateY(-15px);">
                            {InsertValue(goodsData.GoodsDescription)}
                            </div>
                          </div>
                        </div>

                        <div class="dangerous-goods-container-details">
                          <div class="dangerous-goods-wrapper">
                            <div class="dangerous-goods-header">
                              <b>Gefahrgut</b>
                            </div>
                        """
        );

        foreach (var dangerousGood in goodsData.DangerousGoods)
            builder.AppendOneDangerousGood(dangerousGood);
    }

    private static void AppendOneDangerousGood(this StringBuilder builder, PdfEdiElements.DangerousGoodData dangerousGoodData)
    {
      builder.Append($"""
                      <div class="dangerous-item-wrapper">
                         <div class="item">
                             <div{MarkHighlighted(dangerousGoodData.UnNumber)}>
                                 <div class="name">UN</div>
                                 {InsertValue(dangerousGoodData.UnNumber)}
                             </div>
                         </div>

                         <div class="item">
                             <div{MarkHighlighted(dangerousGoodData.Class)}>
                                 <div class="name">Class</div>
                                 {InsertValue(dangerousGoodData.Class)}
                             </div>
                         </div>

                         <div class="item">
                             <div{MarkHighlighted(dangerousGoodData.PackingGroup)}>
                                 <div class="name">Pck grp</div>
                                 {InsertValue(dangerousGoodData.PackingGroup)}
                             </div>
                         </div>

                         <div class="item">
                             <div{MarkHighlighted(dangerousGoodData.Description)}>
                                 {InsertValue(dangerousGoodData.Description)}
                             </div>
                         </div>
                       </div>
                      """
      );
    }

    private static StringBuilder AppendStops(this StringBuilder builder, PdfEdiElements data)
    {
        var stops = data.StopsData;

        if (stops.IsEmpty)
            return builder;

        builder.Append("""<div class="reservation-stops-wrapper">""");

        foreach (var stop in stops)
            builder.AppendOneStop(stop);

        builder.Append("</div>");

        return builder;
    }

    private static void AppendOneStop(this StringBuilder builder, PdfEdiElements.StopData stop)
    {
      builder.Append($"""
                      <div{MarkHighlighted(stop)}>
                        <div class="reservation-single-stop">
                          <div class="stop-number-description">
                            <div class="stop-number">
                              <b>Stop {stop.Number}</b>
                            </div>
                            {stop.Description}
                          </div>

                          <div class="receiving-and-returns-type-address">
                            <div class="receiving-and-returns-type">
                              <b>{stop.StopName}</b>
                            </div>
                            <div class="address">
                              <div>Address: {stop.AddressName}</div>
                              <div class="with-margin"> {stop.AddressStreet}</div>
                              <div class="with-margin"> {stop.AddressCountry}</div>
                            </div>
                          </div>
                        </div>
                      </div>
                      """
      );
    }
}