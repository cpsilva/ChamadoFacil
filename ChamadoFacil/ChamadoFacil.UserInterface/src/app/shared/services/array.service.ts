import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ArrayService {
  add(array: any, key: any, value: any, isValidaDuplicado: boolean, msg?: string) {
    let i = array.length;

    const valor = { ...value };

    let exists = false;

    if (isValidaDuplicado) {
      while (i--) {
        if (array[i] && array[i].hasOwnProperty(key) && (array[i][key] == value[key])) {
          exists = true;
          return;
        }
      }

      if (!exists) {
        array.push(valor);
      }
    } else {
      array.push(valor);
    }
  }

  remove(array, key, value) {
    let i = array.length;

    while (i--) {
      if (array[i] && array[i].hasOwnProperty(key) && (arguments.length > 2 && array[i][key] == value)) {
        array.splice(i, 1);
      }
    }

    return array;
  }

  sort(list, column, order) {
    if (order === 'ASC') {
      list.sort((a, b) => {
        if (a[column] > b[column]) {
          return 1;
        }

        if (a[column] < b[column]) {
          return -1;
        }

        return 0;
      });

      return order = 'DESC';
    }

    if (order === 'DESC') {
      list.sort((a, b) => {
        if (a[column] > b[column]) {
          return -1;
        }

        if (a[column] < b[column]) {
          return 1;
        }

        return 0;
      });

      return order = 'ASC';
    }
  }

  exists(array: any, key: any, value: any, msg) {
    let exists = false;

    if (array) {
      let i = array.length;

      while (i--) {
        if (array[i] && array[i].hasOwnProperty(key) && (array[i][key] == value[key])) {
          exists = true;
          break;
        }
      }
    }

    return exists;
  }

  compare(a1, a2): any[] {
    const a = [], diff = [];

    for (let i = 0; i < a1.length; i++) {
      a[a1[i]] = true;
    }

    for (let i = 0; i < a2.length; i++) {
      if (a[a2[i]]) {
        delete a[a2[i]];
      } else {
        a[a2[i]] = true;
      }
    }

    for (let k in a) {
      diff.push(k);
    }

    return diff;
  }

  iterate(obj): any[] {
    const ret = [];

    const walked = [];
    const stack = [{ obj: obj, stack: '' }];
    while (stack.length > 0) {
      const item = stack.pop();
      const objeto = item.obj;
      for (const property in objeto) {
        if (objeto.hasOwnProperty(property)) {
          if (typeof objeto[property] == 'object') {
            let alreadyFound = false;
            for (let i = 0; i < walked.length; i++) {
              if (walked[i] == objeto[property]) {
                alreadyFound = true;
                break;
              }
            }
            if (!alreadyFound) {
              walked.push(objeto[property]);
              stack.push({ obj: objeto[property], stack: item.stack + '.' + property });
            }
          } else {
            ret.push(item.stack + '_' + property + '=' + objeto[property]);
          }
        }
      }
    }

    return ret;
  }
}
