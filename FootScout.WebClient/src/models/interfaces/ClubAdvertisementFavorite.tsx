import UserDTO from '../dtos/UserDTO';
import ClubAdvertisement from './ClubAdvertisement';

interface ClubAdvertisementFavorite {
    id: number;
    clubAdvertisementId: number;
    clubAdvertisement: ClubAdvertisement;
    userId: string;
    user: UserDTO;
}

export default ClubAdvertisementFavorite;