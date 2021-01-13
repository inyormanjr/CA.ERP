import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { PurchaseOrder } from '../models/new-purchase-order';
import { PurchaseOrderService } from '../purchase-order.service';

@Component({
  selector: 'app-po-list',
  templateUrl: './po-list.component.html',
  styleUrls: ['./po-list.component.css']
})
export class PoListComponent implements OnInit {
  purchaseOrdersList$: Observable<PurchaseOrder[]>;
  constructor(private activatedRoute: ActivatedRoute,
  private poservice: PurchaseOrderService) {
    this.purchaseOrdersList$ = this.activatedRoute.data.pipe(map((x: any) => x.data));
  }

  viewAndPrintPDF(id) {
     window.open(this.poservice.getPdfReportingById(id)).print();
  }

  ngOnInit(): void {
  }

}
