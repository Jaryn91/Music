import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PresentationService {
  private songUrl = 'http://localhost:5060/api/presentation';

  constructor() { }

  getPresentation(songs: number[]): void {
      var pptxUrl = this.songUrl + '/pptx?ids=' +  songs.join('?ids=');
      console.log(pptxUrl);
  }
}
