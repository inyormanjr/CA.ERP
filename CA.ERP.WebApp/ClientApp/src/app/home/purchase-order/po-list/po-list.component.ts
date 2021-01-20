import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Store } from '@ngrx/store';
import { noop, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { PaginationResult } from 'src/app/models/data.pagination';
import { PoActionTypes } from '../actions/po.actions.selector';
import { PurchaseOrder } from '../models/new-purchase-order';
import { PurchaseOrderService } from '../purchase-order.service';
import { PurchaseOrderState } from '../reducers';
import { PurchaseOrderSelectorType } from '../reducers/purchase-order.selector.type';

@Component({
  selector: 'app-po-list',
  templateUrl: './po-list.component.html',
  styleUrls: ['./po-list.component.css'],
})
export class PoListComponent implements OnInit {
  poPaginationResult$: Observable<PaginationResult<PurchaseOrder[]>>;
  barcode: string;
  dateSearch: string;
  constructor(
    private activatedRoute: ActivatedRoute,
    private poservice: PurchaseOrderService,
     private store: Store<PurchaseOrderState>
  ) {

     this.activatedRoute.data.pipe(
       map((x: any) => {
        this.store.dispatch(PoActionTypes.loadPurchaseOrderPaginationResult({ paginationResult: x.data }));
         this.poPaginationResult$ = this.store.select(
           PurchaseOrderSelectorType.purchaseOrderPaginationResult
         );
      })
    ).subscribe(noop);
  }

  pageChange(event) {
    this.store.dispatch(
      PoActionTypes.fetchPurchaseOrders({
        params: {
          page: event,
          pageSize: 5,
          startDate: this.dateSearch,
          endDate: this.dateSearch,
        },
      })
    );
  }

  search() {
    this.store.dispatch(PoActionTypes.fetchPurchaseOrders(
      { params: { page: 1, pageSize: 5, startDate: this.dateSearch, endDate: this.dateSearch } }));
  }

  viewAndPrintPDF(id) {
    window.open(this.poservice.getPdfReportingById(id)).print();
  }

  ngOnInit(): void {}
}
