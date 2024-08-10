import PlayerAdvertisement from './PlayerAdvertisement';
import AdvertisementStatus from './AdvertisementStatus';
import PlayerPosition from './PlayerPosition';
import SalaryRange from "./SalaryRange";
import UserDTO from '../dtos/UserDTO';

interface ClubOffer {
    id: number;
    playerAdvertisementId: number;
    playerAdvertisement: PlayerAdvertisement;
    advertisementStatusId: number;
    advertisementStatus: AdvertisementStatus;
    playerPositionId: number;
    playerPosition: PlayerPosition;
    clubName: string;
    league: string;
    region: string;
    salary: number;
    additionalInformation: string;
    creationDate: string;
    endDate: string;
    userClubId: string;
    userClub: UserDTO;
}

export default ClubOffer;