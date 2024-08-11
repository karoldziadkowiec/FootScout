import axios from 'axios';
import ApiURL from '../../config/ApiConfig';
import AccountService from '../../services/api/AccountService';
import UserDTO from '../../models/dtos/UserDTO';
import UserUpdateDTO from '../../models/dtos/UserUpdateDTO';
import ClubHistoryModel from '../../models/interfaces/ClubHistory';
import PlayerAdvertisement from '../../models/interfaces/PlayerAdvertisement';
import PlayerAdvertisementFavorite from '../../models/interfaces/PlayerAdvertisementFavorite';
import ClubOffer from '../../models/interfaces/ClubOffer';

const UserService = {
  async getUser(userId: string): Promise<UserDTO> {
    try {
      const authorizationHeader = await AccountService.getAuthorizationHeader();
      const response = await axios.get<UserDTO>(`${ApiURL}/users/${userId}`, {
        headers: {
          'Authorization': authorizationHeader
        }
      });
      return response.data;
    }
    catch (error) {
      if (axios.isAxiosError(error)) {
        console.error('Error fetching user, details:', error.response?.data || error.message);
      }
      else {
        console.error('Unexpected error:', error);
      }
      throw error;
    }
  },

  async getUsers(): Promise<UserDTO[]> {
    try {
      const authorizationHeader = await AccountService.getAuthorizationHeader();
      const response = await axios.get<UserDTO[]>(`${ApiURL}/users`, {
        headers: {
          'Authorization': authorizationHeader
        }
      });
      return response.data;
    }
    catch (error) {
      if (axios.isAxiosError(error)) {
        console.error('Error fetching users, details:', error.response?.data || error.message);
      }
      else {
        console.error('Unexpected error:', error);
      }
      throw error;
    }
  },

  async updateUser(userId: string, userUpdateDto: UserUpdateDTO): Promise<void> {
    try {
      const authorizationHeader = await AccountService.getAuthorizationHeader();
      await axios.put(`${ApiURL}/users/${userId}`, userUpdateDto, {
        headers: {
          'Authorization': authorizationHeader
        }
      });
    }
    catch (error) {
      if (axios.isAxiosError(error)) {
        console.error('Error updating user, details:', error.response?.data || error.message);
      }
      else {
        console.error('Unexpected error:', error);
      }
      throw error;
    }
  },

  async deleteUser(userId: string): Promise<void> {
    try {
      const authorizationHeader = await AccountService.getAuthorizationHeader();
      await axios.delete(`${ApiURL}/users/${userId}`, {
        headers: {
          'Authorization': authorizationHeader
        }
      });
    }
    catch (error) {
      if (axios.isAxiosError(error)) {
        console.error('Error deleting user, details:', error.response?.data || error.message);
      }
      else {
        console.error('Unexpected error:', error);
      }
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
      if (axios.isAxiosError(error)) {
        console.error("Error fetching user's club histories, details:", error.response?.data || error.message);
      }
      else {
        console.error('Unexpected error:', error);
      }
      throw error;
    }
  },

  async getUserPlayerAdvertisements(userId: string): Promise<PlayerAdvertisement[]> {
    try {
      const authorizationHeader = await AccountService.getAuthorizationHeader();
      const response = await axios.get<PlayerAdvertisement[]>(`${ApiURL}/users/${userId}/player-advertisements`, {
        headers: {
          'Authorization': authorizationHeader
        }
      });
      return response.data;
    }
    catch (error) {
      if (axios.isAxiosError(error)) {
        console.error("Error fetching user's player advertisements, details:", error.response?.data || error.message);
      }
      else {
        console.error('Unexpected error:', error);
      }
      throw error;
    }
  },

  async getUserActivePlayerAdvertisements(userId: string): Promise<PlayerAdvertisement[]> {
    try {
      const authorizationHeader = await AccountService.getAuthorizationHeader();
      const response = await axios.get<PlayerAdvertisement[]>(`${ApiURL}/users/${userId}/player-advertisements/active`, {
        headers: {
          'Authorization': authorizationHeader
        }
      });
      return response.data;
    }
    catch (error) {
      if (axios.isAxiosError(error)) {
        console.error("Error fetching user's active player advertisements, details:", error.response?.data || error.message);
      }
      else {
        console.error('Unexpected error:', error);
      }
      throw error;
    }
  },

  async getUserInactivePlayerAdvertisements(userId: string): Promise<PlayerAdvertisement[]> {
    try {
      const authorizationHeader = await AccountService.getAuthorizationHeader();
      const response = await axios.get<PlayerAdvertisement[]>(`${ApiURL}/users/${userId}/player-advertisements/inactive`, {
        headers: {
          'Authorization': authorizationHeader
        }
      });
      return response.data;
    }
    catch (error) {
      if (axios.isAxiosError(error)) {
        console.error("Error fetching user's inactive player advertisements, details:", error.response?.data || error.message);
      }
      else {
        console.error('Unexpected error:', error);
      }
      throw error;
    }
  },

  async getUserPlayerAdvertisementFavorites(userId: string): Promise<PlayerAdvertisementFavorite[]> {
    try {
      const authorizationHeader = await AccountService.getAuthorizationHeader();
      const response = await axios.get<PlayerAdvertisementFavorite[]>(`${ApiURL}/users/${userId}/player-advertisements/favorites`, {
        headers: {
          'Authorization': authorizationHeader
        }
      });
      return response.data;
    }
    catch (error) {
      if (axios.isAxiosError(error)) {
        console.error("Error fetching user's favorite player advertisements, details:", error.response?.data || error.message);
      }
      else {
        console.error('Unexpected error:', error);
      }
      throw error;
    }
  },

  async getUserActivePlayerAdvertisementFavorites(userId: string): Promise<PlayerAdvertisementFavorite[]> {
    try {
      const authorizationHeader = await AccountService.getAuthorizationHeader();
      const response = await axios.get<PlayerAdvertisementFavorite[]>(`${ApiURL}/users/${userId}/player-advertisements/favorites/active`, {
        headers: {
          'Authorization': authorizationHeader
        }
      });
      return response.data;
    }
    catch (error) {
      if (axios.isAxiosError(error)) {
        console.error("Error fetching user's active favorite player advertisements, details:", error.response?.data || error.message);
      }
      else {
        console.error('Unexpected error:', error);
      }
      throw error;
    }
  },

  async getUserInactivePlayerAdvertisementFavorites(userId: string): Promise<PlayerAdvertisementFavorite[]> {
    try {
      const authorizationHeader = await AccountService.getAuthorizationHeader();
      const response = await axios.get<PlayerAdvertisementFavorite[]>(`${ApiURL}/users/${userId}/player-advertisements/favorites/inactive`, {
        headers: {
          'Authorization': authorizationHeader
        }
      });
      return response.data;
    }
    catch (error) {
      if (axios.isAxiosError(error)) {
        console.error("Error fetching user's inactive favorite player advertisements, details:", error.response?.data || error.message);
      }
      else {
        console.error('Unexpected error:', error);
      }
      throw error;
    }
  },

  async getReceivedClubOffers(userId: string): Promise<ClubOffer[]> {
    try {
      const authorizationHeader = await AccountService.getAuthorizationHeader();
      const response = await axios.get<ClubOffer[]>(`${ApiURL}/users/${userId}/club-offers/received`, {
        headers: {
          'Authorization': authorizationHeader
        }
      });
      return response.data;
    }
    catch (error) {
      if (axios.isAxiosError(error)) {
        console.error("Error fetching user's received club offers, details:", error.response?.data || error.message);
      }
      else {
        console.error('Unexpected error:', error);
      }
      throw error;
    }
  },

  async getSentClubOffers(userId: string): Promise<ClubOffer[]> {
    try {
      const authorizationHeader = await AccountService.getAuthorizationHeader();
      const response = await axios.get<ClubOffer[]>(`${ApiURL}/users/${userId}/club-offers/sent`, {
        headers: {
          'Authorization': authorizationHeader
        }
      });
      return response.data;
    }
    catch (error) {
      if (axios.isAxiosError(error)) {
        console.error("Error fetching user's sent club offers, details:", error.response?.data || error.message);
      }
      else {
        console.error('Unexpected error:', error);
      }
      throw error;
    }
  }
};

export default UserService;