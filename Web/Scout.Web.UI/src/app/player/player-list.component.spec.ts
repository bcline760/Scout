/// <reference path="../../../node_modules/@types/jasmine/index.d.ts" />
import { TestBed, async, ComponentFixture, ComponentFixtureAutoDetect } from '@angular/core/testing';
import { BrowserModule, By } from "@angular/platform-browser";
import { PlayerListComponent } from './player-list.component';

let component: PlayerListComponent;
let fixture: ComponentFixture<PlayerListComponent>;

describe('player.list component', () => {
    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [ PlayerListComponent ],
            imports: [ BrowserModule ],
            providers: [
                { provide: ComponentFixtureAutoDetect, useValue: true }
            ]
        });
        fixture = TestBed.createComponent(PlayerListComponent);
        component = fixture.componentInstance;
    }));

    it('should do something', async(() => {
        expect(true).toEqual(true);
    }));
});