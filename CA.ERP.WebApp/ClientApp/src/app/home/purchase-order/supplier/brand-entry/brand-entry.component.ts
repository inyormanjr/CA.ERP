import { Component, EventEmitter, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { AlertifyService } from 'src/app/services/alertify/alertify.service';
import { BrandService } from '../services/brand.service';
                                                                                   
@Component({
  selector: 'app-brand-entry',
  templateUrl: './brand-entry.component.html',
  styleUrls: ['./brand-entry.component.css']
})
export class BrandEntryComponent implements OnInit {

  brandEntryForm: FormGroup;
  public event: EventEmitter<any> = new EventEmitter()
  constructor(public bsModalRef: BsModalRef,
    private fb : FormBuilder,
    private brandService : BrandService,
    private alertifyService : AlertifyService) { 
    this.brandEntryForm = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required]
    });
  }


  get bef(){
    return this.brandEntryForm.controls;
  }

  ngOnInit(): void {
  }

  saveNewBrand(){
      this.alertifyService.confirm('Create new brand?', ()=>{
        if(!this.brandEntryForm.invalid){
          let newBrand = {data : this.brandEntryForm.value};
          this.brandService.create(newBrand).subscribe((res : any)=>{
              let response = {response : res, brandEntry : newBrand}
              this.event.emit(response);
              this.bsModalRef.hide();
              this.alertifyService.message('Brand successfully added.');
          },error =>{
            this.alertifyService.error(`Error ${error.status} : ${error.title}`)
          });
         }else {
          this.alertifyService.error('Please fill up all required fields.')
        }
      });
  }
}
 