import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent, HomeDisplayComponent } from './home.components';

const homeRoutes: Routes = [
    {
        path: 'home',
        component: HomeComponent,
        children: [
            {
                path: '',
                component: HomeDisplayComponent
            }
        ]
    }
]
@NgModule({
    imports: [
        RouterModule.forChild(homeRoutes)
    ],
    exports: [
        RouterModule
    ],
    providers: [
    ]
})
export class HomeRoutingModule { }
