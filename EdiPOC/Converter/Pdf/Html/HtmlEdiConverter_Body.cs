using System.Text;

namespace EdiPOC.Converter.Pdf.Html;

internal static partial class HtmlEdiConverter
{
    private static StringBuilder AppendBody(this StringBuilder builder, PdfEdiElements data)
    {
        var bodyData = data.BodyData;
        builder.Append($$"""
                         <body>
                         <main id="pdf-root">

                         <div class="pdf-wrapper">

                         <div class="logo-and-address-wrapper">
                           <div class="logo">
                             <svg xmlns="http://www.w3.org/2000/svg" id="Vrstva_1" version="1.1" viewBox="190 200 500 176">
                               <defs>
                                 <style>
                                   .st0 {
                                     fill: none;
                                   }
                               
                                   .st1 {
                                     fill: #002269;
                                   }
                               
                                   .st2 {
                                     fill: #a0a0a0;
                                   }

                                   .st3 {
                                     fill: #c20430;
                                   }
                                 </style>
                               </defs>
                               <rect class="st1" x="227.6" y="245.5" width="26" height="104.3"/>
                               <rect class="st2" x="293" y="245.5" width="321.3" height="104.3"/>
                               <rect class="st3" x="266.7" y="245.5" width="13.1" height="104.3"/>
                               <polygon class="st1" points="312.7 277.6 330.6 277.6 336.4 301.6 336.5 301.6 342.3 277.6 360.2 277.6 360.2 318.4 348.3 318.4 348.3 292.2 348.2 292.2 341.1 318.4 331.8 318.4 324.7 292.2 324.6 292.2 324.6 318.4 312.7 318.4 312.7 277.6"/>
                               <polygon class="st1" points="365.2 277.6 398.9 277.6 398.9 288.1 377.7 288.1 377.7 293.1 397 293.1 397 302.9 377.7 302.9 377.7 307.9 399.6 307.9 399.6 318.4 365.2 318.4 365.2 277.6"/>
                               <polygon class="st1" points="412 288.1 400.5 288.1 400.5 277.6 436 277.6 436 288.1 424.5 288.1 424.5 318.4 412 318.4 412 288.1"/>
                               <path class="st1" d="M437.6,277.6h23.9c1.8,0,3.5.2,5.1.7,1.6.5,3,1.2,4.2,2.2,1.2,1,2.2,2.2,2.9,3.6.7,1.4,1.1,3.1,1.1,5.1s-.1,2.1-.4,3.2c-.2,1.1-.6,2-1.1,2.9-.5.9-1.1,1.7-1.9,2.5-.8.7-1.7,1.3-2.7,1.7,1.7.6,3.1,1.9,4.1,3.7,1,1.8,1.7,4,1.9,6.5,0,.5,0,1.1.1,1.9,0,.8.1,1.6.2,2.5,0,.9.2,1.7.4,2.5.2.8.4,1.4.7,1.9h-12.6c-.3-1-.5-2.1-.7-3.1-.2-1-.3-2.1-.3-3.2,0-1-.2-1.9-.3-2.9-.1-1-.4-1.8-.8-2.6-.4-.8-.9-1.4-1.6-1.8-.7-.5-1.7-.7-2.9-.7h-6.7v14.2h-12.6v-40.8ZM450.2,295.4h6.6c.6,0,1.2,0,1.9-.1.7,0,1.3-.3,1.8-.5.5-.3,1-.7,1.3-1.2.4-.5.5-1.3.5-2.2,0-1.3-.4-2.3-1.3-3-.9-.7-2.5-1.1-4.9-1.1h-5.9v8.1Z"/>
                               <path class="st1" d="M490.7,277.6h12.3l14.9,40.8h-13l-1.7-5.8h-13l-1.8,5.8h-12.6l15-40.8ZM500.6,303.8l-3.7-12.6h-.1l-3.9,12.6h7.7Z"/>
                               <polygon class="st1" points="518.7 277.6 531.6 277.6 543.5 299.4 543.6 299.4 543.6 277.6 555.5 277.6 555.5 318.4 543.2 318.4 530.7 296.1 530.6 296.1 530.6 318.4 518.7 318.4 518.7 277.6"/>
                               <path class="st1" d="M570.6,304.7c0,1,.2,1.9.5,2.7.5,1.2,1.3,2,2.5,2.4,1.2.4,2.4.6,3.5.6s1,0,1.7-.1c.6,0,1.2-.3,1.7-.6.5-.3,1-.7,1.3-1.1.3-.5.5-1.1.5-1.9s-.1-1-.4-1.3c-.3-.4-.7-.7-1.4-1-.6-.3-1.5-.7-2.7-1-1.1-.4-2.6-.8-4.3-1.3-1.6-.5-3.2-.9-4.9-1.5-1.7-.5-3.1-1.2-4.5-2.1-1.3-.9-2.4-1.9-3.2-3.3-.8-1.3-1.3-3-1.3-5.1s.5-4.4,1.4-6.1c.9-1.7,2.2-3.1,3.7-4.2,1.5-1.1,3.3-1.9,5.3-2.4,2-.5,4-.8,6.1-.8s4.3.2,6.3.7c2,.5,3.8,1.2,5.4,2.3s2.8,2.4,3.8,4.1c1,1.7,1.5,3.8,1.5,6.3h-11.9c.1-.8,0-1.4-.3-1.9-.3-.5-.7-1-1.2-1.3-.5-.4-1.1-.6-1.8-.8-.6-.2-1.3-.2-1.9-.2s-.9,0-1.4.1c-.5,0-1,.2-1.5.4-.5.2-.8.5-1.1.8-.3.4-.5.8-.5,1.4,0,.7.4,1.3,1.1,1.8.7.5,1.6.9,2.7,1.3,1.1.4,2.4.7,3.7,1,1.4.3,2.8.7,4.3,1.1,1.4.4,2.9.9,4.2,1.5,1.4.6,2.6,1.3,3.7,2.2,1.1.9,2,2,2.6,3.3.6,1.3,1,2.9,1,4.8,0,2.7-.6,4.9-1.7,6.7-1.1,1.8-2.6,3.2-4.3,4.3-1.8,1.1-3.8,1.9-6.1,2.3-2.2.5-4.5.7-6.8.7s-1.7,0-2.9-.2-2.4-.4-3.8-.7c-1.3-.4-2.7-.9-4-1.5-1.3-.6-2.5-1.5-3.6-2.6-1.1-1.1-1.9-2.4-2.6-4-.7-1.6-1-3.4-1-5.6h12.6Z"/>
                               <rect class="st0" x="227.6" y="245.5" width="386.7" height="104.3"/>
                             </svg>
                           </div>

                           <div class="address">
                             <div class="from">
                               <div>Von: METRANS A.S.</div>
                               <div>Podleská 926/5</div>
                               <div>CZ 10400 Praha 10</div>
                             </div>

                             <div class="to">
                               <div{{MarkHighlighted(bodyData, PdfEdiElements.CarrierName)}}>An: {{InsertValue(bodyData, PdfEdiElements.CarrierName)}}</div>
                               <div{{MarkHighlighted(bodyData, PdfEdiElements.CarrierStreet)}}>{{InsertValue(bodyData, PdfEdiElements.CarrierStreet)}}</div>
                               <div{{MarkHighlighted(bodyData, PdfEdiElements.CarrierCountry)}}>{{InsertValue(bodyData, PdfEdiElements.CarrierCountry)}}</div>
                             </div>
                           </div>
                         </div>

                         <div class="transport-text-warning-text-wrapper">
                            <div{{MarkHighlighted(bodyData, PdfEdiElements.OrderHeader)}}>
                              <div class="transport-text">{{InsertValue(bodyData, PdfEdiElements.OrderHeader)}}</div>
                            </div>
                            <div class="warning-text">{{InsertValue(bodyData, PdfEdiElements.WarningTextKey)}}</div>
                         </div>

                         <div class="notes-wrapper">
                            <div class="notes-header">Bemerkungen:</div>
                            <div{{MarkHighlighted(bodyData, PdfEdiElements.Notes)}}>
                              <div class="notes-text">{{InsertValue(bodyData, PdfEdiElements.Notes)}}</div>
                            </div>
                         </div>

                         <div class="additional-info-wrapper">
                            <div{{MarkHighlighted(bodyData, PdfEdiElements.NoteOutsideBorder)}}>
                              {{InsertValue(bodyData, PdfEdiElements.NoteOutsideBorder)}}
                            </div>
                         </div>

                         </div>
                         <div class="page-break"></div>
                         <div class="pdf-wrapper">

                         <div class="reservation-wrapper">
                           <div class="shipment-details-wrapper">
                             <div class="port-ship-reeder-port-of-loading-waster-wrapper">
                               <div class="port-ship-reeder-wrapper">
                                 <div{{MarkHighlighted(bodyData, PdfEdiElements.Harbour)}}>
                                   <div class="name">Hafen:</div> {{InsertValue(bodyData, PdfEdiElements.Harbour)}}
                                 </div>
                                 <div{{MarkHighlighted(bodyData, PdfEdiElements.ShipName)}}>
                                   <div class="name">Schiff:</div> {{InsertValue(bodyData, PdfEdiElements.ShipName)}}
                                 </div>
                                 <div{{MarkHighlighted(bodyData, PdfEdiElements.ShippingCompany)}}>
                                   <div class="name">Reeder:</div> {{InsertValue(bodyData, PdfEdiElements.ShippingCompany)}}
                                 </div>
                               </div>

                               <div class="port-of-loading-waste-wrapper">
                                 <div{{MarkHighlighted(bodyData, PdfEdiElements.HarbourOfLoading)}}>
                                   <div class="name">Abgangshafen:</div>
                                   {{InsertValue(bodyData, PdfEdiElements.HarbourOfLoading)}}
                                 </div>

                                 <div{{MarkHighlighted(bodyData, PdfEdiElements.IsWeighingRequest)}}>
                                   <div class="name">Abfall:</div>
                                   {{InsertValue(bodyData, PdfEdiElements.IsWeighingRequest)}}
                                 </div>
                               </div>
                             </div>

                             <div class="reservation-waybill-type-weight-wrapper">
                               <div class="reservation-waybill">
                                 <div{{MarkHighlighted(bodyData, PdfEdiElements.ReferenceNumber)}}>
                                   <div class="name"><b>BuchNr. Ref.:</b></div>
                                   {{InsertValue(bodyData, PdfEdiElements.ReferenceNumber)}}
                                 </div>

                                 <div{{MarkHighlighted(bodyData, PdfEdiElements.CmrNumber)}}>
                                   <div class="name"><b>Frachtbrief#:</b></div>
                                   {{InsertValue(bodyData, PdfEdiElements.CmrNumber)}}
                                 </div>
                               </div>

                               <div class="type-weight">
                                 <div{{MarkHighlighted(bodyData, PdfEdiElements.ContainerType)}}>
                                   <div class="name"><b>Type:</b></div>
                                   {{InsertValue(bodyData, PdfEdiElements.ContainerType)}}
                                 </div>

                                 <div{{MarkHighlighted(bodyData, PdfEdiElements.Weight)}}>
                                   <div class="name"><b>Gewicht:</b></div>
                                   {{InsertValue(bodyData, PdfEdiElements.Weight)}}
                                 </div>
                               </div>
                             </div>
                           </div>

                           <div class="delivery-details">
                             <div{{MarkHighlighted(bodyData, PdfEdiElements.DeliveryDateTime)}}>
                               <div class="name"><b>Zustellungstermin:</b></div>
                               {{InsertValue(bodyData, PdfEdiElements.DeliveryDateTime)}}
                             </div>

                             <div{{MarkHighlighted(bodyData, PdfEdiElements.TerminalReturnDateTime)}}>
                               <div class="name"><b>Terminal Rückgabe:</b></div>
                               {{InsertValue(bodyData, PdfEdiElements.TerminalReturnDateTime)}}
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
                            <div class="goods-description" style="white-space: pre-line; transform: translateY(-15px);">
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

    private static void AppendOneDangerousGood(this StringBuilder builder,
        PdfEdiElements.DangerousGoodData dangerousGoodData)
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