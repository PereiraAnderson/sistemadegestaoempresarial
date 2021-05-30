import { EnumUsuarioPerfil } from '../models/enums/enumUsuarioPerfil';

export class UtilService {

  public static getValuesEnumUsuarioPerfil = (): string[] => {
    var keys = Object.keys(EnumUsuarioPerfil);
    return keys.slice(keys.length / 2);
  }
}
