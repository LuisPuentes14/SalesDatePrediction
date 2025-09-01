import { Component, OnInit } from '@angular/core';
import { CustomersService } from '../../api/services';
import { ButtonModule } from 'primeng/button';
import { TableModule } from 'primeng/table';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { OrderDto, SalesDatePredictionDto } from '../../api/models';
import { Dialog } from 'primeng/dialog';
import { OrdersCustomersComponent } from "../orders-customers/orders-customers.component";
import { CreateOrderCustomerComponent } from "../create-order-customer/create-order-customer.component";

@Component({
  standalone: true,
  selector: 'app-list-customers',
  imports: [TableModule, CommonModule, ButtonModule, HttpClientModule, Dialog, OrdersCustomersComponent, CreateOrderCustomerComponent],
  templateUrl: './list-customers.component.html',
  styleUrl: './list-customers.component.css'
})
export class ListCustomersComponent implements OnInit {

  customers!: SalesDatePredictionDto[];
  ordersdto!: OrderDto[];
  idCustomer!: number;
  nameCustomer!: string;
  visible: boolean = false;
  visibleFormNewOrder: boolean = false;

  first = 0;

  rows = 10;

  constructor(private api: CustomersService) {
    this.api.apiCustomersSalesPredictionGet$Json().subscribe(
      (response) => {
        this.customers = response;      
      },
      (error) => {
        console.error('Error fetching data:', error);
      }
    );
  }


  ngOnInit() { }

  next() {
    this.first = this.first + this.rows;
  }

  prev() {
    this.first = this.first - this.rows;
  }

  reset() {
    this.first = 0;
  }

  pageChange(event: { first: number; rows: number; }) {
    this.first = event.first;
    this.rows = event.rows;
  }

  isLastPage(): boolean {
    return this.customers ? this.first + this.rows >= this.customers.length : true;
  }

  isFirstPage(): boolean {
    return this.customers ? this.first === 0 : true;
  }

  showOrdersByCustomer(idCustomer: number, nameCustomer: string) {
    this.idCustomer = idCustomer;
    this.nameCustomer = nameCustomer;
    this.visible = true;
  }

  loadOrders() {
    this.api.apiCustomersOrdersByCustomerIdGet$Json({ id: 1 }).subscribe(
      (response) => {
        this.ordersdto = response;       
      },
      (error) => {
        console.error('Error fetching data:', error);
      }
    );
  }

  showCreateOrder(idCustomer: number, nameCustomer: string) {
    this.idCustomer = idCustomer;
    this.nameCustomer = nameCustomer;
    this.visibleFormNewOrder = true;
  }

}
