import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { map } from 'rxjs/operators';
import { PurchaseOrderService } from '../purchase-order.service';

@Component({
  selector: 'app-purchase-order-pdf-viewer',
  templateUrl: './purchase-order-pdf-viewer.component.html',
  styleUrls: ['./purchase-order-pdf-viewer.component.css']
})
export class PurchaseOrderPdfViewerComponent implements OnInit {
  pdfSrc: any;
  constructor(private activatedRoute: ActivatedRoute, private poservice: PurchaseOrderService) {
    this.activatedRoute.params.subscribe(result => {
     this.pdfSrc = this.poservice
       .getPdfReportingById(result.id);
    });
  }
  printPDF() {
    window.open(this.pdfSrc).print();
  }
  ngOnInit(): void {
  }

}
