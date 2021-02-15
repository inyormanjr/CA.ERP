import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { ModalModule } from 'ngx-bootstrap/modal';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { NgxPaginationModule } from 'ngx-pagination';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { TabsModule } from 'ngx-bootstrap/tabs';
@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    BsDatepickerModule.forRoot(),
    NgxDatatableModule,
    ModalModule.forRoot(),
    BsDropdownModule.forRoot(),
    NgxPaginationModule,
    TabsModule.forRoot(),
    CollapseModule.forRoot(),
  ],
  exports: [
    BsDatepickerModule,
    BsDropdownModule,
    ModalModule,
    NgxDatatableModule,
    NgxPaginationModule,
    TabsModule,
    CollapseModule
  ],
})
export class NgxBootstrapModulesModule {}
