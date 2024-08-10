import axios from 'axios';
import ApiURL from '../../config/ApiConfig';
import AccountService from './AccountService';
import PlayerAdvertisementFavoriteCreateDTO from '../../models/dtos/PlayerAdvertisementFavoriteCreateDTO';

const PlayerAdvertisementFavoriteService = {
    async addToFavorites(dto: PlayerAdvertisementFavoriteCreateDTO): Promise<void> {
        try {
            const authorizationHeader = await AccountService.getAuthorizationHeader();
            await axios.post(`${ApiURL}/player-advertisements/favorites`, dto, {
                headers: {
                    'Authorization': authorizationHeader
                }
            });
        }
        catch (error) {
            if (axios.isAxiosError(error)) {
                console.error('Error adding player advertisement to favorites, details:', error.response?.data || error.message);
            }
            else {
                console.error('Unexpected error:', error);
            }
            throw error;
        }
    },

    async deleteFromFavorites(playerAdvertisementFavoriteId: number): Promise<void> {
        try {
            const authorizationHeader = await AccountService.getAuthorizationHeader();
            await axios.delete(`${ApiURL}/player-advertisements/favorites/${playerAdvertisementFavoriteId}`, {
                headers: {
                    'Authorization': authorizationHeader
                }
            });
        }
        catch (error) {
            if (axios.isAxiosError(error)) {
                console.error('Error deleting player advertisement from favorites, details:', error.response?.data || error.message);
            }
            else {
                console.error('Unexpected error:', error);
            }
            throw error;
        }
    },

    async checkPlayerAdvertisementIsFavorite(playerAdvertisementId: number, userId: string): Promise<number> {
        try {
            const authorizationHeader = await AccountService.getAuthorizationHeader();
            const response = await axios.get<number>(`${ApiURL}/player-advertisements/favorites/check/${playerAdvertisementId}/${userId}`, {
                headers: {
                    'Authorization': authorizationHeader
                }
            });
            return response.data;
        }
        catch (error) {
            if (axios.isAxiosError(error)) {
                console.error('Error checking if player advertisement is favorite, details:', error.response?.data || error.message);
            }
            else {
                console.error('Unexpected error:', error);
            }
            throw error;
        }
    }
};

export default PlayerAdvertisementFavoriteService;