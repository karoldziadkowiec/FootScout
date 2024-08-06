import axios from 'axios';
import ApiURL from '../../config/ApiConfig';
import AccountService from './AccountService';
import PlayerPosition from '../../models/interfaces/PlayerPosition';

const PlayerPositionService = {
    async getPlayerPositions(): Promise<PlayerPosition[]> {
        try {
            const authorizationHeader = await AccountService.getAuthorizationHeader();
            const response = await axios.get<PlayerPosition[]>(`${ApiURL}/player-positions`, {
                headers: {
                    'Authorization': authorizationHeader
                }
            });
            return response.data;
        } 
        catch (error) {
            console.error("Error fetching player positions:", error);
            throw error;
        }
    },

    async getPlayerPositionName(positionId: number): Promise<string> {
        try {
            const authorizationHeader = await AccountService.getAuthorizationHeader();
            const response = await axios.get<string>(`${ApiURL}/player-positions/${positionId}`, {
                headers: {
                    'Authorization': authorizationHeader
                }
            });
            return response.data;
        } 
        catch (error) {
            console.error("Error fetching selected player position:", error);
            throw error;
        }
    }
};

export default PlayerPositionService;