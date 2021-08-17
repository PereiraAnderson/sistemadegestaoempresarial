import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BancoComponent } from './components/banco/banco.component';
import { CadastroUsuarioComponent } from './components/cadastro-usuario/cadastro-usuario.component';
import { LoginComponent } from './components/login/login.component';
import { LogoutComponent } from './components/logout/logout.component';
import { PontoComponent } from './components/ponto/ponto.component';
import { PontosComponent } from './components/pontos/pontos.component';
import { RequerimentoComponent } from './components/requerimento/requerimento.component';
import { RequerimentosComponent } from './components/requerimentos/requerimentos.component';
import { UsuarioComponent } from './components/usuario/usuario.component';
import { UsuariosComponent } from './components/usuarios/usuarios.component';
import { AuthGuardService } from './services/auth-guard.service';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'cadastro-usuario/:id', component: CadastroUsuarioComponent },
  { path: '', component: PontoComponent, canActivate: [AuthGuardService] },
  { path: 'logout', component: LogoutComponent, canActivate: [AuthGuardService] },
  { path: 'banco', component: BancoComponent, canActivate: [AuthGuardService] },
  { path: 'usuarios', component: UsuariosComponent, canActivate: [AuthGuardService] },
  { path: 'usuario', component: UsuarioComponent, canActivate: [AuthGuardService] },
  { path: 'requerimentos', component: RequerimentosComponent, canActivate: [AuthGuardService] },
  { path: 'requerimento', component: RequerimentoComponent, canActivate: [AuthGuardService] },
  { path: 'pontos', component: PontosComponent, canActivate: [AuthGuardService] },
  { path: 'ponto', component: PontoComponent, canActivate: [AuthGuardService] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
