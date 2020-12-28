import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-branchEntry',
  templateUrl: './branchEntry.component.html',
  styleUrls: ['./branchEntry.component.scss']
})
export class BranchEntryComponent implements OnInit {
  branchFormEntry: FormGroup;
  constructor(private fB: FormBuilder) {
    this.branchFormEntry = fB.group({
      name: ['', Validators.required],
      branchNo: ['', Validators.required],
      code: ['', Validators.required],
      address: ['', Validators.required],
      contact: ['', Validators.required]
    });
  }

  ngOnInit() {
  }

}
