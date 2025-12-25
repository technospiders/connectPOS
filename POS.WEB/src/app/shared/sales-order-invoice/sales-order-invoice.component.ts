import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { CompanyProfile } from '@core/domain-classes/company-profile';
import { SalesOrder } from '@core/domain-classes/sales-order';
import { SalesOrderItem } from '@core/domain-classes/sales-order-item';
import { SecurityService } from '@core/security/security.service';

@Component({
  selector: 'app-sales-order-invoice',
  templateUrl: './sales-order-invoice.component.html',
  styleUrls: ['./sales-order-invoice.component.scss']
})
export class SalesOrderInvoiceComponent implements OnInit, OnChanges {

  @Input() salesOrder: SalesOrder;
  salesOrderForInvoice: SalesOrder;
  companyProfile: CompanyProfile;
  salesOrderItems: SalesOrderItem[];
  salesOrderReturnsItems: SalesOrderItem[];

  constructor(private securityService: SecurityService) { }

  ngOnInit(): void {
    this.subScribeCompanyProfile();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['salesOrder']) {
      this.salesOrder.totalQuantity = this.salesOrder.salesOrderItems.map(item => item.status == 0 ? item.quantity : (-1) * item.quantity).reduce((prev, next) => prev + next);
      this.salesOrderItems = this.salesOrder.salesOrderItems.filter(c => c.status == 0);
      this.salesOrderReturnsItems = this.salesOrder.salesOrderItems.filter(c => c.status == 1);
      this.salesOrderForInvoice = this.salesOrder;
      this.salesOrder = null;
    }
    setTimeout(() => {
      this.printInvoice();
    }, 1000);
  }

  subScribeCompanyProfile() {
    this.securityService.companyProfile.subscribe(data => {
      this.companyProfile = data;
    });
  }

  printInvoice() {
    let name = this.salesOrderForInvoice.orderNumber;
    let printContents, popupWin;
    printContents = document.getElementById('salesOrderForInvoice').innerHTML;
    popupWin = window.open('', '_blank', 'top=0,left=0,height=100%,width=auto');
    popupWin.document.open();
    popupWin.document.write(`
        <html>
          <head>
            <title>${name}</title>
            <style>
            @page { size: auto;  margin: 0mm;  margin-top:0px; }

            @media print {
              * {
                font-family: "Hind-Vadodara", sans-serif;
                -webkit-print-color-adjust: exact;
              }
            }
.TP80mm {
            @media print {
              * {
                     font-size: 10px;
              }
            }
    body {
      margin: 0;
      padding: 0;
    }
    .p0{
    padding: 0px;
    }
    .m0{
    margin: 0px;
    }
    
    .print-page {
      width: 80mm;
      margin: auto;
      padding: 10px;
    }
    
    .center-section {
      text-align: center;
    }
    
    table {
      width: 100%;
      border-collapse: collapse;
    }
    
    table, th, td {
      border: 1px dotted grey;
    }
    
    th, td {
      padding: 5px;
      text-align: left;
    }
    
    .signature-section {
      margin-top: 20px;
      text-align: right;
    }
  }
                .dotted-line {
              border-bottom: 1px dotted #000; /* Dotted line for dividers */
              margin: 10px 0;
            }
            </style>
            <script>
            function loadHandler(){

            var is_chrome = function () { return Boolean(window.chrome); }
        if(is_chrome)
        {
           window.print();
           setTimeout(function(){window.close();}, 1000);
           //give them 10 seconds to print, then close
        }
        else
        {
           window.print();
           window.close();
        }
        }
        </script>
          </head>
      <body onload="loadHandler()">${printContents}</body>
        </html>
    `
    );
    popupWin.document.close();
  }

}
