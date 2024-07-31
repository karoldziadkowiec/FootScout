import axios from 'axios';
import ApiURL from '../../config/ApiConfig';
import AccountService from './AccountService';
import ClubHistoryCreateDTO from '../../models/dtos/ClubHistoryCreateDTO';

const ClubHistoryService = {
    async getClubHistory(clubHistoryId: number): Promise<ClubHistoryCreateDTO> {
        try {
            const authorizationHeader = await AccountService.getAuthorizationHeader();
            const response = await axios.get<ClubHistoryCreateDTO>(`${ApiURL}/club-history/${clubHistoryId}`, {
                headers: {
                    'Authorization': authorizationHeader
                }
            });
            return response.data;
        } catch (error) {
            console.error("Error fetching club history:", error);
            throw error;
        }
    },

    async getAllClubHistory(): Promise<ClubHistoryCreateDTO[]> {
        try {
            const authorizationHeader = await AccountService.getAuthorizationHeader();
            const response = await axios.get<ClubHistoryCreateDTO[]>(`${ApiURL}/club-history`, {
                headers: {
                    'Authorization': authorizationHeader
                }
            });
            return response.data;
        } catch (error) {
            console.error("Error fetching all club histories:", error);
            throw error;
        }
    },

    async getUserClubHistory(userId: string, clubHistoryId: number): Promise<ClubHistoryCreateDTO> {
        try {
            const authorizationHeader = await AccountService.getAuthorizationHeader();
            const response = await axios.get<ClubHistoryCreateDTO>(`${ApiURL}/users/${userId}/club-history/${clubHistoryId}`, {
                headers: {
                    'Authorization': authorizationHeader
                }
            });
            return response.data;
        } catch (error) {
            console.error("Error fetching user's club history:", error);
            throw error;
        }
    },

    async getUserAllClubHistory(userId: string): Promise<ClubHistoryCreateDTO[]> {
        try {
            const authorizationHeader = await AccountService.getAuthorizationHeader();
            const response = await axios.get<ClubHistoryCreateDTO[]>(`${ApiURL}/users/${userId}/club-history`, {
                headers: {
                    'Authorization': authorizationHeader
                }
            });
            return response.data;
        } catch (error) {
            console.error("Error fetching user's club histories:", error);
            throw error;
        }
    },

    async createClubHistory(clubHistory: ClubHistoryCreateDTO): Promise<void> {
        try {
            const authorizationHeader = await AccountService.getAuthorizationHeader();
            await axios.post(`${ApiURL}/club-history`, clubHistory, {
                headers: {
                    'Authorization': authorizationHeader
                }
            });
        } catch (error) {
            console.error("Error adding new club history:", error);
            throw error;
        }
    },

    async updateClubHistory(clubHistoryId: number, clubHistory: ClubHistoryCreateDTO): Promise<void> {
        try {
            const authorizationHeader = await AccountService.getAuthorizationHeader();
            await axios.put(`${ApiURL}/club-history/${clubHistoryId}`, clubHistory, {
                headers: {
                    'Authorization': authorizationHeader
                }
            });
        } catch (error) {
            console.error("Error updating club history:", error);
            throw error;
        }
    },

    async deleteClubHistory(clubHistoryId: number): Promise<void> {
        try {
            const authorizationHeader = await AccountService.getAuthorizationHeader();
            await axios.delete(`${ApiURL}/club-history/${clubHistoryId}`, {
                headers: {
                    'Authorization': authorizationHeader
                }
            });
        } catch (error) {
            console.error("Error deleting club history:", error);
            throw error;
        }
    }
};

export default ClubHistoryService;