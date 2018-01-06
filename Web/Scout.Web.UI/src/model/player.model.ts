export class PlayerBattingStatistics {
    public playerBattingStatisticsId: number;
    public playerId: number;
    public teamId: number;
    public plateAppearances: number;
    public atBats: number;
    public hits: number;
    public doubles: number;
    public triples: number;
    public homeruns: number;
    public runsBattedIn: number;
    public sacrificeHits: number;
    public sacrificeFlies: number;
    public walks: number;
    public intentionalWalks: number;
    public hitByPitch: number;
    public strikeouts: number;
    public stolenBases: number;
    public caughtStealing: number;
    public groundedIntoDoublePlay: number;
    public battingAverage: number;
    public onBasePercentage: number;
    public sluggingPercentage: number;
    public onBasePlusSlugging: number;
    public onBasePlusSluggingAdj: number;
}

export class PlayerAdvancedBattingStatistics {
    public playerId: number;
    public teamId: number;
    public battingAverage: number;
    public totalAverage: number;
    public onBasePercentage: number;
    public sluggingPercentage: number;
    public onBasePlusSlugging: number;
    public onBasePlusSluggingAdj: number;
    public battingAvgOfBallsInPlay: number;
    public homeRunPercentage: number;
    public strikeoutPercentage: number;
    public walkPercentage: number;
    public extraBaseHitPercentage: number;
    public inPlayPercentage: number;
}

export class PlayerPitchingStatistics {
    public playerId: number;
    public teamId: number;
    public pitchingStint: number;
    public gamesWon: number;
    public gamesLost: number;
    public gamesPlayed: number;
    public completeGames: number;
    public shutouts: number;
    public gamesSaved: number;
    public inningsPitched: number;
    public hits: number;
    public runs: number;
    public earnedRuns: number;
    public walks: number;
    public strikeouts: number;
    public homeruns: number;
    public earnedRunAverage: number;
    public intentionalWalks: number;
    public hitBatsmen: number;
    public wildPitches: number;
    public balks: number;
    public timesInducedGidp: number;
}

export class Player {
    public playerId: number;
    public playerIdentifier: string;
    public retrosheetId: string;
    public firstName: string;
    public lastName: string;
    public birthDate?: Date;
    public birthStateProvince: string;
    public birthCountry: string;
    public deathDate?: Date;
    public deathCity: string;
    public deathStateProvince: string;
    public deathCountry: string;
    public primaryPosition: string;
    public draftTeamId?: number;
    public draftYear?: number;
    public majorLeagueDebut?: number;
    public bats: string;
    public throws: string;
    public height?: number;
    public weight?: number;

    public battingStatistics: PlayerBattingStatistics[];
    public pitchingStatistics: PlayerPitchingStatistics[];
    public advancedBattingStatistics: PlayerAdvancedBattingStatistics[];
}
