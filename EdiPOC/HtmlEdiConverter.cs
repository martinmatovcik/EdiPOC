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
            .Append($$"""
                      <body>
                      <main class="pdf-wrapper" id="pdf-root">

                      <div class="logo-and-address-wrapper">
                        <div class="logo">
                          <svg xmlns="http://www.w3.org/2000/svg" id="Vrstva_1" version="1.1" viewBox="190 200 500 176">
                      <!-- Generator: Adobe Illustrator 30.0.0, SVG Export Plug-In . SVG Version: 2.1.1 Build 123)  -->
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
                            <div{{MarkHighlighted(addressToCompany)}}>An: {{InsertValue(addressToCompany)}}</div>
                            <div{{MarkHighlighted(addressToStreet)}}>{{InsertValue(addressToStreet)}}</div>
                            <div{{MarkHighlighted(addressToCountry)}}>{{InsertValue(addressToCountry)}}</div>
                          </div>
                        </div>
                      </div>

                      <div class="transport-text-warning-text-wrapper">
                         <div {{MarkHighlighted(transportText)}}>
                           <div class="transport-text">{{InsertValue(transportText)}}</div>
                         </div>
                         <div class="warning-text">{{InsertValue(warningText)}}</div>
                      </div>

                      <div class="notes-wrapper">
                         <div class="notes-header">Bemerkungen:</div>
                         <div {{MarkHighlighted(notes)}}>
                           <div class="notes-text">{{InsertValue(notes)}}</div>
                         </div>
                      </div>

                      <div class="additional-info-wrapper">
                         <div {{MarkHighlighted(additionalInfo)}}>
                           {{InsertValue(additionalInfo)}}
                         </div>
                      </div>

                      <div class="reservation-wrapper">
                        <div class="shipment-details-wrapper">
                          <div class="port-ship-reeder-port-of-loading-waster-wrapper">
                            <div class="port-ship-reeder-wrapper">
                              <div{{MarkHighlighted(reservationPort)}}>
                                <div class="name">Hafen:</div> {{InsertValue(reservationPort)}}
                              </div>
                              <div{{MarkHighlighted(reservationShip)}}>
                                <div class="name">Schiff:</div> {{InsertValue(reservationShip)}}
                              </div>
                              <div{{MarkHighlighted(reservationReeder)}}>
                                <div class="name">Reeder:</div> {{InsertValue(reservationReeder)}}
                              </div>
                            </div>

                            <div class="port-of-loading-waste-wrapper">
                              <div{{MarkHighlighted(reservationPortOfLoading)}}>
                                <div class="name">Abgangshafen:</div>
                                {{InsertValue(reservationPortOfLoading)}}
                              </div>

                              <div{{MarkHighlighted(reservationWaste)}}>
                                <div class="name">Abfall:</div>
                                {{InsertValue(reservationWaste)}}
                              </div>
                            </div>
                          </div>

                          <div class="reservation-waybill-type-weight-wrapper">
                            <div class="reservation-waybill">
                              <div{{MarkHighlighted(reservationRefNumber)}}>
                                <div class="name"><b>BuchNr. Ref.:</b></div>
                                {{InsertValue(reservationRefNumber)}}
                              </div>

                              <div{{MarkHighlighted(reservationWaybill)}}>
                                <div class="name"><b>Frachtbrief#:</b></div>
                                {{InsertValue(reservationWaybill)}}
                              </div>
                            </div>

                            <div class="type-weight">
                              <div{{MarkHighlighted(reservationType)}}>
                                <div class="name"><b>Type:</b></div>
                                {{InsertValue(reservationType)}}
                              </div>

                              <div{{MarkHighlighted(reservationWeight)}}>
                                <div class="name"><b>Gewicht:</b></div>
                                {{InsertValue(reservationWeight)}}
                              </div>
                            </div>
                          </div>
                        </div>

                        <div class="delivery-details">
                          <div{{MarkHighlighted(reservationDeliveryDate)}}>
                            <div class="name"><b>Zustellungstermin:</b></div>
                            {{InsertValue(reservationDeliveryDate)}}
                          </div>

                          <div{{MarkHighlighted(reservationTerminalReturnDate)}}>
                            <div class="name"><b>Terminal Rückgabe:</b></div>
                            {{InsertValue(reservationTerminalReturnDate)}}
                          </div>
                        </div>

                        <div class="content-details">
                          <div{{MarkHighlighted(reservationContents)}}>
                            <div class="name"><b>Inhalt:</b></div>
                            {{InsertValue(reservationContents)}}
                          </div>
                        </div>

                        <div class="dangerous-goods-container-details">
                          <div class="dangerous-goods-wrapper">
                            <div class="dangerous-goods-header">
                              <b>Gefahrgut</b>
                            </div>

                            <div class="item">
                               <div {{MarkHighlighted(reservationDangerousGoodsNumber)}}>
                                 <div class="name">UN</div>
                                 {{InsertValue(reservationDangerousGoodsNumber)}}
                               </div>
                            </div>

                            <div class="item">
                            <div {{MarkHighlighted(reservationDangerousGoodsClass)}}>
                              <div class="name">Class</div>
                              {{InsertValue(reservationDangerousGoodsClass)}}
                            </div>
                            </div>

                            <div class="item">
                               <div {{MarkHighlighted(reservationDangerousGoodsGroup)}}>
                                 <div class="name">Pck grp</div>
                                 {{InsertValue(reservationDangerousGoodsGroup)}}
                               </div>
                            </div>

                            <div class="item">
                               <div {{MarkHighlighted(reservationDangerousGoodsDesc)}}>
                                 {{InsertValue(reservationDangerousGoodsDesc)}}
                               </div>
                            </div>
                          </div>

                          <div class="middle-part"></div>

                          <div class="container-wrapper">
                            <div class="item">
                               <div {{MarkHighlighted(reservationContainer)}}>
                                 <div class="name"><b>Container:</b></div>
                                 {{InsertValue(reservationContainer)}}
                               </div>
                            </div>

                            <div class="item">
                               <div {{MarkHighlighted(reservationSeal)}}>
                                 <div class="name"><b>Siegel:</b></div>
                                 {{InsertValue(reservationSeal)}}
                               </div>
                            </div>

                            <div class="item">
                               <div {{MarkHighlighted(reservationCustoms)}}>
                                 <div class="name"><b>Art zollverfahren:</b></div>
                                 {{InsertValue(reservationCustoms)}}
                               </div>
                            </div>

                            <div class="item">
                               <div {{MarkHighlighted(reservationDangerousGoodsGroup)}}>
                                 <div class="name"><b>Bemerkungen:</b></div>
                                 {{InsertValue(reservationDangerousGoodsGroup)}}
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