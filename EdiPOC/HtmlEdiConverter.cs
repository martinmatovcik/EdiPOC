using System.Text;

namespace EdiPOC;

internal static class HtmlEdiConverter
{
    private record Data(bool IsHighlighted, string? Value);

    private record StopData(
        Data AddressCountry,
        Data AddressName,
        Data AddressStreet,
        Data Description,
        Data Number,
        Data ReceivingType);

    public static string Convert()
    {
        var builder = new StringBuilder();
        builder.AppendHead().AppendBody().EndHtml();
        var s = builder.ToString();
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

                                          .from {

                                          }

                                          .to {

                                          }
                                        }
                                      }

                                      .transport-text-warning-text-wrapper {
                                        width: 100%;
                                        display: flex;
                                        flex-direction: row;

                                        .transport-text {
                                          display: flex;
                                          flex: 1;
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

    private static StringBuilder AppendBody(this StringBuilder builder)
    {
        var additionalInfo = new Data(
            false,
            "Der Fahrer muss sich mit PSA ausrüsten und alle für das Betreten der Be- und Entladezone   \n" +
            "erforderlichen Regeln einhalten.  \n" +
            "Bei Problemen/Rueckfragen oder Verzoegerungen bitte um umgehende Info zwecks Weiter\u0002\n" +
            "leitung an unseren Kunden. Sonst die Extrakosten koennen wir leider nicht akzeptieren.  \n" +
            "Unregelmässigkeiten sind unbedingt vor Verlassen des Terminals an den Customer Service und   \n" +
            "das Terminal zu melden.  \n" +
            "\n" +
            "Vielen Dank,  \n" +
            "J. Gablik  \n" +
            "Tel.:  \n" +
            "E-mail:mail@mmail.cz");

        // --- Address To ---
        var addressToCompany = new Data(false, "MHT");
        var addressToCountry = new Data(false, "DE 04808 WURZEN");
        var addressToStreet = new Data(false, "INDUSTRIESTRASSE 4-6dlhaadresariadne");

        // --- Notes ---
        var notes = new Data(false,
            "MRKU 761461-6; SUDU 130506-6; MRKU 708775-2; TLLU 358577-0; TCLU 240554-1; MRKU 756575-9; MSKU 526132-1; MSKU 795322-6; ");

        // --- Reservation ---
        var reservationContainer = new Data(false, "MRKU 761461-6");
        var reservationContents = new Data(false, "Agricultural Machines");
        var reservationCustoms = new Data(false, "T1");

        // Reservation: Dangerous Goods
        var reservationDangerousGoodsClass = new Data(false, "9");
        var reservationDangerousGoodsDesc = new Data(false, "UMWELTGEFÄHRDENDER STOFF, FEST, N.A.G.");
        var reservationDangerousGoodsGroup = new Data(false, "III");
        var reservationDangerousGoodsNumber = new Data(false, "3077");

        var reservationDeliveryDate = new Data(false, "29.05.24 - 07:30");
        var reservationPort = new Data(false, "JP-TOKYO");
        var reservationPortOfLoading = new Data(false, "HMBG");
        var reservationReeder = new Data(false, "MSC/459IHA1124865");
        var reservationRemarks = new Data(false, "DIRECT ZUM EMPF.");
        var reservationRefNumber = new Data(false, "RAUI08957001");
        var reservationSeal = new Data(false, "MLKR0449053");
        var reservationShip = new Data(false, "MADISON MAERSK");
        var reservationTerminalReturnDate = new Data(false, "29.05.24 bis 20:00");
        var reservationType = new Data(false, "40hc");
        var reservationWaste = new Data(false, "NEIN");
        var reservationWaybill = new Data(false, "LEJ2024683158");
        var reservationWeight = new Data(false, "12765");

        // --- Transport & Warning Text ---
        var transportText = new Data(false, "TRANSPORTAUFTRAG - IMPORT Nr.: FOUI07888/8");
        var warningText = new Data(false, "Aenderung");

        return builder
            .Append($"""
                     <body>
                     <main class="pdf-wrapper" id="pdf-root">

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
                           <div{MarkHighlighted(addressToCompany)}>An: {InsertValue(addressToCompany)}</div>
                           <div{MarkHighlighted(addressToStreet)}>{InsertValue(addressToStreet)}</div>
                           <div{MarkHighlighted(addressToCountry)}>{InsertValue(addressToCountry)}</div>
                         </div>
                       </div>
                     </div>

                     <div class="transport-text-warning-text-wrapper">
                        <div {MarkHighlighted(transportText)}>
                          <div class="transport-text">{InsertValue(transportText)}</div>
                        </div>
                        <div class="warning-text">{InsertValue(warningText)}</div>
                     </div>

                     <div class="notes-wrapper">
                        <div class="notes-header">Bemerkungen:</div>
                        <div {MarkHighlighted(notes)}>
                          <div class="notes-text">{InsertValue(notes)}</div>
                        </div>
                     </div>

                     <div class="additional-info-wrapper">
                        <div {MarkHighlighted(additionalInfo)}>
                          {InsertValue(additionalInfo)}
                        </div>
                     </div>

                     <div class="reservation-wrapper">
                       <div class="shipment-details-wrapper">
                         <div class="port-ship-reeder-port-of-loading-waster-wrapper">
                           <div class="port-ship-reeder-wrapper">
                             <div{MarkHighlighted(reservationPort)}>
                               <div class="name">Hafen:</div> {InsertValue(reservationPort)}
                             </div>
                             <div{MarkHighlighted(reservationShip)}>
                               <div class="name">Schiff:</div> {InsertValue(reservationShip)}
                             </div>
                             <div{MarkHighlighted(reservationReeder)}>
                               <div class="name">Reeder:</div> {InsertValue(reservationReeder)}
                             </div>
                           </div>

                           <div class="port-of-loading-waste-wrapper">
                             <div{MarkHighlighted(reservationPortOfLoading)}>
                               <div class="name">Abgangshafen:</div>
                               {InsertValue(reservationPortOfLoading)}
                             </div>

                             <div{MarkHighlighted(reservationWaste)}>
                               <div class="name">Abfall:</div>
                               {InsertValue(reservationWaste)}
                             </div>
                           </div>
                         </div>

                         <div class="reservation-waybill-type-weight-wrapper">
                           <div class="reservation-waybill">
                             <div{MarkHighlighted(reservationRefNumber)}>
                               <div class="name"><b>BuchNr. Ref.:</b></div>
                               {InsertValue(reservationRefNumber)}
                             </div>

                             <div{MarkHighlighted(reservationWaybill)}>
                               <div class="name"><b>Frachtbrief#:</b></div>
                               {InsertValue(reservationWaybill)}
                             </div>
                           </div>

                           <div class="type-weight">
                             <div{MarkHighlighted(reservationType)}>
                               <div class="name"><b>Type:</b></div>
                               {InsertValue(reservationType)}
                             </div>

                             <div{MarkHighlighted(reservationWeight)}>
                               <div class="name"><b>Gewicht:</b></div>
                               {InsertValue(reservationWeight)}
                             </div>
                           </div>
                         </div>
                       </div>

                       <div class="delivery-details">
                         <div{MarkHighlighted(reservationDeliveryDate)}>
                           <div class="name"><b>Zustellungstermin:</b></div>
                           {InsertValue(reservationDeliveryDate)}
                         </div>

                         <div{MarkHighlighted(reservationTerminalReturnDate)}>
                           <div class="name"><b>Terminal Rückgabe:</b></div>
                           {InsertValue(reservationTerminalReturnDate)}
                         </div>
                       </div>

                       <div class="content-details">
                         <div{MarkHighlighted(reservationContents)}>
                           <div class="name"><b>Inhalt:</b></div>
                           {InsertValue(reservationContents)}
                         </div>
                       </div>

                       <div class="dangerous-goods-container-details">
                         <div class="dangerous-goods-wrapper">
                           <div class="dangerous-goods-header">
                             <b>Gefahrgut</b>
                           </div>

                           <div class="item">
                              <div {MarkHighlighted(reservationDangerousGoodsNumber)}>
                                <div class="name">UN</div>
                                {InsertValue(reservationDangerousGoodsNumber)}
                              </div>
                           </div>

                           <div class="item">
                           <div {MarkHighlighted(reservationDangerousGoodsClass)}>
                             <div class="name">Class</div>
                             {InsertValue(reservationDangerousGoodsClass)}
                           </div>
                           </div>

                           <div class="item">
                              <div {MarkHighlighted(reservationDangerousGoodsGroup)}>
                                <div class="name">Pck grp</div>
                                {InsertValue(reservationDangerousGoodsGroup)}
                              </div>
                           </div>

                           <div class="item">
                              <div {MarkHighlighted(reservationDangerousGoodsDesc)}>
                                {InsertValue(reservationDangerousGoodsDesc)}
                              </div>
                           </div>
                         </div>

                         <div class="middle-part"></div>

                         <div class="container-wrapper">
                           <div class="item">
                              <div {MarkHighlighted(reservationContainer)}>
                                <div class="name"><b>Container:</b></div>
                                {InsertValue(reservationContainer)}
                              </div>
                           </div>

                           <div class="item">
                              <div {MarkHighlighted(reservationSeal)}>
                                <div class="name"><b>Siegel:</b></div>
                                {InsertValue(reservationSeal)}
                              </div>
                           </div>

                           <div class="item">
                              <div {MarkHighlighted(reservationCustoms)}>
                                <div class="name"><b>Art zollverfahren:</b></div>
                                {InsertValue(reservationCustoms)}
                              </div>
                           </div>

                           <div class="item">
                              <div {MarkHighlighted(reservationDangerousGoodsGroup)}>
                                <div class="name"><b>Bemerkungen:</b></div>
                                {InsertValue(reservationDangerousGoodsGroup)}
                              </div>
                           </div>
                         </div>
                       </div>
                     </div>
                     """
            )
            .AppendStops()
            .Append("</main>")
            .Append("</body>");
    }


    private static StringBuilder AppendStops(this StringBuilder builder)
    {
        //TODO: Create and render in foreach

        var stops = new List<StopData>
        {
            // Stop 1
            new(
                new Data(false, "DE-04158 Leipzig"),
                new Data(false, "Deutsche Umschlaggesellschaft Schiene–Straße (DUSS) mbH"),
                new Data(false, "Hans-Grade-Str. 2"),
                new Data(false, "multistop desc1"),
                new Data(false, "1"),
                new Data(false, "Abnahmeterminal")),
            // Stop 2
            new(
                new Data(false, "DE 06237 LEUNA"),
                new Data(false, "EUNA HARZE GMBH"),
                new Data(false, "AM HAUPTTOR -BAU 6619"),
                new Data(false, "multistop desc2"),
                new Data(false, "2"),
                new Data(false, "Empfänger")
            ),
            // Stop 3
            new(
                new Data(false, "DE-04158 Leipzig"),
                new Data(false, "DB Intermodel Services GmbH"),
                new Data(false, "Am Exer 10"),
                new Data(false, "multistop desc3"),
                new Data(false, "3"),
                new Data(false, "Rücklieferung")
            )
        };

        return builder
            .Append("""<div class="reservation-stops-wrapper">""")
//             .Append($"""
//                      <div class="reservation-single-stop">
//                        <div class="stop-number-description">
//                          <div class="stop-number">
//                            <b>Stop {stop.number.value}</b>
//                          </div>
//                          <div{MarkHighlighted()}>
//                            {stop.description.value ?? ""}
//                          </div>
//                        </div>
//
//                        <div class="receiving-and-returns-type-address">
//                          <div class="receiving-and-returns-type">
//                            <b>{stop.receivingAndReturnsType.value ?? ""}</b>
//                          </div>
//                          <div class="address">
//                            <div{MarkHighlighted()}>Address: {stop.address?.name.value ?? ""}</div>
//                            <div class="with-margin">{stop.address?.street.value ?? ""}</div>
//                            <div class="with-margin">{stop.address?.countryIsoPostalCodeCity.value ?? ""}</div>
//                          </div>
//                        </div>
//                      </div>
//                      """
//             )
            .Append("</div>");
    }

    private static string InsertValue(Data data) => data.Value ?? string.Empty;

    private static string MarkHighlighted(Data data)
    {
        const string highlighted = " class =\"highlighted\""; //space at the start is mandatory
        return data.IsHighlighted ? highlighted : string.Empty;
    }

    private static StringBuilder EndHtml(this StringBuilder builder) => builder.Append("</html>");
}