import { Component, OnInit } from '@angular/core';
import { SongsService } from 'src/app/api/songs.service';
import { ISong } from '../isong';
import { CdkDragDrop, moveItemInArray, transferArrayItem} from '@angular/cdk/drag-drop';

@Component({
  selector: 'app-song-list',
  templateUrl: './song-list.component.html',
  styleUrls: ['./song-list.component.css'],
  providers: [SongsService]
})
export class SongListComponent implements OnInit {
  filteredSongs: ISong[];
  songs: ISong[];
  selectedSongs: ISong[];
  _songFilter: string;
  _createPrestationPptxDiv = true;
  _createPrestationZipDiv = false;
  _loadingDiv = false;


  get loadingDiv(): boolean {
    return this._loadingDiv;
  }
  set loadingDiv(value: boolean) {
    this._loadingDiv = value;
  }

  get createPrestationPptxDiv(): boolean {
    return this._createPrestationPptxDiv;
  }
  set createPrestationPptxDiv(value: boolean) {
    this._createPrestationPptxDiv = value;
  }


  get createPrestationZipDiv(): boolean {
    return this._createPrestationZipDiv;
  }
  set createPrestationZipDiv(value: boolean) {
    this._createPrestationZipDiv = value;
  }


  get songFilter(): string {
    return this._songFilter;
  }
  set songFilter(value: string) {
    this._songFilter = value;
    this.filteredSongs = this.songFilter ? this.performFilter(this.songFilter) : this.songs;
  }

  constructor(private songService: SongsService) {
  }

  ngOnInit() {
    this.resetPresentation();
  }

  performFilter(filterBy: string): ISong[] {
    filterBy = filterBy.toLocaleLowerCase();
    return this.songs.filter((song: ISong) =>
        song.name.toLocaleLowerCase().indexOf(filterBy) !== -1);
  }

  drop(event: CdkDragDrop<string[]>) {
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      transferArrayItem(event.previousContainer.data,
                        event.container.data,
                        event.previousIndex,
                        event.currentIndex);
    }
  }
  resetPresentation(): void {
    this.songs = this.songService.getSongs();
    this.filteredSongs = this.songs;
    this.songFilter = '';
    this.selectedSongs = [];
    this._createPrestationPptxDiv = true;
  }

  createPptx(): void {
    this._createPrestationPptxDiv = false;
    this._loadingDiv = true;
  }

  createZip(): void {


  }

}
