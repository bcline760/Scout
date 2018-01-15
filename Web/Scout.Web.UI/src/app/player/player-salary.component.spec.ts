/// <reference path="../../../node_modules/@types/jasmine/index.d.ts" />
import { TestBed, async, ComponentFixture, ComponentFixtureAutoDetect } from '@angular/core/testing';
import { BrowserModule, By } from "@angular/platform-browser";
import { PlayerSalaryComponent } from './player-salary.component';

let component: PlayerSalaryComponent;
let fixture: ComponentFixture<PlayerSalaryComponent>;

describe('player.salary component', () => {
    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [ PlayerSalaryComponent ],
            imports: [ BrowserModule ],
            providers: [
                { provide: ComponentFixtureAutoDetect, useValue: true }
            ]
        });
        fixture = TestBed.createComponent(PlayerSalaryComponent);
        component = fixture.componentInstance;
    }));

    it('should do something', async(() => {
        expect(true).toEqual(true);
    }));
});