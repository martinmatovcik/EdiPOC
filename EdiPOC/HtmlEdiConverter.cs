using System.Collections.Immutable;
using System.Text;

namespace EdiPOC;

internal static class HtmlEdiConverter
{
    public static string Convert(Edi.Domain.Edi edi)
    {
        var bodyData = PdfEdiElements.GetBodyData(edi);

        var builder = new StringBuilder();
        builder.AppendHead().AppendBody(bodyData, edi).EndHtml();
        return builder.ToString();
    }

    private static StringBuilder AppendHead(this StringBuilder builder)
    {
        return builder.Append($$"""
                                <!DOCTYPE html>
                                <html lang="de">
                                <head>
                                  <meta charset="UTF-8" />
                                  <title>Reservation PDF</title>
                                  <style>
                                  
                                    .page-break {
                                      display: block;
                                      break-before: page;
                                      page-break-before: always;
                                    }
                                  
                                    @page {
                                      margin: 0;
                                      size: A4;
                                    }
                                  
                                    .pdf-wrapper {
                                      font-family: Arial, Helvetica, sans-serif;
                                      max-width: 800px;
                                      padding: 20px;
                                      display: flex;
                                      flex-direction: column;

                                      gap: 20px;
                                      
                                      {{HighlightedClassStyles}}

                                      .logo-and-address-wrapper {
                                        width: 100%;
                                        display: flex;
                                        flex-direction: row;
                                        align-items: center;

                                        .logo {
                                          width: 50%;
                                        }

                                        .address {
                                          height: 100%;
                                          width: 50%;
                                          display: flex;
                                          flex-direction: row;
                                          gap: 32px;
                                          border: 3px solid black;

                                          .from {}

                                          .to {}
                                        }
                                      }

                                      .transport-text-warning-text-wrapper {
                                        width: 100%;
                                        display: grid;
                                        grid-template-columns: 1fr auto;
                                        align-items: center;

                                        .transport-text {
                                         
                                        }

                                        .warning-text {
                                          padding-left: 5px;
                                          padding-right: 5px;
                                          background-color: red;
                                          font-weight: bold;
                                        }
                                      }

                                      .notes-wrapper {
                                        width: 100%;
                                        display: flex;
                                        flex-direction: column;
                                        border: 3px solid black;
                                        gap: 20px;
                                        min-height: 200px;

                                        .notes-header {

                                        }

                                        .notes-text {

                                        }
                                      }

                                      .additional-info-wrapper {
                                        white-space: pre-line;
                                      }

                                      .reservation-wrapper {
                                        width: 100%;
                                        display: flex;
                                        flex-direction: column;
                                        border: 3px solid black;

                                        .shipment-details-wrapper {
                                          width: 100%;
                                          display: flex;
                                          flex-direction: column;
                                          border-bottom: 3px solid black;

                                          .port-ship-reeder-port-of-loading-waster-wrapper {
                                            display: flex;
                                            flex-direction: row;
                                            width: 100%;

                                            .name {
                                              display: inline-block;
                                              min-width: 150px;
                                            }

                                            .port-ship-reeder-wrapper {
                                              flex: 1;
                                            }

                                            .port-of-loading-waste-wrapper {
                                              width: 30%;
                                            }
                                          }

                                          .reservation-waybill-type-weight-wrapper {
                                            display: flex;
                                            flex-direction: row;

                                            .name {
                                              display: inline-block;
                                              min-width: 150px;
                                            }

                                            .reservation-waybill {
                                              flex: 1;

                                              .name {
                                                display: inline-block;
                                              }
                                            }

                                            .type-weight {
                                              width: 30%;
                                            }
                                          }
                                        }

                                        .delivery-details {
                                          width: 100%;
                                          display: flex;
                                          flex-direction: column;
                                          border-bottom: 3px solid black;

                                          .name {
                                            display: inline-block;
                                            min-width: 320px;
                                          }
                                        }

                                        .content-details {
                                          width: 100%;
                                          min-height: 40px;

                                          .name {
                                            display: inline-block;
                                            min-width: 120px;
                                          }
                                        }

                                        .dangerous-goods-container-details {
                                          width: 100%;
                                          display: flex;
                                          flex-direction: row;

                                          .dangerous-goods-wrapper {
                                            width: 45%;
                                            border-top: 3px solid black;
                                            border-right: 3px solid black;

                                            .item {
                                              .name {
                                                display: inline-block;
                                                min-width: 100px;
                                              }
                                            }
                                          }

                                          .middle-part {
                                            width: 10%;
                                            border-top: 3px solid white;
                                          }

                                          .container-wrapper {
                                            width: 45%;
                                            border-top: 3px solid black;
                                            border-left: 3px solid black;

                                            .item {
                                              .name {
                                                display: inline-block;
                                                min-width: 120px;
                                              }
                                            }
                                          }
                                        }
                                      }

                                      .reservation-stops-wrapper {
                                        width: 100%;
                                        display: flex;
                                        flex-direction: column;
                                        gap: 20px;

                                        .reservation-single-stop {
                                          display: flex;
                                          flex-direction: row;
                                          border: 3px solid black;
                                          width: 100%;

                                          .stop-number-description {
                                            width: 40%;
                                            display: flex;
                                            flex-direction: column;

                                            .stop-number {
                                              flex: 1;
                                            }
                                          }

                                          .receiving-and-returns-type-address {
                                            width: 60%;

                                            .address {
                                              .with-margin {
                                                margin-left: 60px;
                                              }
                                            }
                                          }
                                        }
                                      }
                                    }
                                  </style>
                                </head>
                                """
        );
    }

