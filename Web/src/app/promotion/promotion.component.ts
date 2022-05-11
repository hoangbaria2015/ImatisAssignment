import { PromotionDto } from './../../models/promotion.dto';
import { Component, OnInit } from '@angular/core';
import { PromotionService } from '../../services/promotion.service';

@Component({
  selector: 'app-promotion',
  templateUrl: './promotion.component.html'
})
export class PromotionComponent implements OnInit {

  promotions: PromotionDto[] = [];

  constructor(private promotionService: PromotionService) { }

  ngOnInit(): void {
    this.promotionService.getAll().subscribe(result => {
      this.promotions = result;
      console.log(this.promotions);
    })
  }

}
