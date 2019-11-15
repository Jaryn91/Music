import { ISong } from './isong';


export interface ISongResponse {
  songs: ISong[];
  information?: any;
  alertMessage?: any;
}
