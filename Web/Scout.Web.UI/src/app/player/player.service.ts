import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, map, tap } from 'rxjs/operators';
import { Observable } from 'rxjs/Observable';

import { Response } from '../../model/api.model';
import { Player, PlayerListItem, PlayerBattingStatistics, PlayerAdvancedBattingStatistics, PlayerPitchingStatistics } from '../../model/player.model';


@Injectable()
export class PlayerService {
    private m_http: HttpClient;
    private m_baseUrl = 'api/players';

    constructor(private http: HttpClient) {
        this.m_http = http;
    }

    getPlayerByCode(code: string): Observable<Response<Player>> {
        const url = `${this.m_baseUrl}/withcode/${code}`;

        return this.m_http.get<Response<Player>>(url);
    }

    getPlayers(): Observable<Response<PlayerListItem[]>> {
        return this.m_http.get<Response<PlayerListItem[]>>(this.m_baseUrl);
    }
}
