import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgxWebstorageModule } from 'ngx-webstorage';
import { NgxMaskModule, IConfig } from 'ngx-mask';

import { AppMaterialModule } from './modules/app.material.module';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { LogoutComponent } from './components/logout/logout.component';
import { LoginComponent } from './components/login/login.component';
import { UsuariosComponent } from './components/usuarios/usuarios.component';
import { AuthInterceptor } from './services/auth.interceptor';
import { HomeComponent } from './components/home/home.component';
import { UsuarioComponent } from './components/usuario/usuario.component';
import { CommonModule } from '@angular/common';
import { AuthGuardService } from './services/auth-guard.service';
import { CadastroUsuarioComponent } from './components/cadastro-usuario/cadastro-usuario.component';
import { PontoComponent } from './components/ponto/ponto.component';
import { PontosComponent } from './components/pontos/pontos.component';

import { NgxMatDatetimePickerModule, NgxMatTimepickerModule } from '@angular-material-components/datetime-picker';
import { NgxMatMomentModule } from '@angular-material-components/moment-adapter';
import { registerLocaleData } from '@angular/common';
import localePt from '@angular/common/locales/pt';

registerLocaleData(localePt);
import { LOCALE_ID } from '@angular/core';
import { BancoComponent } from './components/banco/banco.component';
import { RequerimentoComponent } from './components/requerimento/requerimento.component';
import { RequerimentosComponent } from './components/requerimentos/requerimentos.component';


export const options: Partial<IConfig> | (() => Partial<IConfig>) = null;

@NgModule({
  declarations: [
    AppComponent,
    LogoutComponent,
    LoginComponent,
    HomeComponent,
    CadastroUsuarioComponent,
    UsuariosComponent,
    UsuarioComponent,
    PontosComponent,
    PontoComponent,
    BancoComponent,
    RequerimentoComponent,
    RequerimentosComponent
  ],
  imports: [
    CommonModule,
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgxWebstorageModule.forRoot(),
    NgxMaskModule.forRoot(),

    AppRoutingModule,
    AppMaterialModule,

    NgxMatTimepickerModule,
    NgxMatDatetimePickerModule,
    NgxMatMomentModule
  ],
  providers: [
    AuthGuardService,
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    { provide: LOCALE_ID, useValue: 'pt-BR' }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
