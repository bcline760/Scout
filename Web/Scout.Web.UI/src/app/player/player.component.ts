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
    route: ActivatedRoute;
    router: Router;
    location: Location;

    service: PlayerService;
    player: Player;
    playerBatting: PlayerBattingStatistics[];
    playerPitching: PlayerPitchingStatistics[];


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
            const pCode: string = code;
            this.service.getPlayerByCode(pCode).subscribe(player => {
                if (player.result == OperationResult.Success) {
                    this.player = player.responseBody;
                }
            }, error => {
                console.log("");

                this.router.navigate(['./error']);
            });
        }
    }
}