    private const string HighlightedClassStyles = ".highlighted { color: red; }";

    private static StringBuilder AppendBody(
        this StringBuilder builder,
        IImmutableDictionary<string, PdfEdiElements.Data> bodyData,
        Edi.Domain.Edi edi)
    {
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

        builder.AppendDangerousGoods(edi);
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

        builder.AppendStops(edi)
            .Append("</main>")
            .Append("</body>");

        return builder;
    }

    private static StringBuilder AppendDangerousGoods(this StringBuilder builder, Edi.Domain.Edi edi)
    {
        var goodsData = PdfEdiElements.GetGoodsData(edi);

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

        return builder;
    }

    private static StringBuilder AppendOneDangerousGood(this StringBuilder builder,
        PdfEdiElements.DangerousGoodData dangerousGoodData)
    {
        return builder.Append($"""
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

    private static StringBuilder AppendStops(this StringBuilder builder, Edi.Domain.Edi edi)
    {
        var stops = PdfEdiElements.GetStopsData(edi);

        if (stops.IsEmpty)
            return builder;

        builder.Append("""<div class="reservation-stops-wrapper">""");

        foreach (var stop in stops)
            builder.AppendOneStop(stop);

        builder.Append("</div>");

        return builder;
    }

    private static StringBuilder AppendOneStop(this StringBuilder builder, PdfEdiElements.StopData stop)
    {
        return builder.Append($"""
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

    private static string InsertValue(IImmutableDictionary<string, PdfEdiElements.Data> bodyData, string dataKey) =>
        InsertValue(GetDataForBody(bodyData, dataKey));

    private static string InsertValue(PdfEdiElements.Data data) => data.Value ?? string.Empty;

    private static string MarkHighlighted(IImmutableDictionary<string, PdfEdiElements.Data> bodyData, string dataKey) =>
        MarkHighlighted(GetDataForBody(bodyData, dataKey));

    private static string MarkHighlighted(PdfEdiElements.Data data) => MarkHighlighted(data.IsHighlighted);

    private static string MarkHighlighted(PdfEdiElements.StopData data) => MarkHighlighted(data.IsHighlighted);

    private static string MarkHighlighted(bool isHighlighted)
    {
        const string highlighted = " class =\"highlighted\""; //space at the start is mandatory
        return isHighlighted ? highlighted : string.Empty;
    }

    private static PdfEdiElements.Data GetDataForBody(IImmutableDictionary<string, PdfEdiElements.Data> bodyData,
        string dataKey)
    {
        bodyData.TryGetValue(dataKey, out var data);
        if (data is null)
            throw new KeyNotFoundException($"Key {dataKey} not found in body data for Edi.PDF generation");

        return data;
    }

    private static StringBuilder EndHtml(this StringBuilder builder) => builder.AppendLine("</html>");
}