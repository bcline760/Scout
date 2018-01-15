import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { Subject } from 'rxjs/Subject';

import { PlayerService } from './player.service';
import { Response, OperationResult } from '../../model/api.model';
import { Player, PlayerBattingStatistics, PlayerAdvancedBattingStatistics, PlayerPitchingStatistics } from '../../model/player.model';

@Component({
    selector: 'app-player-detail',
    templateUrl: './player-detail.component.html',
    styleUrls: ['./player-detail.component.css']
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
