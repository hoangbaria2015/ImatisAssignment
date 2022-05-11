import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateUpdatePromotionComponent } from './promotion/create-update-promotion/create-update-promotion.component';
import { PromotionComponent } from './promotion/promotion.component';

const routes: Routes = [
  { path: 'promotion', component: PromotionComponent },
  { path: 'promotion/create', component: CreateUpdatePromotionComponent },
  { path: 'promotion/edit/:id', component: CreateUpdatePromotionComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
