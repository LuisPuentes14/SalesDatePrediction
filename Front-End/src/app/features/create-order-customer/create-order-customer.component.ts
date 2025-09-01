import { Component, Input, OnInit } from '@angular/core';
import { CreateOrderDto, EmployeeDto, ProductDto, ShipperDto } from '../../api/models';

import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, FormsModule, Validators, ReactiveFormsModule } from '@angular/forms';

// PrimeNG Imports para Angular 19
import { InputTextModule } from 'primeng/inputtext';
import { DropdownModule } from 'primeng/dropdown';
import { CalendarModule } from 'primeng/calendar';
import { InputNumberModule } from 'primeng/inputnumber';
import { ButtonModule } from 'primeng/button';
import { CustomersService, EmployeesService, ProductsService, ShippersService } from '../../api/services';
import { ActivatedRoute } from '@angular/router';
import { Toast } from 'primeng/toast';
import { MessageService } from 'primeng/api';


@Component({
  selector: 'app-create-order-customer',
  imports: [
    CommonModule,
    FormsModule,
    InputTextModule,
    DropdownModule,
    CalendarModule,
    InputNumberModule,
    ButtonModule,
    ReactiveFormsModule,
    Toast,
    
  ],
  providers: [ MessageService],
  standalone: true,
  templateUrl: './create-order-customer.component.html',
  styleUrl: './create-order-customer.component.css'
})
export class CreateOrderCustomerComponent implements OnInit {

  @Input() idCustomer!: number;
  @Input() nameCustomer!: string;

  createOrderDto: CreateOrderDto = {};

  employees: EmployeeDto[] = [];
  shippers: ShipperDto[] = [];
  products: ProductDto[] = [];

  shipName: string = '';
  shipAddress: string = '';
  shipCity: string = '';
  shipCountry: string = '';
  orderDate: Date | null = null;
  requiredDate: Date | null = null;
  shippedDate: Date | null = null;
  freight: number | null = null;

  selectedEmployee: EmployeeDto | null = null;
  selectedShipper: ShipperDto | null = null;
  selectedProduct: ProductDto | null = null;
  unitPrice: number | null = null;
  quantity: number | null = null;
  discount: number | null = null;

  CreateOrderForm = new FormGroup({
    selectedEmployee: new FormControl<any | null>(null, [Validators.required]),
    selectedShipper: new FormControl<any | null>(null, [Validators.required]),
    shipName: new FormControl('', [Validators.required]),
    shipAddress: new FormControl('', [Validators.required]),
    shipCountry: new FormControl('', [Validators.required]),
    shipCity: new FormControl('', [Validators.required]),
    orderDate: new FormControl('', [Validators.required]),
    requiredDate: new FormControl('', [Validators.required]),
    shippedDate: new FormControl('', [Validators.required]),
    freight: new FormControl<number | null>(null, [Validators.required]),
    selectedProduct: new FormControl<any | null>(null, [Validators.required]),
    unitPrice: new FormControl<number | null>(null, [Validators.required]),
    quantity: new FormControl<number | null>(null, [Validators.required]),
    discount: new FormControl<number | null>(null, [Validators.required]),
  });

  constructor(
    private apiShippers: ShippersService,
    private apiEmployees: EmployeesService,
    private apiProducts: ProductsService,
    private apiOrders: CustomersService,
    private routes: ActivatedRoute,
     private messageService: MessageService,
  ) {

  }
  ngOnInit(): void {
    this.loadShippers();
    this.loadEmployees();
    this.loadProducts();
  }


  loadShippers() {

    this.apiShippers.apiShippersAllShippersGet$Json().subscribe(
      (response) => {
        this.shippers = response;
        console.log(this.shippers);
      },
      (error) => {
        console.error('Error fetching data:', error);
      });
  }


  loadEmployees() {

    this.apiEmployees.apiEmployeesAllEmployeesGet$Json().subscribe(
      (response) => {
        this.employees = response;
        console.log(this.employees);
      },
      (error) => {
        console.error('Error fetching data:', error);
      });

  }
  loadProducts() {

    this.apiProducts.apiProductsAllProductsGet$Json().subscribe(
      (response) => {
        this.products = response;
        console.log(this.products);
      },
      (error) => {
        console.error('Error fetching data:', error);
      });
  }

  onSave() {

    if (this.CreateOrderForm.valid) {

      const formValue = this.CreateOrderForm.value;

      this.createOrderDto = {
        custId: this.idCustomer,
        shipName: formValue.shipName,
        shipAddress: formValue.shipAddress,
        shipCity: formValue.shipCity,
        shipCountry: formValue.shipCountry,
        orderDate: this.normalizeDate(formValue.orderDate),
        requiredDate: this.normalizeDate(formValue.requiredDate),
        shippedDate: this.normalizeDate(formValue.shippedDate),
        freight: formValue.freight ? Number(formValue.freight) : undefined,
        empId: formValue.selectedEmployee?.empId,
        shipperId: formValue.selectedShipper?.shipperId,
        productId: formValue.selectedProduct?.productId,
        unitPrice: formValue.unitPrice || undefined,
        qty: formValue.quantity || undefined,
        discount: formValue.discount || undefined
      };

      console.log(this.createOrderDto);

      this.apiOrders.apiCustomersCreateOrderPost$Response({ body: this.createOrderDto }).subscribe(
        (response) => {
          console.log(response.status);
        },
        (error) => {
          console.error('Error fetching data:', error);
        }
      );

      this.show('success', 'OK', 'Order Created Successfully');
      this.CreateOrderForm.reset(); // Limpiar el formulario después de enviar

    } else {
      this.CreateOrderForm.markAllAsTouched(); // Para mostrar errores si no ha tocado los campos
      return; // Salir si el formulario no es válido
    }
  }

  normalizeDate(value: any): string | undefined {
    if (!value) return undefined;

    // Si viene como string tipo "2025-09-01T00:00:00"
    if (typeof value === 'string') {
      return value.split('T')[0];
    }

    // Si viene como Date
    if (value instanceof Date) {
      return value.toISOString().split('T')[0];
    }

    return undefined;
  }

  show(tipo: string, mensaje: string, detail: string) {
    this.messageService.add({
      severity: tipo,
      summary: mensaje,
      detail: detail,
    });
  }


}
