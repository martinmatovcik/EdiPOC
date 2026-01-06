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
                                     height: 0;
                                     line-height: 0;
                                     page-break-before: always;
                                     break-before: page;
                                   }
                                 
                                   .avoid-page-break {
                                     page-break-inside: avoid;
                                     break-inside: avoid-page;
                                   }
                                 
                                   @page {
                                     margin: 0;
                                     size: A4;
                                   }
                                 
                                   .pdf-wrapper {
                                     font-family: Arial, Helvetica, sans-serif;
                                     max-width: 800px;
                                     padding: 20px;
                                     display: block;
                                   }
                                 
                                   .pdf-wrapper > * {
                                     margin-bottom: 20px;
                                   }
                                 
                                   .pdf-wrapper > *:last-child {
                                     margin-bottom: 0;
                                   }
                                 
                                   .pdf-wrapper {{{HighlightedClassStyles}}}
                                 
                                   .pdf-wrapper .logo-and-address-wrapper {
                                     width: 100%;
                                     display: table;
                                     table-layout: fixed;
                                   }
                                 
                                   .pdf-wrapper .logo-and-address-wrapper .logo,
                                   .pdf-wrapper .logo-and-address-wrapper .address {
                                     display: table-cell;
                                     vertical-align: middle;
                                   }
                                 
                                   .pdf-wrapper .logo-and-address-wrapper .logo {
                                     width: 50%;
                                   }
                          
                                   .pdf-wrapper .logo-and-address-wrapper .address {
                                     width: 50%;
                                     height: 100%;
                                     border: 3px solid black;
                                   }
                                 
                                   .pdf-wrapper .logo-and-address-wrapper .address .from,
                                   .pdf-wrapper .logo-and-address-wrapper .address .to {
                                     display: inline-block;
                                     vertical-align: top;
                                     width: 45%;
                                     font-size: inherit;
                                   }
                                 
                                   .pdf-wrapper .logo-and-address-wrapper .address .from {
                                     padding-right: 16px;
                                   }
                                   
                                   .pdf-wrapper .logo-and-address-wrapper .address .to {
                                     padding-left: 16px;
                                   }
                                 
                                   .pdf-wrapper .transport-text-warning-text-wrapper {
                                     width: 100%;
                                     display: table;
                                     table-layout: fixed;
                                   }
                                 
                                   .pdf-wrapper .transport-text-warning-text-wrapper .transport-text,
                                   .pdf-wrapper .transport-text-warning-text-wrapper .warning-text {
                                     display: table-cell;
                                     vertical-align: middle;
                                   }
                                 
                                   .pdf-wrapper .transport-text-warning-text-wrapper .transport-text {
                                     width: 100%;
                                   }
                                 
                                   .pdf-wrapper .transport-text-warning-text-wrapper .warning-text {
                                     width: 12%;
                                     white-space: nowrap;
                                     padding-left: 5px;
                                     padding-right: 5px;
                                     background-color: red;
                                     font-weight: bold;
                                   }
                                 
                                   .pdf-wrapper .notes-wrapper {
                                     width: 97%;
                                     border: 3px solid black;
                                     min-height: 200px;
                                     display: block;
                                     padding: 10px;
                                   }
                                 
                                   .pdf-wrapper .notes-wrapper .notes-header {
                                     margin-bottom: 20px;
                                   }
                                 
                                   .pdf-wrapper .additional-info-wrapper {
                                     white-space: pre-line;
                                   }
                                 
                                   .pdf-wrapper .reservation-wrapper {
                                     width: 99.5%;
                                     border: 3px solid black;
                                     display: block;
                                   }
                                 
                                   .pdf-wrapper .reservation-wrapper .shipment-details-wrapper {
                                     width: 100%;
                                     border-bottom: 3px solid black;
                                     display: block;
                                   }
                                 
                                   .pdf-wrapper .reservation-wrapper .shipment-details-wrapper .port-ship-reeder-port-of-loading-waster-wrapper {
                                     width: 100%;
                                     display: table;
                                     table-layout: fixed;
                                   }
                                 
                                   .pdf-wrapper .reservation-wrapper .shipment-details-wrapper .port-ship-reeder-port-of-loading-waster-wrapper .port-ship-reeder-wrapper,
                                   .pdf-wrapper .reservation-wrapper .shipment-details-wrapper .port-ship-reeder-port-of-loading-waster-wrapper .port-of-loading-waste-wrapper {
                                     display: table-cell;
                                     vertical-align: top;
                                   }
                                 
                                   .pdf-wrapper .reservation-wrapper .shipment-details-wrapper .port-ship-reeder-port-of-loading-waster-wrapper .port-ship-reeder-wrapper {
                                     width: 70%;
                                   }
                                 
                                   .pdf-wrapper .reservation-wrapper .shipment-details-wrapper .port-ship-reeder-port-of-loading-waster-wrapper .port-of-loading-waste-wrapper {
                                     width: 30%;
                                   }
                                 
                                   .pdf-wrapper .reservation-wrapper .shipment-details-wrapper .port-ship-reeder-port-of-loading-waster-wrapper .name {
                                     display: inline-block;
                                     min-width: 150px;
                                   }
                                 
                                   .pdf-wrapper .reservation-wrapper .shipment-details-wrapper .reservation-waybill-type-weight-wrapper {
                                     width: 100%;
                                     display: table;
                                     table-layout: fixed;
                                   }
                                 
                                   .pdf-wrapper .reservation-wrapper .shipment-details-wrapper .reservation-waybill-type-weight-wrapper .reservation-waybill,
                                   .pdf-wrapper .reservation-wrapper .shipment-details-wrapper .reservation-waybill-type-weight-wrapper .type-weight {
                                     display: table-cell;
                                     vertical-align: top;
                                   }
                                 
                                   .pdf-wrapper .reservation-wrapper .shipment-details-wrapper .reservation-waybill-type-weight-wrapper .reservation-waybill {
                                     width: 70%;
                                   }
                                 
                                   .pdf-wrapper .reservation-wrapper .shipment-details-wrapper .reservation-waybill-type-weight-wrapper .type-weight {
                                     width: 30%;
                                   }
                                 
                                   .pdf-wrapper .reservation-wrapper .shipment-details-wrapper .reservation-waybill-type-weight-wrapper .name {
                                     display: inline-block;
                                     min-width: 150px;
                                   }
                                 
                                   .pdf-wrapper .reservation-wrapper .shipment-details-wrapper .reservation-waybill-type-weight-wrapper .reservation-waybill .name {
                                     display: inline-block;
                                   }
                                 
                                   .pdf-wrapper .reservation-wrapper .delivery-details {
                                     width: 100%;
                                     border-bottom: 3px solid black;
                                     display: block;
                                   }
                                 
                                   .pdf-wrapper .reservation-wrapper .delivery-details .name {
                                     display: inline-block;
                                     min-width: 320px;
                                   }
                                 
                                   .pdf-wrapper .reservation-wrapper .content-details {
                                     width: 100%;
                                     min-height: 40px;
                                     display: block;
                                   }
                                 
                                   .pdf-wrapper .reservation-wrapper .content-details .name {
                                     display: inline-block;
                                     min-width: 120px;
                                   }
                                 
                                   .pdf-wrapper .reservation-wrapper .dangerous-goods-container-details {
                                     width: 100%;
                                     display: table;
                                     table-layout: fixed;
                                   }
                                 
                                   .pdf-wrapper .reservation-wrapper .dangerous-goods-container-details .dangerous-goods-wrapper,
                                   .pdf-wrapper .reservation-wrapper .dangerous-goods-container-details .middle-part,
                                   .pdf-wrapper .reservation-wrapper .dangerous-goods-container-details .container-wrapper {
                                     display: table-cell;
                                     vertical-align: top;
                                   }
                                 
                                   .pdf-wrapper .reservation-wrapper .dangerous-goods-container-details .dangerous-goods-wrapper {
                                     width: 45%;
                                     border-top: 3px solid black;
                                     border-right: 3px solid black;
                                   }
                                 
                                   .pdf-wrapper .reservation-wrapper .dangerous-goods-container-details .middle-part {
                                     width: 10%;
                                     border-top: 0;
                                   }
                                 
                                   .pdf-wrapper .reservation-wrapper .dangerous-goods-container-details .container-wrapper {
                                     width: 45%;
                                     border-top: 3px solid black;
                                     border-left: 3px solid black;
                                   }
                                 
                                   .pdf-wrapper .reservation-wrapper .dangerous-goods-container-details .dangerous-goods-wrapper .item .name {
                                     display: inline-block;
                                     min-width: 100px;
                                   }
                                 
                                   .pdf-wrapper .reservation-wrapper .dangerous-goods-container-details .container-wrapper .item .name {
                                     display: inline-block;
                                     min-width: 120px;
                                   }
                                 
                                   .pdf-wrapper .reservation-stops-wrapper {
                                     width: 100%;
                                     display: block;
                                   }
                                 
                                   .pdf-wrapper .reservation-stops-wrapper .reservation-single-stop {
                                     margin-bottom: 20px;
                                   }
                                 
                                 
                                   .pdf-wrapper .reservation-stops-wrapper .reservation-single-stop {
                                     width: 99.5%;
                                     border: 3px solid black;
                                     display: table;
                                     table-layout: fixed;
                                     page-break-inside: avoid;
                                     break-inside: avoid-page;
                                     margin-bottom: 20px;
                                   }
                                 
                                   .pdf-wrapper .reservation-stops-wrapper .reservation-single-stop .stop-number-description,
                                   .pdf-wrapper .reservation-stops-wrapper .reservation-single-stop .receiving-and-returns-type-address {
                                     display: table-cell;
                                     vertical-align: top;
                                   }
                                 
                                   .pdf-wrapper .reservation-stops-wrapper .reservation-single-stop .stop-number-description {
                                     width: 40%;
                                   }
                                 
                                   .pdf-wrapper .reservation-stops-wrapper .reservation-single-stop .receiving-and-returns-type-address {
                                     width: 60%;
                                   }
                                 
                                   .pdf-wrapper .reservation-stops-wrapper .reservation-single-stop .stop-number-description .stop-number {
                                     display: block;
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