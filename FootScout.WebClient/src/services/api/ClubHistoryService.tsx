import axios from 'axios';
import ApiURL from '../../config/ApiConfig';
import AccountService from './AccountService';
import ClubHistoryModel from '../../models/interfaces/ClubHistory';
import ClubHistoryCreateDTO from '../../models/dtos/ClubHistoryCreateDTO';

const ClubHistoryService = {
    async getClubHistory(clubHistoryId: number): Promise<ClubHistoryModel> {
        try {
            const authorizationHeader = await AccountService.getAuthorizationHeader();
            const response = await axios.get<ClubHistoryModel>(`${ApiURL}/club-history/${clubHistoryId}`, {
                headers: {
                    'Authorization': authorizationHeader
                }
            });
            return response.data;
        } 
        catch (error) {
            console.error("Error fetching club history:", error);
            throw error;
        }
    },

    async getAllClubHistory(): Promise<ClubHistoryModel[]> {
        try {
            const authorizationHeader = await AccountService.getAuthorizationHeader();
            const response = await axios.get<ClubHistoryModel[]>(`${ApiURL}/club-history`, {
                headers: {
                    'Authorization': authorizationHeader
                }
            });
            return response.data;
        } 
        catch (error) {
            console.error("Error fetching all club histories:", error);
            throw error;
        }
    },

    async getUserClubHistory(userId: string): Promise<ClubHistoryModel[]> {
        try {
            const authorizationHeader = await AccountService.getAuthorizationHeader();
            const response = await axios.get<ClubHistoryModel[]>(`${ApiURL}/users/${userId}/club-history`, {
                headers: {
                    'Authorization': authorizationHeader
                }
            });
            return response.data;
        } 
        catch (error) {
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
        } 
        catch (error) {
            console.error("Error adding new club history:", error);
            throw error;
        }
    },

    async updateClubHistory(clubHistoryId: number, clubHistory: ClubHistoryModel): Promise<void> {
        try {
            const authorizationHeader = await AccountService.getAuthorizationHeader();
            await axios.put(`${ApiURL}/club-history/${clubHistoryId}`, clubHistory, {
                headers: {
                    'Authorization': authorizationHeader
                }
            });
        } 
        catch (error) {
            if (axios.isAxiosError(error)) {
                console.error('Error details:', error.response?.data || error.message);
            } else {
                console.error('Unexpected error:', error);
            }
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
        } 
        catch (error) {
            console.error("Error deleting club history:", error);
            throw error;
        }
    }
};

export default ClubHistoryService;