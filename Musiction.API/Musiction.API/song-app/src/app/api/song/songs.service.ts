import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { ISongResponse } from '../../songs/isong-response';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SongsService {
  private songUrl = 'http://localhost:5060/api/songs';

  constructor(private http: HttpClient) { }

  getSongs(): Observable<ISongResponse> {
    return this.http.get<ISongResponse>(this.songUrl);
  }

  handleError(handleError: any): import('rxjs').OperatorFunction<ISongResponse, any> {
    throw new Error('Method not implemented.');
  }

}
