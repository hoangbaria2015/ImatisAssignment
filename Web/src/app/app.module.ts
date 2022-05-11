import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PromotionService } from '../services/promotion.service';
import { PromotionComponent } from './promotion/promotion.component';
import { CreateUpdatePromotionComponent } from './promotion/create-update-promotion/create-update-promotion.component';
import { CustomerService } from 'src/services/customer.service';
import { FormsModule } from '@angular/forms';
import { PackageService } from 'src/services/package.service';

@NgModule({
  declarations: [
    AppComponent,
    PromotionComponent,
    CreateUpdatePromotionComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [PromotionService, CustomerService, PackageService],
  bootstrap: [AppComponent]
})
export class AppModule { }
