import { Component, Input, SimpleChanges  } from '@angular/core';
import { CustomersService } from '../../api/services';
import { OrderDto } from '../../api/models';
import { ActivatedRoute } from '@angular/router';
import { OnInit } from '@angular/core';
import { TableModule } from 'primeng/table';
import { CommonModule } from '@angular/common';


@Component({
  standalone: true,
  selector: 'app-orders-customers',
  imports: [TableModule, CommonModule],
  templateUrl: './orders-customers.component.html',
  styleUrl: './orders-customers.component.css'
})
export class OrdersCustomersComponent implements OnInit {

  ordersdto!: OrderDto[];
   @Input() idCustomer!: number;
   @Input() nameCustomer!: string;

  constructor(private api: CustomersService, private routes: ActivatedRoute) {

    this.api.apiCustomersOrdersByCustomerIdGet$Json({ id: this.idCustomer }).subscribe(
      (response) => {
        this.ordersdto = response;
        console.log(this.ordersdto);
      },
      (error) => {
        console.error('Error fetching data:', error);
      }
    );
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['idCustomer'] ) {
      this.api.apiCustomersOrdersByCustomerIdGet$Json({ id: this.idCustomer }).subscribe(
      (response) => {
        this.ordersdto = response;
        console.log(this.ordersdto);
      },
      (error) => {
        console.error('Error fetching data:', error);
      }
    );
    }
  }

  ngOnInit() {    
    
    

    
  }
}
