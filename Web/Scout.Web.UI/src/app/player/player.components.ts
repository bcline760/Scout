import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { Subject } from 'rxjs/Subject';

import { PlayerService } from './player.service';
import { Response, OperationResult } from '../../model/api.model';
import { Player, PlayerBattingStatistics, PlayerAdvancedBattingStatistics, PlayerPitchingStatistics, PlayerListItem } from '../../model/player.model';

class PlayersSelectList {
    public letter: string;
    public players: PlayerListItem[];
}

@Component({
    selector: 'app-player',
    templateUrl: './html/player.component.html',
    styleUrls: ['./css/player.component.css']
})
export class PlayerComponent {

}

@Component({
    selector: 'app-player-list',
    templateUrl: './html/player-list.component.html',
    styleUrls: ['./css/player-list.component.css']
})
/** player.list component*/
export class PlayerListComponent implements OnInit {
    route: ActivatedRoute;
    router: Router;
    location: Location;

    service: PlayerService;
    playerSelectList: PlayersSelectList[];
    playerNamedList: PlayerListItem[];

    hideSelectList: boolean;
    hideNamedList: boolean;

    public loading: boolean = false;
    startingLetter: string;

    constructor(
        service: PlayerService,
        route: ActivatedRoute,
        location: Location,
        router: Router) {
        this.router = router;
        this.route = route;
        this.service = service;
        this.location = location;
    }

    ngOnInit() {
        const letter = this.route.snapshot.paramMap.get('letter');
        if (letter == null) {
            this.hideSelectList = false;
            this.hideNamedList = true;
            this.getPlayers();
        } else {
            this.hideNamedList = false;
            this.hideSelectList = true;
            this.startingLetter = letter.toUpperCase();
            this.getPlayersStartingWithLetter(letter);
        }
    }

    getPlayers(): void {
        this.loading = true;
        const thisHack = this;
        this.route.data.subscribe((data: { player: Response<PlayerListItem[]> }) => {
            thisHack.loading = false;
            if (data.player) {
                if (data.player.result == OperationResult.Success) {
                    let alphabet: string[] = ['a', 'b', 'c', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'];
                    let players: PlayerListItem[] = data.player.responseBody;
                    let selectItem: PlayersSelectList = null;
                    this.playerSelectList = [];

                    for (var i = 0; i < alphabet.length; i++) {
                        selectItem = new PlayersSelectList();
                        selectItem.letter = alphabet[i];
                        selectItem.players = players.filter((v, i, ar) => {
                            return v.lastName != null && v.lastName.toLowerCase().startsWith(selectItem.letter);
                        })
                        this.playerSelectList.push(selectItem);
                    }
                }
            } else {

            }
        });
    }

    getPlayersStartingWithLetter(letter: string): void {
        this.loading = true;
        this.route.data.subscribe((data: { player: Response<PlayerListItem[]> }) => {
            if (data.player) {
                if (data.player.result == OperationResult.Success) {
                    let players: PlayerListItem[] = data.player.responseBody;

                    let filteredPlayers: PlayerListItem[] = players.filter((v, i, ar) => {
                        return v.lastName != null && v.lastName.toLowerCase().startsWith(letter);
                    });

                    this.playerNamedList = filteredPlayers.sort((a, b) => {
                        if (a > b)
                            return 1;
                        else if (a < b)
                            return -1;
                        else
                            return 0;
                    });
                }
            } else {

            }
        });
    }
}

@Component({
    selector: 'app-player-detail',
    templateUrl: './html/player-detail.component.html',
    styleUrls: ['./css/player-detail.component.css']
})
/** player.detail component*/
export class PlayerDetailComponent implements OnInit {
    public loading: boolean;
    route: ActivatedRoute;
    router: Router;
    location: Location;

    service: PlayerService;
    player: Player = null;
    playerBatting: PlayerBattingStatistics[] = null;
    playerAdvancedBatting: PlayerAdvancedBattingStatistics[] = null;
    playerPitching: PlayerPitchingStatistics[] = null;
    hideBatting: boolean;
    hidePitching: boolean;

    dtOptions: DataTables.Settings = {};
    dtTrigger: Subject<any> = new Subject();

    constructor(
        service: PlayerService,
        route: ActivatedRoute,
        location: Location,
        router: Router) {
        this.router = router;
        this.route = route;
        this.service = service;
        this.location = location;
    }

    ngOnInit() {
        this.dtOptions = {
            pagingType: 'full_numbers',
            pageLength: 10
        };
        this.getPlayer();
    }

    getPlayer(): void {
        this.loading = true;
        const thisHack = this; //Seriously? I still have to do this in TypeScript?
        this.route.data.subscribe((data: { player: Response<Player> }) => {
            thisHack.loading = false;
            if (data.player === undefined) {
                //thisHack.router.navigate(['./error']);
            } else {
                if (data.player.result == OperationResult.Success) {
                    thisHack.player = data.player.responseBody;
                    if (thisHack.player.battingStatistics !== undefined) {
                        thisHack.playerBatting = thisHack.player.battingStatistics.sort((a, b) => {
                            if (a.teamYear > b.teamYear)
                                return 1;
                            else if (a.teamYear < b.teamYear)
                                return -1;
                            else
                                return 0;
                        });
                    }
                    if (thisHack.player.pitchingStatistics !== undefined) {
                        thisHack.playerPitching = data.player.responseBody.pitchingStatistics.sort((a, b) => {
                            if (a.teamYear > b.teamYear)
                                return 1;
                            else if (a.teamYear < b.teamYear)
                                return -1;
                            else
                                return 0;
                        });
                    }

                    if (thisHack.player.advancedBattingStatistics !== undefined) {
                        thisHack.playerAdvancedBatting = data.player.responseBody.advancedBattingStatistics.sort((a, b) => {
                            if (a.teamYear > b.teamYear)
                                return 1;
                            else if (a.teamYear < b.teamYear)
                                return -1;
                            else
                                return 0;
                        });
                    }
                    thisHack.hideBatting = thisHack.playerBatting == null;
                    thisHack.hidePitching = thisHack.playerPitching == null;
                    thisHack.dtTrigger.next();
                } else {
                    //thisHack.router.navigate(['./error']);
                }
            }
        });
    }
}

@Component({
    selector: 'app-player-salary',
    templateUrl: './html/player-salary.component.html',
    styleUrls: ['./css/player-salary.component.css']
})
/** player.salary component*/
export class PlayerSalaryComponent {
    /** player.salary ctor */
    constructor() {

    }
}
