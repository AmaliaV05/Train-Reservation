"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppModule = void 0;
var platform_browser_1 = require("@angular/platform-browser");
var core_1 = require("@angular/core");
var forms_1 = require("@angular/forms");
var http_1 = require("@angular/common/http");
var router_1 = require("@angular/router");
var animations_1 = require("@angular/platform-browser/animations");
var button_1 = require("@angular/material/button");
var icon_1 = require("@angular/material/icon");
var form_field_1 = require("@angular/material/form-field");
var select_1 = require("@angular/material/select");
var input_1 = require("@angular/material/input");
var ngx_scrollbar_1 = require("ngx-scrollbar");
var datepicker_1 = require("@angular/material/datepicker");
var core_2 = require("@angular/material/core");
var card_1 = require("@angular/material/card");
var app_component_1 = require("./app.component");
var nav_menu_component_1 = require("./nav-menu/nav-menu.component");
var home_component_1 = require("./home/home.component");
var date_filter_component_1 = require("./trains/date-filter/date-filter.component");
var car_list_component_1 = require("./trains/car-list/car-list.component");
var reservation_finish_component_1 = require("./reservations/reservation-finish/reservation-finish.component");
var select_car_component_1 = require("./trains/select-car/select-car.component");
var material_moment_adapter_1 = require("@angular/material-moment-adapter");
var date_adapter_service_1 = require("./trains/date-adapter.service");
var multiple_seats_component_1 = require("./trains/multiple-seats/multiple-seats.component");
var modify_reservation_component_1 = require("./reservations/modify-reservation/modify-reservation.component");
var feature_flag_service_1 = require("./core/feature-flag.service");
var core_module_1 = require("./core/core.module");
var AppModule = /** @class */ (function () {
    function AppModule() {
    }
    AppModule = __decorate([
        (0, core_1.NgModule)({
            declarations: [
                app_component_1.AppComponent,
                nav_menu_component_1.NavMenuComponent,
                home_component_1.HomeComponent,
                date_filter_component_1.DateFilterComponent,
                car_list_component_1.CarListComponent,
                select_car_component_1.SelectCarComponent,
                multiple_seats_component_1.MultipleSeatsComponent,
                reservation_finish_component_1.ReservationFinishComponent,
                modify_reservation_component_1.ModifyReservationComponent
            ],
            imports: [
                platform_browser_1.BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
                http_1.HttpClientModule,
                forms_1.FormsModule,
                forms_1.ReactiveFormsModule,
                animations_1.BrowserAnimationsModule,
                icon_1.MatIconModule,
                button_1.MatButtonModule,
                form_field_1.MatFormFieldModule,
                select_1.MatSelectModule,
                input_1.MatInputModule,
                datepicker_1.MatDatepickerModule,
                core_2.MatNativeDateModule,
                card_1.MatCardModule,
                ngx_scrollbar_1.NgScrollbarModule,
                core_module_1.CoreModule,
                router_1.RouterModule.forRoot([
                    { path: '', component: home_component_1.HomeComponent, pathMatch: 'full' },
                    { path: 'train/:id/available-seats', component: car_list_component_1.CarListComponent },
                    { path: 'finish-reservation', component: reservation_finish_component_1.ReservationFinishComponent },
                    { path: 'modify-reservation', component: modify_reservation_component_1.ModifyReservationComponent },
                ], { relativeLinkResolution: 'legacy' })
            ],
            exports: [
                icon_1.MatIconModule
            ],
            providers: [
                { provide: core_2.ErrorStateMatcher, useClass: core_2.ShowOnDirtyErrorStateMatcher },
                { provide: core_2.MAT_DATE_LOCALE, useValue: 'en-GB' },
                { provide: core_2.MAT_DATE_FORMATS, useValue: material_moment_adapter_1.MAT_MOMENT_DATE_FORMATS },
                { provide: core_2.DateAdapter, useClass: date_adapter_service_1.MomentUtcDateAdapter },
                { provide: core_1.APP_INITIALIZER, useFactory: feature_flag_service_1.getFlags, deps: [feature_flag_service_1.FeatureFlagService], multi: true },
            ],
            bootstrap: [app_component_1.AppComponent]
        })
    ], AppModule);
    return AppModule;
}());
exports.AppModule = AppModule;
//# sourceMappingURL=app.module.js.map