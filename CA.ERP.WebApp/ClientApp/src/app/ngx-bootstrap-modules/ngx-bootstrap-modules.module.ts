import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { ModalModule } from 'ngx-bootstrap/modal';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { NgxPaginationModule } from 'ngx-pagination';
@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    BsDatepickerModule.forRoot(),
    NgxDatatableModule,
    ModalModule.forRoot(),
    BsDropdownModule.forRoot(),
    NgxPaginationModule,
  ],
  exports: [
    BsDatepickerModule,
    BsDropdownModule,
    ModalModule,
    NgxDatatableModule,
    NgxPaginationModule,
  ],
})
export class NgxBootstrapModulesModule {}
