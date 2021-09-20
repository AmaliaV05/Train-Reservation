import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { NgScrollbarModule } from 'ngx-scrollbar';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { ErrorStateMatcher, MatNativeDateModule, ShowOnDirtyErrorStateMatcher } from '@angular/material/core';
import { MatCardModule } from '@angular/material/card';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { TrainsComponent } from './trains/trains.component';
import { DateFilterComponent } from './trains/date-filter/date-filter.component';
import { CarListComponent } from './trains/car-list/car-list.component';
import { ReservationsComponent } from './reservations/reservations.component';
import { ReservationFinishComponent } from './reservations/reservation-finish/reservation-finish.component';
import { InputErrorStateMatcherEmail } from './reservations/reservation-finish/email/email-validation.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    TrainsComponent,
    DateFilterComponent,
    CarListComponent,
    ReservationsComponent,
    ReservationFinishComponent,
    InputErrorStateMatcherEmail
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    MatIconModule,
    MatButtonModule,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatCardModule,
    NgScrollbarModule,
    RouterModule.forRoot([
    { path: '', component: HomeComponent, pathMatch: 'full' },
    { path: 'counter', component: CounterComponent },
    { path: 'fetch-data', component: FetchDataComponent },
    { path: 'train/:id/available-seats', component: CarListComponent },
    { path: 'finish-reservation', component: ReservationFinishComponent},
], { relativeLinkResolution: 'legacy' })
  ],
  providers: [
    { provide: ErrorStateMatcher, useClass: ShowOnDirtyErrorStateMatcher }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
