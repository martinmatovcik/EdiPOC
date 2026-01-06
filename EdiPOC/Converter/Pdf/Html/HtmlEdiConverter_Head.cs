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
                                   }
                                   
                                   .pdf-wrapper {{{HighlightedClassStyles}}}
                                   
                                   .pdf-wrapper .logo-and-address-wrapper {
                                     width: 100%;
                                     display: flex;
                                     flex-direction: row;
                                     align-items: center;
                                   }
                                 
                                   .pdf-wrapper .logo-and-address-wrapper .logo {
                                     width: 50%;
                                   }
                                 
                                   .pdf-wrapper .logo-and-address-wrapper .address {
                                     height: 100%;
                                     width: 50%;
                                     display: flex;
                                     flex-direction: row;
                                     gap: 32px;
                                     border: 3px solid black;
                                   }
                                 
                                   .pdf-wrapper .transport-text-warning-text-wrapper {
                                     width: 100%;
                                     display: grid;
                                     grid-template-columns: 1fr auto;
                                     align-items: center;
                                   }
                                 
                                   .pdf-wrapper .transport-text-warning-text-wrapper .warning-text {
                                     padding-left: 5px;
                                     padding-right: 5px;
                                     background-color: red;
                                     font-weight: bold;
                                   }
                                 
                                   .pdf-wrapper .notes-wrapper {
                                     width: 100%;
                                     display: flex;
                                     flex-direction: column;
                                     border: 3px solid black;
                                     gap: 20px;
                                     min-height: 200px;
                                   }
                                 
                                   .pdf-wrapper .additional-info-wrapper {
                                     white-space: pre-line;
                                   }
                                 
                                   .pdf-wrapper .reservation-wrapper {
                                     width: 100%;
                                     display: flex;
                                     flex-direction: column;
                                     border: 3px solid black;
                                   }
                                 
                                   .pdf-wrapper .reservation-wrapper .shipment-details-wrapper {
                                     width: 100%;
                                     display: flex;
                                     flex-direction: column;
                                     border-bottom: 3px solid black;
                                   }
                                 
                                   .pdf-wrapper .reservation-wrapper .shipment-details-wrapper .port-ship-reeder-port-of-loading-waster-wrapper {
                                     display: flex;
                                     flex-direction: row;
                                     width: 100%;
                                   }
                                 
                                   .pdf-wrapper .reservation-wrapper .shipment-details-wrapper .port-ship-reeder-port-of-loading-waster-wrapper .name {
                                     display: inline-block;
                                     min-width: 150px;
                                   }
                                 
                                   .pdf-wrapper .reservation-wrapper .shipment-details-wrapper .port-ship-reeder-port-of-loading-waster-wrapper .port-ship-reeder-wrapper {
                                     flex: 1;
                                   }
                                 
                                   .pdf-wrapper .reservation-wrapper .shipment-details-wrapper .port-ship-reeder-port-of-loading-waster-wrapper .port-of-loading-waste-wrapper {
                                     width: 30%;
                                   }
                                 
                                   .pdf-wrapper .reservation-wrapper .shipment-details-wrapper .reservation-waybill-type-weight-wrapper {
                                     display: flex;
                                     flex-direction: row;
                                   }
                                 
                                   .pdf-wrapper .reservation-wrapper .shipment-details-wrapper .reservation-waybill-type-weight-wrapper .name {
                                     display: inline-block;
                                     min-width: 150px;
                                   }
                                 
                                   .pdf-wrapper .reservation-wrapper .shipment-details-wrapper .reservation-waybill-type-weight-wrapper .reservation-waybill {
                                     flex: 1;
                                   }
                                 
                                   .pdf-wrapper .reservation-wrapper .shipment-details-wrapper .reservation-waybill-type-weight-wrapper .reservation-waybill .name {
                                     display: inline-block;
                                   }
                                 
                                   .pdf-wrapper .reservation-wrapper .shipment-details-wrapper .reservation-waybill-type-weight-wrapper .type-weight {
                                     width: 30%;
                                   }
                                 
                                   .pdf-wrapper .reservation-wrapper .delivery-details {
                                     width: 100%;
                                     display: flex;
                                     flex-direction: column;
                                     border-bottom: 3px solid black;
                                   }
                                 
                                   .pdf-wrapper .reservation-wrapper .delivery-details .name {
                                     display: inline-block;
                                     min-width: 320px;
                                   }
                                 
                                   .pdf-wrapper .reservation-wrapper .content-details {
                                     width: 100%;
                                     min-height: 40px;
                                   }
                                 
                                   .pdf-wrapper .reservation-wrapper .content-details .name {
                                     display: inline-block;
                                     min-width: 120px;
                                   }
                                 
                                   .pdf-wrapper .reservation-wrapper .dangerous-goods-container-details {
                                     width: 100%;
                                     display: flex;
                                     flex-direction: row;
                                   }
                                 
                                   .pdf-wrapper .reservation-wrapper .dangerous-goods-container-details .dangerous-goods-wrapper {
                                     width: 45%;
                                     border-top: 3px solid black;
                                     border-right: 3px solid black;
                                   }
                                 
                                   .pdf-wrapper .reservation-wrapper .dangerous-goods-container-details .dangerous-goods-wrapper .item .name {
                                     display: inline-block;
                                     min-width: 100px;
                                   }
                                 
                                   .pdf-wrapper .reservation-wrapper .dangerous-goods-container-details .middle-part {
                                     width: 10%;
                                     border-top: 3px solid white;
                                   }
                                 
                                   .pdf-wrapper .reservation-wrapper .dangerous-goods-container-details .container-wrapper {
                                     width: 45%;
                                     border-top: 3px solid black;
                                     border-left: 3px solid black;
                                   }
                                 
                                   .pdf-wrapper .reservation-wrapper .dangerous-goods-container-details .container-wrapper .item .name {
                                     display: inline-block;
                                     min-width: 120px;
                                   }
                                 
                                   .pdf-wrapper .reservation-stops-wrapper {
                                     width: 100%;
                                     display: flex;
                                     flex-direction: column;
                                     gap: 20px;
                                   }
                                 
                                   .pdf-wrapper .reservation-stops-wrapper .reservation-single-stop {
                                     display: flex;
                                     flex-direction: row;
                                     border: 3px solid black;
                                     width: 100%;
                                   }
                                 
                                   .pdf-wrapper .reservation-stops-wrapper .reservation-single-stop .stop-number-description {
                                     width: 40%;
                                     display: flex;
                                     flex-direction: column;
                                   }
                                 
                                   .pdf-wrapper .reservation-stops-wrapper .reservation-single-stop .stop-number-description .stop-number {
                                     flex: 1;
                                   }
                                 
                                   .pdf-wrapper .reservation-stops-wrapper .reservation-single-stop .receiving-and-returns-type-address {
                                     width: 60%;
                                   }
                                 
                                   .pdf-wrapper .reservation-stops-wrapper .reservation-single-stop .receiving-and-returns-type-address .address .with-margin {
                                     margin-left: 60px;
                                   }
                                 </style>
                                 
                                 </head>
                                 """
        );
    }

    private const string HighlightedClassStyles = ".highlighted { color: red; }";
}