import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { ModalModule } from 'ngx-bootstrap/modal';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    BsDatepickerModule.forRoot(),
    NgxDatatableModule,
    ModalModule.forRoot(),
    BsDropdownModule.forRoot(),
  ],
  exports: [
    BsDatepickerModule,
    BsDropdownModule,
    ModalModule,
    NgxDatatableModule,
  ],
})
export class NgxBootstrapModulesModule {}
