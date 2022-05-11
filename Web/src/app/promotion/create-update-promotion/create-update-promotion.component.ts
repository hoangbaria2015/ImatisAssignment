import { CustomerDto } from './../../../models/customer.dto';
import { PromotionDto } from './../../../models/promotion.dto';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PromotionService } from 'src/services/promotion.service';
import { CustomerService } from 'src/services/customer.service';
import { PackageDto } from 'src/models/package.dto';
import { PackageService } from 'src/services/package.service';
import { EmployeePackageDto } from 'src/models/employee-package.dto';

@Component({
  selector: 'app-create-update-promotion',
  templateUrl: './create-update-promotion.component.html'
})
export class CreateUpdatePromotionComponent implements OnInit {
  promotion: PromotionDto = { employeePackages : [] };

  customers?: CustomerDto[] = [];

  packages: PackageDto[] = [];

  totalQuantity?: number = 0;
  totalAmount?: number = 0;

  get id(): string | null {
    return this.getParam('id');
  }

  getTotalQuantity() {
    this.totalQuantity = this.promotion?.employeePackages?.reduce((sum, current) => {
      if(current?.quantity)
        sum += current?.quantity
      return sum;
    }, 0);
  }
  
  getTotalAmount() {
    this.totalAmount = this.promotion?.employeePackages?.reduce((sum, current) => {
      if(current?.amount)
        sum += current?.amount
      return sum;
    }, 0);
  }

  calculateAmount(employeePackage :EmployeePackageDto){
    employeePackage.amount = 0;

    let packageDto = this.packages.find(x => x.id == employeePackage.packageId);

    if(packageDto && packageDto?.price && employeePackage?.quantity){
      employeePackage.amount = packageDto?.price * employeePackage?.quantity;
    }

    this.getTotalQuantity();
    this.getTotalAmount();
  }

  constructor(
    private activeRoute: ActivatedRoute,
    protected readonly router: Router,
    private promotionService: PromotionService,
    private customerService: CustomerService,
    private packageService: PackageService
    ) { 

    }

  ngOnInit(): void {
    if(this.id){
      console.log(this.id);
      this.promotionService.getById(this.id).subscribe(result => {
        this.promotion = result;
        this.getTotalQuantity();
        this.getTotalAmount();
      });
    }

    this.customerService.getAll().subscribe(result => {
      this.customers = result;
    });
    
    this.packageService.getAll().subscribe(result => {
      this.packages = result;
    });
  }

  submit():void {
    this.promotionService.save(this.promotion).subscribe(result => {
      this.router.navigate(['/promotion']);
    });
  }

  addEmployeePackage() {
    this.promotion?.employeePackages?.push({
      quantity: 0,
      packageId: this.packages[0].id
    });
  }

  private getParam(param: string): string | null {
    return this.activeRoute.snapshot.paramMap.get(param);
  }
}
