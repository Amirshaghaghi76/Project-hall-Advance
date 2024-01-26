import { Routes } from '@angular/router';
import { LoginComponent } from './components/account/login/login.component';
import { RegisterComponent } from './components/account/register/register.component';
import { AddHallComponent } from './components/add-hall/add-hall.component';
import { CommentComponent } from './components/comment/comment.component';
import { HomeComponent } from './components/home/home.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { SearchHallComponent } from './components/search-hall/search-hall.component';
import { WeddingAdviceComponent } from './components/wedding-advice/wedding-advice.component';

export const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'home', component: HomeComponent },
    { path: 'add-hall', component: AddHallComponent },
    { path: 'search-hall', component: SearchHallComponent },
    { path: 'wedding-advice', component: WeddingAdviceComponent },
    { path: 'account/register', component: RegisterComponent },
    { path: 'account/login', component: LoginComponent },
    { path: 'comment', component: CommentComponent },

    { path: '**', component: NotFoundComponent, pathMatch: 'full' },];
