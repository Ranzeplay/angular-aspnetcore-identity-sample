import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class StorageService {

  constructor() { }

  public clear() {
    localStorage.clear();
  }

  public getString(name: string) {
    return localStorage.getItem(name);
  }

  public update(name: string, value) {
    localStorage.setItem(name, value);
  }

  public remove(name: string) {
    localStorage.removeItem(name);
  }
}
