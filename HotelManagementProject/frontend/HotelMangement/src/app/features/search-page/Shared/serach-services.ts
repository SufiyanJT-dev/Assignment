import { Search } from "../compounents/left-sidecomponent/type/SerachInterface";
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class SerachServices {
  private data: Search | null = null;

  constructor() {
    const saved = localStorage.getItem('Search');
    this.data = saved ? JSON.parse(saved) : null;
  }

  setData(data: Search) {
    this.data = data;
    localStorage.setItem('Search', JSON.stringify(data));
  }

  getData(): Search | null {
    return this.data;
  }
}
