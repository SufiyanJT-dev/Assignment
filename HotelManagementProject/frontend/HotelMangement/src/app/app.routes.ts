import { Routes } from '@angular/router';
import { HomePage } from './features/Home/home-page/home-page';
import { Hoteldetails } from './features/Hotels/hoteldetails/hoteldetails';
import { LoginAuth } from './features/auth/login-auth/login-auth';
import { AdminDashboard } from './features/Admin/admin-dashboard/admin-dashboard';
import { SearchPage } from './features/search-page/search-page';

import { Employees } from './features/Admin/admin-dashboard/pages/employees/employees';
import { Hotel } from './features/Admin/admin-dashboard/pages/hotel/hotel';
import { Rooms } from './features/Admin/admin-dashboard/pages/rooms/rooms';
import { RoomTypesComponent } from './features/Admin/admin-dashboard/pages/room-type/room-type';
import { BookingComponent } from './features/Admin/admin-dashboard/pages/booking/booking';
import { BookingPage } from './features/booking-page/booking-page';


export const routes: Routes = [
    { path: '', redirectTo: 'Home', pathMatch: 'full' },
    { path: 'Home', component: HomePage },
    { path: 'login', component: LoginAuth },
    { path: 'result-page', component: SearchPage },
    {path:'BookingDeatils',component:BookingPage},
    { path: 'HotelDetails', component: Hoteldetails },
    {
        path: 'Admin-DashBoard',
        component: AdminDashboard,
        children: [
            { path: 'booking', component: BookingComponent },
            { path: 'employees', component: Employees },
            {
                path: 'hotel', component: Hotel
            },
            { path: 'room', component: Rooms },
            {path:'RoomType',component:RoomTypesComponent}


        ]
    }
];
