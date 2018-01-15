import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';

import { PlayerService } from '../player/player.service';
import { Response, OperationResult } from '../../model/api.model';
import { PlayerListItem } from '../../model/player.model';

class PlayersSelectList {
    public letter: string;
    public players: PlayerListItem[];
}

@Component({
    selector: 'app-players',
    templateUrl: './players.component.html',
    styleUrls: ['./players.component.css']
})
export class PlayersComponent implements OnInit {
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
        this.service.getPlayers().subscribe(s => {
            if (s.result == OperationResult.Success) {
                let alphabet: string[] = ['a', 'b', 'c', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'];
                let players: PlayerListItem[] = s.responseBody;
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
            this.loading = false;
        }, error => {
            console.log(error);

            this.router.navigate(['./error']);
        });
    }

    getPlayersStartingWithLetter(letter: string): void {
        this.loading = true;
        this.service.getPlayers().subscribe(s => {
            if (s.result == OperationResult.Success) {
                let players: PlayerListItem[] = s.responseBody;

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
            this.loading = false;
        }, error => {

        });
    }
}
