import { BrowserModule } from '@angular/platform-browser';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { NgScrollbarModule } from 'ngx-scrollbar';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { DateAdapter, ErrorStateMatcher, MatNativeDateModule, MAT_DATE_FORMATS, MAT_DATE_LOCALE, ShowOnDirtyErrorStateMatcher } from '@angular/material/core';
import { MatCardModule } from '@angular/material/card';
import { MAT_MOMENT_DATE_FORMATS } from '@angular/material-moment-adapter';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { DateFilterComponent } from './components/trains/date-filter/date-filter.component';
import { CarListComponent } from './components/trains/car-list/car-list.component';
import { ReservationFinishComponent } from './components/reservations/reservation-finish/reservation-finish.component';
import { SelectCarComponent } from './components/trains/select-car/select-car.component';
import { MomentUtcDateAdapter } from './core/services/date-adapter.service';
import { MultipleSeatsComponent } from './components/trains/multiple-seats/multiple-seats.component';
import { ModifyReservationComponent } from './components/reservations/modify-reservation/modify-reservation.component';
import { FeatureFlagService, getFlags } from './core/services/feature-flag.service';
import { CoreModule } from './core/core.module';
import { ErrorInterceptor } from './core/interceptors/error.interceptor';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    DateFilterComponent,
    CarListComponent,
    SelectCarComponent,
    MultipleSeatsComponent,    
    ReservationFinishComponent,
    ModifyReservationComponent
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
    CoreModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'train/:id/available-seats', component: CarListComponent },
      { path: 'finish-reservation', component: ReservationFinishComponent },
      { path: 'modify-reservation', component: ModifyReservationComponent },
    ], { relativeLinkResolution: 'legacy' })
  ],
  exports: [
    MatIconModule
  ],
  providers: [
    { provide: ErrorStateMatcher, useClass: ShowOnDirtyErrorStateMatcher },
    { provide: MAT_DATE_LOCALE, useValue: 'en-GB' },
    { provide: MAT_DATE_FORMATS, useValue: MAT_MOMENT_DATE_FORMATS },
    { provide: DateAdapter, useClass: MomentUtcDateAdapter },
    { provide: APP_INITIALIZER, useFactory: getFlags, deps: [FeatureFlagService], multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
