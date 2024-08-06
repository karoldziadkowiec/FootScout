import Achievements from './AchievementsDTO';

interface ClubHistoryModelDTO {
    playerPositionId: number;
    clubName: string;
    league: string;
    region: string;
    startDate: string;
    endDate: string;
    achievements: Achievements;
    userId: string;
}

export default ClubHistoryModelDTO;