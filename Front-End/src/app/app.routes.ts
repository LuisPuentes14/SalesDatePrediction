import { Routes } from '@angular/router';

export const routes: Routes = [

    {
      path: 'customers',
      title: 'Clientes',
      loadComponent: () => import('./features/list-customers/list-customers.component')
                         .then(m => m.ListCustomersComponent),
      children: [],
    },   
    {
      path: '',
      pathMatch: 'full',
      redirectTo: 'customers'
    }
];
