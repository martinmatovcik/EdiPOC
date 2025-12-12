using System.Text;

namespace EdiPOC.Converter.Pdf.Html;

internal static partial class HtmlEdiConverter
{
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
}