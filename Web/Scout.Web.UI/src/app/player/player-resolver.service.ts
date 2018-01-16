import 'rxjs/add/operator/map';
import 'rxjs/add/operator/take';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import {
    Router, Resolve, RouterStateSnapshot,
    ActivatedRouteSnapshot
} from '@angular/router';

import { PlayerService } from './player.service';
import { Response } from '../../model/api.model';
import { Player, PlayerListItem } from '../../model/player.model';

@Injectable()
export class PlayerResolverService implements Resolve<Response<Player>> {
    constructor(private service: PlayerService, private router: Router) {

    }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<Response<Player>> {
        const code = route.paramMap.get('id');
        const pCode: string = code;
        return this.service.getPlayerByCode(pCode).take(1).map(player => {
            if (player) {
                return player;
            } else {
                this.router.navigate(['./error']);
                return null;
            }
        });
    }
}

@Injectable()
export class PlayerListResolver implements Resolve<Response<PlayerListItem[]>>{
    constructor(private service: PlayerService, private router: Router) {

    }
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot):
        Response<PlayerListItem[]> |
        Observable<Response<PlayerListItem[]>> |
        Promise<Response<PlayerListItem[]>> {
        return this.service.getPlayers().map(players => {
            if (players) {
                return players;
            } else {
                //Do error checking
                return null;
            }
        })
    }
}