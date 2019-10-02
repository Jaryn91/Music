import { Component, OnInit } from '@angular/core';
import { SongsService } from 'src/app/api/songs.service';
import { ISong } from '../isong';

@Component({
  selector: 'app-song-list',
  templateUrl: './song-list.component.html',
  styleUrls: ['./song-list.component.css'],
  providers: [SongsService]
})
export class SongListComponent implements OnInit {
  filteredSongs: ISong[];
  songs: ISong[];
  _songFilter: string;
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
    this.songs = this.songService.getSongs();
    this.filteredSongs = this.songs;
    this.songFilter = 'aa';
  }

  performFilter(filterBy: string): ISong[] {
    filterBy = filterBy.toLocaleLowerCase();
    return this.songs.filter((song: ISong) =>
        song.name.toLocaleLowerCase().indexOf(filterBy) !== -1);
  }

}
