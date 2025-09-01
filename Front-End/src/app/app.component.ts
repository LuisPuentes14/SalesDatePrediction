import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Toast  } from 'primeng/toast';

import { ButtonModule } from 'primeng/button';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet,Toast, ButtonModule, ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
   providers: [MessageService]
})
export class AppComponent {
  title = 'SalesDatePrediction';  
}
