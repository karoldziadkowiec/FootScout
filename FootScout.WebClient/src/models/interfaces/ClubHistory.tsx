import Achievements from '../dtos/AchievementsDTO';
import UserDTO from '../dtos/UserDTO';

interface ClubHistoryModel {
    id: number;
    position: string;
    clubName: string;
    league: string;
    region: string;
    startDate: string;
    endDate: string;
    achievementsId: number;
    achievements: Achievements;
    userId: string;
    user: UserDTO;
}

export default ClubHistoryModel;