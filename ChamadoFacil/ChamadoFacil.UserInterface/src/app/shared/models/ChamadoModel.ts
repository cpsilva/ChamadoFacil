import { categoriaModel } from './CategoriaModel';
import { usuarioModel } from './UsuarioModel';

export class chamadoModel {
  id: number;
  titulo: string;
  mensagem: string;
  urgencia: number;
  categoriaId: number;
  usuarioId: number;
  categoria: categoriaModel;
  usuario: usuarioModel;
}
