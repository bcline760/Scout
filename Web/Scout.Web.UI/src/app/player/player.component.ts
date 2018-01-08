import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';

import { PlayerService } from '../player.service';
import { Response, OperationResult } from '../../model/api.model';
import { Player, PlayerBattingStatistics, PlayerAdvancedBattingStatistics, PlayerPitchingStatistics } from '../../model/player.model';


@Component({
    selector: 'app-player',
    templateUrl: './player.component.html',
    styleUrls: ['./player.component.css']
})
export class PlayerComponent implements OnInit {
    public loading: boolean;
    route: ActivatedRoute;
    router: Router;
    location: Location;

    service: PlayerService;
    player: Player;
    playerBatting: PlayerBattingStatistics[];
    playerPitching: PlayerPitchingStatistics[];
    hideBatting: boolean;
    hidePitching: boolean;

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
        this.getPlayer();
    }

    getPlayer(): void {
        const code = this.route.snapshot.paramMap.get('id');
        if (code == null) {
            this.router.navigate(['./error']);
        } else {
            this.loading = true;
            const pCode: string = code;
            this.service.getPlayerByCode(pCode).subscribe(player => {
                if (player.result == OperationResult.Success) {
                    this.player = player.responseBody;
                    this.playerBatting = player.responseBody.battingStatistics.sort((a, b) => {
                        if (a.teamYear > b.teamYear)
                            return 1;
                        else if (a.teamYear < b.teamYear)
                            return -1;
                        else
                            return 0;
                    });
                    this.playerPitching = player.responseBody.pitchingStatistics;

                    this.hideBatting = this.playerBatting === undefined || this.playerBatting.length == 0;
                    this.hidePitching = this.playerPitching === undefined || this.playerPitching.length == 0;
                }
                this.loading = false;
            }, error => {
                console.log("");
                this.loading = false;
                this.router.navigate(['./error']);
            });
            
        }
    }
}
