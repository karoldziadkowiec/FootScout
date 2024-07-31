import Achievements from './AchievementsDTO';

interface ClubHistoryModel {
    position: string;
    clubName: string;
    league: string;
    region: string;
    startDate: string;
    endDate: string;
    achievements: Achievements;
    userId: string;
}

export default ClubHistoryModel;