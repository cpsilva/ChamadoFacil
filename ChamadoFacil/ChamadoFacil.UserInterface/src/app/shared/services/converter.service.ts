export class ConverterService {
  objectToQueryString(obj) {
    var str = [];

    this.iterate(obj, str);

    return str.join("&");
  }

  private iterate(obj, str, propertyParent = undefined) {
    for (var property in obj) {
      if (obj.hasOwnProperty(property)) {
        if (typeof obj[property] == "object") {
          this.iterate(obj[property], str, property);
        }
        else {
          var key = property;

          if (propertyParent) { key = propertyParent + "." + property; }

          if (obj[property]) {
            str.push(encodeURIComponent(key) + "=" + encodeURIComponent(obj[property]));
          }
        }
      }
    }
  }
}
