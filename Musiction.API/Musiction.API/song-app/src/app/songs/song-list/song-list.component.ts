import { Component, OnInit } from '@angular/core';
import { ISong } from '../isong';
import { CdkDragDrop, moveItemInArray, transferArrayItem} from '@angular/cdk/drag-drop';
import { PresentationService } from 'src/app/api/presentation/presentation.service';
import { SongsService } from 'src/app/api/song/songs.service';

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
  errorMessage: string;

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

  constructor(private songService: SongsService, private presentationService: PresentationService) {
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
    this.songService.getSongs().subscribe({
      next: songsResponse => {
        this.songs = songsResponse.songs;
        this.filteredSongs = this.songs;
      },
      error(err) {this.errorMessage = err; }
    });
    this.songFilter = '';
    this.selectedSongs = [];
    this._createPrestationPptxDiv = true;
  }

  createPptx(): void {
    this._createPrestationPptxDiv = false;
    this._loadingDiv = true;
    var songIds = this.selectedSongs.map(x => {
      return x.id;
    });
    this.presentationService.getPresentation(songIds);
  }

  createZip(): void {
  }
}
