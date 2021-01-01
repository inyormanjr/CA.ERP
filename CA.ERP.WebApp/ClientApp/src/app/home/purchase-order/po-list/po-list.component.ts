import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { PurchaseOrder } from '../models/new-purchase-order';

@Component({
  selector: 'app-po-list',
  templateUrl: './po-list.component.html',
  styleUrls: ['./po-list.component.css']
})
export class PoListComponent implements OnInit {
  purchaseOrdersList$: Observable<PurchaseOrder[]>;
  constructor(private activatedRoute: ActivatedRoute) {
    console.log(this.activatedRoute.data);
    this.purchaseOrdersList$ = this.activatedRoute.data.pipe(map((x: any) => x.data));
  }

  ngOnInit(): void {
  }

}
