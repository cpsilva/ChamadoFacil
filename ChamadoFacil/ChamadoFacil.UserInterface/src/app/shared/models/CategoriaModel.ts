import { chamadoModel } from "./ChamadoModel";

export class categoriaModel {
  id: number;
  descricao: string;
  chamados: chamadoModel[];
}
